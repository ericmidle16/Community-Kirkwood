/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/17
/// Summary:  View the list of tasks assigned to volunteers by eventID in the database.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// What was Changed: Added proper navigation link
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/05/01
/// What was Changed: Removed refrences to edit and unassign task because those features were never made
/// </summary>	

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{

    public partial class ViewEventVolunteers : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        TaskManager taskManager = new TaskManager();
        UserManager _userManager = new UserManager();

        List<TaskAssignedViewModel> assignedVolunteers = new List<TaskAssignedViewModel>();

        private int eventID;
        private string eventName;
        Event _event;

        public ViewEventVolunteers(Event @event)
        {
            _event = @event;
            eventName = _event.Name;
            eventID = _event.EventID;
            InitializeComponent();
            validatePermission();
        }

        public ViewEventVolunteers(int eventID, string eventName)
        {
            this.eventID = eventID;
            if (eventName == null)
            {
                eventID = 100000;
                eventName = "Bake Sale";
            }
            InitializeComponent();
            validatePermission();
        }

        private void validatePermission()
        {
            //btnEditTask.Visibility = Visibility.Hidden;
            //btnUnassign.Visibility = Visibility.Hidden;
            btnRequest.Visibility = Visibility.Hidden;
            btnViewUnassignedVolunteers.Visibility = Visibility.Hidden;
            btnViewAllTasks.Visibility = Visibility.Collapsed;
            btnViewUnassignedVolunteers.Visibility = Visibility.Collapsed;

            if (_event != null){
                ProjectManager _projectManager = new ProjectManager();
                Project project = _projectManager.GetProjectByID(_event.ProjectID);

                if (main.IsProjectStarter(project.ProjectID))
                {
                    btnRequest.Visibility = Visibility.Visible;
                    btnViewUnassignedVolunteers.Visibility = Visibility.Visible;
                }
                if (main.IsVolunteer(project.ProjectID))
                {
                    btnViewAllTasks.Visibility = Visibility.Visible;
                }
                if (main.HasProjectRole("Project Starter", project.ProjectID) ||
                    main.HasProjectRole("Volunteer Director", project.ProjectID) ||
                    main.HasProjectRole("Event Coordinator", project.ProjectID) ||
                    main.HasProjectRole("Moderator", project.ProjectID) ||
                    main.HasProjectRole("BAckground Checker", project.ProjectID) ||
                    main.HasSystemRole("Admin"))
                {
                    btnViewUnassignedVolunteers.Visibility= Visibility.Visible;
                }
            }
        }


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/17
        /// Summary:  Go back
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/06
        /// What was Changed: Added the literal one line of code needed.
        /// </summary>
        private void btnBackViewEventVolunteerList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/17
        /// Summary:  Edit or add task
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        private void btnEditTask_Click(object sender, RoutedEventArgs e)
        {
            if (gridVolunteersAndTasksByEventID.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid Volunteer Selected", "Invalid Volunteer", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                TaskAssignedViewModel assignment = (TaskAssignedViewModel)gridVolunteersAndTasksByEventID.SelectedItem;
                if (assignment == null)
                {
                    MessageBox.Show("Invalid Volunteer Selected", "Invalid Volunteer", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    // go to UpdateTaskAssignment page
                }
            }
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/17
        /// Summary:  Go view all tasks.
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        private void btnViewAllTasks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new ViewTasksByEventID(eventID, eventName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/17
        /// Summary:  Go view all unassigned volunteers
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        private void btnViewUnassignedVolunteers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new viewUnassignedVolunteers(_event));

        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/17
        /// Summary:  Generate the external contact grid
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/09
        /// What was Changed: Removed hardcoding
        /// </summary>
        private void gridVolunteersAndTasksByEventID_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                

                assignedVolunteers = taskManager.GetVolunteersAndTasksByEventID(eventID);
                lblViewEventVolunteers.Content = "Volunteers Assigned to " + eventName + " (" + assignedVolunteers.Count + ")";

                gridVolunteersAndTasksByEventID.ItemsSource = assignedVolunteers;
                gridVolunteersAndTasksByEventID.Columns[0].Header = "Given Name";
                gridVolunteersAndTasksByEventID.Columns[1].Header = "Family Name";
                gridVolunteersAndTasksByEventID.Columns[4].Header = "Task Name";
                gridVolunteersAndTasksByEventID.Columns[5].Header = "Task Description";
                gridVolunteersAndTasksByEventID.Columns[6].Visibility = Visibility.Hidden;
                gridVolunteersAndTasksByEventID.Columns[7].Visibility = Visibility.Hidden;
                gridVolunteersAndTasksByEventID.Columns[8].Visibility = Visibility.Hidden;

            }
            catch (Exception)
            {
                MessageBox.Show("There was an error loading the volunteers and their tasks.", "Error Loading List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            // FIX THIS
            NavigationService.GetNavigationService(this)?.Navigate(new RequestVolunteer(_event));

        }


        private void btnUnassign_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
