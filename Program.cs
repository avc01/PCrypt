using PCrypt.MenuHelper;

namespace PCrypt
{
    public class Program
    {
        private static bool isEnding = false;

        static void Main(string[] args)
        {
            do
            {
                isEnding = Menu.MenuNavigation();

            } while (!isEnding);
        }
    }
}
