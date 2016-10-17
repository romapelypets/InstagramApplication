using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InstagramWrapper.Model;

namespace InstagramApplication.Models
{
    public class HomeModel
    {
        public InstagramResponse UserFeed { get; set; }
        public OuthUser user { get; set; }
    }

  
}