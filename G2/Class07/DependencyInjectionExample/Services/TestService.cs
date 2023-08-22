using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TestService : ITestService
    {
        public int TestMethodOne(int input) 
        {
            return input;
        }

        public string TestMethodTwo(string input)
        {
            return input;
        }
    }
}
