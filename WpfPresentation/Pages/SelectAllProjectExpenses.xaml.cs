/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/27
/// Summary:
///     Page to view all expenses based on a certain project
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for SelectAllProjectExpenses.xaml
    /// </summary>
    public partial class SelectAllProjectExpenses : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private IProjectManager _projectManager = new ProjectManager();
        private IExpenseManager _expenseManager = new ExpenseManager();
        private List<Expense> _expenses;
        Project project = null;

        public SelectAllProjectExpenses(Project project)
        {
            this.project = project;
            InitializeComponent();

            GetPrivileges();
        }

        private void GetPrivileges()
        {
            btnAddExpense.Visibility = Visibility.Collapsed;
            btnView.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Project Starter", project.ProjectID) ||
                main.HasProjectRole("Purchaser", project.ProjectID) ||
                main.HasProjectRole("Accountant", project.ProjectID) ||
                main.SystemRoles.Contains("Admin"))
            {
                btnAddExpense.Visibility = Visibility.Visible;
                btnView.Visibility = Visibility.Visible;
            }
        }

        private void grdExpense_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(main.HasProjectRole("Accountant", project.ProjectID) ||
               main.HasProjectRole("Purchaser", project.ProjectID) ||
               main.HasProjectRole("Project Starter", project.ProjectID) ||
               main.HasSystemRole("Admin"))
            {
                int selectedExpense = grdExpense.SelectedIndex;

                // make sure header not selected
                if(selectedExpense >= 0)
                {
                    ((MainWindow)Window.GetWindow(this)).mainFrame.Navigate(new ViewSingleExpense(
                        _expenses[selectedExpense].ExpenseID,
                        _expenses[selectedExpense].ProjectID));
                }
            }
            
        }

        private void btnAddExpense_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).mainFrame.Navigate(new AddExpenseDesktop(project));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ProjectVM project = _projectManager.GetProjectInformationByProjectID(_expenses.First().ProjectID);
            NavigationService.GetNavigationService(this)?.Navigate(new ViewProjectFunds(project));
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Expense selectedExpense = (Expense)grdExpense.SelectedItem;
            if(selectedExpense == null)
            {
                MessageBox.Show("Please select an expense to view.", "Invalid Expense", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NavigationService.GetNavigationService(this)?.Navigate(new ViewSingleExpense(selectedExpense.ExpenseID,selectedExpense.ProjectID));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _expenses = _expenseManager.GetAllExpensesByProjectID(project.ProjectID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                _expenses = new List<Expense>();
            }

            grdExpense.ItemsSource = _expenses;
            lblTitle.Content = "Expenses for " + project.Name + " (" + _expenses.Count() + ")";

            if(grdExpense.Columns.Count > 0)
            {
                if(grdExpense.Columns.Count > 4)
                {
                    grdExpense.Columns.RemoveAt(0);
                }

                if(grdExpense.Columns.Count > 4)
                {
                    grdExpense.Columns.RemoveAt(0);
                }
                

                // Modify column headers
                if(grdExpense.Columns.Count > 0)
                {
                    grdExpense.Columns[0].Header = "Expense Type";
                }

                if(grdExpense.Columns.Count > 1)
                {
                    grdExpense.Columns[1].Header = "Expense Date";
                }
                if(grdExpense.Columns.Count > 2)
                {
                    grdExpense.Columns[2].Header = "Amount ($)";
                }
                if(grdExpense.Columns.Count > 3)
                {
                    grdExpense.Columns[3].Header = "Description";
                }

            }
        }
    }
}