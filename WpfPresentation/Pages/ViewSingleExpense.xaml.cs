/// <summary>
/// Creator:  
/// Created:  
/// Summary:  
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025/04/25
/// What was Changed: Added a button to submit invoice 
/// </summary>

using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using DataDomain;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewSingleExpense.xaml
    /// </summary>
    public partial class ViewSingleExpense : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private ExpenseManager _expenseManager = new ExpenseManager();
        private Expense _expense;
        private int _projectId;

        public ViewSingleExpense(int expenseId, int projectId)
        {
            InitializeComponent();

            Expense expense = _expenseManager.GetExpenseByExpenseIDProjectID(expenseId, projectId);

            if(expense != null)
            {
                txtExpenseDate.Text = expense.Date.ToString();
                txtExpenseAmount.Text = expense.Amount.ToString();
                txtExpenseType.Text = expense.ExpenseTypeID;
                txtExpenseDescription.Text = expense.Description;
                _expense = expense;
                _projectId = projectId;
            } 
            else
            {
                MessageBox.Show("Expense could not be found", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);    
            }
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnSubmitInvoice.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Accountant", _expense.ProjectID) || 
                main.HasProjectRole("Purchaser", _expense.ProjectID) ||
                main.HasProjectRole("Project Starter", _expense.ProjectID) ||
                main.HasSystemRole("Admin"))
            {
                btnSubmitInvoice.Visibility = Visibility.Visible;
            }
        }


        private void btnBackAllExpenses_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        private void btnSubmitInvoice_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new SubmitInvoice(_expense));
        }
    }
}
