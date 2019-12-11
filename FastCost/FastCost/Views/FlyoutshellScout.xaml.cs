using FastCost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutshellScout : Shell
    {
        public FlyoutshellScout()
        {
            SidebarItems sidebaritems = new SidebarItems();
            sidebaritems.Items = "Items";
            sidebaritems.Dashboard = "Dashboard";
            sidebaritems.Components = "Components";
            //sidebaritems.Projects = "Projects";
            sidebaritems.Users = "Edit Profile";
            BindingContext = sidebaritems;
            InitializeComponent();
            
        }
    }
}