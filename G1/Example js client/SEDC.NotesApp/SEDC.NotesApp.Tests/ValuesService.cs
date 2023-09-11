using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Tests
{
    public class ValuesService
    {
        public int? Sum(int num1, int num2)
        {
            if (num1 < 0 || num2 < 0)
                return null;
            return num1 + num2;
        }

        public bool IsFirstNumLarger(int num1, int num2)
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
}
