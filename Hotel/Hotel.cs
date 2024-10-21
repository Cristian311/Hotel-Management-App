using System;
using System.Collections.Generic;
using Hotel_Management_App.Reservations;// Ensure this matches the updated namespace
using Hotel_Management_App.Rooms;
using Hotel_Management_App.UI;

namespace Hotel_Management_App.Hotel
{
    internal class Hotel
    {
        private string HotelName { get; set; } // Made property
        private static bool appIsRunning = true;
        private int NumberOfRooms { get; set; } // Made property
        private int NumberOfGuests { get; set; } // Made property
        private const int NumberOfDaysShown = 7;

        private DateTime today = DateTime.Today;

        private List<Reservation> reservations = new List<Reservation>(); // Updated to English
        private List<Room> rooms = new List<Room>(); // Updated to English
        private Dictionary<Reservation, List<Room>> reservationRoomPairs = new Dictionary<Reservation, List<Room>>(); // Updated to English

        private ReservationManager managerReservations; // Updated to English
        private RoomManager managerRooms; // Updated to English

        public Hotel(string hotelName, int numberOfRooms)
        {
            HotelName = hotelName; // Use property
            NumberOfRooms = numberOfRooms; // Use property
            managerRooms = new RoomManager(rooms);
            managerRooms.InitializeRooms(numberOfRooms); // Ensure this populates the rooms list

            managerReservations = new ReservationManager(reservations, rooms, reservationRoomPairs);
            managerReservations.InitializeReservations(); // Ensure this initializes reservations correctly
        }

        public void startApplication()
        {
            while (appIsRunning)
            {
                ConsoleUI.AppHeader(HotelName);
                viewDates(NumberOfDaysShown);
                seeRoomsAndReservations(NumberOfDaysShown); // Renamed method for clarity
                ConsoleUI.AppFunctionsText();
                getAction();
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void getAction()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    managerReservations.AddReservationInput(); // Ensure this matches the updated method name
                    break;
                case "2":
                    managerReservations.DeleteReservationInput();
                    break;
                default:
                    Console.WriteLine("Please enter a valid number!");
                    break;
            }
        }

        private void viewDates(int numberOfDaysShown)
        {
            Console.Write("                          | ");
            for (DateTime date = DateTime.Today; date <= DateTime.Today.AddDays(numberOfDaysShown); date = date.AddDays(1))
            {
                Console.Write(date.ToString("dd/MM") + " | ");
            }
            Console.WriteLine();
        }

        private void seeRoomsAndReservations(int numberOfDaysShown) // Renamed for clarity
        {
            DateTime today = DateTime.Today;
            for (int i = 1; i <= NumberOfRooms; i++)
            {
                Console.Write($"[{i - 1}] Room {i} {rooms[i - 1].Name} | ");

                    foreach (var r in reservationRoomPairs)
                    {
                        if (r.Key.Rooms.Contains(rooms[i - 1]))
                        {
                            Console.Write(r.Key.GuestName + ' ' + r.Key.StartDate.ToString("dd/MM") + '-' + r.Key.EndDate.ToString("dd/MM"));
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                
                Console.WriteLine();
            }
        }
    }
}
