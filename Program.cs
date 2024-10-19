

namespace Hotel_Management_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hotel.Hotel hotel = new Hotel.Hotel("Casa Sarofin Costinesti", 15);
            hotel.startApplication();
        }
    }
}
