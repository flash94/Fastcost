using FastCost.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FastCost.Models
{
    public class ComponentItemsModel
    {
        public int Id { get; set; }
        public int Item_Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string Image { get; set; }
        public string PrefferedPrice { get; set; }
        public string DealersAddress { get; set; }
        public string DealerPhone { get; set; }
        public string City { get; set; }
        public string SellerWeblink { get; set; }
        public string date { get; set; }
        public int component_Id { get; set; }
        public string ComponentName { get; set; }
        public string ComponentDescription { get; set; }
        public string NoOfItems { get; set; }

        public int Quantity { get; set; }
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

        public List<ComponentItemsModel> Item { get; set; }
    }
}
