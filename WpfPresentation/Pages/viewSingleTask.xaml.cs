/// <summary>
/// Josh Nicholson
/// Created: 2025/03/06
/// Summary: Class for the viewSingleTask xaml 
/// error checking and input to the database
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/07
using System.Windows;
using System.Windows.Controls;
using LogicLayer;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for viewSingleTask.xaml
    /// </summary>
    public partial class viewSingleTask : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        TaskManager _taskManager = new TaskManager();
        DataDomain.Task selectedTask;

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/06
        /// 
        /// Summary: Fills the task data of the task selected previously into the boxes on page call
        /// </summary>
        public viewSingleTask(int taskID)
        {
            InitializeComponent();
            selectedTask = _taskManager.GetTaskByTaskID(taskID);

            if (selectedTask != null)
            {
                txtTaskName.Text = selectedTask.Name;
                txtTaskType.Text = selectedTask.TaskType;
                txtTaskDesc.Text = selectedTask.Description;
                txtTaskDate.Text = selectedTask.TaskDate.ToString("M-d-yyyy");
            }
            else
            {
                MessageBox.Show("Task could not be found", "Database Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // delete
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new createEditTask(selectedTask.TaskID));
        }
    }
}
