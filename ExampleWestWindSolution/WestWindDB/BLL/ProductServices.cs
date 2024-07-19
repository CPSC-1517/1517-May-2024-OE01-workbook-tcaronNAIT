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
        #endregion
    }
}
