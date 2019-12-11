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
    public partial class ProjectsPageTop : ContentView
    {
        public ProjectsPageTop()
        {
            InitializeComponent();
        }

        private async void AddProjectClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new CreateProjectTabbed());
        }
    }
}