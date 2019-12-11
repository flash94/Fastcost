using FastCost.Models;
using Rg.Plugins.Popup.Services;
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
    public partial class ItemPopUp
    {
        public ItemPopUp()
        {
            InitializeComponent();
            
        }

        public void AddQuantity(Object Sender, EventArgs args)
        {
            MessagingCenter.Send(new ItemsModel() { Quantity = Int32.Parse(ItemQuantity.Text)}, "PopUpData");
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}