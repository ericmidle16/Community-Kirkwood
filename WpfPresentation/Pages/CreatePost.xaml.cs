using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using WpfPresentation.Pages;
using System.Windows.Navigation;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for CreatePost.xaml
    /// </summary>
    public partial class CreatePost : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        int _threadId;
        int _projectID;
        IPostManager _postManager;

        public CreatePost(int threadId, int projectID)
        {
            InitializeComponent();
            _threadId = threadId;
            _projectID = projectID;
            _postManager = new PostManager();
        }


        private void btnReply_Click(object sender, RoutedEventArgs e)
        {
            string content = tbxBoxPost.Text.Trim();

            if(string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Post content cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(content.Length > 500)
            {
                MessageBox.Show("Post content cannot exceed 500 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Post post = new Post
            {
                ThreadID = _threadId,
                UserID = main.UserID,
                Content = content
            };

            try
            {
                if(_postManager.CreateForumPost(post))
                {
                    MessageBox.Show("Post submitted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    tbxBoxPost.Clear();
                    NavigationService.GetNavigationService(this)?.Navigate(new ViewForumPosts(_threadId, _projectID));
                }
                else { MessageBox.Show("Post could not be added to database.", "Failure", MessageBoxButton.OK, MessageBoxImage.Information); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while submitting your post:\n{ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
