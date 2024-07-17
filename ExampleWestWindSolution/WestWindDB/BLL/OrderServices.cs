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
	public class OrderServices
	{
		private readonly WestWindContext _context;

		internal OrderServices(WestWindContext context)
		{
			_context = context;
		}

		#region Queries

		public List<Order> GetAllOrders()
		{
			return _context.Orders.Include(o => o.SalesRep).Include(o => o.Customer).OrderBy(o => o.SalesRep.FullName).ToList();
		}

		#endregion
	}
}
