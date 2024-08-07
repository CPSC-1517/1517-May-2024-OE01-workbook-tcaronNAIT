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
        private Product SameProduct = new();

        [Inject]
        private CategoryServices _categoryServices { get; set; }
        private List<Category> categories = [];

        [Inject]
        private SupplierServices _supplierServices { get; set; }
        private List<Supplier> suppliers = [];

        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IDialogService _dialogService { get; set; }
        [Inject] ISnackbar _snackBar { get; set; }

        private List<string> errorMessages = [];
        private MudForm form = new();
        private string[] errors = [];
        private string feedback = string.Empty;
        private bool isNew = false;

        protected override void OnInitialized()
        {
            try
            {
                categories = _categoryServices.GetAllCategories();
                suppliers = _supplierServices.GetAllSuppliers();
                if(productId.HasValue)
                {
                    try
                    {

                        //This CurrentProduct points to the product retrieved from the list of products in our DBSet<Product>
                        CurrentProduct = _productServices.Products_GetByProductID(productId.Value);
                        //How to escape just having a pointer, making a whole new Product.
                        //May be the behavior you want if you want to rollback changes, or only Discontinue and not make other updates, etc.
                        //SameProduct = new Product
                        //{
                        //    ProductID = CurrentProduct.ProductID,
                        //    UnitPrice = CurrentProduct.UnitPrice,
                        //    Discontinued = CurrentProduct.Discontinued,
                        //    CategoryID = CurrentProduct.CategoryID,
                        //    ManifestItems = CurrentProduct.ManifestItems,
                        //    ProductName = CurrentProduct.ProductName,
                        //    MinimumOrderQuantity = CurrentProduct.MinimumOrderQuantity,
                        //    QuantityPerUnit = CurrentProduct.QuantityPerUnit,
                        //    OrderDetails = CurrentProduct.OrderDetails,
                        //    Category = CurrentProduct.Category,
                        //    Supplier = CurrentProduct.Supplier,
                        //    SupplierID = CurrentProduct.SupplierID,
                        //    UnitsOnOrder = CurrentProduct.UnitsOnOrder
                        //};
                        //CurrentProduct.Discontinued = false;
                        //SameProduct.UnitPrice = 4;
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add(GetInnerException(ex).Message);
                    }
                    
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
                    else
                    {
                        feedback = $"Product {CurrentProduct.ProductName} was successfully updated.";
                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Save Error: {GetInnerException(ex).Message}");
                }
            }
        }

        private void AddProduct()
        {
            errorMessages.Clear();
            form.Validate();
            if(form.IsValid)
            {
                try
                {
                    //The product when created return to us the new product id!
                    int newProductID = _productServices.Product_AddProduct(CurrentProduct);

                    feedback = $"Product {CurrentProduct.ProductName} was created (Product ID: {newProductID})";

                    CurrentProduct.ProductID = newProductID;

                    //Navigate manager updated the Address Bar to include the new Product ID
                    _navigationManager.NavigateTo($"product/{newProductID}");
                    //IsNew changed to false will update the appearance of the button to display Update Product and call the UpdateProduct Method when clicked. (this uses the terinary operator we programmed in the button)
                    isNew = false;
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Save Error: {GetInnerException(ex).Message}");
                }
            }
        }
        private async Task DeleteProduct()
        {
            bool? results = await _dialogService.ShowMessageBox("Confirm Delete", "Are you sure you want to delete the product?", yesText: "Delete", cancelText: "Cancel");

            if(results == true)
            {
                try
                {
                    int rowAffected = _productServices.Product_PhysicalDelete(CurrentProduct);

                    //If no rows are affected, someone likely deleted it before us (racing deletes), so we display that the product is no longer on file.
                    if(rowAffected == 0)
                    {
                        errorMessages.Add($"Product {CurrentProduct.ProductName} (id: {CurrentProduct.ProductID}) is no longer on file.");
                    }
                    else
                    {
                        feedback = $"Product {CurrentProduct.ProductName} (id: {CurrentProduct.ProductID}) has been deleted.";

                        //Option to stay on the same page and clear the product.
                        ////Clear the current product
                        //CurrentProduct = new Product();
                        ////Force the Address bar to update
                        //_navigationManager.NavigateTo($"product");
                        ////Treat the page as if the user wants to add a new product.
                        //isNew = true;

                        //Option to Navigate back to the search page and show snackbar message about the deletion.
                        _snackBar.Add($"Product {CurrentProduct.ProductName} (id: {CurrentProduct.ProductID}) has been deleted.", Severity.Success, config => { config.ShowCloseIcon = false; });
                        _navigationManager.NavigateTo($"products");

                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Delete Error: {GetInnerException(ex).Message}");
                }
            }
        }

        private void DeactivateProduct()
        {
            errorMessages.Clear();
            feedback = string.Empty;
            form.Validate();

            if(form.IsValid)
            {
                try
                {
                    int rowsAffected = _productServices.Product_LogicalDelete(CurrentProduct);

                    if (rowsAffected == 0)
                    {
                        errorMessages.Add($"Product {CurrentProduct.ProductName} has not been discontinued. Please check to see if the product is still on file.");
                    }
                    else
                    {
                        feedback = $"Product {CurrentProduct.ProductName} was successfully updated and discontinued.";
                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Save Error: {GetInnerException(ex).Message}");
                }
            }
        }
        private void ReactivateProduct()
        {
            errorMessages.Clear();
            feedback = string.Empty;
            form.Validate();

            if(form.IsValid)
            {
                try
                {
                    int rowsAffected = _productServices.Product_Reactivate(CurrentProduct);

                    if (rowsAffected == 0)
                    {
                        errorMessages.Add($"Product {CurrentProduct.ProductName} has not been reactivated. Please check to see if the product is still on file.");
                    }
                    else
                    {
                        feedback = $"Product {CurrentProduct.ProductName} was successfully updated and reactivated.";
                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Save Error: {GetInnerException(ex).Message}");
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
