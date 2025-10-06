using Azure.Core;
using DataDomain;
using LogicLayer;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using WpfPresentation.Pages;
using WpfPresentation.Windows;

namespace WpfPresentation
{
    public partial class MainWindow : Window
    {

        static MainWindow _this = null;

        private UserVM? _accessToken = null;
        public static MainWindow GetMainWindow()
        {
            return _this;
        }

        public MainWindow()
        {
            InitializeComponent();
            _this = this;

            mainFrame.Navigate(new ViewHomepage());
            UserChanged();
        }

        public bool isLoggedIn { get { return _accessToken != null; } }
        public  int UserID
        {
            get
            {
                return _accessToken.UserID;
            }
        }

        public string GivenName
        {
            get
            {
                StringBuilder gn = new StringBuilder();
                gn.Append(_accessToken.GivenName);
                return gn.ToString();
            }
        }
        public string FamilyName
        {
            get
            {
                StringBuilder fn = new StringBuilder();
                fn.Append(_accessToken.FamilyName);
                return fn.ToString();
            }
        }
        public string Email
        {
            get
            {
                StringBuilder e = new StringBuilder();
                e.Append(_accessToken.Email);
                return e.ToString();
            }
        }
        public byte[] Image
        {
            get
            {
                return _accessToken.Image;
            }
        }
        public string ImageMimeType
        {
            get
            {
                StringBuilder imt = new StringBuilder();
                imt.Append(_accessToken.ImageMimeType);
                return imt.ToString();
            }
        }
        public IEnumerable<string> SystemRoles
        {
            get
            {
                if (_accessToken == null)
                {
                    return ["Guest"];
                }
                string[] roles = new string[_accessToken.Roles.Count];
                _accessToken.Roles.CopyTo(roles);
                return roles;
            }
        }
        public IEnumerable<UserProjectRole> ProjectRoles
        {
            get
            {
                if(_accessToken == null)
                {
                    return [];
                }
                UserProjectRole[] userProjectRoles = new UserProjectRole[_accessToken.ProjectRoles.Count];
                _accessToken.ProjectRoles.CopyTo(userProjectRoles);
                return userProjectRoles;
            }
        }
        public IEnumerable<string> ProjectRoleValues
        {
            get
            {
                if (_accessToken == null)
                {
                    return [];
                }
                string[] userProjectRoles = new string[_accessToken.ProjectRoles.Count];
                foreach (UserProjectRole role in _accessToken.ProjectRoles)
                {
                    userProjectRoles.Append(role.ProjectRole);
                }
                return userProjectRoles;
            }
        }
        public bool HasProjectRole(string RoleName, int ProjectID)
        {
            foreach(UserProjectRole upr in ProjectRoles)
            {
                if(upr.ProjectRole == RoleName && upr.ProjectId == ProjectID)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasSystemRole(string RoleName)
        {
            foreach (UserProjectRole upr in ProjectRoles)
            {
                if (upr.ProjectRole == RoleName)
                {
                    return true;
                }
            }
            return false;
        }


        public void sendToLogin()
        {
            _accessToken = new UserVM();
            mainFrame.Navigate(new LogInDesktop(_accessToken));
        }

        private void btnLogInOut_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogInOut.Content.Equals("Log In"))
            {
                _accessToken = new UserVM();
                mainFrame.Navigate(new LogInDesktop(_accessToken));
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) 
                {
                    _accessToken = null;
                    UpdateUser();
                    MessageBox.Show("You are now logged out", "Logged Out", MessageBoxButton.OK, MessageBoxImage.Information);
                    mainFrame.Navigate(new ViewHomepage());
                }
            }

        }


