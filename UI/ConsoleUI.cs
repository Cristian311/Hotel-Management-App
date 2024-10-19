using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_App.UI
{
    internal class ConsoleUI
    {
        public static void appFunctionsText()
        {
            Console.WriteLine();
            Console.WriteLine("Funcii: ");
            Console.WriteLine("1. Adauga o rezervare");
            Console.WriteLine("2. Sterge o rezervare");
            Console.WriteLine("3. Vezi detalii la o rezervare");
            Console.WriteLine("4. Vezi detalii la o camera");
            Console.WriteLine("5. Actualizeaza o rezervare");
            Console.WriteLine("6. Iesi");
            Console.Write("Ce vrei sa faci?: ");
        }
        public static void appHeader(string HotelName)
        {
            Console.WriteLine($"*** {HotelName} ***");
            Console.WriteLine("    -----------------------");
            Console.WriteLine();
        }

    }
}
