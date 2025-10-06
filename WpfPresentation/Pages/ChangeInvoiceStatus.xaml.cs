/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the event class that is for event that the notification is need for
/// It make connection between the event and project that the notification is needed for.
/// </summary>
///
/// <remarks>
/// Updater: Stan Anderson
/// Updated: 2025/04/09
/// 
/// Updater: Syler Bushlack
/// Updated: 2025/04/30
/// What was changed: Fixed bugs in the btnSave_Click method
/// </remarks>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ChangeInvoiceStatus.xaml
    /// </summary>
    public partial class ChangeInvoiceStatus : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private InvoiceManager _changeInvoiceStatusManager = new InvoiceManager();
        Invoice _invoice = null;

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: Initializes the ChangeInvoiceStatus page and sets up the InvoiceManager instance. 
        /// This constructor prepares the UI components and ensures user data management
        /// can be accessed during the changeInvoiceStatus. 
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: removed hardcoding
        /// </summary>
        public ChangeInvoiceStatus(int id)
        {
            _invoice = _changeInvoiceStatusManager.GetInvoiceByInvoiceID(id);
            InitializeComponent();
        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: This is the button shutdown every activities that the user is doing when on the 
        /// UI page when the page loaded and take the user where they were before the page loaded
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: This is the button that save the user choice of invoice status
        /// This UI will check if the user choose an invoice status and check if the invoice ID and
        /// status exist and also throw an error message if there is an error.
        /// Last Upaded By: Syler Bushlack
        /// Last Updated: 2025/04/30
        /// What Was Changed: uncommented "result = _changeInvoiceStatusManager.SelectChangeInvoiceStatusByInvoiceID(_invoice.InvoiceID, _invoice.Status);"
        ///     and changed the method called to EditChangeInvoiceStatusByInvoiceID which is the method that updates invoices. Also changed the 
        ///     initializing of result from true to false
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
             _invoice.Status = txtStatus.Text;
            
            try
            {
                if (_invoice.Status == "Pending")
                {
                    MessageBox.Show("Error: 'Pending' is not allowed", "Invalid Status", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("There was an error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            if(string.IsNullOrEmpty(_invoice.Status))
            {
                MessageBox.Show("Please select status and put in the amount", "Invalid Status", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool result = false;

            try
            {
                result = _changeInvoiceStatusManager.EditChangeInvoiceStatusByInvoiceID(_invoice.InvoiceID, _invoice.Status);
                if (result == true)
                {
                    MessageBox.Show("Invoice status was updated successful", "Invoice Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Invoice status update failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary:This is a page loaded event, that loaded all the parameter of invoice ID
        /// selected to change it status for the user to see before changing it status.
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtInvoiceID.Text = _invoice.InvoiceID + "";
            txtInvoiceNumber.Text = _invoice.InvoiceNumber;
            txtExpenseID.Text = _invoice.ExpenseID + "";
            txtProjectID.Text = _invoice.ProjectID.ToString();
            txtInvoiceDate.Text = _invoice.InvoiceDate.ToString();
            txtStatus.Text = _invoice.Status;
            txtInvoiceDescription.Text = _invoice.Description;
        }
    }
}
