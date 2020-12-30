using System;
using System.Windows.Controls;

namespace MiniMeStudio.Views
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnHome_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

        private void BtnFeedback_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FrmMain.NavigationService.Navigate(new Uri("Views/FeedbackPage.xaml", UriKind.Relative));
        }

        private void BtnUsers_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //FrmMain.NavigationService.Navigate(new Uri("Views/TestUserPage.xaml", UriKind.Relative));
            FrmMain.NavigationService.Navigate(new Uri("Views/UsersPage.xaml", UriKind.Relative));
        }

        private void BtnNotify_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnBrand_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnProfile_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
