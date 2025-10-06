/// <summary>
/// Creator: Eric Idle
/// Created: 2025/03/14
/// 
/// Page to view add an expense based on a certain project
/// 
/// </summary>
///
/// <remarks>
/// Updater Name Eric Idle
/// Updated: 2025/04/30
///     Commented out debugging code
/// </remarks>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for AddExpenseDesktop.xaml
    /// </summary>
    public partial class AddExpenseDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IProjectManager _projectManager = new ProjectManager();
        private IExpenseManager _expenseManager = new ExpenseManager();
        Project project = null;
        private Expense _expense;
        private IExpenseTypeManager _expenseTypeManager = new ExpenseTypeManager();

        public AddExpenseDesktop(Project project)
        {
            this.project = project;
            InitializeComponent();

            populateExpenseTypeComboBox();
        }

        private void btnSubmitExpense_Click(object sender, RoutedEventArgs e)
        {
            decimal amount;

            try
            {
                if(dpExpenseDate.SelectedDate == null)
                {
                    MessageBox.Show("Please select a valid date");
                    return;
                }
                if(txtExpenseAmount == null || txtExpenseAmount.Text == "")
                {
                    MessageBox.Show("Add an Expense amount", "Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if(!decimal.TryParse(txtExpenseAmount.Text, out amount))
                {
                    MessageBox.Show("Add a valid number amount", "Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if(amount < 0)
                {
                    MessageBox.Show("Add a valid number amount above 0.00", "Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if(amount > 999999.99m)
                {
                    MessageBox.Show("Add a valid number amount under 1,000,000.00", "Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if(cmbExpenseType == null || cmbExpenseType.Text == "")
                {
                    MessageBox.Show("Add an Expense type", "Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if(txtExpenseDescription == null || txtExpenseDescription.Text == "")
                {
                    MessageBox.Show("Add an Expense description", "Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Expense newExpense = new Expense()
                {
                    ProjectID = project.ProjectID,
                    ExpenseTypeID = cmbExpenseType.Text,
                    Date = DateTime.Parse(dpExpenseDate.Text),
                    Amount = amount,
                    Description = txtExpenseDescription.Text
                };

                bool success = _expenseManager.AddExpenseByProjectID(newExpense);

                if(success)
                {
                    _projectManager.UpdateAvailableFunds(project.ProjectID, amount, false);

                    MessageBox.Show("Expense Successfully Added", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                    ((MainWindow)Window.GetWindow(this)).mainFrame.Navigate(new SelectAllProjectExpenses(project));
                }
                else
                {
                    MessageBox.Show("Add Failed", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelExpense_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        public void populateExpenseTypeComboBox()
        {
            try
            {
                List<ExpenseType> _expenseTypes = _expenseTypeManager.GetAllExpenseTypes();
                foreach (ExpenseType expenseType in _expenseTypes)
                { 
                    cmbExpenseType.Items.Add(expenseType.ExpenseTypeID);    
                }
            }
            catch(Exception)
            {

                MessageBox.Show("Failed to obtain Expense types.", "Procedure Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}