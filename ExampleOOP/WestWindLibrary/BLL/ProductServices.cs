using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindLibrary.DAL;
using WestWindLibrary.Entities;

namespace WestWindLibrary.BLL
{
    public class ProductServices
    {
        #region Setup
        //Access to the database!
        private readonly WestWindContext _context;

        //this constructor is used to obtain the instance of the WestWindContext class
        //This provides our access to the database for all others methods.
        internal ProductServices(WestWindContext context)
        {
            _context = context;
        }

        #endregion

        #region Queries
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        #endregion

        #region Create, Update, Delete

        #endregion
    }
}
