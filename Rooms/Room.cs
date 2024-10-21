using System;

namespace Hotel_Management_App.Rooms
{
    internal class Room
    {
        public string Name { get; } // Changed to PascalCase
        public bool IsOccupied { get; set; } // Changed to PascalCase and made it a property

        public int NumberOfGuests { get; }
        public int NumberOfToilets { get; }

        public Room(string name, int numberOfGuests, int numberOfToilets)
        {
            Name = name; // Use property Name
            NumberOfGuests = numberOfGuests;
            NumberOfToilets = numberOfToilets;
            IsOccupied = false; // Initialize the occupancy status
        }
    }
}
