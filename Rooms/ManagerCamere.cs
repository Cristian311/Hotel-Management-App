using Hotel_Management_App.Rezervari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_App.Rooms
{
    internal class ManagerCamere
    {
        private List<Camere> camere = new List<Camere>();
        public ManagerCamere(List<Camere> camere)
        {
            this.camere = camere;
        }
        public void initCamere(int numberOfRooms)
        {
            for (int i = 1; i <= numberOfRooms; i++)
            {
                if (i % 2 == 0)
                    camere.Add(new Camere("CAMERA 2 PERSOANE", 2, 1));
                else
                    camere.Add(new Camere("CAMERA 4 PERSOANE", 4, 1));
            }
        }
    }
}
