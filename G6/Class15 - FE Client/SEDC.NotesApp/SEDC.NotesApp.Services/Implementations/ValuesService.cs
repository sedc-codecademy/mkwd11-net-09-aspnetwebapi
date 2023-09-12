using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Services.Implementations
{
    public class ValuesService
    {
        public int? SumPositiveNumbers(int num1, int num2)
        {
            if(num1 < 0 || num2 < 0)
            {
                return null;
            }

            return num1 + num2;
        }

        public bool IsFirstNumLarger(int num1, int num2)
        {
            return num1 > num2;
        }

        public string GetNumberName(int num)
        {
            List<string> names = new List<string>(){
            "Zero", "One", "Two", "Three", "Four",
            "Five", "Six", "Seven", "Eight", "Nine"};

            return names[num];
        }
    }
}
