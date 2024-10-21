using System;
using System.Collections.Generic;
using Hotel_Management_App.Rooms;

namespace Hotel_Management_App.Reservations // Updated namespace to English
{
    internal class Reservation
    {
        public string GuestName { get; set; } // Changed to property
        public DateTime StartDate { get; set; } // Changed to property and PascalCase
        public DateTime EndDate { get; set; } // Changed to property and PascalCase
        public int ReservationId { get; set; } // Changed to property and PascalCase
        private int NumberOfRooms { get; set; } // Changed to property and PascalCase
        private int NumberOfGuests { get; set; } // Changed to property and PascalCase
        private double Price { get; set; } // Changed to property and PascalCase
        public List<Room> Rooms { get; set; } // Changed to Room and PascalCase

        public Reservation(string guestName, DateTime startDate, DateTime endDate, List<Room> rooms)
        {
            GuestName = guestName; // Use property
            StartDate = startDate; // Use property
            EndDate = endDate; // Use property
            Rooms = rooms; // Use property
        }
    }
}
