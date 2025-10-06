///<summary>
/// Creator: Dat Tran
/// Created: 2025-02-26
/// Summary: This xaml code displays an interface to let users edit the values of the database. Can save or cancel the changes. 
/// Last updated by: Stan Anderson
/// Last updated: 2025/04/08
/// Changes: Validation for price
///</summary>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataDomain;
using LogicLayer;

namespace WpfPresentation.Pages
{ 
    public partial class EditNeedList : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference
        private NeedList _editNeedList;
        private NeedListManager _needListManager;
        
        
        public EditNeedList(NeedList needlist, NeedListManager needListManager)
        {
            InitializeComponent();
            this._editNeedList = needlist;
            this._needListManager = needListManager;
            tbDescription.Text = _editNeedList.Description;
            tbItemName.Text = _editNeedList.Name;
            tbPrice.Text = _editNeedList.Price.ToString();
            tbQuantity.Text = _editNeedList.Quantity.ToString();
            _needListManager = new NeedListManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            try
            {
                int quantity = Int32.Parse(tbQuantity.Text);
                decimal price = Decimal.Parse(tbQuantity.Text);
                result = _needListManager.UpdateNeedList(_editNeedList.ProjectID,tbItemName.Text,quantity,price, tbDescription.Text, _editNeedList.Name, _editNeedList.Quantity, _editNeedList.Price, _editNeedList.Description, _editNeedList.ItemID);
                if (result)
                {
                    MessageBox.Show("Update successful!", "Success",MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Update failed. Please try again.", "Failed.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Update failed. Check to make sure numbers are valid",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

       

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {_editNeedList.Name}?",
                                                          "Confirm Deletion",
                                                          MessageBoxButton.YesNo,
                                                          MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    int rowsAffected = _needListManager.DeleteFromNeedList(_editNeedList.ItemID);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item successfully deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GoBack();
                    }

                    else
                    {
                        MessageBox.Show("Item was not deleted. It may not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (ApplicationException appEx)
                {
                    MessageBox.Show($"Application Error: {appEx.Message}", "Deletion Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected Error: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}", "Deletion Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
            
    }
    
}
