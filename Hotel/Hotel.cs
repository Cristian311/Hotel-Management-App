using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_Management_App.Rezervari;
using Hotel_Management_App.Rooms;
using Hotel_Management_App.UI;

namespace Hotel_Management_App.Hotel
{
    internal class Hotel
    {
        private string HotelName;
        private static bool appIsRunning = true;
        private int numberOfRooms {  get; set; }
        private int numberOfGuests {  get; set; }
        private const int numberOfDaysShown = 7;

        private DateTime today = DateTime.Today;


        private List<Rezervare> rezervari = new List<Rezervare>();
        private List<Rooms.Camere> camere = new List<Rooms.Camere>();
        private Dictionary<Rezervare, List<Rooms.Camere>> perechiRezervariCamere = new Dictionary<Rezervare, List<Rooms.Camere>>();

        private ManagerRezervari managerRezervari;
        private ManagerCamere managerCamere;

        public Hotel(string HotelName, int numberOfRooms)
        {
            this.HotelName = HotelName;
            this.numberOfRooms = numberOfRooms;
            managerCamere = new ManagerCamere(camere);
            managerCamere.initCamere(numberOfRooms);

            managerRezervari = new ManagerRezervari(rezervari, camere, perechiRezervariCamere);
            managerRezervari.initRezervari();
        }
        
        public void startApplication()
        {
            while(appIsRunning)
            {
                
                ConsoleUI.appHeader(HotelName);
                viewDates(numberOfDaysShown);
                veziCamereSiRezervari(numberOfDaysShown);
                ConsoleUI.appFunctionsText();

                Console.ReadKey();
            }
        }
        
        private void getAction()
        {
            string input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    AddReservation();
                    break;

            }
        }

        private void AddReservation()
        {

        }
        private void viewDates(int numberOfDaysShown)
        {
            Console.Write("                          | ");
            for (DateTime date = DateTime.Today; date <= DateTime.Today.AddDays(numberOfDaysShown); date = date.AddDays(1))
            {
                Console.Write(date.ToString("dd/MM") + " | "); // You can format the date as you wish
            }
            Console.WriteLine();
        }
        private void veziCamereSiRezervari(int numberOfDaysShown)
        {
            DateTime astazi = DateTime.Today;
            for (int i = 1; i <= numberOfRooms; i++)
            {
                Console.Write($"Camera {i} {camere[i - 1]._name}| ");
                
                for (DateTime date = DateTime.Today; date <= DateTime.Today.AddDays(numberOfDaysShown); date = date.AddDays(1))
                {
                    foreach (var r in perechiRezervariCamere)
                    {
                        if (ManagerRezervari.dateInBetween(date, r.Key.startingDate, r.Key.endingDate) && r.Key.camere.Contains(camere[i-1]))
                        {
                            //Console.Write($"{date.ToString("dd/MM")} {r.Key.GuestName} | {camere[i-1]._name}");
                            Console.Write(r.Key.GuestName + " |");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
