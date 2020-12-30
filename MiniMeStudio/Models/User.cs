using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMeStudio.Models
{
    public class LoggedInUser
    {
        public User[] Table { get; set; }
    }

    public class User
    {
       
        public int UserID { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IsAdmin { get; set; }
        public string UserPassword { get; set; }
        public int FirstTimeLogin { get; set; }
        public string EncryptedEmailAddress { get; set; }
        public string DeviceID { get; set; }
        public DateTime DTC { get; set; }
        public DateTime DTU { get; set; }
        public int UC { get; set; }
        public int UU { get; set; }
        public int AccountID { get; set; }

        public string AdminPassword { get; set; }

    }

    public class UserAuthResponse
    {
        public List<Table> Table { get; set; }
    }

    public class Table
    {
        public int yRecCnt { get; set; }
        public int yUserID { get; set; }
    }

}
