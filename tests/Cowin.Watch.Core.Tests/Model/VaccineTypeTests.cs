using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cowin.Watch.Core
{
    [TestClass()]
    public class VaccineTypeTests
    {
        [TestMethod()]
        public void When_CovidShield_Is_Generated_String_Is_Covishield_Test()
        {
            var expected = "covishield";
            var actual = VaccineType.CovidShield().ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void When_Covaxin_Is_Generated_String_Is_Covaxin_Test()
        {
            var expected = "covaxin";
            var actual = VaccineType.Covaxin().ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WhenGeneratedFromInvalid_Then_ArgumentExceptionIsThrown()
        {
            var invalidVaccineType = String.Empty;

            Action act = () => VaccineType.From(invalidVaccineType);

            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod()]
        public void When_Generated_From_covishield_object_equals_CovidShield_Test()
        {
            var expected = VaccineType.CovidShield();
            var actual = VaccineType.From("covishield");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void When_Generated_From_covaxin_object_equals_Covaxin_Test()
        {
            var expected = VaccineType.Covaxin();
            var actual = VaccineType.From("covaxin");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void When_CovidShield_Is_Generated_IsCovidShield_Is_True_Test()
        {
            var vaccine = VaccineType.CovidShield();

            Assert.IsTrue(vaccine.IsCovidShield());
        }

        [TestMethod()]
        public void When_Covaxin_Is_Generated_IsCovaxin_Is_True_Test()
        {
            var vaccine = VaccineType.Covaxin();

            Assert.IsTrue(vaccine.IsCovaxin());
        }

        [TestMethod()]
        public void When_CovidShield_Is_Generated_IsCovaxin_Is_False_Test()
        {
            var vaccine = VaccineType.CovidShield();

            Assert.IsFalse(vaccine.IsCovaxin());
        }

        [TestMethod()]
        public void When_Covaxin_Is_Generated_IsCovidshield_Is_True_Test()
        {
            var vaccine = VaccineType.Covaxin();

            Assert.IsFalse(vaccine.IsCovidShield());
        }

        [TestMethod()]
        public void When_String_Is_CovidShield_IsCovidShield_Is_True_Test()
        {
            string vaccineStr = "covishield";

            Assert.IsTrue(VaccineType.IsCovidShield(vaccineStr));
        }

        [TestMethod()]
        public void When_String_Is_Covaxin_IsCovaxin_Is_True_Test()
        {
            string vaccineStr = "covaxin";

            Assert.IsTrue(VaccineType.IsCovaxin(vaccineStr));
        }

        [TestMethod()]
        public void When_String_Is_Not_CovidShield_IsCovidShield_Is_True_Test()
        {
            string vaccineStr = "RandomStr";

            Assert.IsFalse(VaccineType.IsCovidShield(vaccineStr));
        }

        [TestMethod()]
        public void When_String_Is_Not_Covaxin_IsCovaxin_Is_True_Test()
        {
            string vaccineStr = "RandomStr";

            Assert.IsFalse(VaccineType.IsCovaxin(vaccineStr));
        }

        [TestMethod()]
        public void When_CovidShield_Is_Generated_It_Equals_covishield_str_Test()
        {
            var vaccineStr = "covishield";
            var vaccine = VaccineType.CovidShield();

            Assert.IsTrue(vaccine.Equals(vaccineStr));
        }

        [TestMethod()]
        public void When_Covaxin_Is_Generated_It_Equals_covaxin_str_Test()
        {
            var vaccineStr = "covaxin";
            var vaccine = VaccineType.Covaxin();

            Assert.IsTrue(vaccine.Equals(vaccineStr));
        }

        [TestMethod()]
        public void When_CovidShield_Is_Generated_It__does_not_Equals_other_str_Test()
        {
            var vaccineStr = "covaxin";
            var vaccine = VaccineType.CovidShield();

            Assert.IsFalse(vaccine.Equals(vaccineStr));
        }

        [TestMethod()]
        public void When_Covaxin_Is_Generated_It_does_not_Equals_other_str_Test()
        {
            var vaccineStr = "covishield";
            var vaccine = VaccineType.Covaxin();

            Assert.IsFalse(vaccine.Equals(vaccineStr));
        }
    }
}