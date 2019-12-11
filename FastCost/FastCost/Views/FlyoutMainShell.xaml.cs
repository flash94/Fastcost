
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
    public partial class FlyoutMainShell : Shell
    {
        int userid;
        public FlyoutMainShell(int Id)
        {
            userid = Id;
            SidebarItems sidebaritems = new SidebarItems();
            sidebaritems.Items = "Items";
            sidebaritems.Dashboard = "Dashboard";
            sidebaritems.Components = "Components";
            sidebaritems.Projects = "Projects";
            sidebaritems.Users = "Users";
            BindingContext = sidebaritems;
            InitializeComponent();
        }

        public async void LoadSingleUser()

        {
            //string url = $"http://192.168.1.118:5000/items/{Item_Id}";

            HttpClient client = new HttpClient();
            var url = ConstantsValue.MainAddress + ConstantsValue.GetSingleUser + userid;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
            var result = await client.GetStringAsync(url);
            var UserDetail = JsonConvert.DeserializeObject<List<GetUsersModel>>(result);
            //SingleUserDetail = new ObservableCollection<GetUsersModel>(UserDetail);

            //BindingContext = ItemsList;
            //UserDetails.BindingContext = UserDetail[0];

        }
    }
}