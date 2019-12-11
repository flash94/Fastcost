using FastCost.Models;
using FastCost.Services;
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
    public partial class Components : ContentPage
    {
      
        public Components()
        {
            InitializeComponent();
            GetComponents();
        }

        public ObservableCollection<ComponentsModel> AllComponents { get; private set; }

        public async void GetComponents()
        {
            HttpClient client = new HttpClient();
            var dashboardEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllComponents;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
            var result = await client.GetStringAsync(dashboardEndpoint);
            var ComponentsList = JsonConvert.DeserializeObject<List<ComponentsModel>>(result);
            AllComponents = new ObservableCollection<ComponentsModel>(ComponentsList);

            Emplist.ItemsSource = AllComponents;
            //BindingContext = AllComponents;

                        

        }

        async void ViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as ComponentsModel;
            await DisplayAlert("Alert", "You Pressed Something!", "OK" + selectedItem.Id);
            await Shell.Current.Navigation.PushModalAsync(new EditComponentTabbed(selectedItem.Id));
            //await this.Navigation.PushModalAsync(new EditItem(selectedItem.Item_Id));


            //((ListView)sender).SelectedItem = null;
        }

        private async void AddComponentClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddItems());
            await Shell.Current.Navigation.PushModalAsync(new CreateComponentTabbed());
        }
        protected void ComponentRefreshing(object sender, EventArgs e)
        {
            GetComponents();
            Emplist.EndRefresh();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetComponents();
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


                HttpClient client = new HttpClient();
                string url = $"http://192.168.1.118:5000/projects?ProjectName={e.NewTextValue}";
                //var url = ConstantsValue.MainAddress + ConstantsValue.SearchItems + e.NewTextValue;


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
                GetComponents();
                acindicator.IsRunning = false;
                acindicator.IsVisible = false;


            }
        }

    }
}