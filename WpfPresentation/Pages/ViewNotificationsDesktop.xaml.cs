using DataDomain;
using LogicLayer;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewNotifications.xaml
    /// </summary>
    public partial class ViewNotificationsDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private NotificationManager _notificationManager = new NotificationManager();
        
        // Keep both original and display lists
        private List<NotificationVM> _allNotifications;
        private ObservableCollection<NotificationVM> _displayNotifications;

        public ViewNotificationsDesktop()
        {
            InitializeComponent();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            // fetch and store
            _allNotifications = _notificationManager
                .GetAllNotificationsByUserID(main.UserID)
                .ToList();

            _displayNotifications = new ObservableCollection<NotificationVM>(_allNotifications);
            _displayNotifications.CollectionChanged += OnDisplayNotificationsChanged;
            lbxNotifications.ItemsSource = _displayNotifications;

            // initial toggle
            UpdateEmptyMessage();
        }

        private void UpdateEmptyMessage()
        {
            if (_displayNotifications.Count == 0)
            {
                lbxNotifications.Visibility = Visibility.Collapsed;
                txtEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                lbxNotifications.Visibility = Visibility.Visible;
                txtEmpty.Visibility = Visibility.Collapsed;
            }
        }
        private void OnDisplayNotificationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateEmptyMessage();
        }

        private void btnViewed_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is NotificationVM vm)
            {
                // mark viewed in backend
                try
                {
                    _notificationManager.MarkNotificationAsViewed(vm.NotificationID);
                }
                catch(Exception ex) {
                    MessageBox.Show("Setting notification as viewed failed");
                }
                
                // update view-model
                vm.IsViewed = true;
                lbxNotifications.Items.Refresh();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = (txtSearch.Text ?? "").Trim().ToLower();
            List<NotificationVM> filtered = _allNotifications
                .Where(n => n.Name.ToLower().Contains(keyword)
                         || n.Content.ToLower().Contains(keyword))
                .ToList();

            _displayNotifications.Clear();
            foreach (var n in filtered)
                _displayNotifications.Add(n);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            // show only unviewed
            List<NotificationVM> unviewed = _allNotifications
                .Where(n => !n.IsViewed)
                .ToList();

            _displayNotifications.Clear();
            foreach (var n in unviewed)
                _displayNotifications.Add(n);
        }
    }
}
