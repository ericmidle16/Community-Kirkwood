/// <summary>
/// Josh Nicholson
/// Created: 2025/02/20
/// Summary: Class for the createEditTask xaml 
/// error checking and input to the database
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/07
/// What was Changed: Updated UI
/// </summary>
using System.Windows;
using System.Windows.Controls;
using LogicLayer;
using DataDomain;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class createEditTask : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        TaskManager _taskManager = new TaskManager();

        DataDomain.Task _selectedTask;
        int _eventID;
        int _projectID;
        bool _isEdit;

        // creating a new task
        public createEditTask(int eventID, int projectID)
        {
            InitializeComponent();
            populateTaskTypes();

            _isEdit = false;
            _projectID = projectID;
            _eventID = eventID;
        }

        // editing a task
        public createEditTask(int taskID) {
            InitializeComponent();
            populateTaskTypes();

            _selectedTask = _taskManager.GetTaskByTaskID(taskID);
            _isEdit = true;

            txtTaskName.Text = _selectedTask.Name;
            cmbTaskType.SelectedItem = _selectedTask.TaskType;
            txtTaskDesc.Text = _selectedTask.Description;
            dtpkTaskDate.SelectedDate = _selectedTask.TaskDate;
            chbTaskActive.IsChecked = _selectedTask.Active;
        }

        public void populateTaskTypes() {
            try
            {
                List<TaskTypeObject> taskType = _taskManager.GetAllTaskTypes();
                foreach (TaskTypeObject tasktype in taskType)
                {
                    cmbTaskType.Items.Add(tasktype.TaskType);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to obtain Task types.", "Procedure Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// 
        /// Summary: Saves the task to the database if the inputs are valid,
        /// and one with the entered task name doesn't already exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTaskSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text) || txtTaskName.Text.Length > 100)
            {
                MessageBox.Show("Please enter a valid name.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTaskName.Focus();
                txtTaskName.SelectAll();
                return;
            }

            if (txtTaskDesc.Text.Length > 250)
            {
                MessageBox.Show("Please enter a valid description.", "Too Many Characters", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTaskDesc.Focus();
                txtTaskDesc.SelectAll();
                return;
            }

            if (string.IsNullOrEmpty(cmbTaskType.Text))
            {
                MessageBox.Show("Please select a valid type.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTaskName.Focus();
                txtTaskName.SelectAll();
                return;
            }

            if (!dtpkTaskDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please enter a valid date", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                dtpkTaskDate.Focus();
                return;
            }

            DataDomain.Task newTask = new DataDomain.Task()
            {
                Name = txtTaskName.Text,
                Description = txtTaskDesc.Text,
                TaskDate = (DateTime)dtpkTaskDate.SelectedDate,
                TaskType = (string)cmbTaskType.SelectedValue,
                Active = (bool)chbTaskActive.IsChecked
            };

            if (_isEdit) 
            {
                newTask.TaskID = _selectedTask.TaskID;

                try 
                {
                    _taskManager.UpdateTaskByTaskID(newTask);
                    MessageBox.Show("Task was updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("This task name is taken.", "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            } 
            else
            {
                newTask.ProjectID = _projectID;
                if (_eventID == 0)
                {
                    // newTask.EventID = null;
                }
                else
                {
                    newTask.EventID = _eventID;
                }


                try
                {
                    _taskManager.AddTask(newTask);
                    MessageBox.Show("Task was created successfully.", "Task Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("This task name is taken.", "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnTaskCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.GoBack();
        }
    }
}
