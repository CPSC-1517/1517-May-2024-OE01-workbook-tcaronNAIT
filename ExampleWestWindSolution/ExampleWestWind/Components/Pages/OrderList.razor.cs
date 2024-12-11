using Microsoft.AspNetCore.Components;
using WestWindDB.BLL;
using WestWindDB.Entities;

namespace ExampleWestWind.Components.Pages
{
	public partial class OrderList
	{
		private List<Order> orders = [];
        private List<string> errorMessages = [];
        private string customerSearch = string.Empty;
        private bool noOrders = false;

        [Inject] private OrderServices _orderServices { get; set; }

        protected override void OnInitialized()
        {
            //orders = _orderServices.GetAllOrders();
            base.OnInitialized();
        }

        private void SearchOrders()
        {
            noOrders = false;
            orders.Clear();
            errorMessages.Clear();
            if (string.IsNullOrWhiteSpace(customerSearch))
            {
                errorMessages.Add("Please enter a partial or full customer name to search.");
            }
            else
            {
                try
                {
                    orders = _orderServices.GetOrders_ByCustomerName(customerSearch);
                    if(orders.Count == 0)
                    {
                        noOrders = true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Record Retrieval Error: {GetInnerException(ex).Message}");
                }
            }
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
