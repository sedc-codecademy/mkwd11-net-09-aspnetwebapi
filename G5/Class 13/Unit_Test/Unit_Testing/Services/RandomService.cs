using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RandomService
    {
        public int? Sum(int num1, int num2)
        {
            var res = num1 + num2;
            if (num1 > 0 && num2 > 0 && res < 0) return null;

            return res;
        }

        public bool isFirstNumberLarger(int num1, int num2)
        {
            return num1 > num2;
        }

        public string GetDigitName(int num)
        {
            List<string> digitNames = new List<string>()
            {
                "Zero","One","Two","Three","Four"
            };

            return digitNames[num];
        }
    }
}
