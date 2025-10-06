/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/12
/// Summary:  Assign a task to a volunteer.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/06
/// What was Changed: Added navigational features
/// </summary>	

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class TaskAssignment : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        TaskManager _taskManager = new TaskManager();
        List<DataDomain.Task> _tasks = new List<DataDomain.Task>();

        private int eventID;

        private string eventName;

        private string volunteerName;

        private int volunteerUserID;

        private Event _event;

        public TaskAssignment()
        {
            InitializeComponent();
            ValidateInformation();
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnSubmitTaskAssignment.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Project Starter", _event.ProjectID) || 
                main.HasProjectRole("Event Coordinator", _event.ProjectID) ||
                main.HasProjectRole("Volunteer Director", _event.ProjectID) ||
                main.HasSystemRole("Admin"))
            {
                btnSubmitTaskAssignment.Visibility = Visibility.Visible;
            }
        }


        public TaskAssignment(Event @event, int volunteerUserID, string volunteerName)
        {
            _event = @event;
            this.eventID = _event.EventID;
            this.eventName = _event.Name;
            this.volunteerUserID= volunteerUserID;
            this.volunteerName = volunteerName;
            InitializeComponent();
            ValidateInformation();
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/13
        /// Summary:  This method validates that the information forwarded to this page.
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed:
        /// </summary>
        public void ValidateInformation()
        {
            if (volunteerUserID == 0)
            {
                btnSubmitTaskAssignment.IsEnabled = false;
                MessageBox.Show("The volunteer selected is invalid and a task cannot be assigned at this time.", "Invalid Volunteer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (eventID == 0)
            {
                btnSubmitTaskAssignment.IsEnabled = false;
                MessageBox.Show("The event selected is invalid and a task cannot be assigned at this time.", "Invalid Event", MessageBoxButton.OK, MessageBoxImage.Error);
                eventName = "Event";
            }
            lblTaskAssignment.Content = "Assign task to " + volunteerName + "\n for " + eventName;
        }

        

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/12
        /// Summary:  This method validates and inserts the new assignment.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/20
        /// What was Changed: Added navigation to view all volunteers assigned to the event.
        /// </summary>
        private void btnSubmitTaskAssignment_Click(object sender, RoutedEventArgs e)
        {
            if (gridTasksForTaskAssignment.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid Task Selected", "Invalid Task", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    int volunteerID = volunteerUserID;
                    DataDomain.Task task = (DataDomain.Task)gridTasksForTaskAssignment.SelectedItem;
                    if(task == null)
                    {
                        MessageBox.Show("Invalid Task Selected", "Invalid Task", MessageBoxButton.OK, MessageBoxImage.Warning);
                    } 
                    else
                    {
                        int taskID = task.TaskID;
                        string taskName = task.Name;

                        MessageBoxResult confirmAssignment = MessageBox.Show("Assign " + volunteerName + " the task of " + taskName + " for " + eventName + "?"
                                                                                , "Confirm Assignment", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (confirmAssignment == MessageBoxResult.Yes)
                        {
                            if (_taskManager.TaskAssignment(taskID, volunteerID))
                            {
                                MessageBox.Show("The assignment was successful", "Task Assigned", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.NavigationService.Navigate(new ViewEventVolunteers(_event));
                            }
                            else
                            {
                                MessageBox.Show("An unexpected error occured trying to add the task.\nPlease try again."
                                               , "Task Assignment Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Assigning Task", "Task Assignment Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
           
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/12
        /// Summary:  This method cancels the current process of task assignment.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/28
        /// What was Changed: Added going back
        /// </summary>
        private void btnCancelTaskAssignment_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/12
        /// Summary:  This method retrieves and populates the TaskAssignmentByEventID list table.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/25
        /// What was Changed: Updated to a data grid.
        /// </summary>
        private void gridTasksForTaskAssignment_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _tasks = _taskManager.GetTasksByEventID(eventID);
                lblTaskAssignmentByEventIDList.Content = "Tasks (" + _tasks.Count + ")";
                
                gridTasksForTaskAssignment.ItemsSource = _tasks;
                gridTasksForTaskAssignment.Columns[0].Visibility = Visibility.Hidden;
                gridTasksForTaskAssignment.Columns[3].Visibility = Visibility.Hidden;
                gridTasksForTaskAssignment.Columns[5].Visibility = Visibility.Hidden;
                gridTasksForTaskAssignment.Columns[6].Visibility = Visibility.Hidden;



            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading Task List", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
