using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvmPilkarze.Model
{
    internal class Pilkarz
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public uint Wiek { get; set; }
        public uint Waga { get; set; }



        public Pilkarz(string imie, string nazwisko, uint wiek, uint waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
        }


        public Pilkarz(string imie, string nazwisko) : this(imie, nazwisko, 30, 75) { }

        public Pilkarz Edit(Pilkarz pilkarz)
        {
            this.Imie = Imie;
            this.Nazwisko = Nazwisko;
            this.Wiek = Wiek;
            this.Waga = Waga;

            return this;

        }

      

        public string pobierzImie() { return this.Imie; }
        public string pobierzNazwisko() { return this.Nazwisko; }
        public uint pobierzWiek() { return this.Wiek; }
        public uint pobierzWaga() { return this.Waga; }
    }

}
