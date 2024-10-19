using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_App.Rooms
{
    internal class Camere
    {
        public string _name { get; }
        public bool occupied = false;

        public int nrOfGuests { get; }
        public int nrOfToilets { get; }
        public Camere(string name, int nrOfGuests, int nrOfToilets)
        {
            _name = name;
            this.nrOfGuests = nrOfGuests;
            this.nrOfToilets = nrOfToilets;
        }

    }
}
