using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using FastCost.Views;
using Xamarin.Forms.Xaml;
using FastCost.Services;
using System.Net.Http;
using FastCost.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPageTop : ContentView
    {
        public ObservableCollection<ItemsModel> Items { get; private set; }
        

        public ItemsPageTop()
        {
            InitializeComponent();
            //InitSearchBar();
            
            
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
                    
                }

                //string url = $"http://192.168.1.118:5000/items?ItemName={e.NewTextValue}";
                var url = ConstantsValue.MainAddress + ConstantsValue.SearchItems + e.NewTextValue;
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var ItemsList = JsonConvert.DeserializeObject<List<ItemsModel>>(result);
                ConstantsValue.itemslist = ItemsList;

                if (ItemsList != null)
                {
                    ViewItems viewItems = new ViewItems();
                    acindicator.IsRunning = false;
                    acindicator.IsVisible = false;

                    Itemsearch.IsVisible = true;
                    viewItems.GetItems();
                    
                    Itemsearch.ItemsSource = ConstantsValue.itemslist;
                    
                }
            }

            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //viewItems.GetItems();

                acindicator.IsRunning = false;
                acindicator.IsVisible = false;
                Itemsearch.IsVisible = false;
                Itemsearch.ItemsSource = "";
            }
        }

    }
}