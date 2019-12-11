using FastCost.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FastCost.Services
{
    public class ServiceApi
    {
        public static ObservableCollection<AddedItems> addedItems;
        public static async Task<ObservableCollection<AddedItems>> GetEmplyee()
        {
            addedItems = new ObservableCollection<AddedItems>();
            var client = new HttpClient();
            string urlFomrate = ConstantsValue.BaseAddress + ConstantsValue.Emplyeelist;
            string requestContent = urlFomrate;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestContent);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    addedItems = JsonConvert.DeserializeObject<ObservableCollection<AddedItems>>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return addedItems;
        }
    }
}
