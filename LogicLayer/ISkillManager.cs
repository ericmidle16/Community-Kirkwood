/// <summary>
/// Creator: Josh Nicholson
/// Created: 2025/02/14
/// Summary: Class for the interface of the Skill manager
/// Last Updated By: Josh Nicholson
/// Last Updated: 2025/03/27
/// What was Changed: Compiled to the main project clone
/// </summary>
using DataDomain;

namespace LogicLayer
{
    public interface ISkillManager
    {
        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The manager interface for the GetAllSkills stored procedure.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<Skill> GetAllSkills();

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The manager interface for the GetUserSkillsByUserID stored procedure.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<UserSkill> GetUserSkillsByUserID(int userID);

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The manager interface for the AddUserSkill stored procedure.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddUserSkill(int userID, string skillID);

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The manager interface for the RemoveUserSkill stored procedure.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool RemoveUserSkill(int userID, string skillID);

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The manager interface for the AddSkill stored procedure.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddSkill(Skill skill);
    }
}
