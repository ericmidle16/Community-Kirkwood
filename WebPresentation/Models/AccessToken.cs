using DataDomain;

namespace WebPresentation.Models
{
    public class AccessToken
    {
        DataDomain.User _legacyUser;
        List<string> _roles;
        List<UserProjectRole> _projectRoles;

        public AccessToken(string email)
        {
            if (email == "" || email == null)
            {
                _legacyUser = new DataDomain.User()
                {
                    UserID = 0,
                    Email = "",
                    FamilyName = "",
                    GivenName = "",
                    PhoneNumber = "",
                    City = "",
                    State = "",
                    Suspended = false,
                    ReadOnly = false,
                    Active = false,
                    RestrictionDetails = ""
                };
                return;
            }

            LogicLayer.UserManager userManager = new LogicLayer.UserManager();
            _legacyUser = userManager.RetrieveUserDetailsByEmail(email);
            _roles = userManager.GetRolesForUser(_legacyUser.UserID);
            _projectRoles = userManager.GetProjectRolesByUserID(_legacyUser.UserID);
        }

        public bool IsLoggedIn { get { return _legacyUser.UserID != 0; } }
        public int UserID { get { return _legacyUser.UserID; } }
        public string GivenName { get { return _legacyUser.GivenName; } }
        public string FamilyName { get { return _legacyUser.FamilyName; } }
        public List<String> Roles { get { return _roles; } }
        public List<UserProjectRole> ProjectRoles { get { return _projectRoles; } }

        public bool HasProjectRole(String roleName, int ProjectID)
        {
            foreach (UserProjectRole pr in ProjectRoles)
            {
                if (pr.ProjectRole == roleName && pr.ProjectId == ProjectID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
