using Microsoft.EntityFrameworkCore;
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
    }
}
