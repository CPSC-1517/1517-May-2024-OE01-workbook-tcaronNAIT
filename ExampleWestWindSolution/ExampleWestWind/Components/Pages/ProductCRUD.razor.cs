﻿using Microsoft.AspNetCore.Components;
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
        private string feedback = string.Empty;
        bool isNew = false;

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
                else
                {
                    isNew = true;
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

        private void UpdateProduct()
        {
            errorMessages.Clear();
            feedback = string.Empty;
            form.Validate();

            if(form.IsValid)
            {
                try
                {
                    int rowsAffected = _productServices.Products_UpdateProduct(CurrentProduct);

                    if (rowsAffected == 0)
                    {
                        errorMessages.Add($"Product {CurrentProduct.ProductName} has not been updated. Please check to see if the product is still on file.");
                    }

                    feedback = $"Product {CurrentProduct.ProductName} was successfully updated.";
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Save Error: {GetInnerException(ex).Message}");
                }
            }
        }

        private void AddProduct()
        {

        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}