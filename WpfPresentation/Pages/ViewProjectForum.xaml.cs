/// <summary>
/// Created By: Jackson Manternach
/// Created: 2025/03/13
/// Summary:
///     Presentation code for the ViewProjectForum feature
/// 
/// Updater Name: Skyann Heintz
/// Updated: 2025-04-03
/// What Changed: added InsertForumPost, userID, datePosted,tbContentDown
/// 
/// Updater Name: Skyann Heintz
/// Updated: 2025-04-15
/// What Changed: Commented out set user ID, added code to get the current user, added DateTime to Insert
///     User cannot make a post without being logged in
///     
/// Last Updated By: Nik Bell & Kate Rich
/// Last Updated: 2025-04-18
/// What Was Changed:
///     Replaced the LoadThreads() method with grdThreads_Loaded - replaced TextBlocks with a DataGrid.
///     Added connection for Viewing a Thread & its Replies.
///     
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-24
/// What Was Changed:
///     Added validation for users forum writting permissions and added a button to a mute user page
///     
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-30
/// What Was Changed:
///     Moved the code from grdThreads_Loaded to a seperate method called LoadThreads so it coulde be referenced by the 
///     EnterButton_Click method to update the list after a new forum was created. Also added the Page_Loaded method
///     to update the forum title to include the project name
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class ViewProjectForum : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private ThreadManager _threadManager = new ThreadManager();
        private IForumPermissionManager _forumPermissionManager = new ForumPermissionManager(); // Added by Syler Bushlack
        private IProjectManager _projectManager = new ProjectManager(); // Added by Syler Bushlack
        List<ThreadVM> _threads;
        private int _projectID;
        private DateTime datePosted;


        public ViewProjectForum(int projectID)
        {
            InitializeComponent();
            _projectID = projectID;
            validateUserForumPermissions(); // Added by Syler Bushlack
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnView.Visibility = Visibility.Collapsed;
            btnMute.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Moderator", _projectID) ||
                main.HasSystemRole("Admin"))
            {
                btnMute.Visibility = Visibility.Visible;
            }
            if (main.ProjectRoles.Any(upr => upr.ProjectId == _projectID))
            {
                btnView.Visibility = Visibility.Visible;
            }
        }

        public ViewProjectForum()
        {
            InitializeComponent();
        }


        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string content = tbContent.Text;
                string threadName = tbThreadName.Text;
                datePosted = DateTime.Now;

                if(main.isLoggedIn == false || main.UserID <= 0)
                {
                    MessageBox.Show("You must be logged in to create a post.", "Authentication Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if(string.IsNullOrWhiteSpace(content))
                {
                    MessageBox.Show("Please enter your post.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if(content.Length > 250)
                {
                    MessageBox.Show("Your post cannot exceed 250 characters.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if(string.IsNullOrWhiteSpace(threadName))
                {
                    MessageBox.Show("Please enter your thread name.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if(content.Length > 100)
                {
                    MessageBox.Show("Your post cannot exceed 100 characters.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool success = _threadManager.InsertForumPost(main.UserID, content, _projectID, threadName, datePosted);
                if(success)
                {
                    MessageBox.Show("Post added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    tbThreadName.Clear();
                    tbContent.Clear();
                    lbThreadName.Visibility = Visibility.Collapsed;
                    tbThreadName.Visibility = Visibility.Collapsed;
                    tbContent.Visibility = Visibility.Collapsed;
                    EnterButton.Visibility = Visibility.Collapsed;

                    LoadThreads();
                }
                else
                {
                    MessageBox.Show("Failed to add post. You may not have the permissions to post in this thread.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/03/02
        /// Summary: This method allows the user to 
        /// hit the enter key on their keyboard to enter their 
        /// post / message instead of having to always hit the button.
        /// It calls on the EnterButton_Click method when the enter 
        /// button is clicked on the keyboard to ensure that the 
        /// insert works.
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void tbContent_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if enter key is pressed
            if(e.Key == Key.Enter)
            {
                // Prevents the default behavior (adding a new line)
                e.Handled = true;

                EnterButton_Click(sender, e);
            }
        }


        private void btnStartNewThread_Click(object sender, RoutedEventArgs e)
        {

            // Show thread creation UI
            lbThreadName.Visibility = Visibility.Visible;
            tbThreadName.Visibility = Visibility.Visible;
            tbContent.Visibility = Visibility.Visible;
            EnterButton.Visibility = Visibility.Visible;
        }

        // Author: Kate Rich
        private void grdThreads_Loaded(object sender, RoutedEventArgs e)
        {
            LoadThreads();
        }

        // Author: Kate Rich
        private void LoadThreads()
        {
            try
            {
                _threads = _threadManager.GetThreadsByProjectID(_projectID);

                grdThreads.ItemsSource = _threads;
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message);
            }
        }

        // Author: Nik Bell
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if(grdThreads.SelectedItem == null)
            {
                MessageBox.Show("Please select a thread to view it.");
                return;
            }
            int id = ((ThreadVM)grdThreads.SelectedItem).ThreadID;

            NavigationService.GetNavigationService(this)?.Navigate(new ViewForumPosts(id, _projectID));
        }

        /// <summary>
        /// Creator: Syler Bushlack
        /// Created: 2025/04/24
        /// Summary: Checks the user's forum write permissions and disables the buttons if they don't have permissions
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void validateUserForumPermissions()
        {
            if (main.isLoggedIn)
            {
                if(!_forumPermissionManager.SelectUserWriteAccess(main.UserID, _projectID))
                {
                    //btnEdit.IsEnabled = false;
                    btnStartNewThread.IsEnabled = false;
                }
                else
                {
                    //btnEdit.IsEnabled = true;
                    btnStartNewThread.IsEnabled = true;
                }
            }
        }

        // Author: Syler Bushlack
        private void btnMute_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new MuteVolunteers(_projectID));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(_projectID));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Project project = _projectManager.GetProjectByID(_projectID);
                txtForumTitle.Content = "Welcome to the " + project.Name + " Forum!";
            }
            catch (Exception)
            {

            }
        }
    }
}