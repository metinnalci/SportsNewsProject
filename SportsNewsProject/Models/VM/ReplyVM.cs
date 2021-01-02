using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class ReplyVM
    {
        public int parentid { get; set; }
        public string useremail { get; set; }
        public int newsid { get; set; }
        public string replycomment { get; set; }

    }
}
