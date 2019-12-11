using FastCost.Models;
using FastCost.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class EditItem : ContentPage
    {

        public ObservableCollection<ItemsModel> Prices { get; set; }

        string myId;
        public EditItem(string Item_Id)
{
            InitializeComponent();
            //FillList();
            myId = Item_Id;
            LoadSingleItem(Item_Id);
            LoadItemPrice(Item_Id);

            
        }

       public async void LoadSingleItem(string Item_Id)
           
        {
            //string url = $"http://192.168.1.118:5000/items/{Item_Id}";
            var url = ConstantsValue.MainAddress + ConstantsValue.AllItems + Item_Id;
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(url);
            var ItemsList = JsonConvert.DeserializeObject<ItemsModel>(result);
            //BindingContext = ItemsList;
            SingleItemDetails.BindingContext = ItemsList;
        }

        public async void LoadItemPrice(string Item_Id)
        {
            var url = ConstantsValue.MainAddress + ConstantsValue.ItemPrices + Item_Id;
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(url);
            var ItemsPriceList = JsonConvert.DeserializeObject<List<ItemPriceModel>>(result);
            Emplist.ItemsSource = ItemsPriceList;

        }

        private async void MakePreffered(object Sender, EventArgs args)
        {
            Button button = (Button)Sender;

            //var button = sender as Button;
            var Selecteditem = button.CommandParameter as ItemPriceModel;
            var itemId = Selecteditem.Item_Id;

            ItemPriceModel itemprice = new ItemPriceModel()
            {
                City = Selecteditem.City,
                SellerWeblink = Selecteditem.SellerWeblink,
                DealersAddress = Selecteditem.DealersAddress,
                DealerPhone = Selecteditem.DealerPhone,
                Price = Selecteditem.Price,
                DealersName = Selecteditem.DealersName,
    };
            HttpClient client = new HttpClient();

            var SelectPrice = ConstantsValue.ChoosePrefferedPrice + itemId;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            var json = JsonConvert.SerializeObject(itemprice);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpContent result = new StringContent(json);
            result.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(SelectPrice, result);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert(Selecteditem.Price, "Set as your Preffered Price", "OK");
            }


        }

        public void EditItemRefreshing(object sender, EventArgs e)
        {
            LoadItemPrice(myId);
            Emplist.EndRefresh();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItemPrice(myId);
        }
        private async void backClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void AddPriceClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddItems());
            await Shell.Current.Navigation.PushModalAsync(new AddPrice(myId));
        }

    }
}