/// <summary>
/// Josh Nicholson
/// Created: 2025/02/14
/// Summary: Class for accessing and creating 
/// fake data to test the SkillAccessor
/// </summary>
using DataDomain;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class SkillAccessorFake : ISkillAccessor
    {
        private List<Skill> _skills;
        private List<UserSkill> _userskills;

        public SkillAccessorFake()
        {
            _skills = new List<Skill>();
            _userskills = new List<UserSkill>();

            _skills.Add(new Skill()
            { 
                SkillID = "Skill 1",
                Description = "Description 1"
            });
            _skills.Add(new Skill()
            {
                SkillID = "Skill 2",
                Description = "Description 2"
            });
            _skills.Add(new Skill()
            {
                SkillID = "Skill 3",
                Description = "Description 3"
            });
        }

        public bool AddUserSkill(int userID, string skillID)
        {
            int oldCount = _userskills.Count;

            // lambda expression to check that a skill with skillid exists
            Skill skill = _skills.Find(p => p.SkillID == skillID);

            if (skill != null)
            {
                _userskills.Add(new UserSkill()
                {
                    UserID = userID,
                    SkillID = skillID
                });

                return true;
            }
            throw new Exception("Test Failed");
        }

        public bool RemoveUserSkill(int userID, string skillID)
        {
            UserSkill userSkill = _userskills.Find(p => p.UserID == userID && p.SkillID == skillID);
            if (userSkill == null)
            {
                throw new Exception("Test Failed");
            }
            else
            {
                return _userskills.Remove(userSkill);
            }
        }

        public List<Skill> GetAllSkills()
        {
            return _skills;
        }

        public List<UserSkill> GetUserSkillsByUserID(int userID)
        {
            return _userskills;
        }

        public bool AddSkill(Skill newSkill)
        {
            int oldCount = _skills.Count;

            // lambda expression to check if a skill with ID already exists
            Skill skill = _skills.Find(p => p.SkillID == newSkill.SkillID);

            if (skill == null)
            {
                _skills.Add(newSkill);

                if (_skills.Count != oldCount)
                {
                    return true;
                }
            }

            throw new Exception("Test Failed");
        }
    }
}
