using System;
using System.IO;
using System.Text;

namespace PCrypt.DataFileHelper
{
    public class DataFile
    {
        public static string GetFile(string folderPath) 
        {
            Console.Write($"{Environment.NewLine}Add the file name(including extension):");

            var fileName = Console.ReadLine();

            return File.ReadAllText(Path.Combine(folderPath, fileName));
        }

        public static void SaveEncryptedFile(string fileData, string folderPath) 
        {
            var fileName = $"encrypted_{DateTime.Now:yyyy-MM-dd}.txt";

            var filePath = Path.Combine(folderPath, fileName);

            using (FileStream fileStream = File.Create(filePath))
            {
                var data = new UTF8Encoding(true).GetBytes(fileData);

                fileStream.Write(data, 0, data.Length);
            };
        }

        public static void GenerateDecryptedFile(string fileData, string folderPath)
        {
            var fileName = $"decrypted_{DateTime.Now:yyyy-MM-dd}.txt";

            var filePath = Path.Combine(folderPath, fileName);

            using (FileStream fileStream = File.Create(filePath))
            {
                var data = new UTF8Encoding(true).GetBytes(fileData);

                fileStream.Write(data, 0, data.Length);
            };
        }
    }
}
