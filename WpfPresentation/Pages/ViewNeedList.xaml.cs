///<summary>
/// Creator: Dat Tran
/// Created: 2025/02/19
/// Summary: This xaml code displays a data grid of the current list of needs. 
/// Last updated by: Stan Anderson
/// Last updated: 2025/04/08
/// Changes: Added connections and permissions
///</summary>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using DataDomain;
using LogicLayer;

namespace WpfPresentation.Pages
{
    public partial class ViewNeedList : Page
    {
         MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private List<NeedList> _needList ;
        NeedListManager _needListManager = new NeedListManager();
        private int _ProjectID;
        
        public ViewNeedList(int ProjectID)
        {
            _ProjectID = ProjectID;
            InitializeComponent();
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnAddItem.Visibility = Visibility.Hidden;
            btnEditItem.Visibility = Visibility.Hidden;
            btnEditItem.IsEnabled = false;

            if (main.isLoggedIn)
            {
                if (main.IsProjectStarter(_ProjectID) || main.HasProjectRole("Event Coordinator",  _ProjectID))
                {
                    btnAddItem.Visibility = Visibility.Visible;
                    btnEditItem.IsEnabled = true;
                    btnEditItem.Visibility = Visibility.Visible;
                }
            }
        }
        //private void GetPrivileges()
        //{
        //    if (main.HasProjectRole("Project Starter", _ProjectID) || 
        //        main.HasProjectRole("Event Coordinator", _ProjectID))
        //    {
        //        btnAddItem.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        btnAddItem.Visibility = Visibility.Collapsed;
        //    }
        //}

        private void grdNeedList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _needList = _needListManager.GetNeedList(_ProjectID); // Assigning to the class-level list

                grdNeedList.ItemsSource = _needList;
                grdNeedList.Columns[0].Visibility = Visibility.Hidden;
                grdNeedList.Columns[1].Visibility = Visibility.Hidden;
                grdNeedList.Columns[2].Header = "Name";
                grdNeedList.Columns[3].Header = "Quantity";
                grdNeedList.Columns[4].Header = "Price";
                grdNeedList.Columns[5].Header = "Description";
                grdNeedList.Columns[6].Header = "Obtained?";

                lblNeedlist.Content = "Need List (" + _needList.Count + ")";
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;

                MessageBox.Show(message);
            }
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-26
        /// Summary: Double clicking an item directs the user to the EditNeedlist UI 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        ///</summary>
        private void grdNeedList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NeedList needList = (NeedList)grdNeedList.SelectedItem;
            if (main.HasProjectRole("Project Starter", _ProjectID) || 
                main.HasProjectRole("Event Coordinator", _ProjectID) ||
                main.HasSystemRole("Admin"))
            {
                var editNeedListManager = new EditNeedList(needList, _needListManager);
                //var result = editNeedListManager.ShowDialog();
                this.NavigationService.Navigate(new EditNeedList(needList, _needListManager));
            }
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025/03/05
        /// Summary: This button directs the user to the EditNeedlist UI 
        /// Last updated by: Stan Anderson
        /// Last updated: 2025/04/07
        /// Changes: Updated MessageBox
        ///</summary
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            
                if (grdNeedList.SelectedItem != null)
                {
                    NeedList needList = (NeedList)grdNeedList.SelectedItem;
                    var editNeedListManager = new EditNeedList(needList, _needListManager);
                    //var result = editNeedListManager.ShowDialog();
                    this.NavigationService.Navigate(new EditNeedList(needList, _needListManager));
                }
                else
                {
                        MessageBox.Show("No item selected.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddItemDeskTop(_ProjectID));
        }



        

    }
}
