///<summary>
/// Creator: Dat Tran
/// Created: 2025-02-12
/// Summary: This xaml code displays a data grid that contains the list of volunteers that are unassigned. 
/// Last updated by: Stan Anderson
/// Last updated: 2025/04/08
/// Changes: Added navigation
///</summary>

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
using DataDomain;
using LogicLayer;

namespace WpfPresentation.Pages
{
 
    public partial class viewUnassignedVolunteers : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private int eventID;
        private string eventName;
        private Event _event;
        AvailabilityManager _unassignedVolunteersManager = new AvailabilityManager();
        
        public viewUnassignedVolunteers(Event @event)
        {
            InitializeComponent();
            _event = @event;
            this.eventID = _event.EventID;
            this.eventName = _event.Name;
            GetPrivileges();
        }

        private void GetPrivileges()
        {
            btnAssignVolunteer.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Project Starter", _event.ProjectID) || 
                main.HasProjectRole("Volunteer Director", _event.ProjectID) ||
                main.HasSystemRole("Admin"))
            {
                btnAssignVolunteer.Visibility = Visibility.Visible;
            }
        }


        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            List<UserAvailability> user;
            try
            {
                user = _unassignedVolunteersManager.GetAvailableUsers(true, eventID);
                grdUnassignedVolunteers.ItemsSource = user;


                grdUnassignedVolunteers.Columns[0].Visibility = Visibility.Hidden;
                grdUnassignedVolunteers.Columns[1].Header = "User ID";
                grdUnassignedVolunteers.Columns[2].Header = "First Name";
                grdUnassignedVolunteers.Columns[3].Header = "Last Name";
                grdUnassignedVolunteers.Columns[4].Header = "City";
                grdUnassignedVolunteers.Columns[5].Header = "State";
                grdUnassignedVolunteers.Columns[6].Header = "Available?";

                lblUnassignedVolunteers.Content = "Unassigned Volunteers (" + user.Count + ")";

                tempSolution();
                
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;

                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAssignVolunteer_Click(object sender, RoutedEventArgs e)
        {
            int index = grdUnassignedVolunteers.SelectedIndex;
            if (index != -1)
            {
                User volunteer = (User)grdUnassignedVolunteers.SelectedItem;
                NavigationService.GetNavigationService(this)?.Navigate(new TaskAssignment(_event, volunteer.UserID, volunteer.GivenName + " " + volunteer.FamilyName));

            }
            else
            {
                MessageBox.Show("Please select a volunteer.", "Invalid Volunteer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }





        private void tempSolution()
        {
            TaskManager taskManager = new TaskManager();
            UserManager userManager = new UserManager();
            LogicLayer.EventManager eventManager = new LogicLayer.EventManager();
            int project = eventManager.SelectEventByID(eventID).ProjectID;
            List<User> users = userManager.GetApprovedUserByProjectID(project);
            List<DataDomain.TaskAssignedViewModel> tasks = taskManager.GetVolunteersAndTasksByEventID(eventID);
            List<User> availables = new List<User>();
            foreach (User user in users)
            {
                bool free = true;
                foreach (TaskAssignedViewModel task in tasks)
                {
                    if(task.UserID == user.UserID)
                    {
                        free = false;
                        break;
                    }
                }
                if (free)
                {
                    availables.Add(user);
                }
            }

            grdUnassignedVolunteers.ItemsSource = availables;
            lblUnassignedVolunteers.Content = "Unassigned Volunteers (" + availables.Count + ")";

            grdUnassignedVolunteers.Columns[0].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[1].Header = "Given Name";
            grdUnassignedVolunteers.Columns[2].Header = "Family Name";
            grdUnassignedVolunteers.Columns[3].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[4].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[8].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[9].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[10].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[11].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[12].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[13].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[14].Visibility = Visibility.Hidden;
            grdUnassignedVolunteers.Columns[15].Visibility = Visibility.Hidden;


        }

    }
}
