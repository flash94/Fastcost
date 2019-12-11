using FastCost.Models;
using FastCost.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardProjectManager : ContentPage
    {
        public DashboardProjectManager()
        {
            GetDashboardDetail();
            InitializeComponent();
        }
        public async void GetDashboardDetail()
        {
            using (var client = new HttpClient())
            {
                //send a Get request
                var dashboardEndpoint = ConstantsValue.MainAddress + ConstantsValue.Dashboard;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
                var result = await client.GetStringAsync(dashboardEndpoint);
                //string response = await result.Content.ReadAsStringAsync();

                //handling the answer
                //var dashboardList = JsonConvert.DeserializeObject<List<DashboardModel>>(result);
                //List<List<DashboardModel>> dashboardList = JsonConvert.DeserializeObject<List<List<DashboardModel>>>(result);
                var dashboardList = JsonConvert.DeserializeObject<List<DashboardModel[]>>(result);

                //DashboardLayout.BindingContext = dashboardList;
                string NumberOfProjects = dashboardList[0][0].NumberOfProjects;
                string NumberOfComponents = dashboardList[1][0].NumberOfComponents;
                string NumberOfItems = dashboardList[2][0].NumberOfItems;
                string NumberOfUsers = dashboardList[3][0].NumberOfUsers;


                DashboardModel dashboardmodel = new DashboardModel();
                dashboardmodel.NumberOfProjects = NumberOfProjects;
                dashboardmodel.NumberOfComponents = NumberOfComponents;
                dashboardmodel.NumberOfItems = NumberOfItems;
                dashboardmodel.NumberOfUsers = NumberOfUsers;

                BindingContext = dashboardmodel;

            }
        }

        private async void AllItems(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new ViewItems());
        }

        async void AllComponents(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new Components());
        }

        async void AllProjects(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new Projects());
        }

    }


}
