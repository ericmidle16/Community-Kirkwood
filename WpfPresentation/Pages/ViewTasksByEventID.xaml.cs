/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/12
/// Summary:  View the list of task by eventID in the database.
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// What was Changed: Updated UI and added connections
/// </summary>	

using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation.Pages
{
    public partial class ViewTasksByEventID : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        TaskManager _taskManager = new TaskManager();
        LogicLayer.EventManager _eventManager = new LogicLayer.EventManager();
        List<DataDomain.Task> _tasks = new List<DataDomain.Task>();

        private int _eventID;
        private string _eventName;
        private Project _project;
        private int _projectID;

        public ViewTasksByEventID()
        {
            InitializeComponent();
        }

        public ViewTasksByEventID(Project project) 
        {
            _eventID = 0;
            _project = project;
            _projectID = project.ProjectID;
            InitializeComponent();
            validatePermission();
        }

        public ViewTasksByEventID(int eventID, string eventName)
        {
            _eventID = eventID;
            _eventName = eventName;
            _projectID = _eventManager.SelectEventByID(eventID).ProjectID;
            InitializeComponent();
            validatePermission();
        }

        private void validatePermission()
        {
            btnCreateTask.Visibility = Visibility.Hidden;
            btnEditTask.Visibility = Visibility.Hidden;
            btnViewTask.Visibility = Visibility.Hidden;
            if (main.isLoggedIn)
            {
                if (main.HasProjectRole("Project Starter", _projectID) ||
                    main.HasProjectRole("Event Coordinator", _projectID) ||
                    main.HasProjectRole("Volunteer Director", _projectID) ||
                    main.HasSystemRole("Admin"))
                {
                        btnCreateTask.Visibility = Visibility.Visible;
                        btnEditTask.Visibility = Visibility.Visible;
                }
                if (main.IsVolunteer(_projectID) ||
                    main.HasSystemRole("Admin"))
                {
                    btnViewTask.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/14
        /// Summary:  This method will go back to the previous page
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/28
        /// What was Changed: Added the ability to go back
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/14
        /// Summary:  This method will edit the selected task
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/04/04
        /// What was Changed: Added navigation
        /// </summary>
        private void btnEditTask_Click(object sender, RoutedEventArgs e)
        {
            if (gridTasksByEventID.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task to edit.", "Invalid Task", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                DataDomain.Task task = (DataDomain.Task)gridTasksByEventID.SelectedItem;
                if(task == null)
                {
                    MessageBox.Show("Please select a task to edit.", "Invalid Task", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    NavigationService.GetNavigationService(this)?.Navigate(new createEditTask(task.TaskID));
                }
            }
        }


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/14
        /// Summary:  This method will go to a create a new task page
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed:
        /// </summary>
        private void btnCreateTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new createEditTask(_eventID, _projectID));
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/12
        /// Summary:  This method retrieves and populates the TasksByEventID list table.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/25
        /// What was Changed: Changed list view into data grid
        /// </summary>
        private void gridTasksByEventID_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_eventID == 0)
                {
                    _tasks = _taskManager.GetTasksByProjectID(_projectID);
                    _eventName = "Project";
                }
                else
                {
                    _tasks = _taskManager.GetTasksByEventID(_eventID);
                }

                lblTasksByEventID.Content = "Tasks for " + _eventName + " (" + _tasks.Count + ")";

                gridTasksByEventID.ItemsSource = _tasks;
                gridTasksByEventID.Columns[0].Visibility = Visibility.Hidden;
                gridTasksByEventID.Columns[3].Visibility = Visibility.Hidden;
                gridTasksByEventID.Columns[4].Visibility = Visibility.Hidden;
                gridTasksByEventID.Columns[5].Visibility = Visibility.Hidden;
                gridTasksByEventID.Columns[6].Visibility = Visibility.Hidden;

            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading Task List", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnViewTask_Click(object sender, RoutedEventArgs e)
        {
            if(gridTasksByEventID.SelectedIndex != -1)
            {
                int taskID = ((DataDomain.Task)gridTasksByEventID.SelectedItem).TaskID;
                this.NavigationService.Navigate(new viewSingleTask(taskID));
            }
            else
            {
                MessageBox.Show("Please select a task to view.", "Invalid Task", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        



    }
}
