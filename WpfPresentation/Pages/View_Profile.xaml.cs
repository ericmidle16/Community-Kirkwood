///<summary>
/// Creator: Dat Tran
/// Created: 2025-02-08
/// Summary: This xaml code displays a data grid to display the current list of users in the database. 
/// Last updated by: Stan Anderson
/// Last updated: 2025-04-08
/// Changes: Added connections, changed Profile Manager to User Manager
///</summary>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Azure.Core;
using DataDomain;
using LogicLayer;

namespace WpfPresentation.Pages
{
    

    public partial class View_Profile : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        UserManager _profileManager = new UserManager();
        User user = new User();
        public View_Profile(string email)
        {
            InitializeComponent();
            
            try
            {
                user = _profileManager.RetrieveUserDetailsByEmail(email);

                user.UserID = _profileManager.GetUserByEmail(email).UserID;

                txtName.Content = user.GivenName + " " + user.FamilyName + "'s Profile";
                
                if(user.City != null || user.State != null)
                {
                    if(user.City != null && user.State != null)
                    {
                        txtLocation.Text = user.City + ", " + user.State;
                    }
                    else
                    {
                        txtLocation.Text = user.City + user.State;
                    }
                }
                else
                {
                    txtLocation.Text = "";

                }


                txtEmail.Text = user.Email;

                txtProfileDescription.Text = user.Biography;
                if (user.Image != null)
                {

                    imgProfile.Source = ImageUtils.ConvertByteArrayToBitmapImage(user.Image);
                }
                else
                {
                    imgProfile.Source = null;
                }

                validatePermissions();
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading profile", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public View_Profile(int UserID)
        {
            InitializeComponent();

            try
            {
                user = _profileManager.GetUserInformationByUserID(UserID);

                user.UserID = UserID;

                txtName.Content = user.GivenName + " " + user.FamilyName + "'s Profile";

                if (user.City != null || user.State != null)
                {
                    if (user.City != null && user.State != null)
                    {
                        txtLocation.Text = user.City + ", " + user.State;
                    }
                    else
                    {
                        txtLocation.Text = user.City + user.State;
                    }
                }
                else
                {
                    txtLocation.Text = "";

                }


                txtEmail.Text = user.Email;

                txtProfileDescription.Text = user.Biography;
                if(user.Image != null)
                {
                    imgProfile.Source = ImageUtils.ConvertByteArrayToBitmapImage(user.Image);
                }
                else
                {
                    imgProfile.Source = null;
                }

                validatePermissions();
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading profile", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new UpdateUserDesktop(user.Email));

        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new viewAssignedProjects(user.UserID, user.GivenName + " " + user.FamilyName, user.Email));
        }

        private void btnDonations_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewDonationHistory(user));
        }

        private void btnAvailability_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewAvailabilityDeskTop(user.UserID));
        }

        private void btnVehicles_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewPersonalVehicles(user.UserID));
        }

        private void btnSkills_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new updateSkills(user.UserID));

        }



        // author: Stan Anderson
        private void validatePermissions()
        {
            if(main.isLoggedIn && main.UserID == user.UserID)
            {
                btnEditProfile.Visibility = Visibility.Visible;
            }
            else
            {
                btnEditProfile.Visibility = Visibility.Hidden;
            }
            if (main.isLoggedIn && main.UserID == user.UserID)
            {
                btnEditProfile.Visibility = Visibility.Visible;
            }
            else
            {
                btnEditProfile.Visibility = Visibility.Hidden;
            }
        }

    }


}
