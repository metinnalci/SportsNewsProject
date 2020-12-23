using SportsNewsProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class RolesVM
    {
        public int RoleId { get; set; }
        public string EnumRol { get; set; }

        public bool Ischecked { get; set; }
    }
}
