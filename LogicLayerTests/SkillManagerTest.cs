/// <summary>
/// Josh Nicholson
/// Created: 2025/02/14
/// Summary: Class for testing SkillManager methods
/// with the fake data from the SkillAccessorFakes
/// </summary>
using LogicLayer;
using DataAccessFakes;
using DataDomain;

namespace LogicLayerTests
{
    [TestClass]
    public class SkillManagerTest
    {
        private ISkillManager _skillManager;

        [TestInitialize]
        public void TestSetup()
        {
            _skillManager = new SkillManager(new SkillAccessorFake());
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: TestMethod to test that the AddUserSkill 
        /// method will create a UserSkill as intended
        /// </summary>
        [TestMethod]
        public void TestUserSkillCreation_ShouldAddUserSkillToList()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;
            int userID = 100000;
            string skillID = "Skill 1";

            // Act
            actualResult = _skillManager.AddUserSkill(userID, skillID);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: TestMethod to test that the AddUserSkill 
        /// method will not create a UserSkill with invalid data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserSkillCreation_ShouldntAddUserSkillToList_InvalidSkillID()
        {
            // Arrange
            int userID = 100000;
            string skillID = "Invalid Skill";

            // Act
            _skillManager.AddUserSkill(userID, skillID);

            // Assert
            // redundent due to ExpectedException
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: TestMethod to test that the GetAllSkills 
        /// method pulls all of the correct data being requested
        /// </summary>
        [TestMethod]
        public void TestGetAllSkills_ShouldContainThreeSkills()
        {
            // Arrange
            int expectedCount = 3;
            int actualCount = 0;

            // Act
            actualCount = _skillManager.GetAllSkills().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: TestMethod to test that the GetAllSkills 
        /// method pulls all of the correct data being requested
        /// </summary>
        [TestMethod]
        public void TestRemoveUserSkills_ShouldRemoveUserSkillsByUserIDSkillID()
        {
            // Arrange
            bool actualResult = false;
            bool expectedResult = true;
            int userID = 100000;
            string skillID = "Skill 1";

            // Act
            _skillManager.AddUserSkill(userID, skillID);
            actualResult = _skillManager.RemoveUserSkill(userID, skillID);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/14
        /// Summary: TestMethod to test that the GetAllSkills 
        /// method pulls all of the correct data being requested
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRemoveUserSkills_ShouldntRemoveUserSkill_InvalidUserID()
        {
            // Arrange
            int userID = 100000;
            int invalidUserID = 100001;
            string invalidSkillID = "This is not a skill";
            string skillID = "Skill 1";

            // Act
            _skillManager.AddUserSkill(userID, skillID);
            _skillManager.AddUserSkill(invalidUserID, skillID);
            _skillManager.AddUserSkill(userID, invalidSkillID);
            _skillManager.RemoveUserSkill(invalidUserID, invalidSkillID);
            
            // Assert
            // redundent due to ExpectedException
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/03/13
        /// Summary: TestMethod to test that the createSkill 
        /// method will create a skill as intended
        /// </summary>
        [TestMethod]
        public void TestSkillCreation_ShouldAddSkillToList_WhenValidSkillIsProvided()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;
            var newSkill = new Skill()
            {
                SkillID = "New Skill",
                Description = "This text does not matter but the ID does"
            };

            // Act
            actualResult = _skillManager.AddSkill(newSkill);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/03/13
        /// Summary: TestMethod to test that the createSkill
        /// method will not create a skill with invalid data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSkillCreation_ShouldNotAddSkillToList_WhenInvalidSkillIsProvided()
        {
            // Arrange
            var newSkill = new Skill()
            {
                SkillID = "Skill 1",
                Description = "This text does not matter but the ID does"
            };

            // Act
            _skillManager.AddSkill(newSkill);

            // Assert
            // redundent due to ExpectedException
        }
    }
}
