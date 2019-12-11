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
    public partial class ComponentsPageTop : ContentView
    {
        public ComponentsPageTop()
        {
            InitializeComponent();
        }
        private async void AddComponentClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddItems());
            await Shell.Current.Navigation.PushModalAsync(new CreateComponentTabbed());
        }
    }
}