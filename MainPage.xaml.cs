using System;
using System.Diagnostics;

namespace auta;

public partial class MainPage : ContentPage
{
    private Auto auto;

    private const int CenaBazowa = 75000;
    private const int LakierInny = 9000;
    private const int FelgiAluminiowe = 7000;
    private const int Czujniki = 6500;
    private const int Klima = 8500;
    private const int Nawigacja = 5000;

    public MainPage()
    {
        InitializeComponent();

        auto = new Auto();

        PickerKolor.SelectedIndex = 0;
        AktualizujWycene();
    }

    private void PickerKolor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string kolor = PickerKolor.SelectedItem.ToString();
        auto.SetKolor(kolor);

        ImgAuto.Source = $"{kolor}.png";
        AktualizujWycene();
    }

    private void Felgi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (aluminiowe.IsChecked)
            auto.SetFelgi("aluminiowe");
        else
            auto.SetFelgi("stalowe");

        AktualizujWycene();
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        auto.SetWyposazenie(new bool[]
        {
            chek1.IsChecked,
            chek2.IsChecked,
            chek3.IsChecked
        });

        AktualizujWycene();
    }

    private void AktualizujWycene()
    {
        int cena = CenaBazowa;
        string wynik = $"Cena bazowa: {CenaBazowa} PLN\n";

        if (auto.Kolor != "szary")
        {
            cena += LakierInny;
            wynik += $"Lakier ({auto.Kolor}): {LakierInny} PLN\n";
        }

        if (auto.Felgi == "aluminiowe")
        {
            cena += FelgiAluminiowe;
            wynik += $"Felgi aluminiowe: {FelgiAluminiowe} PLN\n";
        }

        if (auto.Wyposazenie[0])
        {
            cena += Czujniki;
            wynik += $"Czujniki parkowania: {Czujniki} PLN\n";
        }

        if (auto.Wyposazenie[1])
        {
            cena += Klima;
            wynik += $"Climatronic: {Klima} PLN\n";
        }

        if (auto.Wyposazenie[2])
        {
            cena += Nawigacja;
            wynik += $"Nawigacja: {Nawigacja} PLN\n";
        }

        wynik += $"RAZEM: {cena} PLN";

        price.Text = wynik;
    }
    private class Auto
    {
        private string kolor;
        private string felgi;
        private bool[] wyposazenie;

        public string Kolor => kolor;
        public string Felgi => felgi;
        public bool[] Wyposazenie => wyposazenie;

        public Auto()
        {
            kolor = "szary";
            felgi = "stalowe";
            wyposazenie = new bool[] { false, false, false };
        }

        public Auto(string k, string f, bool[] w)
        {
            kolor = k;
            felgi = f;
            wyposazenie = w;
        }

        public void SetKolor(string k) => kolor = k;
        public void SetFelgi(string f) => felgi = f;
        public void SetWyposazenie(bool[] w) => wyposazenie = w;
    }
}
