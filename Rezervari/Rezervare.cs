using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_Management_App.Rooms;
namespace Hotel_Management_App.Rezervari
{
    internal class Rezervare
    {
        public string GuestName;
        public DateTime startingDate;
        public DateTime endingDate;
        private int rezervareId;
        private int numberOfRooms;
        private int numberOfGuests;
        private double Pret;
        public List<Camere> camere = new List<Camere>();

        public Rezervare(string GuestName, DateTime startingDate, DateTime endingDate, List<Camere> camere)
        {
            this.GuestName = GuestName;
            this.startingDate = startingDate;
            this.endingDate = endingDate;
            this.camere = camere;
        }

        
    }
}
