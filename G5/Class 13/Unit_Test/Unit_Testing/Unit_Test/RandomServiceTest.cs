using Services;

namespace Unit_Test
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
        public void Sum_BothNumbersAreValid_ResultNumber()
        {
            //Arrange
            int num1 = 10;
            int num2 = 20;
            int? expectedResult = 30;

            //Act
            int? result = _randomService.Sum(num1, num2);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Sum_LargeNumberIntegers_NUll()
        {
            //Arrange
            int num1 = 2111111111;
            int num2 = 2111111111;

            //Act
            int? result = _randomService.Sum(num1, num2);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void isFirstNumberLarger_FirstNumberIsLarger_True()
        {
            //Arrange
            int num1 = 10;
            int num2 = 8;

            //Act
            var result = _randomService.isFirstNumberLarger(num1, num2);

           //Assert
           Assert.IsTrue(result);
        }

        [TestMethod]
        public void isFirstNumberLarger_SecondNumberIsLarger_False()
        {
            //Arrange
            int num1 = 10;
            int num2 = 18;

            //Act
            var result = _randomService.isFirstNumberLarger(num1, num2);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetDigitName_NumberName_ReturnTwo()
        {
            //Arrange
            int num = 2;

            //Act
            string result = _randomService.GetDigitName(num);

            //Assert
            Assert.AreEqual("Two", result);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetDigitName_OutOfRange_Exception()
        {
            //Arrange
            int num = 5;

            //Act
            string result = _randomService.GetDigitName(num);
        }

        [TestMethod]
        public void GetDigitName_OutOfRange_ArgumentException()
        {
            //Arrange
            int num = 5;

            //Act && Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=>_randomService.GetDigitName(num));
        }
    }
}
