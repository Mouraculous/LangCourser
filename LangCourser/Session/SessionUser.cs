using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISBD_project.Models;

namespace ISBD_project.Session
{
    public class SessionUser
    {
        public Account Account { get; set; }
        public string Role { get; set; }
    }
}