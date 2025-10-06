/// <summary>
/// Created By: Syler Bushlack
/// Created: 2025/04/30
/// Summary:
/// Interaction logic for ViewSingleInvoice.xaml
/// 
/// Updater Name:
/// Updated:
/// What Changed:
/// </summary>
using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewSingleInvoice.xaml
    /// </summary>
    public partial class ViewSingleInvoice : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private InvoiceManager _invoiceManager = new InvoiceManager();
        private ExpenseManager _expenseManager = new ExpenseManager();
        Invoice _invoice = null;

        public ViewSingleInvoice(int id)
        {
            InitializeComponent();
            try
            {
                _invoice = _invoiceManager.GetInvoiceByInvoiceID(id);
            }
            catch (Exception ex)
            {
                NavigationService.GoBack();
            }
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnChange.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Accountant", _invoice.ProjectID) || 
                main.HasProjectRole("Purchaser", _invoice.ProjectID) ||
                main.HasProjectRole("Accountant", _invoice.ProjectID) ||
                main.SystemRoles.Contains("Admin"))
            {
                btnChange.Visibility = Visibility.Visible;
            }
        }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtInvoiceNumber.Text = _invoice.InvoiceNumber;
            txtInvoiceDate.Text = _invoice.InvoiceDate.ToShortDateString();
            txtStatus.Text = _invoice.Status;
            txtInvoiceDescription.Text = _invoice.Description;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ChangeInvoiceStatus(_invoice.InvoiceID));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
