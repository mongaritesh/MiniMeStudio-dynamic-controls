using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMeStudio.Models
{
    public static class Globals
    {
        public static User LoggedInUser { get; set; }
        public static Boolean IsUserLoggedIn { get; set; }


        public const string MiniMeAPI = "https://localhost:5001/database?SP_Name=";
        public const string DefaultWebURL = "https://mmsultra.shawmansoftware.com";
        public const string DirPath = "./RememberMe.txt";

        public static string AccountID { get; set; }


    }


}
