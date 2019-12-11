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
    public partial class EditComponentTabbed : TabbedPage
    {
        public ObservableCollection<AddedItems> employees;
        AddedItemBl _EBL = new AddedItemBl();

        public ObservableCollection<ComponentItemsModel> Items { get; private set; }
        int compId;
        public EditComponentTabbed(int Id)
        {
            InitializeComponent();
            //FillList();
            LoadSingleComponent(Id);
            LoadComponentItems(Id);
            compId = Id;
        }

        public async void LoadSingleComponent(int Id)

        {
            //string url = $"http://192.168.1.118:5000/component/{Id}";
            
            HttpClient client = new HttpClient();
            string url = ConstantsValue.MainAddress + ConstantsValue.SingleComponent + Id;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            var result = await client.GetStringAsync(url);
            var ComponentList = JsonConvert.DeserializeObject<List<ComponentsModel>>(result);
            //BindingContext = ItemsList;
            SingleComponentDetails.BindingContext = ComponentList[0];
        }

        public async void LoadComponentItems(int Id)
        {
            Emplist.ItemsSource = ConstantsValue.editComponentList;

            if (ConstantsValue.editComponentList.Count==0)
            {
                //string url = $"http://192.168.1.118:5000/component/items/{Id}";
                HttpClient client = new HttpClient();

                string url = ConstantsValue.MainAddress + ConstantsValue.ComponentItems + Id;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

                var result = await client.GetStringAsync(url);
                var ComponentItemsList = JsonConvert.DeserializeObject<List<ComponentItemsModel>>(result);
                ConstantsValue.editComponentList = ComponentItemsList;
                Emplist.ItemsSource = ConstantsValue.editComponentList;
            }
            

        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();


            var addedItems = ConstantsValue.editComponentList;
            Items = new ObservableCollection<ComponentItemsModel>(addedItems);
            Emplist.ItemsSource = Items;


        }

        private void RemoveItem(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            var Selecteditem = button.CommandParameter as ComponentItemsModel;
            var SelectedId = Selecteditem.Item_Id;
            //ConstantsValue.listItemA.RemoveAll(x => x.Item_Id == SelectedId);
            var itemToRemove = ConstantsValue.editComponentList.Single(r => r.Item_Id == SelectedId);
            ConstantsValue.editComponentList.Remove(itemToRemove);
            var addedItems = ConstantsValue.editComponentList;
            Items = new ObservableCollection<ComponentItemsModel>(addedItems);
            Emplist.ItemsSource = Items;
        }

        private async void arrowClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void UpdateComponentClicked(object sender, EventArgs e)
        {


            ComponentItemsModel componentitemsmodel = new ComponentItemsModel();
            componentitemsmodel.ComponentName = componentName.Text;
            componentitemsmodel.ComponentDescription = componentDescription.Text;
            Int32 length = ConstantsValue.editComponentList.Count;
            componentitemsmodel.NoOfItems = length.ToString();
            componentitemsmodel.Item = ConstantsValue.editComponentList;

            string json = JsonConvert.SerializeObject(componentitemsmodel, Formatting.Indented);

            //var content = new StringContent(json, Encoding.UTF8, "application/json");

            
            

            HttpClient client = new HttpClient();
            //string url = $"http://192.168.1.118:5000/component/items/{compId}";

            var url = ConstantsValue.EditComponent + compId;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PutAsync(url, content);

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Component Updated Successful", "Ok");
                componentName.Text = "";
                componentDescription.Text = "";
                Emplist.ItemsSource = null;
                ConstantsValue.editComponentList.Clear();
            }
            else
            {
                await DisplayAlert("Error ", "Component Not Updated", "Ok");
            }

        }

    }
}