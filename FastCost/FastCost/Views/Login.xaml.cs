using FastCost.Models;
using FastCost.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            App.StartCheckIfInternet(lbl_NoInternet, this);
        }

        public async void loginclick(object sender, EventArgs e)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            Users users = new Users(text_email.Text, txtPassword.Text)
            {
                Email = text_email.Text,
                Password = txtPassword.Text
            };
            
                if (users.CheckInformation())
                {
                var json = JsonConvert.SerializeObject(users);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Token", HelperAppSettings.Token);
                var loginEndpoint = ConstantsValue.loginurl;

                var result = await client.PostAsync(loginEndpoint, content);



                string responsee = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    var profile = JsonConvert.DeserializeObject<Models.Users>(result.Content.ReadAsStringAsync().Result);

                    ConstantsValue.userprofile = profile;
                    
                    HelperAppSettings.Token = profile.token;
                    HelperAppSettings.LastName = profile.LastName;
                    HelperAppSettings.MiddleName = profile.MiddleName;
                    HelperAppSettings.Email = profile.Email;
                    HelperAppSettings.PhoneNumber = profile.PhoneNumber;
                    //HelperAppSettings.role = string.Join(",", profile.roleId);
                    HelperAppSettings.Address = profile.Address;
                    HelperAppSettings.Password = profile.Password;
                    HelperAppSettings.Role = profile.Role;
                    HelperAppSettings.id = profile.id;
                    int Id = profile.id;
                    FlyoutMainShell fpm = new FlyoutMainShell(Id);

                    if (profile.Role == 1)
                    {

                        Application.Current.MainPage = fpm;

                    }
                    else if (profile.Role == 5)
                    {
                        Application.Current.MainPage = new FlyoutshellProjectManager();
                    }
                    else if (profile.Role == 10)
                    {
                        Application.Current.MainPage = new FlyoutshellScout();
                    }



                //    var json = JsonConvert.SerializeObject(users);
                //    var content = new StringContent(json, Encoding.UTF8, "application/json");

                //    HttpClient client = new HttpClient();
                //    var loginEndpoint = ConstantsValue.MainAddress + ConstantsValue.Login;
                    
                    
                //    var result = await client.PostAsync(loginEndpoint, content);


                //string responsee = await result.Content.ReadAsStringAsync();
                    //if (result.IsSuccessStatusCode)
                    //{
                    
                    //Application.Current.MainPage = new FlyoutMainShell();
                    //await Shell.Current.Navigation.PushModalAsync(new FlyoutMainShell());
                    //Shell.NavBarIsVisible = "False"
                }
                    else
                    {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    await DisplayAlert("Login Error", "Invalid Username or Password", "Ok");
                        
                }

                    //App.UserDatabase.SaveUser(user);

                }
                else
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    await DisplayAlert("Login", "Login details not correct", "ok");
                    
            }
            
        }
    }
}