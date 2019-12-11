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
    public partial class CreateComponentTabbed : TabbedPage
    {
        public ObservableCollection<ItemsModel> Items { get; private set; }

        public CreateComponentTabbed()
        {
            InitializeComponent();
        }

        private async void arrowClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddItems());
            //await Shell.Current.GoToAsync("//ItemsPage");
            await Shell.Current.Navigation.PopModalAsync();
        }

        public void GetAddedItems()
        {

            var addedItems = ConstantsValue.listItemA;
            Items = new ObservableCollection<ItemsModel>(addedItems);
            Emplist.ItemsSource = Items;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    var addedItems = ConstantsValue.listItemA;
        //    Items = new ObservableCollection<ItemsModel>(addedItems);
        //    Emplist.ItemsSource = Items;

        //}

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();


            var addedItems = ConstantsValue.listItemA;
            Items = new ObservableCollection<ItemsModel>(addedItems);
            Emplist.ItemsSource = Items;


        }

        private void RemoveItem(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            var Selecteditem = button.CommandParameter as ItemsModel;
            var SelectedId = Selecteditem.Item_Id;
            //ConstantsValue.listItemA.RemoveAll(x => x.Item_Id == SelectedId);
            var itemToRemove = ConstantsValue.listItemA.Single(r => r.Item_Id == SelectedId);
            ConstantsValue.listItemA.Remove(itemToRemove);
            var addedItems = ConstantsValue.listItemA;
            Items = new ObservableCollection<ItemsModel>(addedItems);
            Emplist.ItemsSource = Items;
        }

        private async void CreateComponentClicked(object sender, EventArgs e)
        {
            

            ComponentsModel componentmodel = new ComponentsModel();
            componentmodel.ComponentName = componentName.Text;
            componentmodel.ComponentDescription = componentDescription.Text;
            Int32 length = ConstantsValue.listItemA.Count;
            componentmodel.NoOfItems = length.ToString();
            componentmodel.Item = ConstantsValue.listItemA;

            string json = JsonConvert.SerializeObject(componentmodel, Formatting.Indented);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            var addComponent = ConstantsValue.BaseAddress + ConstantsValue.SingleComponent;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
            var result = await client.PostAsync(addComponent, content);

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Component Created Successful", "Ok");
                componentName.Text = "";
                componentDescription.Text = "";
                Emplist.ItemsSource = null;
                ConstantsValue.listItemA.Clear();
            }
            else
            {
                await DisplayAlert("Error ", "Component Not Created", "Ok");
            }

        }
    }
}