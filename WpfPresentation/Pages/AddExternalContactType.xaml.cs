/// <summary>
/// Jacob McPherson
/// Created: 2025/02/18
/// 
/// Interaction logic for frmAddExternalContactType.xaml
/// </summary>
///
/// <remarks>
/// Updater Name: Stan Anderson
/// Updated: 2025/04/06
/// </remarks>
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class frmAddExternalContactType : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private ExternalContactManager _contactManager;
        public frmAddExternalContactType()
        {
            InitializeComponent();
            _contactManager = new ExternalContactManager();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are You Sure You Want to Cancel?", "Cancel?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text == null || txtName.Text == "")
            {
                MessageBox.Show("Add a contact type name.", "Missing Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtDescription.Text == null || txtDescription.Text == "")
            {
                MessageBox.Show("Add a description.", "Missing Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool success = false;
            try
            {
                success = _contactManager.AddExternalContactType(txtName.Text, txtDescription.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Adding External Contact Type", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(success)
            {
                MessageBox.Show("Contact type was added.", "Contact Added", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("There was an error adding contact type.", "Error Adding Contact Type", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
