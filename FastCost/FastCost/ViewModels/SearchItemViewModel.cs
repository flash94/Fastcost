using FastCost.Models;
using FastCost.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FastCost.ViewModels
{
    class SearchItemViewModel : BaseViewModel
        
    {
    //    HttpClient _client;
    //    public async Task LoadProducts(string term)
    //    {

    //        //var items = new List<Customer> {new Customer {Name="Tolu Ogundiji", AccountNo ="0034142919", FirstName="Tolu", LastName="Ogundiji" },
    //        //new Customer{Name="Atunmobi Mayowa", AccountNo ="00123498", FirstName ="Mayowa", LastName="Ogundiji"
    //        //} };
    //        List<Models.ItemsModel> profile = new List<ItemsModel>();
            
    //        if (ConstantsValue.itemslist == null)
    //        {


    //            _client = new HttpClient();

    //            var uri = new Uri(string.Format(helper.customersearchurl, string.Empty));
    //            string url = $"http://192.168.0.5/api/Masters/DeleteEmployee?EmployeeId={_employee.Id}";

    //            _client.DefaultRequestHeaders.Clear();
    //            _client.DefaultRequestHeaders.Add("Token", helper.userprofile.token);



    //            HttpResponseMessage response = null;

    //            response = await _client.GetAsync(uri);



    //            if (response.IsSuccessStatusCode)
    //            {
    //                profile = JsonConvert.DeserializeObject<List<Models.RegisterCustomerModel>>(response.Content.ReadAsStringAsync().Result);


    //                helper.customerslist = profile;

    //            }
    //        }
    //        else
    //        {


    //            term = term.ToLower();
    //            // System.Diagnostics.Debug.WriteLine("term " + term);

    //            var modprofile = (from k in helper.customerslist where k.firstName.ToLower().Contains(term) || k.lastName.ToLower().Contains(term) || k.phoneNumber1.ToLower().Contains(term) select k).ToList();

    //            if (string.IsNullOrEmpty(term))
    //            {
    //                modprofile = null;

    //            }

    //            Device.BeginInvokeOnMainThread(() =>
    //            {
    //                CustomerSuggestions = new ObservableCollection<RegisterCustomerModel>(modprofile);
    //                OnPropertyChanged("CustomerSuggestions");
    //                //  if (CustomerSuggestions != null)
    //                //{
    //                //  System.Diagnostics.Debug.WriteLine(CustomerSuggestions.Count());
    //                // }
    //            }

    //            );


    //        }

    //    }
    }
}
