using System;

namespace Hotel_Management_App.UI
{
    internal class ConsoleUI
    {
        public static void AppFunctionsText()
        {
            Console.WriteLine();
            Console.WriteLine("Functions: ");
            Console.WriteLine("1. Add a reservation");
            Console.WriteLine("2. Delete a reservation");
            Console.WriteLine("3. View reservation details");
            Console.WriteLine("4. View room details");
            Console.WriteLine("5. Update a reservation");
            Console.WriteLine("6. Exit");
            Console.Write("What would you like to do?: ");
        }

        public static void AppHeader(string hotelName)
        {
            Console.WriteLine($"*** {hotelName} ***");
            Console.WriteLine("    -----------------------");
            Console.WriteLine();
        }
    }
}
