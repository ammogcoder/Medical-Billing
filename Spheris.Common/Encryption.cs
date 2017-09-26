using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

[assembly: CLSCompliant(true)]
namespace Spheris.Common
{
    public static class Encryption
    {
        /// <summary>
        /// Replace this value with some unique key of your own
        /// Best set this in your App start up in a Static constructor
        /// </summary>
        private static string _key = "^&#@*16";

        public static string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// Encodes a stream of bytes using DES encryption with a pass key. Lowest level method that 
        /// handles all work.
        /// </summary>
        /// <param name="value">The value to be encrypted.</param>
        /// <param name="encryptionKey">The key value used to encrypt the value.</param>
        /// <returns>A byte array.</returns>
        public static byte[] EncryptBytes(byte[] value, string encryptionKey)
        {
            if (encryptionKey == null)
                encryptionKey = Key;
            
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

            using (MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider())
            {
                des.Key = hashmd5.ComputeHash(Encoding.ASCII.GetBytes(encryptionKey));
            }
            des.Mode = CipherMode.ECB;

            ICryptoTransform Transform = des.CreateEncryptor();

            byte[] Buffer = value;
            return Transform.TransformFinalBlock(Buffer, 0, Buffer.Length);
        }

        /// <summary>
        /// Encrypts a string into bytes using DES encryption with a Passkey. 
        /// </summary>
        /// <param name="value">The value to be encrypted.</param>
        /// <param name="encryptionKey">The key value used to encrypt the value.</param>
        /// <returns>A byte array.</returns>
        public static byte[] EncryptBytes(string value, string encryptionKey)
        {
            return EncryptBytes(Encoding.ASCII.GetBytes(value), encryptionKey);
        }

        /// <summary>
        /// Encrypts a string using Triple DES encryption with a two way encryption key.String is returned as Base64 encoded value
        /// rather than binary.
        /// </summary>
        /// <param name="value">The value to be encrypted.</param>
        /// <param name="encryptionKey">The key value used to encrypt the value.</param>
        /// <returns>A Base64 encoded value.</returns>
        public static string EncryptString(string value, string encryptionKey)
        {
            return Convert.ToBase64String(EncryptBytes(Encoding.ASCII.GetBytes(value), encryptionKey));
        }

        /// <summary>
        /// Encrypts a string using Triple DES encryption with a two way encryption key.  The default encryption key value will be used.
        /// </summary>
        /// <param name="value">The value to be encrypted.</param>
        /// <returns>A Base64 encoded value.</returns>
        public static string EncryptString(string value)
        {
            return Convert.ToBase64String(EncryptBytes(Encoding.ASCII.GetBytes(value), Encryption.Key));
        }

        /// <summary>
        /// Decrypts a Byte array from DES with an Encryption Key.
        /// </summary>
        /// <param name="DecryptBuffer"></param>
        /// <param name="EncryptionKey"></param>
        /// <returns></returns>
        public static byte[] DecryptBytes(byte[] decryptBuffer, string encryptionKey)
        {
            if (encryptionKey == null)
                encryptionKey = Key;

            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

            using (MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider())
            {
                des.Key = hashmd5.ComputeHash(Encoding.ASCII.GetBytes(encryptionKey));
            }
            des.Mode = CipherMode.ECB;

            ICryptoTransform Transform = des.CreateDecryptor();
            
            return Transform.TransformFinalBlock(decryptBuffer, 0, decryptBuffer.Length);
        }

        public static byte[] DecryptBytes(string encryptedText, string encryptionKey)
        {
            return DecryptBytes(Convert.FromBase64String(encryptedText), encryptionKey);
        }

        /// <summary>
        /// Decrypts a string using DES encryption and a pass key that was used for encryption.
        /// <seealso>Class wwEncrypt</seealso>
        /// </summary>
        /// <param name="DecryptString"></param>
        /// <param name="EncryptionKey"></param>
        /// <returns>String</returns>
        public static string DecryptString(string encryptedText, string encryptionKey)
        {
            return Encoding.ASCII.GetString(DecryptBytes(Convert.FromBase64String(encryptedText), encryptionKey));
        }

        /// <summary>
        /// Decrypts a string using DES encryption and a pass key that was used for encryption.
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public static string DecryptString(string encryptedText)
        {
            return Encoding.ASCII.GetString(DecryptBytes(Convert.FromBase64String(encryptedText), Encryption.Key));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:AvoidTypeNamesInParameters", MessageId = "0#")]
        public static string DecryptConnectionStringPassword(string encryptedConnectionString)
        {
            // Validate parameter.
            if (String.IsNullOrEmpty(encryptedConnectionString))
                throw new ArgumentNullException("encryptedConnectionString");

            string connString = "";
            string[] keyPairs = encryptedConnectionString.Split(';');
            string[] keyValue;

            // Rebuild the connection string.
            for (int ndx = 0; ndx < keyPairs.Length; ndx++)
            {
                // Split the key and value.
                if (!String.IsNullOrEmpty(keyPairs[ndx]))
                {
                    keyValue = keyPairs[ndx].Split('=');

                    // Decrypt the password in the connection string.
                    if (keyValue[0].Equals("password", StringComparison.CurrentCultureIgnoreCase))
                        keyValue[1] = DecryptString(keyValue[1] + "==", Key);

                    // Rebuild the connection string.
                    connString += keyValue[0] + "=" + keyValue[1] + ";";
                }
            }

            // Return the decrypted connection string.
            return (connString);
        }
    }
}
