using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting_Services;

namespace UnitTesting_Tests
{
    [TestClass]
    public class RandomServiceTests
    {
        private readonly RandomService _randomService;
        public RandomServiceTests()
        {
            _randomService = new RandomService();
        }

        // naming unit tests example
        // MethodName_StateUnderTest_ExpectedBehavior
        [TestMethod]
        public void Sum_BothNumbersAreValid_PositiveResultNumber() 
        {
            //Arrange
            var number1 = 5;
            var number2 = 10;
            var expectedResult = 15;

            //Act
            var result = _randomService.Sum(number1, number2);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void IsFirstNumberLarger_FirstNumberIsLarger_True() 
        {
            //Arrange
            int number1 = 10;
            int number2 = 5;

            //Act
            var result = _randomService.IsFirstNumberLarger(number1, number2);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
