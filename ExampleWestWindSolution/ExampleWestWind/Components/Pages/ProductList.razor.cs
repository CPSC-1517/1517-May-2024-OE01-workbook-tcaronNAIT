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
        private string categorySearch = string.Empty;
        private List<Category> categories = [];

        [Inject] ProductServices _productServices { get; set; }
        [Inject] CategoryServices _categoryServices { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                categories = _categoryServices.GetAllCategories();
            }
            catch (Exception ex)
            {
                errorMessages.Add(GetInnerException(ex).Message);
            }
            
            loading = false;
            base.OnInitialized();
        }

        private void SearchProducts()
        {
            products.Clear();
            errorMessages.Clear();
            noProducts = false;
            if(string.IsNullOrWhiteSpace(categorySearch))
            {
                errorMessages.Add("Please select a category to search for products.");
            }
            else
            {
                try
                {
                    products = _productServices.GetProducts_ByCategory(categorySearch);
                    if(products.Count == 0)
                    {
                        noProducts = true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add(GetInnerException(ex).Message);
                }
            }
            
        }

        private void EditProduct(int productId)
        {
            _navigationManager.NavigateTo($"product/{productId}");
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

    }
}
