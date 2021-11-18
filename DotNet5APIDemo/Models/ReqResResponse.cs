using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5APIDemo.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string attendance { get; set; }
    }

    public class Support
    {
        public string url { get; set; }
        public string text { get; set; }
    }

    public class UsersResponse
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<ExpandoObject> data { get; set; }
        public Support support { get; set; }
    }


    public class UserResponse
    {
        public ExpandoObject data { get; set; }
        public Support support { get; set; }
    }
}
