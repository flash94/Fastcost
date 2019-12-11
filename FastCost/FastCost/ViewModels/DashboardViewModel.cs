using FastCost.Models;
using FastCost.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FastCost.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            GetDashboardDetails();
        }

        public async void GetDashboardDetails()
        {
            using (var client = new HttpClient())
            {
                //send a Get request
                var dashboardEndpoint = ConstantsValue.MainAddress + ConstantsValue.Dashboard;
                var result = await client.GetStringAsync(dashboardEndpoint);
                //string response = await result.Content.ReadAsStringAsync();

                //handling the answer
                var DashboardList = JsonConvert.DeserializeObject<List<DashboardModel>>(result);
                
            }
        }
    }


}
