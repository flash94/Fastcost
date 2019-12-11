using FastCost.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProjectsTabbed : TabbedPage
    {
        public ObservableCollection<AddedItems> employees;
        AddedItemBl _EBL = new AddedItemBl();
        public EditProjectsTabbed(int Id)
        {
            InitializeComponent();
            FillList();
        }
        public async void FillList()
        {
            employees = new ObservableCollection<AddedItems>();
            var _data = await _EBL.GetRecord();
            if (_data.Count != 0)
            {
                employees = _data;
                Emplist.ItemsSource = _data;
            }
            else
            {
                employees = null;
            }
        }

        private async void arrowClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

    }
}