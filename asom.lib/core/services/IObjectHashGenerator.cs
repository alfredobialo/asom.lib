using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace asom.lib.core.services
{
    public interface IObjectHashGenerator
    {
        string GenerateHash(object o);
    }

    public class ObjectHashGenerator : IObjectHashGenerator
    {
        public string GenerateHash(object o)
        {
            return _generateHashFor(o);
        }

        private string _generateHashFor(object o)
        {
            if (o == null) throw new ArgumentException("Generate Hash for null object not allowed");
            var serializedObj = JsonConvert.SerializeObject(o);
            var base64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(serializedObj));

            return _createMD5Hash(base64);
        }
        public string _createMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
