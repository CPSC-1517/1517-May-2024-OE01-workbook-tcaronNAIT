using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindDB.DAL;
using WestWindDB.Entities;

namespace WestWindDB.BLL
{
    public class ProductServices
    {
        private readonly WestWindContext _context;

        internal ProductServices(WestWindContext context)
        {
            _context = context;
        }

        #region Queries
        public List<Product> GetAllProducts()
        {
            return _context.Products.Include(p=>p.Supplier).Include(p=>p.Category).ToList();
        }

        public List<Product> GetProducts_ByCategory(string category)
        {
            return _context.Products.Include(p => p.Supplier).Include(p => p.Category).Where(p=>p.Category.CategoryName.ToLower().Equals(category.ToLower())).ToList();
        }

        public Product Products_GetByProductID(int productid)
        {
            //primary key lookup
            //this way we are returning one product ever! (if it exists)
            //.FirstOrDefault only return the first record it finds or null
            return _context.Products.Where(p => p.ProductID == productid).FirstOrDefault();
        }
        #endregion

        #region CRUD
        public int Products_UpdateProduct(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException("You must supply the product information.");
            }

            //Any returns false if no items match and true if it finds 1 or more matches.
            //Always good to check if the item to update still exists! (No one deleted it, etc).
            bool exists = _context.Products.Any(p => p.ProductID == product.ProductID);

            if(!exists)
            {
                throw new ArgumentException($"Product {product.ProductName} (id: {product.ProductID}) is no longer on file.");
            }

            //example of a made-up business rule
            //Make sure product isn't changed to duplicate another product
            exists = _context.Products.Any(p => p.SupplierID == product.SupplierID
                                                && p.ProductName.Equals(product.ProductName)
                                                && p.QuantityPerUnit == product.QuantityPerUnit
                                                && p.ProductID != product.ProductID);

            if(exists)
            {
                throw new ArgumentException($"Product {product.ProductName} with a QPU of {product.QuantityPerUnit} for the indicated supplier already exists.");
            }

            //otherwise if all is good update the product!
            //Staging and indicated to EF which record is being updated.
            EntityEntry<Product> updating = _context.Entry(product);
            updating.State = EntityState.Modified;

            //Commit
            //the returned value for an update is the number of rows affected.
            return _context.SaveChanges();
        }

        public int Product_AddProduct(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException("You must supply the product information.");
            }

            //Business Rule Example
            //Does this product already exist?
            bool exists = _context.Products
                            .Any(p => p.SupplierID  == product.SupplierID
                            && p.ProductName == product.ProductName
                            && p.QuantityPerUnit == product.QuantityPerUnit);

            if(exists)
            {
                throw new ArgumentException("Product already exists on file.");
            }

            //You could also add any other specific logic to check

            //Once you determine the data is good to go into the database
                //then you start the stage and commit

            //Staging
            //IMPORTANT - Remember this is only in local memory
            //This staging has not been sent to the database yet
            //There is no ID for this product
            _context.Products.Add(product);

            //Commit
            //This send the local data to the database
            _context.SaveChanges();

            //Now the product we created has a ID
            //In this case the database automatically created an ID

            return product.ProductID;
        }

        public int Product_PhysicalDelete(Product product)
        {
            //Physical Delete
            //Removed the record from the data
            //If there are child records to prevent the record removal
                //You can remove the record
            //if there are child records AND the child record are required
                //You cannot delete the record!
                //You would need to delete the child first
                //This assumes there is no cascade delete set up in the database

            if(product == null)
            {
                throw new ArgumentNullException("You must supply the product information.");
            }

            //check if the product exists
            Product? exists = _context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

            if(exists == null)
            {
                throw new ArgumentException($"Product {product.ProductName} (id: {product.ProductID}) is no longer on file.");
            }

            //check for child records, this is good practice
            int existingChildren = exists.ManifestItems.Count;
            existingChildren += exists.OrderDetails.Count;

            //Example business rule, if child records exists you cannot delete
            if(existingChildren > 0)
            {
                throw new ArgumentException($"Product {product.ProductName} (id: {product.ProductID}) has related information on file. Unable to delete.");
            }

            //Staging
            EntityEntry<Product> deleting = _context.Entry(exists);
            deleting.State = EntityState.Deleted;

            //Commit
            //the return value from that data for a delete is the number of rows affected

            return _context.SaveChanges();
        }
        #endregion
    }
}
