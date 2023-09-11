namespace Tests
{
    public class ValueService
    {
        public int? Sum(int num1, int num2)
        {
            var result = num1 + num2;
            if (num1 > 0 && num2 > 0 && result < 0) return null;
            return result;
        }
        public bool IsFirstLarger(int num1, int num2)
        {
            return num1 > num2;
        }
        public string GetDigitName(int num)
        {
            List<string> names = new List<string>(){
            "Zero", "One", "Two", "Three", "Four",
            "Five", "Six", "Seven", "Eight", "Nine"};
            return names[num];
        }
    }
    [TestClass]
    public class ValueTests
    {
        private readonly ValueService _valueService;
        public ValueTests()
        {
            _valueService = new ValueService();
        }
        [TestMethod]
        public void Sum_PositiveIntegers_ResultNumber()
        {
            // Arrange
            int num1 = 5;
            int num2 = 10;
            int? expectedResult = 15;
            // Act
            int? result = _valueService.Sum(num1, num2);
            // Assert ( Are Equal - Test will pass if values are equal )
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void Sum_LargeNumberIntegers_Null()
        {
            // Arrange
            int num1 = 2111111111;
            int num2 = 2111111111;
            // Act
            int? result = _valueService.Sum(num1, num2);
            // Assert ( IsNull - Test will pass if the result is null )
            Assert.IsNull(result);
        }
        [TestMethod]
        public void IsFirstLarger_FirstIsLarger_True()
        {
            // Arrange
            int num1 = 199;
            int num2 = 3;
            // Act
            bool result = _valueService.IsFirstLarger(num1, num2);
            // Assert ( IsTrue - Test will pass if the result is true )
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsFirstLarger_FirstIsNotLarger_False()
        {
            // Arrange
            int num1 = 125;
            int num2 = 126;
            // Act
            bool result = _valueService.IsFirstLarger(num1, num2);
            // Assert ( IsFalse - Test will pass if the result is true )
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GetDigitName_SingleDigit_SingleDigitName()
        {
            // Arrange
            int num = 7;
            string expectedResult = "Seven";
            // Act
            string result = _valueService.GetDigitName(num);
            // Assert ( Are Equal - Test will pass if values are equal )
            Assert.AreEqual(expectedResult, result);
        }
        // Expecting Exceptions
        // Expecting with attribute
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetDigitName_DoubleDigit_Exception_Method1()
        {
            // Arrange
            int num = 12;
            // Act / Assert
            _valueService.GetDigitName(num);
        }
        // Expecting with Assert
        [TestMethod]
        public void GetDigitName_DoubleDigit_Exception_Method2()
        {
            // Arrange
            int num = 12;
            // Act / Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _valueService.GetDigitName(num));
        }
    }
}
