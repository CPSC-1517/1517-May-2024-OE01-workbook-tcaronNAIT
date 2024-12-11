using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindDB.DAL;
using WestWindDB.Entities;

namespace WestWindDB.BLL
{
    public class SupplierServices
    {
        private readonly WestWindContext _context;

        public SupplierServices(WestWindContext context)
        {
            _context = context;
        }

        #region Queries
        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.OrderBy(s => s.CompanyName).ToList();
        }
        #endregion
    }
}
