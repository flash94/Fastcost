using FastCost.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FastCost.Services
{
    class ConstantsValue
    {
        public static string BaseAddress = "http://dummy.restapiexample.com";
        public static string Emplyeelist = "/api/v1/employees";
        

        public static string MainAddress = "http://192.168.1.118:5000";
        //public static string MainAddress = "http://192.168.43.210:5000";
        public static string domainurl
        {
            get
            {

                //remote
                return MainAddress;
            }
        }

        public static string Login = "/login";
        public static string Dashboard = "/all";
        public static string AllUsers = "/users";

        public static string AllItems = "/items/";
        public static string AllComponents = "/components/";
        public static string SingleComponent = "/component/";
        public static string AllProjects = "/projects/";

        public static string ItemPrices = "/prices/";
        public static string ImageUrl = "/images/uploaded_images/";
        public static string SearchItems = "/items?ItemName=";
        public static string ComponentItems = "/component/items/";

        

        public static string Register = "/user";
        public static string GetSingleUser = "/user/";
        public static string AdminUpdateUser = "/user/role/";

        public static string loginurl { get { return ConstantsValue.domainurl + "/login"; } }

        public static string customersearchurl { get { return MainAddress + "/items?ItemName"; } }

        public static string UpdateUserImage { get { return MainAddress + "/user/image/one/"; } }

        public static string createuser { get { return MainAddress + Register; } }

        public static string createitem { get { return MainAddress + "/item"; } }
        public static string categories { get { return MainAddress + "/categories"; } }
        public static string UpdateSingleUser { get { return MainAddress + AdminUpdateUser; } }

        public static string ChoosePrefferedPrice { get { return MainAddress + "/chooseprefferedprice/"; } }
        public static string AddNewPrice { get { return MainAddress + "/price/"; } }
        public static string EditComponent { get { return MainAddress + "/component/items/"; } }
        public static string CreateProject { get { return MainAddress + "/project/"; } }




        public static List<ItemsModel> itemslist { get; set; }

        public static List<ItemsModel> listItemA = new List<ItemsModel>();

        public static List<ComponentItemsModel> editComponentList = new List<ComponentItemsModel>();
        public static List<ComponentItemsModel> addComponent = new List<ComponentItemsModel>();
        public static Models.Users userprofile { get; set; }
        //public static List<ItemsModel> listItemA = new ObservableCollection<ItemsModel>();



    }
}
