/// <summary>
/// Creator:  Jennifer Nicewanner
/// Created:  2025-02-07
/// Summary:  Summary
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// What was Changed: UI changes; changed GetUserByEmail --> RetrieveUserDetailsByEmail so I could add image
/// 
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated: 2025-04-22
/// What was Changed:   Modified the logic for Page_Loaded and btnSave_Click methods to allow an active user to have statuses active, active & suspended,
///     active & read only, or all boxes unchecked to be deactivated (this method is new).  The date picker is enabled and required
///     for suspended or read only status.  The restriction box is enabled, but not required for suspended or read only status.  The 
///     feature is updated only by changes in the checkbox selections.
/// </summary>

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Azure.Core;
using DataDomain;
using LogicLayer;
using WpfPresentation.Pages;


namespace WpfPresentation
{
    public partial class ViewSingleUser : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private User _user = null;
        private UserManager _userManager = new UserManager();
        private string email;

        public ViewSingleUser()
        {
            InitializeComponent();
        }

        public ViewSingleUser(string email)
        {
            this.email = email;
            InitializeComponent();
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnRoles.Visibility = Visibility.Collapsed;
            if (main.HasSystemRole("Admin"))
            {
                btnRoles.Visibility = Visibility.Visible;
            }
        }
        private void CheckAdminPrivilege()
        {
            int userid = _userManager.RetrieveUserDetailsByEmail(email).UserID;
            if (_userManager.GetRolesForUser(userid).Contains("Admin"))
            {
                ckbActive.IsEnabled = false;
                ckbSuspended.IsEnabled = false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dateReactivation.DisplayDateStart = DateTime.Now;

            try
            {
                _user = _userManager.RetrieveUserDetailsByEmail(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            txtEmail.Text = _user.Email.ToString();
            txtPhone.Text = _user.PhoneNumber.ToString();
            txtFamilyName.Text = _user.FamilyName.ToString();
            txtGivenName.Text = _user.GivenName.ToString();
            if (_user.Image != null)
            {
                imgImage.Source = ImageUtils.ConvertByteArrayToBitmapImage(_user.Image);
            }
            dateReactivation.SelectedDate = _user.ReactivationDate;
            ckbActive.IsChecked = _user.Active;
            ckbReadOnly.IsChecked = _user.ReadOnly;
            ckbSuspended.IsChecked = _user.Suspended;
            txtRestrictionDetails.Text = _user.RestrictionDetails.ToString();
            txtBio.Text = _user.Biography.ToString();
            CheckAdminPrivilege();
        }

        private void ckbActive_Checked(object sender, RoutedEventArgs e)
        {
            ckbReadOnly.IsEnabled = true;
            ckbSuspended.IsEnabled = true;

            if ((ckbActive.IsChecked == true) && (ckbReadOnly.IsChecked == false) && (ckbSuspended.IsChecked == false))
            {
                dateReactivation.IsEnabled = false;
                txtRestrictionDetails.IsEnabled = false;
            }
        }

        private void ckbReadOnly_Checked(object sender, RoutedEventArgs e)
        {
            ckbActive.IsEnabled = true;
            ckbActive.IsChecked = true;
            ckbSuspended.IsEnabled = false;
            ckbSuspended.IsChecked = false;
            dateReactivation.IsEnabled = true;
            txtRestrictionDetails.IsEnabled = true;
        }

        private void ckbReadOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            ckbActive.IsChecked = true;
            ckbSuspended.IsEnabled = true;
            dateReactivation.SelectedDate = null;
            dateReactivation.IsEnabled = false;
            txtRestrictionDetails.IsEnabled = false;
        }

        private void ckbSuspended_Checked(object sender, RoutedEventArgs e)
        {
            ckbReadOnly.IsEnabled = false;
            ckbReadOnly.IsChecked = false;
            ckbActive.IsEnabled = true;
            ckbActive.IsChecked = true;
            dateReactivation.IsEnabled = true;
            txtRestrictionDetails.IsEnabled = true;
        }

        private void ckbSuspended_Unchecked(object sender, RoutedEventArgs e)
        {
            ckbReadOnly.IsEnabled = true;
            ckbActive.IsEnabled = true;
            ckbActive.IsChecked = true;
            dateReactivation.SelectedDate = null;
            dateReactivation.IsEnabled = false;
            txtRestrictionDetails.IsEnabled = false;
        }

        private void ckbActive_Unchecked(object sender, RoutedEventArgs e)
        {
            ckbReadOnly.IsChecked = false;
            ckbReadOnly.IsEnabled = false;
            ckbSuspended.IsChecked = false;
            ckbSuspended.IsEnabled = false;
            dateReactivation.IsEnabled = false;
            txtRestrictionDetails.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var result = false;

            try
            {
                if (
                    ckbReadOnly.IsChecked.Value != _user.ReadOnly ||
                    ckbSuspended.IsChecked.Value != _user.Suspended)
                {
                    User editedUser = new User();

                    editedUser.Email = txtEmail.Text;
                    editedUser.PhoneNumber = txtPhone.Text;
                    editedUser.FamilyName = txtFamilyName.Text;
                    editedUser.GivenName = txtGivenName.Text;
                    //imgImage.Source = _user.ImageMimeType.ToString();
                    editedUser.ReactivationDate = dateReactivation.SelectedDate;
                    editedUser.Active = ckbActive.IsChecked.Value;
                    if (ckbReadOnly.IsChecked == true || ckbSuspended.IsChecked == true)
                    {
                        if (editedUser.ReactivationDate == null)
                        {
                            MessageBox.Show("Reactivation Date is Required", "User status not updated.", MessageBoxButton.OK, MessageBoxImage.Information);
                            dateReactivation.Focus();
                            return;
                        }
                    }
                    if (ckbReadOnly.IsChecked == true)
                    {
                        editedUser.ReadOnly = true;
                    }
                    //editedUser.ReadOnly = ckbReadOnly.IsChecked.Value;
                    //editedUser.Suspended = ckbSuspended.IsChecked.Value;
                    if (ckbSuspended.IsChecked == true)
                    {
                        editedUser.Suspended = true;
                    }
                    editedUser.RestrictionDetails = txtRestrictionDetails.Text;
                    editedUser.Biography = txtBio.Text;

                    result = _userManager.UpdateUserStatusByID(_user, editedUser);
                }

                if (ckbActive.IsChecked.Value != _user.Active)
                {
                    if (ckbActive.IsChecked.Value == false)
                    {
                        var msgBoxResult = MessageBox.Show("User will be deactivated. Are you sure?", "Deactivate user?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        if (msgBoxResult == MessageBoxResult.Yes)
                        {
                            result = _userManager.DeactivateUserByUserID(_user.UserID);
                        }
                        else
                        {
                            ckbActive.IsChecked = true;
                            return;
                        }
                    }
                    else
                    {
                        ckbActive.IsChecked = true;

                        User editedUser = new User();

                        editedUser.Email = txtEmail.Text;
                        editedUser.PhoneNumber = txtPhone.Text;
                        editedUser.FamilyName = txtFamilyName.Text;
                        editedUser.GivenName = txtGivenName.Text;
                        //imgImage.Source = _user.ImageMimeType.ToString();
                        editedUser.ReactivationDate = dateReactivation.SelectedDate;
                        editedUser.Active = ckbActive.IsChecked.Value;
                        if (ckbReadOnly.IsChecked == true || ckbSuspended.IsChecked == true)
                        {
                            if (editedUser.ReactivationDate == null)
                            {
                                MessageBox.Show("Reactivation Date is Required", "User status not updated.", MessageBoxButton.OK, MessageBoxImage.Information);
                                dateReactivation.Focus();
                                return;
                            }
                        }
                        if (ckbReadOnly.IsChecked == true)
                        {
                            editedUser.ReadOnly = true;
                        }
                        //editedUser.ReadOnly = ckbReadOnly.IsChecked.Value;
                        //editedUser.Suspended = ckbSuspended.IsChecked.Value;
                        if (ckbSuspended.IsChecked == true)
                        {
                            editedUser.Suspended = true;
                        }
                        editedUser.RestrictionDetails = txtRestrictionDetails.Text;
                        editedUser.Biography = txtBio.Text;

                        result = _userManager.UpdateUserStatusByID(_user, editedUser);
                    }
                    //return;                   
                }
            }
            catch (Exception ex)
            {
                string msg = "";
                if (ex.InnerException.Message != null) { msg += ex.InnerException.Message; }
                MessageBox.Show("User status not updated. \n" + ex.Message + "\n" + msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (result)
            {
                //call the look up user by id again 
                _user = _userManager.RetrieveUserDetailsByEmail(email);
                MessageBox.Show("User status updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("User status not updated.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new frmViewAllUsers());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new View_Profile(_user.Email));
        }

        private void btnRoles_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new UpdateUserSystemRole(_user.UserID));
        }
    }
}
