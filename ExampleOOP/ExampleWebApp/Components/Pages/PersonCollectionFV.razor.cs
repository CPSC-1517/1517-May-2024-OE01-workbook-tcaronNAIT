using ExampleOOP;
using ExampleWebApp.Components.ViewModels;
using MudBlazor;
using FluentValidation;
using ExampleWebApp.DTOs;

namespace ExampleWebApp.Components.Pages
{
    public partial class PersonCollectionFV
    {
        private List<string> provinceList = [];
        private PersonDto newPerson = new();
        //private string firstName = string.Empty;
        //private string lastName = string.Empty;
        private string city = string.Empty;
        private string province = string.Empty;
        private int streetNumber;
        private string streetName = string.Empty;
        private string postalCode = string.Empty;
        private List<string> errorMessages = [];
        private Person person = null;
        private MudForm form = new();
        private string[] errors = { };
        private string successMessage = string.Empty;
        private readonly PersonValidationVM personValidator = new();

        protected override void OnInitialized()
        {
            provinceList = new List<string>
            {
                "Alberta",
                "British Columbia",
                "Manitoba",
                "New Brunswick",
                "Newfoundland and Labrador",
                "Northwest Territories",
                "Nova Scotia",
                "Nunavut",
                "Ontario",
                "Prince Edward Island",
                "Quebec",
                "Saskatchewan",
                "Yukon"
            };
            base.OnInitialized();
        }

        private void AddPerson()
        {
            errorMessages.Clear();
            form.Validate();
            if (form.IsValid)
            {
                try
                {
                    person = new Person(newPerson.FirstName, newPerson.LastName, new ResidentAddress(streetNumber, streetName, city, province, postalCode), null);
                    string fileName = @"wwwroot/data/Persons.csv";
                    string line = $"{person.ToString()}\n";
                    File.AppendAllText(fileName, line);
                    successMessage = $"{newPerson.FirstName} {newPerson.LastName} added successfully!";
                }
                catch (Exception ex)
                {
                    errorMessages.Add(GetInnerException(ex).Message);
                }
            }
        }

        //Using JSRunTime service
        //We need to make the method async
        // private async Task ClearForm()
        // {

        //     //WARNING: remember to inject IJSRuntime at the top of the page! (after the @page and PageTitle)

        //     //issue a prompt dialogue to the user to obtain their confirmation of the action!
        //     //Create the message in a generic object array
        //     object[] confirmationMessage = new object[] { "Clearing will lose any unsaved data. Are you sure you want to clear the form?" };

        //     if(await JSRunTime.InvokeAsync<bool>("confirm", confirmationMessage))
        //     {
        //         await form.ResetAsync();
        //         errorMessages.Clear();
        //         successMessage = string.Empty;
        //     }
        // }

        //MudBlazor Method using DialogService
        private async void ClearForm()
        {
            bool? result = await DialogService.ShowMessageBox("Confirm Clear", "Clearing will lose any unsaved data. Are you sure you want to clear the form?", yesText: "Clear Form", cancelText: "No don't!", noText: "Really don't Clear!");

            //Yes results = true, No result = false, Cancel result = null
            if (result != null && result != false)
            {
                await form.ResetAsync();
                errorMessages.Clear();
                successMessage = string.Empty;
            }
        }

        private Exception GetInnerException(Exception ex)
        {
            //drill down into your Exception until there are no more inner exceptions
            //at this point you have the "real" error
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
