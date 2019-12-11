using FastCost.Models;
using FastCost.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FastCost.ViewModels
{
    class ItemsViewModel : INotifyPropertyChanged
    {
        
        public ObservableCollection<ItemsModel> Items;

       // public INavigation Navigation { get; set; }

        public ItemsViewModel()
        {

            //Navigation = _navigation;
            GetItems();
        }

        public async void GetItems()
        {
            HttpClient client = new HttpClient();
            var dashboardEndpoint = ConstantsValue.MainAddress + ConstantsValue.AllItems;
            var result = await client.GetStringAsync(dashboardEndpoint);
            var ItemsList = JsonConvert.DeserializeObject<List<ItemsModel>>(result);
            Items = new ObservableCollection<ItemsModel>(ItemsList);
            IsRefreshing = false;
            
            //TitleDisplayView.ItemsSource = items;

            //string NumberOfProjects;
            //ItemsModel itemmodel = new ItemsModel();
            //itemmodel.ItemName = items[0].ItemName;
            //dashboardmodel.NumberOfComponents = NumberOfComponents;
            //dashboardmodel.NumberOfItems = NumberOfItems;
            //dashboardmodel.NumberOfUsers = NumberOfUsers;
        }

        bool _isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
