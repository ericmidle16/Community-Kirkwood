/// <summary>
/// Creator: Josh Nicholson
/// Created: 2025/02/14
/// Summary: Class for the Skill manager
/// Last Updated By: Josh Nicholson
/// Last Updated: 2025/03/27
/// What was Changed: Compiled to the main project clone
/// </summary>
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;

namespace LogicLayer
{
    public class SkillManager : ISkillManager
    {
        private ISkillAccessor _skillAccessor;

        public SkillManager()
        {
            _skillAccessor = new SkillAccessor();
        }

        public SkillManager(ISkillAccessor skillAccessor)
        {
            _skillAccessor = skillAccessor;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: This method gets all of the skills in the database.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<Skill> GetAllSkills()
        {
            List<Skill> skills = null;

            try
            {
                skills = _skillAccessor.GetAllSkills();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return skills;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: This method gets the skills of a user.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<UserSkill> GetUserSkillsByUserID(int userID) 
        {
            List<UserSkill> userSkills = null;

            try
            {
                userSkills = _skillAccessor.GetUserSkillsByUserID(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return userSkills;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: This method adds a skill to a user.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddUserSkill(int userID, string skillID)
        {
            try
            {
                _skillAccessor.AddUserSkill(userID, skillID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return true;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: This method removes a skill from a user.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool RemoveUserSkill(int userID, string skillID)
        {
            try
            {
                _skillAccessor.RemoveUserSkill(userID, skillID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return true;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: This method adds a skill to the database.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddSkill(Skill skill)
        {
            try
            {
                _skillAccessor.AddSkill(skill);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return true;
        }
    }
}
