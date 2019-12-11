using Plugin.Media;
using System;
using FastCost.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Plugin.Media.Abstractions;
using FastCost.Services;
using Xamarin.Forms.PlatformConfiguration;
using System.Net.Http.Headers;
using FastCost.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateUser : ContentPage
    {
        private MediaFile _mediaFile;
        int myid;
        public UpdateUser(int id)
        {
            InitializeComponent();
            myid = id;
            BindingContext = new RoleViewModel();
            LoadSingleUser();

        }

        public ObservableCollection<GetUsersModel> SingleUserDetail { get; private set; }

        public async void LoadSingleUser()

        {
            //string url = $"http://192.168.1.118:5000/items/{Item_Id}";
            
            HttpClient client = new HttpClient();
            var url = ConstantsValue.MainAddress + ConstantsValue.GetSingleUser + myid;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);
            var result = await client.GetStringAsync(url);
            var UserDetail = JsonConvert.DeserializeObject<List<GetUsersModel>>(result);
            //SingleUserDetail = new ObservableCollection<GetUsersModel>(UserDetail);
           
            //BindingContext = ItemsList;
            UserDetails.BindingContext = UserDetail[0];

            if (UserDetail[0].Image == null) 
            {
                userImage.Source = "userimg.png";
            }
        }
        public async void UploadImageTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Warning", "Picking  a photo is not supported", "OK");

                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 40
            });

            userImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

            var name = System.IO.Path.GetFileName(_mediaFile.Path);

            //using (var memoryStream = new MemoryStream())
            //{
            //    _mediaFile.GetStream().CopyTo(memoryStream);
            //    _mediaFile.Dispose();
            //    bytes = memoryStream.ToArray();
            //}
            //Convert file to byte array, to bitmap and set it to my ImageView

            //byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            //Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            //userImage.SetImageBitmap(bitmap);
        }

        private async void backClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void UpdateUserClicked(object sender, EventArgs e)
        {
            //var content = new MultipartFormDataContent();
            //string name = Path.GetFileName(_mediaFile.Path);
            //content.Add(new StreamContent(_mediaFile.GetStream()),
            //   "\"file\"",
            //   $"\"{_mediaFile.Path} \"");

            //HttpContent fileStreamContent = new StreamContent(new FileStream(_mediaFile.Path, FileMode.Open));
            //var content = new MultipartFormDataContent();
            //content.Add(fileStreamContent, "xml", Path.GetFileName(_mediaFile.Path));


            AddUserModel adduser = new AddUserModel()
            {
                FirstName = FirstName.Text,
                LastName = UserLastname.Text,
                PhoneNumber = UserPhone.Text,
                Address = UserAddress.Text,
                Email = UserEmail.Text,
                Location = UserCity.Text,
                Role = (Rolepicker.SelectedItem as Role).Value,

            };


            HttpClient client = new HttpClient();

            var UpdateSingleUser = ConstantsValue.UpdateSingleUser + myid;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            var json = JsonConvert.SerializeObject(adduser);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpContent result = new StringContent(json);
            result.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(UpdateSingleUser, result);

            if (response.IsSuccessStatusCode)
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                if(_mediaFile != null)
                {
                    var file = _mediaFile.Path;
                    if (string.IsNullOrEmpty(file) == false)
                    {
                        var upfilebytes = System.IO.File.ReadAllBytes(file);

                        ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                        var name = System.IO.Path.GetFileName(file);
                        baContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + System.IO.Path.GetExtension(name).Remove(0, 1));
                        baContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                        {
                            Name = "file",
                            FileName = name
                        };
                        content.Add(baContent, "file", name);

                        //var httpClient = new HttpClient();
                        var updateImageUrl = ConstantsValue.UpdateUserImage + myid;

                        var finalresponse = await client.PutAsync(updateImageUrl, content);
                    }
                }
                

                //await DisplayAlert("Success", FirstName.Text + " Updated Successful", "ok");
                //await Shell.Current.Navigation.PopModalAsync();

            }
            await DisplayAlert("Success", FirstName.Text + " Updated Successful", "ok");
            //await Shell.Current.Navigation.PopModalAsync();
            FirstName.Text = "";
            UserLastname.Text = "";
            UserPhone.Text = "";
            UserAddress.Text = "";
            UserEmail.Text = "";
            UserCity.Text = "";

           

        }
    }
}