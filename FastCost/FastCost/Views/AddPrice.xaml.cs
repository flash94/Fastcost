using FastCost.Models;
using FastCost.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPrice : ContentPage
    {
        string itemId;
        public AddPrice(string Item_Id)
        {

            InitializeComponent();
            itemId = Item_Id;
        }
        private async void backClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async  void AddPriceClicked(object sender, EventArgs e)
        {
            actindicator.IsVisible = true;
            actindicator.IsRunning = true;
            ItemPriceModel newprice = new ItemPriceModel()
            {
                City = DealerCity.Text,
                SellerWeblink = DealerWeb.Text,
                DealersAddress = DealerAddr.Text,
                DealerPhone = DealerPhone.Text,
                Price = Price.Text,
                DealersName = DealerName.Text,
            };



            HttpClient client = new HttpClient();

            var priceEndpoint = ConstantsValue.AddNewPrice + itemId;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            var json = JsonConvert.SerializeObject(newprice);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpContent result = new StringContent(json);
            result.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(priceEndpoint, result);

            if (response.IsSuccessStatusCode)
            {
                actindicator.IsVisible = false;
                actindicator.IsRunning = false;
                await DisplayAlert("Success", Price.Text + " Added Successful", "ok");
                await Shell.Current.Navigation.PopModalAsync();

            }
            DealerCity.Text = "";
            DealerWeb.Text = "";
            DealerAddr.Text = "";
            DealerPhone.Text = "";
            Price.Text = "";
            DealerName.Text = "";
        }

    }
}