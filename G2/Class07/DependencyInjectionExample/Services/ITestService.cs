using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITestService
    {
        int TestMethodOne(int input);
        string TestMethodTwo(string input);
    }
}
