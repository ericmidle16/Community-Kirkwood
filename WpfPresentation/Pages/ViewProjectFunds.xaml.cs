/// <summary>
/// Creator: Eric Idle
/// Created: 2025/04/11
/// Summary:
///     Page to view all funds based on a certain project
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewProjectFunds.xaml
    /// </summary>
    public partial class ViewProjectFunds : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference
        IProjectManager _projectManager = new ProjectManager();

        ProjectVM project = null;

        public ViewProjectFunds(Project project)
        {
            this.project = _projectManager.GetProjectInformationByProjectID(project.ProjectID);
            InitializeComponent();

            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnViewExpense.Visibility = Visibility.Collapsed;
            btnViewDonation.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Project Starter", project.ProjectID) || 
                main.HasProjectRole("Accountant", project.ProjectID) ||
                main.HasProjectRole("Purchaser", project.ProjectID) ||
                main.HasSystemRole("Admin"))
            {
                btnViewExpense.Visibility = Visibility.Visible;
                btnViewDonation.Visibility = Visibility.Visible;
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblTitle.Content = "Funds for " + project.Name;
            txtFundAmount.Text = "$" + project.AvailableFunds;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(project.ProjectID));
        }

        private void btnViewDonation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewAllDonations(project));
        }

        private void btnViewExpense_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new SelectAllProjectExpenses(project));
        }

        private void btnViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewAllProjectInvoices(project));
        }
    }
}