using FastCost.Models;
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
    public partial class CreateProjectTab2 : ContentPage
    {

        public ObservableCollection<ComponentItemsModel> Components { get; set; }

        public CreateProjectTab2()
        {
            InitializeComponent();
            GetComponents();
        }
     

        public async void GetComponents()
        {
            acindicator.IsRunning = true;
            acindicator.IsVisible = true;
            HttpClient client = new HttpClient();
            var componentsEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllComponents;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
            //var dashboardEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllItems;
            //var imageEndpoint = ConstantsValue.MainAddress + ConstantsValue.ImageUrl;
            var result = await client.GetStringAsync(componentsEndpoint);
            var ItemsList = JsonConvert.DeserializeObject<List<ComponentItemsModel>>(result);
            Components = new ObservableCollection<ComponentItemsModel>(ItemsList);

            //BindingContext = ItemsList;

            Emplist.ItemsSource = Components;
            acindicator.IsRunning = false;
            acindicator.IsVisible = false;

        }

        async void ViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as ComponentItemsModel;
            await DisplayAlert("Alert", "You Pressed Something!", "OK" + selectedItem.Item_Id);

            await Shell.Current.Navigation.PushModalAsync(new EditItem(selectedItem.Item_Id.ToString()));
            //await this.Navigation.PushModalAsync(new EditItem(selectedItem.Item_Id));


            //((ListView)sender).SelectedItem = null;
        }

        public async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 2)
            {


              
                    acindicator.IsRunning = true;
                    acindicator.IsVisible = true;


                //string url = $"http://192.168.1.118:5000/items?ItemName={e.NewTextValue}";
                HttpClient client = new HttpClient();
                var url = ConstantsValue.MainAddress + ConstantsValue.SearchItems + e.NewTextValue;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
                
                var result = await client.GetStringAsync(url);
                var ItemsList = JsonConvert.DeserializeObject<List<ItemsModel>>(result);

                    acindicator.IsRunning = false;
                    acindicator.IsVisible = false;

                    //Itemsearch.IsVisible = true;
                    //BindingContext = ItemsList;
                    Emplist.ItemsSource = ItemsList;
                    //Itemsearch.ItemsSource = ItemsList;
                    // Autocomplete.IsEnabled = true;
        
            }

            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                acindicator.IsRunning = true;
                acindicator.IsVisible = true;
                GetComponents();
                acindicator.IsRunning = false;
                acindicator.IsVisible = false;


            }
        }

        public void AddComponentToProject(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;

            //var button = sender as Button;
            var SelectedComponent = button.CommandParameter as ComponentItemsModel;

            var itemId = SelectedComponent.Item_Id;
            ConstantsValue.addComponent.Add(SelectedComponent);
            PopupNavigation.Instance.PushAsync(new ItemPopUp());
            MessagingCenter.Subscribe<ItemsModel>(this, "PopUpData", (value) =>
            {


                var index = ConstantsValue.addComponent.Count - 1;
                ConstantsValue.addComponent[index].Quantity = value.Quantity;

                if (ConstantsValue.addComponent[index].Quantity == value.Quantity)
                {
                    DisplayAlert(SelectedComponent.ComponentName, "Added to Project", "OK");
                }
               
            });

        }
    }
}