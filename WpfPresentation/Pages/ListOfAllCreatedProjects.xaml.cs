/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/04
/// Summary:  Interaction logic for ListOfAllCreatedProjects.xaml
/// 
/// Creator:    Chase Hannen
/// Created:    2025/05/02
/// Summary:    Typo fix
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class ListOfAllCreatedProjects : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IProjectManager _projectManager = new ProjectManager();

        public ListOfAllCreatedProjects()
        {
            InitializeComponent();

            GetPrivileges();
        }

        private void GetPrivileges()
        {
            btnCreateProject.Visibility = Visibility.Collapsed;
            if (main.HasSystemRole("User") || main.HasSystemRole("Admin"))
            {
                btnCreateProject.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/04/17
        /// Summary:  A Method to filter grdProjects based 
        /// on a Product atribute and a string to search
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/04/17
        /// What was Changed: Initial creation	
        /// </summary>
        private void FilterDataGrid()
        {
            List<ProjectVM> tempList = new List<ProjectVM>();
            List<ProjectVM> projects = _projectManager.GetAllProjectVMsByUserID(main.UserID);
            foreach(ProjectVM project in projects)
            {
                if(project.Name.ToLower().Contains(txtSearchBar.Text.ToLower()))
                {
                    tempList.Add(project);
                }
            }
            if(tempList.Count > 0)
            {
                grdProjects.ItemsSource = tempList;
                grdProjects.Columns[0].Header = "Start Date";
                grdProjects.Columns[1].Header = "Project Starter";
                grdProjects.Columns.RemoveAt(2);
                grdProjects.Columns[2].Header = "Location";
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns[3].Header = "Project Name";
                grdProjects.Columns[4].Header = "Project Type";
                grdProjects.Columns.RemoveAt(5);
                grdProjects.Columns.RemoveAt(5);
                grdProjects.Columns.RemoveAt(5);
                grdProjects.Columns[5].Header = "Status";
                grdProjects.Columns[6].Header = "Description";
                grdProjects.Columns.RemoveAt(7);
                grdProjects.Columns.RemoveAt(7);
                grdProjects.Columns.RemoveAt(7);
            }
            lblProjects.Content = "My Projects (" + projects.Count + ")";
        }

        private void txtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterDataGrid();
        }

        private void btnViewProject_Click(object sender, RoutedEventArgs e)
        {
            Project selectedProject = (Project)grdProjects.SelectedItem;
            if(selectedProject == null)
            {
                MessageBox.Show("Please select a project to view.", "Invalid Project", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(selectedProject.ProjectID));
            }
        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new createProject());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<ProjectVM> projects;

            try
            {
                projects = _projectManager.GetAllProjectVMsByUserID(main.UserID);
                grdProjects.ItemsSource = projects;

                grdProjects.Columns[0].Header = "Start Date";
                grdProjects.Columns[1].Header = "Project Starter";
                grdProjects.Columns.RemoveAt(2);
                grdProjects.Columns[2].Header = "Location";
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns.RemoveAt(3);
                grdProjects.Columns[3].Header = "Project Name";
                grdProjects.Columns[4].Header = "Project Type";
                grdProjects.Columns.RemoveAt(5);
                grdProjects.Columns.RemoveAt(5);
                grdProjects.Columns.RemoveAt(5);
                grdProjects.Columns[5].Header = "Status";
                grdProjects.Columns[6].Header = "Description";
                grdProjects.Columns.RemoveAt(7);
                grdProjects.Columns.RemoveAt(7);
                grdProjects.Columns.RemoveAt(7);

                lblProjects.Content = "My Projects (" + projects.Count + ")";
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;

                MessageBox.Show(message, "Error Loading Projects", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}