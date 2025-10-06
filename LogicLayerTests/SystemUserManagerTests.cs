/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/06
/// 
/// Class accessor for fake SystemRole data
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessFakes;
using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class SystemUserManagerTests
    {
        private ISystemRoleManager _systemRoleManager;

        //[TestInitialize]
        //public void TestSetup()
        //{
        //    _systemRoleManager = new SystemRoleManager(new SystemRoleAccessorFake());
        //}


        //[TestMethod]
        //public void TestGetAllSystemRoles()
        //{
        //    int numberOfSystemRoles = 2;
        //    List<SystemRole> systemRoles = _systemRoleManager.GetAllSystemRoles();
        //    Assert.AreEqual(numberOfSystemRoles, systemRoles.Count);
        //}
    }
}
