using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindDB.DAL;
using WestWindDB.Entities;

namespace WestWindDB.BLL
{
    public class CategoryServices
    {
        private readonly WestWindContext _context;

        internal CategoryServices(WestWindContext context)
        {
            _context = context;
        }

        #region Queries

        public List<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(c=>c.CategoryName).ToList();
        }
        #endregion
    }
}
