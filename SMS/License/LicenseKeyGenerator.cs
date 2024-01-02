using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SMS
{
    public class LicenseKeyGenerator
    {

        public static string GenerateLicenseKey(string productIdentifier)
        {   
            return FormatLicenseKey(GetMd5Sum(productIdentifier));
        }
        public static string GetMd5Sum(string productIdentifier)
        {
            System.Text.Encoder enc = System.Text.Encoding.Unicode.GetEncoder();
            byte[] unicodeText = new byte[productIdentifier.Length * 2];
            enc.GetBytes(productIdentifier.ToCharArray(), 0, productIdentifier.Length, unicodeText, 0, true);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string FormatLicenseKey(string productIdentifier)
        {
            productIdentifier = productIdentifier.Substring(0, 28).ToUpper();
            char[] serialArray = productIdentifier.ToCharArray();
            StringBuilder licenseKey = new StringBuilder();

            int j = 0;
            for (int i = 0; i < 16; i++)
            {
                for (j = i; j < 4 + i; j++)
                {
                    licenseKey.Append(serialArray[j]);
                }
                if (j == 16)
                {
                    break;
                }
                else
                {
                    i = (j) - 1;
                    licenseKey.Append("-");
                }
            }
            return licenseKey.ToString();
        }

        public static string GenerateCustomMAC(string productIdentifier)
        {
            return FormatMAC(GetMd5Sum(productIdentifier));
        }
        public static string FormatMAC(string productIdentifier)
        {
            productIdentifier = productIdentifier.Substring(0, 28).ToUpper();
            char[] serialArray = productIdentifier.ToCharArray();
            StringBuilder licenseKey = new StringBuilder();
            
            int j = 0;
            int k = serialArray.Length;
            for (int i = 0; i < 12; i++)
            {
                licenseKey.Append(serialArray[k-(i+1)]);
            }
            return licenseKey.ToString();
        }

    }
}
