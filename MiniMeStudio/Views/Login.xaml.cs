using MiniMeStudio.Contracts.Views;
using MiniMeStudio.Models;
using MiniMeStudio.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
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

namespace MiniMeStudio.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            /// Setting Default Login 
            //tbxUsername.Text = "User1@gmail.com";
            //pbxPassword.Password = "1234";

            string username = tbxUsername.Text;
            string password = pbxPassword.Password;
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Invalid username or password", "Message");
                return;
            }
            var result = Utility.ExecuteSP("User_Exists", "\"'" + username + "'," + password + "\"");
            //var obj = JsonConvert.DeserializeObject<UserAuthResponse>(result);
            /*
            if (Convert.ToBoolean(obj.Table.FirstOrDefault().yRecCnt))
            {
                /// Navigate to Main Window 
                this.Close();
                Application.Current.MainWindow.Show();

            }
            else
            {
                MessageBox.Show("Invalid username or password", "Message");
                return;
            }
            */
        }

        private void newbtnLogin_Click(object sender, RoutedEventArgs e)
        {
            /// Setting Default Login TO BE REMOVED
            //tbxUsername.Text = "rohit@gmail.com";
            //pbxPassword.Password = "abc";

            //tbxUsername.Text = "rohittuteja@gmail.com";
            //pbxPassword.Password = "abc";

            string username = tbxUsername.Text;
            string password = pbxPassword.Password;
            string encryptedEmailAddress = Encrypt(username);
            // TODO - Get the file path from the installation folder
            //string dirParameter = "D:\\Work\\RememberMe.txt";
            string dirParameter = Globals.DirPath;
            //AppDomain.CurrentDomain.BaseDirectory + @"\RememberMe.txt";

            /// TODO - Get this from Global variable            
            string accountid = "1234";
            Globals.AccountID = accountid;

            //When Username or Password is Empty
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show(Properties.Resources.LoginValidationMessage, Properties.Resources.MessageBoxTitle);
                return;
            }

            var result = Utility.ExecuteSP(StoredProcedures.USER_SelectOne, "\"'" + username + "'," + accountid + "\"");
            var obj = JsonConvert.DeserializeObject<LoggedInUser>(result);

            // When User does not exists OR When Password does not match
            if ((obj.Table.Length == 0) || (password != obj.Table.FirstOrDefault<User>().AdminPassword))
            {
                MessageBox.Show(Properties.Resources.LoginValidationMessage, Properties.Resources.MessageBoxTitle);
                return;
            }
            // First Time Login Password Change 
            if (Convert.ToBoolean(obj.Table.FirstOrDefault<User>().FirstTimeLogin))
            {
                /// Pop up change password
                Grid_UpdatePassword.Visibility = Visibility.Visible;
                Grid_UpdatePassword_EmailAddress.Content = username;
                return;
            }

            // If Remember Me Checkbox is Checked
            if ((bool)chkbxRememberMe.IsChecked)
            {
                // Code to Save RememberMe.txt file

                // Code to Get the Processor/Device ID
                string cpuInfo = string.Empty;
                ManagementClass mc = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (cpuInfo == "")
                    {
                        //Get only the first CPU's ID
                        cpuInfo = mo.Properties["processorID"].Value.ToString();
                        break;
                    }
                }
                string Msg = encryptedEmailAddress + "\n" + cpuInfo;
                //MessageBox.Show(Convert.ToString(Msg), "Msg");
                FileStream fParameter = new FileStream(dirParameter, FileMode.Create, FileAccess.Write);
                StreamWriter m_WriterParameter = new StreamWriter(fParameter);
                m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                m_WriterParameter.Write(Msg);
                m_WriterParameter.Flush();
                m_WriterParameter.Close();

                // Save the Encrypted Email Address and Device ID to the DB
                var result2 = Utility.ExecuteSP(StoredProcedures.USER_UpdateEncryptedEmailAddress, "\"'" + encryptedEmailAddress + "', '" + cpuInfo + "', '" + username + "'," + accountid + "\"");
            }
            /// Navigate to Main Window 
            this.Close();
            Application.Current.MainWindow.Show();
        }

        private string Encrypt(string ytextToEncrypt)
        {
            string textToEncrypt = ytextToEncrypt;
            string ToReturn = "";
            string publickey = "rohit123";
            string secretkey = "tuteja12";
            byte[] secretkeyByte = { };
            secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                ToReturn = Convert.ToBase64String(ms.ToArray());
            }
            return ToReturn;
        }

        private string Decrypt(string ytextToDecrypt)
        {
            string textToDecrypt = ytextToDecrypt;
            string ToReturn = "";
            string publickey = "rohit123";
            string privatekey = "tuteja12";
            byte[] privatekeyByte = { };
            privatekeyByte = System.Text.Encoding.UTF8.GetBytes(privatekey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                ToReturn = encoding.GetString(ms.ToArray());
            }
            return ToReturn;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO - Get the file path from the installation folder
            if (File.Exists(Globals.DirPath))
            {
                int counter = 0;
                string line = "", txtEncryptedEmailAddress = "", txtDeviceID = "";

                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(Globals.DirPath);
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 0)
                    {
                        txtEncryptedEmailAddress = line;
                    }
                    if (counter == 1)
                    {
                        txtDeviceID = line;
                    }
                    counter++;
                }
                file.Close();

                var result = Utility.ExecuteSP(StoredProcedures.USER_EncryptedEmailAddress, "\"'" + txtEncryptedEmailAddress + "', '" + txtDeviceID + "'\"");
                //if (String.IsNullOrEmpty(result))
                //{
                var obj = JsonConvert.DeserializeObject<LoggedInUser>(result);

                if (obj.Table.Length == 0)
                {
                    MessageBox.Show(Properties.Resources.LoginValidationMessage, Properties.Resources.MessageBoxTitle);
                    return;
                }
                else
                {
                    /// Navigate to Main Window 
                    this.Close();
                    Application.Current.MainWindow.Show();
                }
            }
            tbxUsername.Focus();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            /// Setting Default Login TO BE REMOVED
            //pbxNewPassword.Password = "new";
            //pbxReEnterPassword.Password = "new";

            string strNewPassword = pbxNewPassword.Password;
            string strReEnterPassword = pbxReEnterPassword.Password;
            string strUsername = (string)Grid_UpdatePassword_EmailAddress.Content;

            /// TODO - Get this from Global variable            
            //string accountid = "1234";

            //When New or Re-Enter Password is Empty
            if (String.IsNullOrEmpty(strNewPassword) || String.IsNullOrEmpty(strReEnterPassword))
            {
                MessageBox.Show(Properties.Resources.LoginValidationMessage, Properties.Resources.MessageBoxTitle);
                return;
            }
            //When New and Re-Enter Password do not match
            if (strNewPassword != strReEnterPassword)
            {
                MessageBox.Show(Properties.Resources.PasswordDoNotMatchMessage, Properties.Resources.MessageBoxTitle);
                return;
            }

            var result = Utility.ExecuteSP(StoredProcedures.USER_UpdatePasswordAndFirstTimeLogin, "\"'" + strUsername + "', '" + strNewPassword + "', 0, 11, " + Globals.AccountID + "\"");

            /// TODO - Check if the update was successful

            MessageBox.Show(Properties.Resources.PasswordUpdatedMessage, Properties.Resources.MessageBoxTitle);
            this.Close();
            Application.Current.MainWindow.Show();
        }
    }
}
