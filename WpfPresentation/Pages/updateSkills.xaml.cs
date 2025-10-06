/// <summary>
/// Josh Nicholson
/// Created: 2025/02/14
/// Summary: Class for the updateSkills xaml 
/// error checking and input to the database
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/10
/// </summary>
using System.Windows;
using System.Windows.Controls;
using LogicLayer;
using DataDomain;

namespace WpfPresentation.Pages
{
    public partial class updateSkills : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        SkillManager _skillManager = new SkillManager();
        UserManager _userManager = new UserManager();
        int _UserID;

        public updateSkills(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
            RefreshSkillList();

            btnUserSkillAdd.IsEnabled = false;
            btnUserSkillRemove.IsEnabled = false;

            validatePermission();
        }

        private void validatePermission()
        {
            btnUserSkillAdd.Visibility = Visibility.Hidden;
            btnUserSkillRemove.Visibility = Visibility.Hidden;
            btnCreateSkill.Visibility = Visibility.Hidden;

            if (main.isLoggedIn && main.UserID == _UserID)
            {
                btnUserSkillAdd.Visibility = Visibility.Visible;
                btnUserSkillRemove.Visibility = Visibility.Visible;
            }
            if (main.SystemRoles.Contains("Admin"))
            {
                btnCreateSkill.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/14
        /// 
        /// Summary: Updates the users skills in the database to all selected 
        /// on the page when the save button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserSkillAdd_Click(object sender, RoutedEventArgs e)
        {
            // Get all skills currently displayed in the DataGrid
            List<Skill> inactiveSkills = grdInactiveSkillList.ItemsSource as List<Skill>;
            List<Skill> selectedSkills = grdInactiveSkillList.SelectedItems.Cast<Skill>().ToList();

            if (inactiveSkills == null || selectedSkills.Count == 0)
            {
                MessageBox.Show("No skills selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            foreach (Skill skill in selectedSkills) 
            {
                try
                {
                    _skillManager.AddUserSkill(_UserID, skill.SkillID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add skill!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            };

            btnUserSkillAdd.IsEnabled = false;
            RefreshSkillList();
        }

        private void btnUserSkillRemove_Click(object sender, RoutedEventArgs e)
        {
            // Get all skills currently displayed in the DataGrid
            List<Skill> activeSkills = grdActiveSkillList.ItemsSource as List<Skill>;
            List<Skill> selectedSkills = grdActiveSkillList.SelectedItems.Cast<Skill>().ToList();

            if (activeSkills == null || selectedSkills.Count == 0)
            {
                MessageBox.Show("No skills selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (Skill skill in selectedSkills)
            {
                try
                {
                    _skillManager.RemoveUserSkill(_UserID, skill.SkillID);
                }
                catch (Exception)
                {
                    throw;
                }
            };

            btnUserSkillRemove.IsEnabled = false;
            RefreshSkillList();
        }

        private void RefreshSkillList()
        {
            try
            {
                List<Skill> allSkills = _skillManager.GetAllSkills();
                List<UserSkill> currentSkills = _skillManager.GetUserSkillsByUserID(_UserID);

                List<Skill> activeSkills = allSkills.Where(skill => currentSkills.Any(userSkill => userSkill.SkillID == skill.SkillID)).ToList();
                List<Skill> inactiveSkills = allSkills.Except(activeSkills).ToList();

                grdInactiveSkillList.ItemsSource = inactiveSkills;
                grdActiveSkillList.ItemsSource = activeSkills;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing skills: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void grdInactiveSkillList_GotFocus(object sender, RoutedEventArgs e)
        {
            btnUserSkillAdd.IsEnabled = true;
            btnUserSkillRemove.IsEnabled = false;

            grdActiveSkillList.UnselectAll();
        }

        private void grdActiveSkillList_GotFocus(object sender, RoutedEventArgs e)
        {
            btnUserSkillRemove.IsEnabled = true;
            btnUserSkillAdd.IsEnabled = false;

            grdInactiveSkillList.UnselectAll();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnCreateSkill_Click(object sender, RoutedEventArgs e)
        {
            if(main.SystemRoles.Contains("Admin"))
            { 
                NavigationService.Navigate(new createSkill(_UserID));
            }
        }

        
    }
}