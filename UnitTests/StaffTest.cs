using FMScoutFramework.Core.Entities.InGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class StaffTest
    {
        // Test candidate values
        public readonly int uid = 7842652;
        public readonly string firstName = "Luka";
        public readonly string lastName = "Kostic";
        public readonly string fullName = "Luca Lúkas Kostic";
        public readonly int currentAbility = 88;
        public readonly int potentialAbility = 111;
        public readonly int currentReputation = 88;
        public readonly int homeReputation = 88;
        public readonly int worldReputation = 0;

        private Staff _testStaff;

        public Staff TestStaff
        {
            get
            {
                if (_testStaff == null)
                {
                    _testStaff = GetTestStaff();
                }

                return _testStaff;
            }
        }

        public Staff GetTestStaff()
        {
            var core = TestHelper.GetLoadedCore();
            return core.Staff.FirstOrDefault(x => x.ID == uid);
        }

        [TestMethod]
        public void StaffFirstName()
        {
            Assert.AreEqual(firstName, TestStaff.Firstname);
        }

        [TestMethod]
        public void StaffLastName()
        {
            Assert.AreEqual(lastName, TestStaff.Lastname);
        }

        [TestMethod]
        public void StaffFullName()
        {
            Assert.AreEqual(fullName, TestStaff.Fullname);
        }

        [TestMethod]
        public void StaffCurrentAbility()
        {
            Assert.AreEqual(currentAbility, TestStaff.CurrentAbility);
        }

        [TestMethod]
        public void StaffPotentialAbility()
        {
            Assert.AreEqual(potentialAbility, TestStaff.PotentialAbility);
        }

        [TestMethod]
        public void StaffCurrentReputation()
        {
            Assert.AreEqual(currentReputation, TestStaff.CurrentReputation);
        }

        [TestMethod]
        public void StaffHomeReputation()
        {
            Assert.AreEqual(homeReputation, TestStaff.HomeReputation);
        }

        [TestMethod]
        public void StaffWorldReputation()
        {
            Assert.AreEqual(worldReputation, TestStaff.WorldReputation);
        }
    }
}
