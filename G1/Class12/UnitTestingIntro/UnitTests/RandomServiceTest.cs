using Services;

namespace UnitTests
{
    [TestClass]
    public class RandomServiceTest
    {
        private readonly RandomService _randomService;

        public RandomServiceTest()
        {
            _randomService = new RandomService();
        }

        [TestMethod]
        public void Sum_BothNumbersAreValid_PositiveResultNumber()
        {
            // Arrange
            int num1 = 10;
            int num2 = 20;
            int? expectedResult = 30;

            // Act
            int? result = _randomService.Sum(num1, num2);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Sum_LargeNumberIntegers_Null()
        {
            // Arrange
            int num1 = -1;
            int num2 = 100;

            // Act
            int? result = _randomService.Sum(num1, num2);

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void IsFirstNumberLarger_FirstNumberIsLarger_True()
        {
            // Arrange
            int num1 = 100;
            int num2 = 10;

            // Act
            bool result = _randomService.IsFirstNumberLarger(num1, num2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFirstNumberLarger_FirstNumberIsLarger_false()
        {
            // Arrange
            int num1 = 10;
            int num2 = 100;

            // Act
            bool result = _randomService.IsFirstNumberLarger(num1, num2);

            // Assert
            Assert.IsFalse(result);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetDigitName_DoubleDigit_Exception()
        {
            // Arrange
            int num = 10;

            // Assert
            string result = _randomService.GetDigitName(num);
        }

        [TestMethod]
        public void GetDigitName_DoubleDigit_Exception_vol2()
        {
            // Arrange
            int num = 10;

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _randomService.GetDigitName(num));
        }
    }
}
