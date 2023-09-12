using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SEDC.NotesApp.Tests
{
    [TestClass]
    public class ValuesServiceTests
    {

        private readonly ValuesService _valuesService;
        public ValuesServiceTests()
        {
            _valuesService = new ValuesService();
        }

        [TestMethod]
        public void Sum_should_return_result_for_positive_nums()
        {
            //Arrange
            int num1 = 1;
            int num2 = 5;
            int? expectedResult = 6;

            //Act
            int? result = _valuesService.Sum(num1, num2);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Sum_should_return_null_on_negative_input()
        {
            //Arrange
            int num1 = -2;
            int num2 = 5;

            //Act
            int? result = _valuesService.Sum(num1, num2);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IsFirstLarger_should_return_true()
        {
            int n1 = 10;
            int n2 = 3;

            bool result = _valuesService.IsFirstNumLarger(n1, n2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFirstLarger_should_return_false()
        {
            int n2 = 10;
            int n1 = 3;

            bool result = _valuesService.IsFirstNumLarger(n1, n2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetDigitName_should_return_value()
        {
            //Arrange
            int num = 5;
            string expectedResult = "Five";

            //Act
            string result = _valuesService.GetDigitName(num);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetDigitName_should_throw_ArgumentOutOfRangeException()
        {
            int num = 13;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _valuesService.GetDigitName(num));
            //Assert.ThrowsException<Exception>(() => _valuesService.GetDigitName(num)); --> FAILS
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetDigitName_is_expected_to_throw_ArgumentOutOfRangeException()
        {
            int num = 13;
            _valuesService.GetDigitName(num);
        }
    }
}
