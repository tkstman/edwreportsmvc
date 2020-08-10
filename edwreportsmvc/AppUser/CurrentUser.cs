using System;
using System.Collections.Generic;

namespace edwreportsmvc.AppUser
{
    public class CurrentUser
    {
        public CurrentUser()
        {

        }

        public int AppId
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public int WorkLocationId
        {
            get;
            set;
        }

        public string WorkLocationName
        {
            get;
            set;
        }

        public DateTime LastLoginDate
        {
            get;
            set;
        }

        public string ActiveElection
        {
            get;
            set;
        }

        public Dictionary<int, string> Constituencies = new Dictionary<int, string>();

        public Dictionary<int, string> WorkLocations = new Dictionary<int, string>();

        public Dictionary<string, int> UserRoles = new Dictionary<string, int>();//tks
    }
}