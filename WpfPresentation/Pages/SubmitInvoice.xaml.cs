/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the presentation layer, what is display when the project load when loading the submit invoice.
/// It make connection to user user input and the database
/// </summary>
///
/// <remarks>
/// Updater  Stan Anderson
/// Updated: 2025/04/08
/// 
/// </remarks>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataDomain;
using LogicLayer;

namespace WpfPresentation.Pages
{
    public partial class SubmitInvoice : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private Invoice Invoice;
        private IInvoiceManager _invoiceManager = new InvoiceManager();
        Invoice _invoice = new Invoice();
        private Expense _expense = null;

        private IProjectManager _projectManager = new ProjectManager();
        Project project = null;

        /// <summary>
        /// Akoi
        /// Created: 2025/02/28
        /// 
        /// this is submit a invoice to the invoice table.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Akoi
        /// Updated: 2025/02/28 
        /// example: Adding extra field to the invoice table, this invoice number
        /// </remarks>
        /// <param name=""></param>

        public SubmitInvoice(Expense expense)
        {
            InitializeComponent();
            _expense = expense;
        }

        /// <summary>
        /// Akoi Kollie
        /// Created: 2025/03/03
        /// This is the cancel button that is use cancel all activity that will be ongoing
        /// It takes you to a working page after you click the cancel button.
        /// </summary> 
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: 2025/04/08
        /// example: made it so it doesn't close the whole program
        /// </remarks>

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Akoi Kollie
        /// Created: 2025/03/03
        /// This is the submit button that that is use to submit the result of all user input of invoices they want to submit
        /// This method is alway use to validate the user input and throught error message if the input it wrong.
        /// </summary> 
        ///
        /// <remarks>
        /// Last Updated By: Kate Rich
        /// Last Updated: 2025-04-25
        /// What Was Changed: Updated the logic for when an invoice insert is successful - the user is redirected to
        ///                 the list of invoices for the project.
        /// </remarks>
        private void btnSubit_Click(object sender, RoutedEventArgs e)
        {
          
            string invoiceNumber = txtInvoiceNumber.Text;
            DateTime invoicedate = InvoiceDate.DisplayDate;
            InvoiceDate.DisplayDateEnd = DateTime.Today;
            string invoicedescription = txtInvoiceDescription.Text;
            string invoiceStatus = txtInvoiceStatus.Text;
            
            if (string.IsNullOrWhiteSpace(invoiceNumber) || string.IsNullOrWhiteSpace(invoicedescription) || string.IsNullOrWhiteSpace(invoiceStatus))
            {
                MessageBox.Show("All fields are required");
                return;
            }
            if (invoiceStatus != "Pending")
            {
                MessageBox.Show("Error: 'Paid and Processed' are not allowed", "Create Expense", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Invoice invoice = new Invoice();
                invoice.InvoiceNumber = invoiceNumber;
                invoice.ExpenseID = _expense.ExpenseID;
                invoice.ProjectID = _expense.ProjectID;
                invoice.InvoiceDate = invoicedate;
                invoice.Status = invoiceStatus;
                invoice.Description = invoicedescription;

                if(_invoiceManager.InsertInvoice(invoice))
                {
                    MessageBox.Show("Invoice was submitted");

                    project = _projectManager.GetProjectByID(invoice.ProjectID);

                    this.NavigationService.Navigate(new ViewAllProjectInvoices(project));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_expense != null)
            {
                txtExpenseId.Text = _expense.ExpenseID.ToString();
                txtExpenseId.IsEnabled = false;
            }

        }
    }
}
