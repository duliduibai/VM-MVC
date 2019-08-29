using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace VM_MVC
{
    public class CryptoService
    {
        public static RSACryptoServiceProvider RSA;
        public static string PemPublicKey;
        public static string PemPrivateKey;
        public static RSACryptoServiceProvider GenerateKey_SaveInContainer(string containerName)
        {
            CspParameters par = new CspParameters();
            par.KeyContainerName = containerName;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024, par);
            return rsa;
        }

        public static RSACryptoServiceProvider GetKeyFromContainer(string containerName)
        {
            CspParameters par = new CspParameters();
            par.KeyContainerName = containerName;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024, par);
            return rsa;
        }

        public static void DeleteKeyFromContainer(string containerName)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = containerName;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
        }

        public static void RSA_GeneratePEMKey()
        {
            if (RSA != null)
            {
                return;
            }
            RsaKeyPairGenerator g = new RsaKeyPairGenerator();
            g.Init(new KeyGenerationParameters(new SecureRandom(), 1024));
            var pair = g.GenerateKeyPair();
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(pair.Private);
            byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetDerEncoded();
            /////PEM秘钥
            PemPrivateKey = Convert.ToBase64String(serializedPrivateBytes);
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pair.Public);
            byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
            //PEM公钥
            PemPublicKey = Convert.ToBase64String(serializedPublicBytes);

            RsaPrivateCrtKeyParameters privateKey = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(PemPrivateKey));
            RsaKeyParameters publicKey = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(PemPublicKey));
            RSACryptoServiceProvider rcsp = new RSACryptoServiceProvider();
            RSAParameters parms = new RSAParameters();
            //So the thing changed is offcourse the ToByteArrayUnsigned() instead of
            //ToByteArray()
            parms.Modulus = privateKey.Modulus.ToByteArrayUnsigned();
            parms.P = privateKey.P.ToByteArrayUnsigned();
            parms.Q = privateKey.Q.ToByteArrayUnsigned();
            parms.DP = privateKey.DP.ToByteArrayUnsigned();
            parms.DQ = privateKey.DQ.ToByteArrayUnsigned();
            parms.InverseQ = privateKey.QInv.ToByteArrayUnsigned();
            parms.D = privateKey.Exponent.ToByteArrayUnsigned();
            parms.Exponent = privateKey.PublicExponent.ToByteArrayUnsigned();
            //So now this now appears to work.
            rcsp.ImportParameters(parms);
            RSA = rcsp;
            //string privateKeyXmlText = rcsp.ToXmlString(true);//XML秘钥
            //string publicKeyXmlText = rcsp.ToXmlString(false);//XML公钥
            //加密解密
            //string texta1 = "abc", texta2 = "", textb1 = "";
            //byte[] cipherbytes;
            //cipherbytes = rcsp.Encrypt(Encoding.UTF8.GetBytes(texta1), false);
            //texta2 = Convert.ToBase64String(cipherbytes);
            //cipherbytes = rcsp.Decrypt(Convert.FromBase64String(texta2), false);
            //textb1 = Encoding.UTF8.GetString(cipherbytes);
        }

        /// <summary>
        /// 对给定的SourceData做MD5加密
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        public static string MD5_Encrypt(string sourceData)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            var fromData = Encoding.Unicode.GetBytes(sourceData);
            var targetData = mD5.ComputeHash(fromData);
            sourceData = "";
            for (int i = 0; i < targetData.Length; i++)
            {
                sourceData += targetData[i].ToString("x");
            }
            return sourceData;
        }
    }
}