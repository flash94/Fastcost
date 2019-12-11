using FastCost.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FastCost.Models
{
    public class ItemsModel
    {
        public string Item_Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string Image { get; set; }
        public string PrefferedPrice { get; set; }
        public string DealersAddress { get; set; }
        public string DealerPhone { get; set; }
        public string City { get; set; }
        public string SellerWeblink { get; set; }
        public string date { get; set; }
        public string DealersName { get; set; }
        public int Quantity { get; set; }
        public string Category_Name { get; set; }
        //public ImageSource ImgSrc { get { return ImageSource.FromUri(new Uri(Image)); } }
        public ImageSource ProductImage
        {
            
            get
            {
                var source = new Uri(ConstantsValue.MainAddress + ConstantsValue.ImageUrl + Image);
                //var source = new Uri("http://192.168.1.118:5000/images/uploaded_images/" + Image);

                return source;
            }
            set { ProductImage = value; }
        }
    }

    
}
