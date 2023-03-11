using System.Security.Cryptography;
using System.Web;

namespace Core.Helper
{
    public  class Helper
    {
        #region Encryption

        public static string ComputeHash(string val)
        {

            if (String.IsNullOrEmpty(val))
                return val;

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            string rdata = Convert.ToBase64String(sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(val)));
            return rdata;

        }

        #endregion


        public static String CreateGenerateKey(System.Int32 length)
        {
            System.Byte[] seedBuffer = new System.Byte[4];
            using (var rngCryptoServiceProvider = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(seedBuffer);
                System.String chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                System.Random random = new System.Random(System.BitConverter.ToInt32(seedBuffer, 0));
                return new System.String(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }
}
