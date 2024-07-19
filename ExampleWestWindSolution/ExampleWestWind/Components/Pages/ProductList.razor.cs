using Microsoft.AspNetCore.Components;
using WestWindDB.Entities;
using WestWindDB.BLL;

namespace ExampleWestWind.Components.Pages
{
    public partial class ProductList
    {
        private List<Product> products = [];
        private List<string> errorMessages = [];
        private bool noProducts = false;
        private bool loading = true;

        [Inject] ProductServices _productServices { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                products = _productServices.GetAllProducts();
            }
            catch (Exception ex)
            {
                errorMessages.Add(ex.Message);
            }
            loading = false;
            base.OnInitialized();
        }

    }
}
