using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_Management_App.Rooms; 

namespace Hotel_Management_App.Rezervari
{
    internal class ManagerRezervari
    {
        private List<Rezervare> rezervari = new List<Rezervare>();
        private List<Camere> camere = new List<Camere>();
        private Dictionary<Rezervare, List<Camere>> perechiRezervariCamere = new Dictionary<Rezervare, List<Camere>>();
        public ManagerRezervari(List<Rezervare> rezervari, List<Camere> camere, Dictionary<Rezervare, List<Camere>> perechiRezervariCamere)
        {
            this.rezervari = rezervari;
            this.camere = camere;
            this.perechiRezervariCamere = perechiRezervariCamere;
        }

        public void initRezervari()
        {
            DateTime today = DateTime.Today;

            AdaugaRezervare("Cristi", new List<Camere> { camere[1], camere[2] }, today, today.AddDays(2));
            AdaugaRezervare("Mario", new List<Camere> { camere[5], camere[6], camere[10] }, today.AddDays(3), today.AddDays(4));
            AdaugaRezervare("gabi", new List<Camere> { camere[11], camere[12] }, today.AddDays(1), today.AddDays(4));
            AdaugaRezervare("Boss", new List<Camere> { camere[1], camere[3] }, today.AddDays(1), today.AddDays(4));

        }
        private void AdaugaRezervareInput()
        {
            Console.Write("Numele turistului: ");
            string NameInput = Console.ReadLine();

            Console.Write("Ce camere vrea (0 - pune automat; Puneti ',' intre numere si folositi doar numere!): ");
            string CamereInput = Console.ReadLine();
            string[] CamereStringArray = CamereInput.Split(',');
            int nrCamere;
            for(int i = 0; i < CamereStringArray.Length; i++)
            {
                int intCameraInput;
                if (!int.TryParse(CamereStringArray[i], intCameraInput))
                Console.Write("Introdu un numar pentru camere te rog!");
                CamereInput = Console.ReadLine();
            }
            if (nrCamere == 0)
            {
                Console.Write("Cati musafiri sunt?: ");
                string musafiriInput = Console.ReadLine();
                int nrMusafiri;
                while (!int.TryParse(musafiriInput, out nrMusafiri))
                {
                    Console.Write("Introdu un numar pentru camere te rog!");
                    musafiriInput = Console.ReadLine();
                }
            }



            Console.Write("Cand vrea sa inceapa rezervarea?: ");
            string startingDateInput = Console.ReadLine();
            while (!DateTime.TryParse(startingDateInput, out DateTime date))
            {
                Console.Write("Introdu o data corecta pentru inceperea rezervarii: ");
                startingDateInput = Console.ReadLine();
            }
            Console.Write("Cand vrea sa termine rezervarea?: ");
            string endingDateInput = Console.ReadLine();
            while (!DateTime.TryParse(startingDateInput, out DateTime date))
            {
                Console.Write("Introdu o data corecta pentru inceperea rezervarii: ");
                startingDateInput = Console.ReadLine();
            }
        }

        private void AdaugaRezervare(string GuestName, List<Camere> camere, DateTime startingDate, DateTime endingDate)
        {
            Rezervare rezervare = new Rezervare(GuestName, startingDate, endingDate, camere);
            bool ok = true;
            Rezervare rezervareSuprapusa = null;
            foreach (var r in rezervari)
            {
                foreach (var c in r.camere)
                {
                    if (camere.Contains(c) && ManagerRezervari.verifRezervariSuprapuse(r.startingDate, startingDate, r.endingDate, endingDate))
                    {
                        ok = false;
                        rezervareSuprapusa = r;
                        break;
                    }
                    if (!ok)
                        break;
                }
            }
            if (ok)
            {
                rezervari.Add(rezervare);
                perechiRezervariCamere.Add(rezervare, camere);
            }
            else
                Console.WriteLine($"Eroare: Rezervarea {rezervare.GuestName} cu {rezervare.camere.Count} camere de la {rezervare.startingDate.ToString("dd/MM")} pana la {rezervare.endingDate.ToString("dd/MM")} \nn-a putut fii facuta pentru ca se suprapune cu o alta rezervare de la {rezervareSuprapusa.GuestName}");
        }
        public static bool dateInBetween(DateTime date, DateTime startdate, DateTime enddate)
        {
            if (date < startdate || date > enddate)
                return false;
            return true;
        }
        public static bool verifRezervariSuprapuse(DateTime firstStartDate, DateTime secondStartDate, DateTime firstEndDate, DateTime secondEndDate)
        {

            if (firstStartDate >= secondStartDate && firstEndDate <= secondEndDate)
                return false;
            return true;

        }
    }
}
