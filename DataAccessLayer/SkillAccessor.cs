/// <summary>
/// Creator: Josh Nicholson
/// Created: 2025/02/14
/// Summary: Class for the Skill accessor
/// Last Updated By: Josh Nicholson
/// Last Updated: 2025/03/27
/// What was Changed: Compiled to the main project clone
/// </summary>
using Microsoft.Data.SqlClient;
using System.Data;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessLayer
{
    public class SkillAccessor : ISkillAccessor
    {
        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The accessor for the GetAllSkills stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<Skill> GetAllSkills()
        {
            List<Skill> skills = new List<Skill>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_skills", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Skill s = new Skill();
                    s.SkillID = r.GetString(0);
                    s.Description = r.GetString(1);
                    skills.Add(s);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return skills;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The accessor for the GetUserSkillsByUserID stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<UserSkill> GetUserSkillsByUserID(int userID) {
            List<UserSkill> userSkills = new List<UserSkill>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_user_skills_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    UserSkill s = new UserSkill();
                    s.UserID = r.GetInt32(0);
                    s.SkillID = r.GetString(1);
                    userSkills.Add(s);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return userSkills;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The accessor for the AddUserSkill stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddUserSkill(int userID, string skillID)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_add_userskill", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            cmd.Parameters.Add(new SqlParameter("@SkillID", SqlDbType.VarChar, 50)).Value = skillID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The accessor for the RemoveUserSkill stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool RemoveUserSkill(int userID, string skillID)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_remove_userskill", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            cmd.Parameters.Add(new SqlParameter("@SkillID", SqlDbType.VarChar, 50)).Value = skillID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: The accessor for the AddSkill stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddSkill(Skill skill)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_add_skill", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SkillID", SqlDbType.VarChar, 50)).Value = skill.SkillID;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 250)).Value = skill.Description;

            try
            {
                conn.Open();
                var r = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
