using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindLibrary.DAL;
using WestWindLibrary.Entities;
using WestWindLibrary.DTOs;
using System.Security.Cryptography;

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

        //Returning all data from categories and products
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Include(p => p.Category).OrderBy(p => p.Category.CategoryName).ThenBy(p => p.ProductName).ToListAsync();

            /* Select * 
                From Product Inner Join Category On Product.Category = Category.CategoryID
                ORDER BY CategoryName, ProductName */
        }

        //return only the needed data for the display
        public async Task<List<ProductListDTO>> GetAllProductsList()
        {
            var test =  await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Category.CategoryName)
                .ThenBy(p => p.ProductName)
                .Select(p => new ProductListDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    CategoryName = p.Category.CategoryName
                })
                .ToListAsync();
            return test;

            /* Select ProductId, ProductName, UnitPrice, CategoryName 
                From Product Inner Join Category On Product.Category = Category.CategoryID
                ORDER BY CategoryName, ProductName */
        }

        #endregion

        #region Create, Update, Delete

        #endregion
    }
}
