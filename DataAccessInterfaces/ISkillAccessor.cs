/// <summary>
/// Josh Nicholson
/// Created: 2025/02/14
/// Summary: Class for the interface of the Skill accessor
/// Last Updated By: Josh Nicholson
/// Last Updated: 2025/03/27
/// What was Changed: Compiled to the main project clone
/// </summary>
using DataDomain;

namespace DataAccessInterfaces
{
    public interface ISkillAccessor
    {
        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the GetAllSkills stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        List<Skill> GetAllSkills();

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the GetUserSkillsByUserID stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        List<UserSkill> GetUserSkillsByUserID(int userID);

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the AddUserSkill stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        bool AddUserSkill(int userID, string skillID);

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the RemoveUserSkill stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        bool RemoveUserSkill(int userID, string skillID);

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the AddSkill stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        bool AddSkill(Skill skill);
    }
}
