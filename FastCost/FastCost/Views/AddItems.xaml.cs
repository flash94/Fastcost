using FastCost.Models;
using FastCost.Services;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
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
    public partial class AddItems : ContentPage
    {
        private MediaFile _mediaFile;
        public AddItems()
        {
            InitializeComponent();
            GetCategories();
        }

        private async void backClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddItems());
            //await Shell.Current.GoToAsync("//ItemsPage");
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void CreateItemClicked(object sender, EventArgs e)
        {
            actindicator.IsVisible = true;
            actindicator.IsRunning = true;
            //ItemsModel additem = new ItemsModel()
            //{
            //    ItemName = itemName.Text,
            //    ItemDescription = itemDescription.Text,
            //    PrefferedPrice = DealerPhone.Text,
            //    DealersAddress = DealerAddr.Text,
            //    DealerPhone = DealerPhone.Text,
            //    City = DealerCity.Text,
            //    SellerWeblink = StoreUrl.Text,
            //    Category_Name = (Categorypicker.SelectedItem as CategoriesModel).category


            //};

            HttpClient client = new HttpClient();
            
            var CreateItem = ConstantsValue.createitem;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            //var json = JsonConvert.SerializeObject(additem);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpContent result = new StringContent(json);
            //result.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //result.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")

            //var response = await client.PostAsync(CreateItem, content);

            if (_mediaFile != null)
            {
                var file = _mediaFile.Path;
                if (string.IsNullOrEmpty(file) == false)
                {
                    var upfilebytes = System.IO.File.ReadAllBytes(file);
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                    var name = System.IO.Path.GetFileName(file);
                    baContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + System.IO.Path.GetExtension(name).Remove(0, 1));
                    baContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "Image",
                        FileName = name,

                    };
                    var categorynew = new List<string>();
                    categorynew.Add(((Categorypicker.SelectedItem as CategoriesModel).category));
                    var categoryArray = categorynew.ToArray();
                    var jsoncategoryArray = JsonConvert.SerializeObject(categoryArray);
                    content.Add(baContent, "Image", name);
                    content.Add(new StringContent(DealerAddr.Text), "DealersAddress");
                    content.Add(new StringContent(itemName.Text), "ItemName");
                    content.Add(new StringContent(itemDescription.Text), "ItemDescription");
                    content.Add(new StringContent(itemPrice.Text), "PrefferedPrice");
                    content.Add(new StringContent(DealerPhone.Text), "DealerPhone");
                    content.Add(new StringContent(DealerCity.Text), "City");
                    content.Add(new StringContent(StoreUrl.Text), "SellerWeblink");
                    content.Add(new StringContent(jsoncategoryArray),"category");
                    //content.Add(new StringContent((Categorypicker.SelectedItem as CategoriesModel).category), "Category_Name");

                    //var httpClient = new HttpClient();


                    var response = await client.PostAsync(CreateItem, content);
                    if (response.IsSuccessStatusCode)
                    {
                        actindicator.IsVisible = false;
                        actindicator.IsRunning = false;
                        await DisplayAlert("Success", itemName.Text + "Created Successful", "ok");
                        await Shell.Current.Navigation.PopModalAsync();

                    }
                   
                }
                itemName.Text = "";
                itemDescription.Text = "";
                DealerPhone.Text = "";
                DealerAddr.Text = "";
                DealerPhone.Text = "";
                DealerCity.Text = "";
                StoreUrl.Text = "";
            }

    }



          

        public async void SelectImage(object sender, EventArgs e)
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

            //userImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

            imageName.Text= System.IO.Path.GetFileName(_mediaFile.Path);

        }

        public async void GetCategories()
        {
            HttpClient client = new HttpClient();
            var dashboardEndpoint = ConstantsValue.categories;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", ConstantsValue.userprofile.token);

            var result = await client.GetStringAsync(dashboardEndpoint);
            var ItemsList = JsonConvert.DeserializeObject<NewCategory>(result);
            //Items = new ObservableCollection<CategoriesModel>(ItemsList);
            Categorypicker.ItemsSource = ItemsList.result;
        }

    }
}