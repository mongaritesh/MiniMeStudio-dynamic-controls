using MiniMeStudio.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Web;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using MiniMeStudio.Models;
using MahApps.Metro.Controls.Dialogs;

namespace MiniMeStudio.Views
{
    /// <summary>
    /// Interaction logic for NewUserPage.xaml
    /// </summary>
    public partial class NewUserPage : Page
    {
        
        public NewUserPage()
        {
            InitializeComponent();
            GetUserDetails();
        }


        private void GetUserDetails()
        {

            var result = Utility.ExecuteSP("User_SelectAllWithGroup", "\"1234\"");
            result = result.Substring(14, result.Length - 16).TrimEnd();
            var obj = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(result);


            List<UserMaster> myUsers = (obj).Select(x => new UserMaster
            {
                Name = "   " + (string)x["FirstName"] + " " + (string)x["LastName"]
                ,
                Email = "   " + (string)x["EmailAddress"]
                ,
                UserGroupName = "   " + ((string)x["UserGroupName"].ToString()).Replace("|", "      ")
                ,
                IsAdmin = ((string)x["IsAdmin"].ToString() == "0" ? "" : "  ADMIN  ")
                ,
                UserID = (int)x["UserID"]

            }).ToList();

            lvUsers.ItemsSource = myUsers;
        }

        public class UserMaster
        {
            public string Name { get; set; }
            public string IsAdmin { get; set; }
            public string Email { get; set; }
            public string Location { get; set; }
            public string UserGroupName { get; set; }
            public int UserID { get; set; }

        }

        public class GroupMaster
        {
            public string UserGroupID { get; set; }
            public string UserGroupName { get; set; }

        }

        public class ListBoxData
        {
            public string UserGroupID { get; set; }
            public string UserGroupName { get; set; }
        }

        
        private void btnEditLine_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            //Game game = button.DataContext as Game;
            //int id = game.ID;
        }
    }
}
