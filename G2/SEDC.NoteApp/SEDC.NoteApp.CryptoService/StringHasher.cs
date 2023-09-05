using System.Text;
using XSystem.Security.Cryptography;

namespace SEDC.NoteApp.CryptoService
{
    // XAct.Core.PCL => nuget for hashing
    public static class StringHasher
    {
        public static string Hash(string inputString)
        {
            var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(inputString);
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            return Encoding.ASCII.GetString(hashedBytes);
        }
    }
}
