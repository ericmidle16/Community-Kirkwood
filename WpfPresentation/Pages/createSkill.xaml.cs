/// <summary>
/// Josh Nicholson
/// Created: ?
/// Summary: Class for the createSkills xaml 
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/09
/// Added the ability to choose to keep making skills or be redirected once skill is made successfully
/// </summary>

using System.Windows;
using System.Windows.Controls;
using LogicLayer;
using DataDomain;

namespace WpfPresentation.Pages
{
    public partial class createSkill : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        SkillManager _skillManager = new SkillManager();
        int id = 0;

        public createSkill()
        {
            InitializeComponent();
        }

        public createSkill(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void btnSkillSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSkillName.Text) || txtSkillName.Text.Length > 50)
            {
                MessageBox.Show("Please enter a valid name.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtSkillName.Focus();
                txtSkillName.SelectAll();
                return;
            }

            if (txtSkillDesc.Text.Length > 250)
            {
                MessageBox.Show("Please enter a valid description.", "Too Many Characters", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtSkillDesc.Focus();
                txtSkillDesc.SelectAll();
                return;
            }

            Skill newSkill = new Skill()
            {
                SkillID = txtSkillName.Text,
                Description = txtSkillDesc.Text
            };

            try
            {
                _skillManager.AddSkill(newSkill);
                MessageBoxResult result = MessageBox.Show("Skill was added successfully.\n\nAre you done making skills?", "Skill Added", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (id != 0)
                    {
                        NavigationService.Navigate(new updateSkills(id));
                    }
                    else
                    {
                        NavigationService.GoBack();
                    }
                }
                else
                {
                    txtSkillName.Text = "";
                    txtSkillDesc.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This skill name is already taken.", "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSkillCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
