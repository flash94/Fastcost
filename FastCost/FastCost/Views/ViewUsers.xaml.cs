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

    
    public partial class ViewUsers : ContentPage
    {
        public ObservableCollection<AddedItems> employees;
        AddedItemBl _EBL = new AddedItemBl();
        public ViewUsers()
        {
            InitializeComponent();
            GetUsers();
        }
        //public async void FillList()
        //{
        //    employees = new ObservableCollection<AddedItems>();
        //    var _data = await _EBL.GetRecord();
        //    if (_data.Count != 0)
        //    {
        //        employees = _data;
        //        Emplist.ItemsSource = _data;
        //    }
        //    else
        //    {
        //        employees = null;
        //    }
        //}

        public ObservableCollection<GetUsersModel> Users { get; set; }

        public async void GetUsers()
        {
            HttpClient client = new HttpClient();
            var allUsersEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllUsers;
            
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
            var result = await client.GetStringAsync(allUsersEndpoint);
            var UsersList = JsonConvert.DeserializeObject<List<GetUsersModel>>(result);
            Users = new ObservableCollection<GetUsersModel>(UsersList);
            Emplist.ItemsSource = Users;
            //Emplist.EndRefresh();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetUsers();

        }

        async void ViewUserTapped(Object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as GetUsersModel;
            await DisplayAlert("Alert", "You Pressed Something!", "OK" + selectedItem.id);
            await Shell.Current.Navigation.PushModalAsync(new UpdateUser(selectedItem.id));
        }
    }
}