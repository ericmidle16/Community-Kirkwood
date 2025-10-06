/// <summary>
/// Creator: Eric Idle
/// Created: 2025/04/10
/// 
/// Page to view all expenses based on a certain project
/// 
/// </summary>
///
/// <remarks>
/// Updater: Akoi Kollie
/// Updated: 2025/04/25
/// 
/// Updater: Syler Bushlack
/// Updated: 2025/04/30
/// What was Changed: added btnView_Click method to redirect the user to a page displaying a single invoice's details
/// </remarks>
using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewAllProjectInvoices.xaml
    /// </summary>
    public partial class ViewAllProjectInvoices : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private InvoiceManager _invoiceManager = new InvoiceManager();
        private List<Invoice> _invoices;
        Project project = null;

        public ViewAllProjectInvoices(Project project)
        {
            this.project = project;
            InitializeComponent();
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnChange.Visibility = Visibility.Collapsed;
            btnView.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Accountant", project.ProjectID) || 
                main.HasProjectRole("Project Starter", project.ProjectID) ||
                main.HasProjectRole("Purchaser", project.ProjectID) ||
                main.SystemRoles.Contains("Admin"))
            {
                btnChange.Visibility = Visibility.Visible;
                btnView.Visibility = Visibility.Visible;
            }
        }

        

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoices = _invoiceManager.GetAllInvoicesByProjectID(project.ProjectID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                _invoices = new List<Invoice>();
            }

            grdInvoice.ItemsSource = _invoices;
            lblTitle.Content = "Invoices for " + project.Name + " (" + _invoices.Count() + ")";

            if (grdInvoice.Columns.Count > 0)
            {
                if (grdInvoice.Columns.Count > 4)
                {
                    grdInvoice.Columns.RemoveAt(0);
                }

                if (grdInvoice.Columns.Count > 4)
                {
                    grdInvoice.Columns.RemoveAt(1);
                }
                if (grdInvoice.Columns.Count > 4)
                {
                    grdInvoice.Columns.RemoveAt(1);
                }


                // Modify column headers
                if (grdInvoice.Columns.Count > 0)
                {
                    grdInvoice.Columns[0].Header = "Invoice Number";
                }

                if (grdInvoice.Columns.Count > 1)
                {
                    grdInvoice.Columns[1].Header = "Invoice Date";
                }
            }
        }
        //Author: Akoi Kollie
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            var selectInvoice = (Invoice)grdInvoice.SelectedItem;
            if (selectInvoice == null)
            {
                MessageBox.Show("Please select an invoice", "not invoice selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NavigationService.GetNavigationService(this)?.Navigate(new ChangeInvoiceStatus(selectInvoice.InvoiceID));
            }

        }

        // Author: Syler Bushlack
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectInvoice = (Invoice)grdInvoice.SelectedItem;
            if (selectInvoice == null)
            {
                MessageBox.Show("Please select an invoice", "not invoice selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NavigationService.GetNavigationService(this)?.Navigate(new ViewSingleInvoice(selectInvoice.InvoiceID));
            }
        }
    }
}
