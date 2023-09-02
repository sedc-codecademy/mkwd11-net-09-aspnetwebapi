using SEDC.NotesApp.Shared.Shared;

namespace SEDC.NotesApp.Shared
{
    public static class ValidationHelper
    {
        public static void ValidateRequiredStringColumn(string value, string field, int maxNumOfChars)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DataException($"{field} is required field");
            }

            if(value.Length > maxNumOfChars)
            {
                throw new DataException($"{field} can not contain more than {maxNumOfChars} characters");
            }
        }

        public static void ValidateStringColumnLength(string value, string field, int maxNumOfChars)
        {
            if (value.Length > maxNumOfChars)
            {
                throw new DataException($"{field} can not contain more than {maxNumOfChars} characters");
            }
        }
    }
}
