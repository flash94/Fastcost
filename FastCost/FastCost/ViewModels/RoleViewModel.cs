using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastCost.ViewModels
{
    public class RoleViewModel
    {
        public List<Role> RolesList { get; set; }

        public RoleViewModel()
        {
            RolesList = GetRoles().OrderBy(t => t.Value).ToList();
        }
        public List<Role> GetRoles()
        {
            var roles = new List<Role>()
        {
            new Role(){key = 1, Value = "Admin"},
            new Role(){key = 2, Value = "scout"},
            new Role(){key = 3, Value = "ProjectManager"}

        };
            return roles;
        }

    }
    public class Role
    {
        public int key { get; set; }
        public string Value { get; set; }
    }
}
