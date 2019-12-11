﻿using FastCost.Models;
using FastCost.Services;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
    public partial class EditComponentTab2 : ContentPage
    {
        public EditComponentTab2()
        {
            InitializeComponent();
            GetItems();
        }
        public ObservableCollection<ComponentItemsModel> Items { get; set; }

        public async void GetItems()
        {
            acindicator.IsRunning = true;
            acindicator.IsVisible = true;
            HttpClient client = new HttpClient();
            var dashboardEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllItems;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
            //var dashboardEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllItems;
            var imageEndpoint = ConstantsValue.MainAddress + ConstantsValue.ImageUrl;
            var result = await client.GetStringAsync(dashboardEndpoint);
            var ItemsList = JsonConvert.DeserializeObject<List<ComponentItemsModel>>(result);
            Items = new ObservableCollection<ComponentItemsModel>(ItemsList);

            //BindingContext = ItemsList;

            Emplist.ItemsSource = Items;
            acindicator.IsRunning = false;
            acindicator.IsVisible = false;

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
                var ItemsList = JsonConvert.DeserializeObject<List<ComponentItemsModel>>(result);


                if (ItemsList != null)
                {
                    acindicator.IsRunning = false;
                    acindicator.IsVisible = false;

                    //Itemsearch.IsVisible = true;
                    //BindingContext = ItemsList;
                    Emplist.ItemsSource = ItemsList;
                    //Itemsearch.ItemsSource = ItemsList;
                    // Autocomplete.IsEnabled = true;
                }
            }

            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //acindicator.IsRunning = true;
                //acindicator.IsVisible = true;
                GetItems();
                //acindicator.IsRunning = false;
                //acindicator.IsVisible = false;


            }
        }

        //public List<ItemsModel> listItemA = new List<ItemsModel>();
        public void AddItemToComponent(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;

            //var button = sender as Button;
            var Selecteditem = button.CommandParameter as ComponentItemsModel;

            var itemId = Selecteditem.Item_Id;
            ConstantsValue.editComponentList.Add(Selecteditem);
            PopupNavigation.Instance.PushAsync(new ItemPopUp());
            MessagingCenter.Subscribe<ItemsModel>(this, "PopUpData", (value) =>
            {

                


                var index = ConstantsValue.editComponentList.Count - 1;
                ConstantsValue.editComponentList[index].Quantity = value.Quantity;

                
            });

            DisplayAlert(Selecteditem.ItemName, "Added to component", "OK");
        }
        
    }
}