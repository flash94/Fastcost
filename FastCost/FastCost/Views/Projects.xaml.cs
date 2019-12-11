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
    public partial class Projects : ContentPage
    {
        public ObservableCollection<AddedItems> employees;
        AddedItemBl _EBL = new AddedItemBl();

        public ObservableCollection<ProjectsModel> AllProjects { get; private set; }

        public Projects()
        {
            InitializeComponent();
            //FillList();
            GetProjects();
        }

        private async void AddProjectClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new CreateProjectTabbed());
        }
        public async void GetProjects()
        {
            HttpClient client = new HttpClient();
            var projectEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllProjects;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
            //var imageEndpoint = ConstantsValue.MainAddress + ConstantsValue.ImageUrl;
            var result = await client.GetStringAsync(projectEndpoint);
            var ProjectsList = JsonConvert.DeserializeObject<List<ProjectsModel>>(result);
            AllProjects = new ObservableCollection<ProjectsModel>(ProjectsList);

            Emplist.ItemsSource = AllProjects;
            //BindingContext = AllComponents;



        }

        async void ViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedComponent = e.Item as ProjectsModel;
            await DisplayAlert("Alert", "You Pressed Something!", "OK" + selectedComponent.Id);
            await Shell.Current.Navigation.PushModalAsync(new EditProjectsTabbed(selectedComponent.Id));
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

                
                HttpClient client = new HttpClient();
                string url = $"http://192.168.1.118:5000/components?ComponentName={e.NewTextValue}";
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
                GetProjects();
                acindicator.IsRunning = false;
                acindicator.IsVisible = false;


            }
        }

        protected void ComponentRefreshing(object sender, EventArgs e)
        {
            GetProjects();
            Emplist.EndRefresh();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetProjects();
        }
    }
}
