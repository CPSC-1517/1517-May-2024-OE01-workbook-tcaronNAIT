using Microsoft.AspNetCore.Components;
using MudBlazor;
using WestWindDB.BLL;
using WestWindDB.Entities;

namespace ExampleWestWind.Components.Pages
{
    public partial class ProductCRUD
    {
        //this parameter matches to the parameter defined in the page directive
        //you must identify with annotation to identify it as a parameter
        //the datatype and name must match the page directive parameter.
        [Parameter]
        public int? productId { get; set; }

        [Inject]
        private ProductServices _productServices { get; set; }
        private Product CurrentProduct = new();

        [Inject]
        private CategoryServices _categoryServices { get; set; }
        private List<Category> categories = [];

        [Inject]
        private SupplierServices _supplierServices { get; set; }
        private List<Supplier> suppliers = [];

        private List<string> errorMessages = [];
        private MudForm form = new();
        private string[] errors = [];

        protected override void OnInitialized()
        {
            try
            {
                categories = _categoryServices.GetAllCategories();
                suppliers = _supplierServices.GetAllSuppliers();
                if(productId.HasValue)
                {
                    CurrentProduct = _productServices.Products_GetByProductID(productId.Value);
                }
            }
            catch (Exception ex)
            {
                errorMessages.Add(GetInnerException(ex).Message);
            }
            base.OnInitialized();
        }
        private string ProductNameValidation(string productName)
        {
            if(string.IsNullOrWhiteSpace(productName))
            {
                return "Product name is required";
            }
            else if (productName.Length > 40)
            {
                return "Product Name must be less than 40 characters.";
            }

            return string.Empty;
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
