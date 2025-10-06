/// <summary>
/// Josh Nicholson
/// Created: 2025/02/14
/// Summary: Class for the Skill object
/// </summary>

namespace DataDomain
{
    public class Skill
    {
        public string SkillID { get; set; }
        public string Description { get; set; }
    }

    public class UserSkill
    {
        public int UserID { get; set; }
        public string SkillID { get; set; }
    }

    public class SkillVM
    {
        public string SkillID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
