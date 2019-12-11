using FastCost.Models;
using FastCost.Services;
using FastCost.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewItems : ContentPage
    {
        //public ObservableCollection<AddedItems> employees;
        //AddedItemBl _EBL = new AddedItemBl();
        //ItemsViewModel items = new ItemsViewModel();
        
        public ViewItems()
        {
            
            InitializeComponent();
            GetItems();
            
            
            //items.GetItems();
            //BindingContext = items;
            //(BindingContext as ItemsViewModel).GetItems();
            //FillList();
            //var t = new ItemsViewModel();
            //BindingContext = t.GetItems();

        }

        public ObservableCollection<ItemsModel> Items { get; set; }

        public async void GetItems()
        {
            HttpClient client = new HttpClient();
            var dashboardEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllItems;
            
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            var result = await client.GetStringAsync(dashboardEndpoint);
            var ItemsList = JsonConvert.DeserializeObject<List<ItemsModel>>(result);
            Items = new ObservableCollection<ItemsModel>(ItemsList);
            

            ConstantsValue.itemslist = ItemsList;

            if (ConstantsValue.itemslist == null)
            {
                Emplist.ItemsSource = Items;
                Emplist.EndRefresh();

            }
            else if(ConstantsValue.itemslist != null)
            {
                Emplist.ItemsSource = null;
                //Emplist.IsVisible = false;
                Emplist.ItemsSource = ConstantsValue.itemslist;
                Emplist.EndRefresh();
                //Emplist.BeginRefresh();


                //Emplist.EndRefresh();
            }

            
            

        }

        async void ViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as ItemsModel;
            await DisplayAlert("Alert", "You Pressed Something!", "OK" + selectedItem.Item_Id);
            await Shell.Current.Navigation.PushModalAsync(new EditItem(selectedItem.Item_Id));
            //await this.Navigation.PushModalAsync(new EditItem(selectedItem.Item_Id));


            //((ListView)sender).SelectedItem = null;
        }

        public void ItemRefreshing(object sender, EventArgs e)
        {
            GetItems();
            Emplist.EndRefresh();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetItems();
        }
        private async void AddItemClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddItems());
            await Shell.Current.Navigation.PushModalAsync(new AddItems());
        }

        public async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 2)
            {


                if (ConstantsValue.itemslist == null)
                {
                    acindicator.IsRunning = true;
                    acindicator.IsVisible = true;
                    // Autocomplete.IsEnabled = false;
                }

                //string url = $"http://192.168.1.118:5000/items?ItemName={e.NewTextValue}";
                HttpClient client = new HttpClient();
                var url = ConstantsValue.MainAddress + ConstantsValue.SearchItems + e.NewTextValue;
                

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
                var result = await client.GetStringAsync(url);
                var ItemsList = JsonConvert.DeserializeObject<List<ItemsModel>>(result);
                //ConstantsValue.itemslist = ItemsList;

                if (ItemsList != null)
                {
                    
                    acindicator.IsRunning = false;
                    acindicator.IsVisible = false;
                    Emplist.ItemsSource = ItemsList;
                 
                }
            }

            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //viewItems.GetItems();

                acindicator.IsRunning = true;
                acindicator.IsVisible = true;
                GetItems();
                acindicator.IsRunning = false;
                acindicator.IsVisible = false;


            }
        }

    }
}