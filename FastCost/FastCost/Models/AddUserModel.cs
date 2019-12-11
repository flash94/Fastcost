using System;
using System.Collections.Generic;
using System.Text;

namespace FastCost.Models
{
    public class AddUserModel
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        //public int Role_Id { get; set; }
        public string Role { get; set; }
        //public string token { get; set; }
        //public string Image { get; set; }
    }
}
