/// <summary>
/// Creator:  Jennifer Nicewanner
/// Created:  2025-04-13
/// Summary:  This is a view for a volunteer starter to see key information re: a volunteer applying for a project.
///           There are 4 data grids with partial info for each grid re: projects this volunteer is participating in, 
///           active vehicles, active skills, and active availability.
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated:     2025-04-13
/// What was Changed: Initial Creation
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated:     2025-04-25
/// What was Changed: Updated the PopulateVehiclesDataGrid
/// </summary>

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataDomain;
using LogicLayer;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewVolunteerProfile.xaml
    /// </summary>
    public partial class ViewVolunteerProfile : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        UserManager _userManager = new UserManager();
        User _user;

        AvailabilityManager _availabilityManager = new AvailabilityManager();
        List<Availability> availabilities = new List<Availability>();

        SkillManager _skillManager = new SkillManager();
        List<UserSkill> userSkills = new List<UserSkill>();

        ProjectManager _projectManager = new ProjectManager();
        List<Project> projects = new List<Project>();

        int ProjectID;

        VehicleManager _vehicleManager = new VehicleManager();
        List<Vehicle> vehicles = new List<Vehicle>();   

        public ViewVolunteerProfile()
        {
            InitializeComponent();
        }

        public ViewVolunteerProfile(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void PopulateProjectsDataGrid()
        {
            try
            {
                projects = _projectManager.GetAllProjectsByUserID(_user.UserID);
                ProjectsDataGrid.ItemsSource = projects;

                ProjectsDataGrid.Columns.RemoveAt(0);
                ProjectsDataGrid.Columns.RemoveAt(2);
                ProjectsDataGrid.Columns.RemoveAt(2);
                ProjectsDataGrid.Columns.RemoveAt(5);
                ProjectsDataGrid.Columns.RemoveAt(5);
                ProjectsDataGrid.Columns.RemoveAt(5);

                ProjectsDataGrid.Columns[1].Header = "Project Type";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem accessing the projects for this user.", ex);
            }
        }

        private void PopulateVehiclesDataGrid()
        {
            try
            {
                vehicles = _vehicleManager.GetAllActiveVehiclesByUserID(_user.UserID);
                VehiclesDataGrid.ItemsSource = vehicles;

                VehiclesDataGrid.Columns.RemoveAt(0);
                VehiclesDataGrid.Columns.RemoveAt(0);
                VehiclesDataGrid.Columns.RemoveAt(0);
                VehiclesDataGrid.Columns.RemoveAt(0);
                VehiclesDataGrid.Columns.RemoveAt(0);
                VehiclesDataGrid.Columns.RemoveAt(0);
                VehiclesDataGrid.Columns.RemoveAt(0);
                VehiclesDataGrid.Columns[1].DisplayIndex = 2;
                VehiclesDataGrid.Columns[2].DisplayIndex = 3;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem accessing the active vehicles for this user.", ex);
            }
        }

        private void PopulateSkillsDataGrid()
        {
            try
            {
                userSkills = _skillManager.GetUserSkillsByUserID(_user.UserID);
                SkillsDataGrid.ItemsSource = userSkills;

                SkillsDataGrid.Columns.RemoveAt(0);
                SkillsDataGrid.Columns[0].Header = "Volunteer Skills";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem accessing the skills for this user.", ex);
            }
        }

        private void PopulateAvailabilityDataGrid()
        {
            try
            {
                availabilities = _availabilityManager.SelectAvailabilityByUser(_user.UserID);
                AvailabilityDataGrid.ItemsSource = availabilities;

                AvailabilityDataGrid.Columns.RemoveAt(0);
                AvailabilityDataGrid.Columns.RemoveAt(0);
                AvailabilityDataGrid.Columns.RemoveAt(0);
                AvailabilityDataGrid.Columns.RemoveAt(0);


            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem accessing the availability for this user.", ex);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtName.Content = _user.GivenName +" "+ _user.FamilyName;
                PopulateProjectsDataGrid();
                PopulateVehiclesDataGrid();
                PopulateSkillsDataGrid();
                PopulateAvailabilityDataGrid();      
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.GetNavigationService(this)?.Navigate(new AcceptVolunteerOffers(ProjectID));
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
