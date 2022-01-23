using PCrypt.DataFileHelper;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PCrypt.Crypt
{
    public class CryptService
    {
        public static void EncryptData(string folderPath) 
        {
            //Create the cipher
            var cipher = CreateCipherEncryption();
            
            //Create the encryptor, convert to bytes, and encrypt
            var cryptTransform = cipher.CreateEncryptor();
            var plainText = Encoding.UTF8.GetBytes(DataFile.GetFile(true, folderPath)); 
            var cipherText = cryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);

            //Show the IV and secret key
            PrintSecretKeyAndIV(cipher, true);

            //Convert to base64
            var cipherTextString64 = Convert.ToBase64String(cipherText);

            //Save the encrypted file
            DataFile.SaveEncryptedFile(cipherTextString64, folderPath);
        }

        public static void DecryptData(string folderPath) 
        {
            //Get user secret key and iv
            var result = GetSecretKeyAndIVFromUser();

            //Create the cipher
            var cipher = CreateCipherDecryption(result.Item2, result.Item1);

            //Create the decryptor, convert from base64 to bytes, decrypt
            var cryptTransform = cipher.CreateDecryptor();
            var cipherText = Convert.FromBase64String(DataFile.GetFile(false, folderPath));
            var plainText = cryptTransform.TransformFinalBlock(cipherText, 0, cipherText.Length);

            //Generate the decrypted file
            var originalPlainText = Encoding.UTF8.GetString(plainText);
            DataFile.GenerateDecryptedFile(originalPlainText, folderPath);

            //Show the IV and secret key
            PrintSecretKeyAndIV(cipher, false);
        }

        private static Aes CreateCipherEncryption()
        {
            var cipher = Aes.Create();
            
            cipher.Padding = PaddingMode.ISO10126;

            return cipher;
        }

        private static Aes CreateCipherDecryption(string secretIV, string secretKey)
        {
            var cipher = Aes.Create();

            cipher.Padding = PaddingMode.ISO10126;
            cipher.IV = Convert.FromBase64String(secretIV);
            cipher.Key = Convert.FromBase64String(secretKey);

            return cipher;
        }

        private static void PrintSecretKeyAndIV(Aes cipher, bool isEncryption) 
        {
            var secretIV = Convert.ToBase64String(cipher.IV);
            var secretKey = Convert.ToBase64String(cipher.Key);

            Console.WriteLine($"{Environment.NewLine}IV used: {secretIV}");
            Console.WriteLine($"Secret key used: {secretKey}");

            if (isEncryption) 
            {
                Console.WriteLine($"{Environment.NewLine}Encryption Succeed");
            }
            else
            {
                Console.WriteLine($"{Environment.NewLine}Decryption Succeed");
            }
        }

        private static Tuple<string, string> GetSecretKeyAndIVFromUser() 
        {
            Console.Write($"{Environment.NewLine}Secret key for decryption?");
            var responseSecretKey = Console.ReadLine();
            Console.Write($"{Environment.NewLine}IV for decryption?");
            var responseIV = Console.ReadLine();

            return Tuple.Create(responseSecretKey, responseIV);
        }
    }
}
