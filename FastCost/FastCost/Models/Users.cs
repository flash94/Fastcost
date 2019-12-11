using FastCost.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FastCost.Models
{
    public class Users
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string token { get; set; }
        public string Image { get; set; }


        public Users(string Username, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }

        public bool CheckInformation()
        {
            if (!string.IsNullOrEmpty(this.Email) && !string.IsNullOrEmpty(this.Password))
                return true;
            else
                return false;
        }

        public ImageSource UserImage
        {

            get
            {
                //var source = new Uri(ConstantsValue.MainAddress + ConstantsValue.ImageUrl + Image);
                var source = new Uri("http://192.168.1.118:5000/images/users_images/" + Image);

                return source;
            }
            set { UserImage = value; }
        }

        public string name
        {
            get
            {
                return this.FirstName + "  " + this.LastName;
            }
        }

        public string capitalizedname
        {
            get
            {
                return this.name.ToUpper();
            }
        }
    }

}
