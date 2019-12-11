using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCost.Models
{
    public class CategoriesModel
    {
        [JsonProperty("category")]
        public string category { get; set; }
    }

    public class NewCategory
    {
        public List<CategoriesModel> result { get; set; }

        public bool success { get; set; }
    }
}
