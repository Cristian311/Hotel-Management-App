using Hotel_Management_App.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_App.Rooms
{
    internal class RoomManager
    {
        private List<Room> rooms = new List<Room>();

        public RoomManager(List<Room> rooms)
        {
            this.rooms = rooms;
        }

        public void InitializeRooms(int numberOfRooms)
        {
            for (int i = 1; i <= numberOfRooms; i++)
            {
                if (i % 2 == 0)
                    rooms.Add(new Room($"2-PERSON ROOM NR.{i}", 2, 1));
                else
                    rooms.Add(new Room($"4-PERSON ROOM NR.{i}", 4, 1));
            }
        }
    }
}
