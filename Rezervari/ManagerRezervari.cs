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
        public void AdaugaRezervareInput()
        {
            Console.Write("Numele turistului: ");
            string NameInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(NameInput))
            {
                Console.WriteLine("Numele turistului nu poate fi gol.");
                return;
            }

            Console.Write("Ce camere vrea (0 - pune automat; Puneti ',' intre numere si folositi doar numere!): ");
            string CamereInput = Console.ReadLine();
            string[] CamereStringArray = CamereInput.Split(',');

            List<int> intCameraInput = new List<int>();
            List<Camere> camereCerute = new List<Camere>();

            if (CamereStringArray[0] != "0")
            {
                while (true)
                {
                    bool allValid = true;

                    for (int i = 0; i < CamereStringArray.Length; i++)
                    {
                        if (!int.TryParse(CamereStringArray[i], out int cameraIndex) || cameraIndex < 0 || cameraIndex >= camere.Count)
                        {
                            Console.Write("Introdu numere valide pentru camere (0 - pune automat): ");
                            CamereInput = Console.ReadLine();
                            CamereStringArray = CamereInput.Split(',');
                            allValid = false;
                            break;
                        }
                        intCameraInput.Add(cameraIndex);
                        camereCerute.Add(camere[cameraIndex]);
                    }

                    if (allValid) break; // Exit if all inputs are valid
                }
            }
            else
            {
                // Handle automatic room assignment here
            }

            DateTime startingDate, endingDate;

            Console.Write("Cand vrea sa inceapa rezervarea?: ");
            string startingDateInput = Console.ReadLine();
            while (!DateTime.TryParse(startingDateInput, out startingDate))
            {
                Console.Write("Introdu o data corecta pentru inceperea rezervarii: ");
                startingDateInput = Console.ReadLine();
            }

            Console.Write("Cand vrea sa termine rezervarea?: ");
            string endingDateInput = Console.ReadLine();
            while (!DateTime.TryParse(endingDateInput, out endingDate) || endingDate <= startingDate)
            {
                Console.Write("Data de finalizare trebuie sa fie dupa data de inceput: ");
                endingDateInput = Console.ReadLine();
            }

            AdaugaRezervare(NameInput, camereCerute, startingDate, endingDate);
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
