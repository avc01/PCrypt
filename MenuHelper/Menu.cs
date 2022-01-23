using PCrypt.Crypt;
using System;

namespace PCrypt.MenuHelper
{
    public class Menu
    {
        public static bool MenuNavigation() 
        {
            try
            {
                Console.Write($"{Environment.NewLine}Add the folder path(exclude any specific file):");
                var folderPath = Console.ReadLine();

                Console.Write($"{Environment.NewLine}Do you want to encrypt(1) or decrypt(2)?");
                var response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        CryptService.EncryptData(folderPath);
                        break;
                    case "2":
                        CryptService.DecryptData(folderPath);
                        break;
                    default:
                        Console.WriteLine($"{Environment.NewLine}No option selected");
                        break;
                }

                Console.Write($"{Environment.NewLine}Do you want to exit R/yes?");
                return Console.ReadLine() == "yes";
            }
            catch (Exception e)
            {
                Console.Write($"{Environment.NewLine}Error: {e.Message} - please try again{Environment.NewLine}");
                return false;
            }
        }
    }
}
