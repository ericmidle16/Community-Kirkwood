/// <summary>
/// Nikolas Bell
/// Created: 2025-03-14
/// 
/// Class that handles UI and diplay code for forum posts.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-31
/// What was Changed: Added edit functionality for the view. Added a double click on the content to
/// to populate the text boxand a key down which allows user to hit the enter key to submit their edit.
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-24
/// What was Changed: Added pin post and un pin post functionality and added validation for a users forum writing privaleges
/// 
/// Last Upaded By: Syler Bushlack
/// Last Updated: 2025/04/30
/// What Was Changed: fixed edit post and delete post
/// </summary>

using DataDomain;
using LogicLayer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewForumPosts.xaml
    /// </summary>
    public partial class ViewForumPosts : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        /// <summary>
        /// Collection of forum posts for the current thread, casted into a data object than enables
        /// the dynamic UI of the page to work. Must be set to public, or else the page cannot acess it.
        /// </summary>
        public ObservableCollection<PostVM> Posts { get; set; }
        private IPostManager _postManager { get; set; }
        private int _threadID { get; set; }
        private int editingPostID = -1; // added by sky
        private int userID; // added by sky

        private ForumPermissionManager _forumPermissionManager = new ForumPermissionManager(); // added by Syler Bushlack
        private int _projectID; // added by Syler Bushlack
        private bool muted = false; // added by Syler Bushlack


        /// <summary>
        /// A default contructor only for debug purposes.
        /// Fetches the first thread from the first available project.
        /// </summary>
        /// <summary>
        /// A default contructor only for debug purposes.
        /// Fetches the first thread from the first available project.
        /// </summary>
        public ViewForumPosts()
        {
            InitializeComponent();
            _postManager = new PostManager();
            _threadID = 100000;
            userID = main.UserID;

            Posts = new ObservableCollection<PostVM>(new List<PostVM>());
            LoadData();
        }

        /// <summary>
        /// Constructor that initializes the forum post view for a specific thread.
        /// </summary>
        /// <param name="threadID">The ID of the thread whose posts will be displayed.</param>
        public ViewForumPosts(int threadID, int projectID)
        {
            InitializeComponent();
           
            _postManager = new PostManager();
            _threadID = threadID;
            _projectID = projectID; // added by Syler Bushlack
            validateUserForumPermissions(); // added by Syler Bushlack

            Posts = new ObservableCollection<PostVM>(new List<PostVM>());
            LoadData();
        }

        
        /// <summary>
        /// Loads/reloads posts for the current thread and binds them to the UI.
        /// </summary>
        private void LoadData()
        {
            try
            {
                if(Posts != null)
                {
                    Posts.Clear();

                    foreach(var post in _postManager.GetAllThreadPosts(_threadID))
                    {
                        Posts.Add(post);
                    }
                    DataContext = this;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Failed to load posts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Create a fake empty list to stop the UI from completely breaking.
                Posts = new ObservableCollection<PostVM>();
            }
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/31
        /// Summary: This handles the user entering their edits of a post. The user cannot enter a post that is too long
        /// and they they must enter their post. Hides the content textbox and the enter button if the user successfully enters
        /// their post.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string content = tbContent.Text;

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


                if(editingPostID > 0) // If we're editing an existing post
                {

                    try
                    {
                        bool success = _postManager.EditForumPost(editingPostID, userID, content); // Assuming 'false' for pinned status
                        if(success)
                        {
                            MessageBox.Show("Post updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            var postToEdit = Posts.FirstOrDefault(post => post.PostID == editingPostID);
                            if(postToEdit != null)
                            {
                                postToEdit.Content = content;
                            }

                            Posts.Clear();
                            foreach(var post in _postManager.GetAllThreadPosts(_threadID))
                            {
                                Posts.Add(post);
                            }

                            tbContent.Clear(); // Clear the input
                            editingPostID = -1; // Reset editingPostID
                            tbContent.Visibility = Visibility.Hidden;
                            EnterButton.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the post.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to add post.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
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

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/31
        /// Summary: This method allows the user to double-click on a post in the ListBox 
        /// to edit it. If the selected post belongs to the current user, the post content 
        /// is loaded into the TextBox for editing, and the editing controls are made visible. 
        /// If the selected post does not belong to the user, a warning message is displayed.
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/04/30
        /// What Was Changed: changed all references to the hard coded userID to instead reference main.UserID
        /// </summary>
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Gets the selected post from the ListBox
            var listBox = sender as ListBox;
            var selectedPost = listBox.SelectedItem as PostVM;

            if(selectedPost != null && !muted)
            {
                // Checks if the current user is the author of the post
                if(main.isLoggedIn && selectedPost.UserID == main.UserID)
                {
                    // Populates the TextBox with the content of the selected post
                    tbContent.Text = selectedPost.Content;
                    // Sets the editingPostID to the ID of the selected post for future editing
                    editingPostID = selectedPost.PostID;

                    tbContent.Visibility = Visibility.Visible;
                    EnterButton.Visibility = Visibility.Visible;
                    tbContent.Focus();
                }
                else
                {
                    MessageBox.Show("You can only edit your own posts.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        /// <summary>
        /// Creator: Nikolas Bell
        /// Created: 2025/04/02
        /// Summary: This method allows the user to delete a selected post in the ListBox. 
        /// If the selected post belongs to the current user, a confirmation prompt is shown. 
        /// Upon confirmation, the post is deleted from the database and removed from the UI.
        /// If the selected post does not belong to the user, a warning message is displayed.
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/04/30
        /// What Was Changed: changed all references to the hard coded userID to instead reference main.UserID
        /// </summary>
        private void tblDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get which Post we're trying to delete.
            if(sender is TextBlock textBlock && textBlock.DataContext is PostVM selectedPost)
            {
                if(selectedPost.UserID != main.UserID)
                {
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this post?",
                        "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if(result == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool success = _postManager.DeleteForumPost(selectedPost.PostID);

                        if(success)
                        {
                            MessageBox.Show($"Post deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the post.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Error deleting post: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        LoadData();
                    }
                }
            }
        }

        private void btnReply_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new CreatePost(_threadID, _projectID));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewProjectForum(_projectID));
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
            if(main.isLoggedIn)
            {
                if(!_forumPermissionManager.SelectUserWriteAccess(main.UserID, _projectID))
                {
                    btnReply.IsEnabled = false;
                    muted = true;
                }
                else
                {
                    btnReply.IsEnabled = true;
                    muted = false;
                }
            }
        }

        /// <summary>
        /// Creator: Syler Bushlack
        /// Created: 2025/04/23
        /// Summary: This method updates the order of the posts after a user clicks to pin a post
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void tblPin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is TextBlock textBlock && textBlock.DataContext is PostVM selectedPost)
            {
                try
                {
                    if(selectedPost.Pinned)
                    {
                        selectedPost.Pinned = false;
                    }
                    else
                    {
                        selectedPost.Pinned = true;
                    }
                    bool pinned = _postManager.EditPostPinnedValue(selectedPost);

                    if(pinned)
                    {
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to pin the post.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Failed to pin the post.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}