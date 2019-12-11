using System;
using System.Collections.Generic;
using System.Text;

namespace FastCost.Models
{
    public class ComponentsModel
    {
        public int Id { get; set; }
        public string ComponentDescription { get; set; }
        public string ComponentName { get; set; }
        public string DateCreated { get; set; }
        public string NoOfItems { get; set; }
        public string TotalCost { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }

        public List<ItemsModel>Item { get; set; }  

    }

}
