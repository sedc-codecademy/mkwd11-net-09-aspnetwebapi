using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TestServiceUpgraded : ITestService
    {
        public int TestMethodOne(int input)
        {
            return input * 10;
        }

        public string TestMethodTwo(string input)
        {
            return input + " upgrade";
        }
    }
}
