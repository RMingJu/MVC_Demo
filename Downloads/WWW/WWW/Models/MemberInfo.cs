using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WWW.Models
{
    public class MemberInfo
    {
        public string Acct { get; set; }
        public string Pass { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? Gender { get; set; }
        public string Birth { get; set; }
        public string County { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public string Edu { get; set; }
        public string Email { get; set; }

    }
}