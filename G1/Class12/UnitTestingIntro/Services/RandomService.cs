namespace Services
{
    public class RandomService
    {
        public int? Sum(int num1, int num2)
        {
            var result = num1 + num2;
            if (num1 < 0 || num2 < 0 || result < 0) return null;
            return result;
        }

        public bool IsFirstNumberLarger(int num1, int num2)
        {
            return num1 > num2;
        }

        public string GetDigitName(int num)
        {
            List<string> names = new List<string>()
            {
                "Zero","One","Two","Three","Four","Five","Six","Seven","Eight","Nine"
            };

            return names[num];
        }
    }
}
