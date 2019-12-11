using FastCost.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastCost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUser : ContentPage
    {
        public EditUser()
        {
            InitializeComponent();
            BindingContext = new RoleViewModel();

        }

        public async void UploadImageTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Warning", "Picking  a photo is not supported", "OK");

                return;
            }
            byte[] bytes;
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 40
            });

            userImage.Source = ImageSource.FromStream(() => file.GetStream());

            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                bytes = memoryStream.ToArray();
            }
            //Convert file to byte array, to bitmap and set it to my ImageView

            //byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            //Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            //userImage.SetImageBitmap(bitmap);
        }

        private async void backClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}