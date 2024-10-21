using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_Management_App.Rooms;

namespace Hotel_Management_App.Reservations
{
    internal class ReservationManager
    {
        private List<Reservation> reservations = new List<Reservation>();
        private List<Room> rooms = new List<Room>();
        private Dictionary<Reservation, List<Room>> reservationRoomPairs = new Dictionary<Reservation, List<Room>>();
        private static int NumberOfReservations = 0;

        public ReservationManager(List<Reservation> reservations, List<Room> rooms, Dictionary<Reservation, List<Room>> reservationRoomPairs)
        {
            this.reservations = reservations;
            this.rooms = rooms;
            this.reservationRoomPairs = reservationRoomPairs;
        }

        public void InitializeReservations()
        {
            DateTime today = DateTime.Today;

            AddReservation("Cristi", new List<Room> { rooms[1], rooms[2] }, today, today.AddDays(2));
            AddReservation("Mario", new List<Room> { rooms[5], rooms[6], rooms[10] }, today.AddDays(3), today.AddDays(4));
            AddReservation("Gabi", new List<Room> { rooms[11], rooms[12] }, today.AddDays(1), today.AddDays(4));
            AddReservation("Boss", new List<Room> { rooms[1], rooms[3] }, today.AddDays(1), today.AddDays(4));
        }

        public void AddReservationInput()
        {
            Console.Write("Tourist Name: ");
            string nameInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nameInput))
            {
                Console.WriteLine("The tourist's name cannot be empty.");
                return;
            }

            Console.Write("Which rooms do they want (0 - auto-assign; separate numbers with ',' and use only numbers!): ");
            string roomInput = Console.ReadLine();
            string[] roomStringArray = roomInput.Split(',');

            List<int> roomIndices = new List<int>();
            List<Room> requestedRooms = new List<Room>();

            if (roomStringArray[0] != "0")
            {
                while (true)
                {
                    bool allValid = true;

                    for (int i = 0; i < roomStringArray.Length; i++)
                    {
                        if (!int.TryParse(roomStringArray[i], out int roomIndex) || roomIndex < 0 || roomIndex >= rooms.Count)
                        {
                            Console.Write("Please enter valid room numbers (0 - auto-assign): ");
                            roomInput = Console.ReadLine();
                            roomStringArray = roomInput.Split(',');
                            allValid = false;
                            break;
                        }
                        roomIndices.Add(roomIndex);
                        requestedRooms.Add(rooms[roomIndex]);
                    }

                    if (allValid) break; // Exit if all inputs are valid
                }
            }
            else
            {
                // Handle automatic room assignment here
            }

            DateTime startingDate, endingDate;

            Console.Write("When does the reservation start?: ");
            string startingDateInput = Console.ReadLine();
            while (!DateTime.TryParse(startingDateInput, out startingDate))
            {
                Console.Write("Please enter a valid start date for the reservation: ");
                startingDateInput = Console.ReadLine();
            }

            Console.Write("When does the reservation end?: ");
            string endingDateInput = Console.ReadLine();
            while (!DateTime.TryParse(endingDateInput, out endingDate) || endingDate <= startingDate)
            {
                Console.Write("The end date must be after the start date: ");
                endingDateInput = Console.ReadLine();
            }

            AddReservation(nameInput, requestedRooms, startingDate, endingDate);
        }

        private void AddReservation(string guestName, List<Room> selectedRooms, DateTime startingDate, DateTime endingDate)
        {
            Reservation newReservation = new Reservation(guestName, startingDate, endingDate, selectedRooms);
            bool isAvailable = true;
            Reservation overlappingReservation = null;

            foreach (var existingReservation in reservations)
            {
                foreach (var room in existingReservation.Rooms)
                {
                    // Check for overlapping reservations
                    if (selectedRooms.Contains(room) &&
                        PeriodsDoNotOverlap(startingDate, endingDate, existingReservation.StartDate,  existingReservation.EndDate))
                    {
                        isAvailable = false;
                        overlappingReservation = existingReservation;
                        break;
                    }
                    if (!isAvailable)
                        break;
                }
                if (!isAvailable)
                    break;
            }

            if (isAvailable)
            {
                newReservation.ReservationId = NumberOfReservations;
                NumberOfReservations++;
                reservations.Add(newReservation);
                reservationRoomPairs.Add(newReservation, selectedRooms);
            }
            else
            {
                Console.WriteLine($"Error: Reservation for {newReservation.GuestName} with {newReservation.Rooms.Count} rooms from {newReservation.StartDate:dd/MM} to {newReservation.EndDate:dd/MM} cannot be made because it overlaps with another reservation from {overlappingReservation.GuestName}.");
            }
        }

        public void DeleteReservationInput()
        {
            foreach (var reservation in reservations)
            {
                Console.Write($"Reservation [{reservation.ReservationId}] by {reservation.GuestName} for ");
                foreach(var room in reservation.Rooms)
                {
                    Console.Write($"{room.Name}, ");
                }
                Console.WriteLine();
            }
            Console.Write("What reservation do you want to delete?: ");
            string input = Console.ReadLine();
            int DeleteReservationID;
            while (!int.TryParse(input, out DeleteReservationID))
            {
                Console.WriteLine("Type a number please!: ");
                input = Console.ReadLine();
            }
            if (!reservations.Contains(reservations[DeleteReservationID]))
            {
                Console.WriteLine("The reservation doesn't exist");
            }
            else
            {
                Console.WriteLine(DeleteReservationID + reservations[DeleteReservationID].GuestName);
                reservationRoomPairs.Remove(reservations[DeleteReservationID]);
                reservations.Remove(reservations[DeleteReservationID]);
                
                Console.WriteLine("Reservation has been deleted");
            }

        }
        public static bool IsDateInRange(DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date <= endDate;
        }

        public static bool PeriodsDoNotOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            // Periods overlap if one starts before the other ends and ends after the other starts.
            return !(end1 < start2 || end2 < start1); // returns false if they do overlap, since the !
        }
        
    }
}
