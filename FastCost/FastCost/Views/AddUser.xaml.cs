using Android.Graphics;
using FastCost.Models;
using FastCost.Services;
using FastCost.ViewModels;
using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUser : ContentPage
    {
        public AddUser()
        {
            InitializeComponent();
            BindingContext = new RoleViewModel();

        }


        private async void backClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void CreateUserClicked(object sender, EventArgs e)
        {
            actindicator.IsVisible = true;
            actindicator.IsRunning = true;
            AddUserModel adduser = new AddUserModel()
            {
                FirstName = UserFirstName.Text,
                LastName = UserLastname.Text,
                PhoneNumber = UserPhone.Text,
                Address = UserAddress.Text,
                Email = UserEmail.Text,
                Password = UserPassword.Text,
                Location = UserCity.Text,
                //Role = Rolepicker.SelectedItem.ToString()
                Role = (Rolepicker.SelectedItem as Role).Value

            };

            

            HttpClient client = new HttpClient();

            var registerEndpoint = ConstantsValue.createuser;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            var json = JsonConvert.SerializeObject(adduser);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpContent result = new StringContent(json);
            result.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(registerEndpoint, result);

            if (response.IsSuccessStatusCode)
            {
                actindicator.IsVisible = false;
                actindicator.IsRunning = false;
                await DisplayAlert("Success", UserFirstName.Text + "Registered Successful", "ok");
                await Shell.Current.Navigation.PopModalAsync();

            }
            UserFirstName.Text = "";
            UserLastname.Text = "";
            UserPhone.Text = "";
            UserAddress.Text = "";
            UserEmail.Text = "";
            UserPassword.Text = "";
            UserCity.Text = "";
        }
    }
    
}