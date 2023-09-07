using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Services;

namespace TestingProject.Tests
{
    [TestClass]
    public class RandomServiceTests
    {

        //MethodName_ValuesThatWeWantToTest_ExpectedResult

        [TestMethod]
        public void SumTwoNumbers_TwoValidNumbers_PositiveResult() {
            //Arrange
            int num1 = 5;
            int num2 = 10;
            int expctedResult = 15;
            RandomService randomService = new RandomService();

            //Act
            int? result = randomService.SumTwoNumbers(num1, num2);

            //Assert
            Assert.AreEqual(expctedResult, result);
        }

        [TestMethod]
        [DataRow(5, 10, 15)]
        [DataRow(1, 11, 12)]
        public void SumTwoNumbers_TwoValidNumbers_PositiveResult2(int num1, int num2, int expctedResult)
        {
            //Arrange
            RandomService randomService = new RandomService();

            //Act
            int? result = randomService.SumTwoNumbers(num1, num2);

            //Assert
            Assert.AreEqual(expctedResult, result);
        }

        [TestMethod]
        public void SumTwoNumbers_LargerNumbers_Null()
        {
            //Arrange
            int num1 = 2111111111;
            int num2 = 2111111111;
            RandomService randomService = new RandomService();

            //Act
            var result = randomService.SumTwoNumbers(num1, num2);

            //Assert
            Assert.IsNull(result);
            //Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void isFirstNumberLarger_FirstIsLarger_True()
        {
            //Arrange
            int num1 = 200;
            int num2 = 100;
            RandomService randomService = new RandomService();

            //Act
            var result = randomService.isFirstNumberLarger(num1, num2);

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void isFirstNumberLarger_FirstIsSmaller_False()
        {
            //Arrange
            int num1 = 100;
            int num2 = 200;
            RandomService randomService = new RandomService();

            //Act
            var result = randomService.isFirstNumberLarger(num1, num2);

            //Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void GetDigitName_ValidNumber_CorrectString()
        {
            //Arrange
            int number = 7;
            string expectedResult = "Seven";
            RandomService randomService = new RandomService();

            //Act
            var result = randomService.GetDigitName(number);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(2, "Two")]
        [DataRow(6, "Six")]
        [DataRow(8, "Eight")]
        public void GetDigitName_ValidNumber_CorrectStringDataRow(int num, string expected)
        {
            //Arrange
            RandomService randomService = new RandomService();

            //Act
            var result = randomService.GetDigitName(num);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetDigitName_DoubleDigitNumber_Exception()
        {
            //Arrange
            int number = 10;
            RandomService randomService = new RandomService();

            //Act
            var result = randomService.GetDigitName(number);

            //Assert
        }
      
        [TestMethod]
        public void GetDigitName_DoubleDigitNumber_Exception2()
        {
            //Arrange
            int number = 10;
            RandomService randomService = new RandomService();

            //Act

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => randomService.GetDigitName(number));
        }


    }
}
