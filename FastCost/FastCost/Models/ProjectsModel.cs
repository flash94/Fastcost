using System;
using System.Collections.Generic;
using System.Text;

namespace FastCost.Models
{
    public class ProjectsModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectCost { get; set; }
        public string DateCreated { get; set; }
        public string TotalCost { get; set; }
        public int NoOfComponent { get; set; }
        public List<ComponentItemsModel> Item { get; set; }
    }

}
