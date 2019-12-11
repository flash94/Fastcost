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
    public partial class CreateProjectTabbed : TabbedPage
    {
        public ObservableCollection<ComponentItemsModel> Components { get; set; }

        public CreateProjectTabbed()
        {
            InitializeComponent();
        }

        private async void arrowClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddItems());
            //await Shell.Current.GoToAsync("//ItemsPage");
            await Shell.Current.Navigation.PopModalAsync();
        }

        public void GetAddedComponents()
        {

            var addedComponents = ConstantsValue.addComponent;
            Components = new ObservableCollection<ComponentItemsModel>(addedComponents);
            Emplist.ItemsSource = Components;
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


            var addedComponents = ConstantsValue.addComponent;
            Components = new ObservableCollection<ComponentItemsModel>(addedComponents);
            Emplist.ItemsSource = Components;


        }

        private void RemoveComponent(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            var Selecteditem = button.CommandParameter as ComponentItemsModel;
            var SelectedId = Selecteditem.component_Id;
            //ConstantsValue.listItemA.RemoveAll(x => x.Item_Id == SelectedId);
            var componentToRemove = ConstantsValue.addComponent.Single(r => r.Item_Id == SelectedId);
            ConstantsValue.addComponent.Remove(componentToRemove);
            var addedComponents = ConstantsValue.addComponent;
            Components = new ObservableCollection<ComponentItemsModel>(addedComponents);
            Emplist.ItemsSource = Components;
        }

        private async void CreateComponentClicked(object sender, EventArgs e)
        {


            ProjectsModel projectmodel = new ProjectsModel();
            projectmodel.ProjectName = projectName.Text;
            projectmodel.ProjectDescription = projectDescription.Text;
            Int32 length = ConstantsValue.addComponent.Count;
            projectmodel.NoOfComponent = length;
            projectmodel.Item = ConstantsValue.addComponent;

            string json = JsonConvert.SerializeObject(projectmodel, Formatting.Indented);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var addProject = ConstantsValue.CreateProject;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            var result = await client.PostAsync(addProject, content);

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Project Created Successful", "Ok");
                projectName.Text = "";
                projectDescription.Text = "";
                Emplist.ItemsSource = null;
                ConstantsValue.addComponent.Clear();
            }
            else
            {
                await DisplayAlert("Error ", "Project Not Created", "Ok");
            }

        }
    }
}