        private void btnProjectBar_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ListOfAllProjects());
        }

        private void btnLocationBar_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ViewLocationList());
        }

        private void btnEventBar_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new PgEventList());
        }

        private void btnUserBar_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new frmViewAllUsers());
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ViewHomepage());
        }

        private void btnExternalContacts_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ViewAllExternalContacts());
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new CreateAccount());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            if (isLoggedIn)
            {
                mainFrame.Navigate(new View_Profile(Email));
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("You must be logged in to view your profile.\n\nWould you like to be redirected to the login page?", "Invalid User", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    mainFrame.Navigate(new LogInDesktop(_accessToken));
                }
            }
        }
        private void mnuScheduleNotification_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ScheduleNotificationCalendarDesktop());
        }

        private void btnCalendarBar_Click(object sender, RoutedEventArgs e)
        {
            if (isLoggedIn)
            {
                mainFrame.Navigate(new ViewCalendarDesktop());
            }
        }

        private void mnuUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (isLoggedIn)
            {
                var updatePassword = new ResetPasswordPopup(_accessToken);
                updatePassword.Show();
            }
        }

        private void btnInbox_Click(object sender, RoutedEventArgs e)
        {
            if (isLoggedIn)
            {
                mainFrame.Navigate(new ViewNotificationsDesktop());
            }
        }


        /// <summary>
        /// Creator: Stan Anderson
        /// Created: 2025/04/09
        /// 
        /// Static method to update the active user
        /// Helper method.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Brodie Pasker
        /// Updated: 2025/04/18
        /// Changed variables to be accessed with getter methods
        /// </remarks>
        /// <param name="newUser"></param>
        public void UpdateUser()
        {
            UserChanged();
        }



        /// <summary>
        /// Creator: Stan Anderson
        /// Created: 2025/04/09
        /// 
        /// Change UI informatino based on a change in user
        /// Helper method.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Brodie Pasker
        /// Updated: 2025/04/18
        /// Changed variables to be accessed with getter methods
        /// </remarks>
        public void UserChanged()
        {
            imgProfile.Source = null;
            btnProfile.Content = null;
            btnProfile.IsEnabled = true;
            statMessage.Content = "Welcome! Log in to your account to start volunteering on community projects!";

            if (isLoggedIn)
            {
                btnLogInOut.Content = "Log Out";
                btnCreateAccount.Visibility = Visibility.Hidden;
                btnInbox.Visibility = Visibility.Visible;
                btnCalendarBar.Visibility = Visibility.Visible;
                if (SystemRoles.Contains("Admin"))
                {
                    btnUserBar.Visibility = Visibility.Visible;
                    mnuScheduleNotification.Visibility = Visibility.Visible;
                } else
                {
                    btnUserBar.Visibility = Visibility.Collapsed;
                    mnuScheduleNotification.Visibility = Visibility.Collapsed;
                }

                statMessage.Content = "Welcome back, " + GivenName + " " + FamilyName + "!";

                if (Image != null)
                {
                    imgProfile.Source = ImageUtils.ConvertByteArrayToBitmapImage(Image);
                    btnProfile.Content = imgProfile;
                }
                else
                {
                    btnProfile.Content = "Profile";
                }
            }
            else
            {
                btnLogInOut.Content = "Log In";
                btnCreateAccount.Visibility = Visibility.Visible;
                btnInbox.Visibility = Visibility.Hidden;
                btnCalendarBar.Visibility = Visibility.Collapsed;
                btnUserBar.Visibility = Visibility.Collapsed;


                btnProfile.IsEnabled = false;
                btnProfile.Content = "Profile";
            }
        }






        /// <summary>
        /// Creator: Stan Anderson
        /// Created: 2025/04/08
        /// 
        /// Checks to see if the active user is a volunteer;
        /// Helper method.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Brodie Pasker
        /// Updated: 2025/04/18
        /// Changed variables to be accessed with getter methods
        /// </remarks>
        /// <param name="projectID"></param>
        public bool IsVolunteer(int projectID)
        {
            bool result = false;

            if (isLoggedIn)
            {
                UserManager _userManager = new UserManager();
                List<User> volunteers = _userManager.GetApprovedUserByProjectID(projectID);
                foreach (User user in volunteers)
                {
                    if (user.UserID == UserID)
                    {
                        result = true;
                        return result;
                    }
                }
                if (HasSystemRole("Admin"))
                {
                    result = true;
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Creator: Stan Anderson
        /// Created: 2025/04/08
        /// 
        /// Checks to see if the active user is the creator of project;
        /// Helper method.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Brodie Pasker
        /// Updated: 2025/04/18
        /// Changed variables to be accessed with getter methods
        /// </remarks>
        /// <param name="projectID"></param>
        public bool IsProjectStarter(int projectID)
        {
            bool result = false;
            ProjectManager _projectManager = new ProjectManager();
            try
            {
                int creator = _projectManager.GetProjectVMByID(projectID).UserID;

                if (isLoggedIn == true && UserID == creator)
                {
                    result = true;
                }
                if (HasSystemRole("Admin"))
                {
                    result = true;
                    return result;
                }
            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }
    }
}