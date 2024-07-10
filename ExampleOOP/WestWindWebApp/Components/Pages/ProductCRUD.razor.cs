using Microsoft.AspNetCore.Components;
using WestWindLibrary.BLL;
using WestWindLibrary.DTOs;
using WestWindLibrary.Entities;

namespace WestWindWebApp.Components.Pages
{
    public partial class ProductCRUD
    {
     //   private List<Product> products = [];
     private List<ProductListDTO> products = [];
        private List<string> errorMessages = [];

        [Inject]
        private ProductServices _productServices { get; set; }

        protected override async Task OnInitializedAsync()
        {
            products = await _productServices.GetAllProductsList();
        }
    }
}
