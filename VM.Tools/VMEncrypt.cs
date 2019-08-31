using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Security;

namespace VM.Tools
{
    public class VMEncrypt
    {
        public static string CreateRSAKey()
        {
            var RSAProvider = new RSACryptoServiceProvider(1024);
            var pemPublicKey = ExportPublicKeyToPemString(RSAProvider);
            CacheSet(pemPublicKey, RSAProvider.ToXmlString(true), new TimeSpan(0, 10, 0));
            return pemPublicKey;
        }

        public static string DecryptRSA(string encryptData, string privateKey)
        {
            string decryptData = "";
            try
            {
                var provider = new RSACryptoServiceProvider();
                provider.FromXmlString(privateKey);

                byte[] result = provider.Decrypt(Convert.FromBase64String(encryptData), false);
                ASCIIEncoding enc = new ASCIIEncoding();
                decryptData = enc.GetString(result);
            }
            catch (Exception e)
            {
                throw new Exception("RSA解密出错！", e);
            }
            return decryptData;
        }

        public static byte[] HexStringToBytes(string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }
            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }
            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return result;
        }

        private static string ExportPublicKeyToPemString(RSACryptoServiceProvider csp)
        {
            var parameters = csp.ExportParameters(false);
            string base64;
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    innerWriter.Write((byte)0x30); // SEQUENCE
                    EncodeLength(innerWriter, 13);
                    innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
                    var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
                    EncodeLength(innerWriter, rsaEncryptionOid.Length);
                    innerWriter.Write(rsaEncryptionOid);
                    innerWriter.Write((byte)0x05); // NULL
                    EncodeLength(innerWriter, 0);
                    innerWriter.Write((byte)0x03); // BIT STRING
                    using (var bitStringStream = new MemoryStream())
                    {
                        var bitStringWriter = new BinaryWriter(bitStringStream);
                        bitStringWriter.Write((byte)0x00); // # of unused bits
                        bitStringWriter.Write((byte)0x30); // SEQUENCE
                        using (var paramsStream = new MemoryStream())
                        {
                            var paramsWriter = new BinaryWriter(paramsStream);
                            EncodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
                            EncodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
                            var paramsLength = (int)paramsStream.Length;
                            EncodeLength(bitStringWriter, paramsLength);
                            bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                        }

                        var bitStringLength = (int)bitStringStream.Length;
                        EncodeLength(innerWriter, bitStringLength);
                        innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
                    }

                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);
            }

            return base64;
        }

        public static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }

            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }

                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        public static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }

                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }
        
        //private static Object Cache => MemoryCache.Default;
        private static Cache Cache => new Cache();

        public static object CacheGet(string key)
        {
            return Cache[key];
        }

        public static void CacheSet(string key, object data, TimeSpan cacheTime)
        {
            Cache.Add(key, data, null, Cache.NoAbsoluteExpiration
                , cacheTime, CacheItemPriority.Default, null);
        }

        public static void CacheRemove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// 对字符串进行加密（不可逆）
        /// </summary>
        /// <param name="password"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string NoneEncrypt(string password, int format)
        {
            string strResult = "";
            switch (format)
            {
                case 0:
                    strResult = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                    break;
                case 1:
                    strResult = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                    break;
                default:
                    strResult = password;
                    break;
            }
            return strResult;
        }
    }
}
