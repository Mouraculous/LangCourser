using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISBD_project.Models;

namespace ISBD_project.Session
{
    //wiem że bullshit ale już jest późno a chcę coś pokazać na zajęciach xD
    public class SessionMocker
    {
        public static SessionMocker Instance;

        public SessionUser LoggedUser { get; set; }

        static SessionMocker()
        {
            Instance = new SessionMocker();
        }
        public SessionMocker()
        {
            LoggedUser = new SessionUser { Account = null, Role = string.Empty };
        }

        public static bool IsSessionActive() => Instance.LoggedUser?.Account != null;
    }
}