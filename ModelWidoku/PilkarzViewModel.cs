using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
using System.Xml.Linq;


namespace mvvmPilkarze.ModelWidoku
{
    using Model;
    using BaseClass;

    class PilkarzViewModel : ViewModelBase
    {
        private ObservableCollection<Pilkarz> _listaPilkarzy = null;

        public ObservableCollection<Pilkarz> listaPilkarzy
        {
            get
            {
                if (_listaPilkarzy == null)
                {
                    _listaPilkarzy = new ObservableCollection<Pilkarz>();
                    
                }
                return _listaPilkarzy;
            }
        }


        public string pobierzImie(int indeks) { return listaPilkarzy[indeks].pobierzImie(); }
        public string pobierzNazwisko(int indeks) { return listaPilkarzy[indeks].pobierzNazwisko(); }
        public uint pobierzWiek(int indeks) { return listaPilkarzy[indeks].pobierzWiek(); }
        public uint pobierzWaga(int indeks) { return listaPilkarzy[indeks].pobierzWaga(); }



        private string _imie = null;
        public string Imie
        {
            get { return _imie; }
            set { _imie = value; onPropertyChanged(nameof(Imie)); }
        }

        private string _nazwisko = null;
        public string Nazwisko
        {
            get { return _nazwisko; }
            set
            { _nazwisko = value; onPropertyChanged(nameof(Nazwisko)); }
        }

        private int _index = -1;
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                if (_index != -1)
                {
                    Imie = pobierzImie(_index);
                    Nazwisko = pobierzNazwisko(_index);
                    Waga = pobierzWaga(_index);
                    Wiek = pobierzWiek(_index);
                }
                onPropertyChanged(nameof(Index));
            }
        }

        private uint _wiek = 0;
        public uint Wiek
        {
            get { return _wiek; }
            set { _wiek = value; onPropertyChanged(nameof(Wiek)); }
        }

        private uint _waga = 0;
        public uint Waga
        {
            get { return _waga; }
            set { _waga = value; onPropertyChanged(nameof(Waga)); }
        }


        public void clear()
        {
            Imie = null;
            Nazwisko = null;
            Waga = 0;
            Wiek = 0;
            Index = -1;
        }

        public ICommand _dodajPilkarza = null;
        public ICommand DodajPilkarza
        {
            get
            {
                if (_dodajPilkarza == null)
                {
                    _dodajPilkarza = new RelayCommand(

                        arg => {
                            listaPilkarzy.Add(new Pilkarz(Imie, Nazwisko, Wiek, Waga)); clear();
                        },
                        arg => (Imie != null) && (Nazwisko != null) && (Waga != 0) && (Wiek != 0));
                }
                return _dodajPilkarza;
            }
        }

        public ICommand _usunPilkarza = null;
        public ICommand UsunPilkarza
        {
            get
            {
                if (_usunPilkarza == null)
                {
                    _usunPilkarza = new RelayCommand(
                        arg =>
                        {
                            if (MessageBox.Show("Czy na pewno chcesz usunąć zawodnika?", "Usuwanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                listaPilkarzy.RemoveAt(Index); onPropertyChanged(nameof(listaPilkarzy));
                                clear();
                            }
                        },
                        arg => Index != -1);
                }
                return _usunPilkarza;
            }
        }


        public ICommand _zaladujPilkarza = null;
        public ICommand zaladujPilkarza
        {
            get
            {
                if (_zaladujPilkarza == null)
                {
                    _zaladujPilkarza = new RelayCommand(
                        arg => { Imie = Imie; Nazwisko = Nazwisko; Wiek = Wiek; Waga = Waga; },
                        arg => Index != -1
                        );
                }
                return _zaladujPilkarza;
            }
        }



        public ICommand _edytujPilkarza = null;
        public ICommand EdytujPilkarza
        {
            get
            {
                if (_edytujPilkarza == null)
                {
                    _edytujPilkarza = new RelayCommand(
                       arg => {
                           if (MessageBox.Show("Czy na pewno chcesz edytować zawodnika?", "Edytowanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                           {
                               listaPilkarzy[Index] = new Pilkarz(Imie, Nazwisko, Wiek, Waga); clear();
                           }
                       },
                       arg => (Index != -1));
                }
                return _edytujPilkarza;
            }
        }

       
        public ICommand Zapisz
        {
            get=>
                 new RelayCommand(
                 arg => { zapisz(); },
                 arg => true);
                
               
            
        }

        public void zapisz()
        {
            
            List<Pilkarz> lista = new List<Pilkarz>();
            foreach(var pilkarz in listaPilkarzy)
            {
                lista.Add(pilkarz);
            }
            XDocument xml = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XComment("Lista piłkarzy z kolekcji"),
            new XElement("osoby",
            from zawodnik in lista
            orderby zawodnik.Imie
            select new XElement("osoba",
            new XElement("imie", zawodnik.Imie),
            new XElement("nazwisko", zawodnik.Nazwisko),
            new XElement("wzrost", zawodnik.Waga),
            new XElement("wiek", zawodnik.Wiek))));

            xml.Save("Zawodnicy.xml");
        }

    }
}
