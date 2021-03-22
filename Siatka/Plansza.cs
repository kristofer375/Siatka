using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Siatka
{
    class Plansza
    {
        private int kolumny;
        private int wiersze;
        private int punkty;
        private int wypelnienia;
        private Tetromino tetromino;
        private Tetromino kolejneTetromino;
        private bool czyPrzegrana;
        //dwuwymiarowa tablica Label, do której pola są zmieniane kolorystycznie w zależności od położenia tetromino
        private Label[,] kontrolne;
        private Brush brak = Brushes.Transparent;
        private Brush szary = Brushes.Gray;
        private Thickness siatka = new Thickness(1, 1, 1, 1);
        //przypisanie wartości z istniejącego grida
        public Plansza(Grid grid)
        {
            kolumny = /*grid.ColumnDefinitions.Count*/10;
            wiersze = /*grid.RowDefinitions.Count*/22;
            punkty = 0;
            wypelnienia = 0;
            kontrolne = new Label[16, 22];
            czyPrzegrana = false;
            //wypełnienie grida siatką
            for (int i = 0; i < kolumny; i++)
            {
                for (int j = 0; j < wiersze; j++)
                {
                    kontrolne[i, j] = new Label();
                    kontrolne[i, j].Background = brak;
                    kontrolne[i, j].BorderThickness = siatka;
                    Grid.SetRow(kontrolne[i, j], j);
                    Grid.SetColumn(kontrolne[i, j], i);
                    grid.Children.Add(kontrolne[i, j]);
                }
            }
            Label obramówka = new Label();
            obramówka.Background = brak;
            obramówka.BorderBrush = Brushes.Black;
            obramówka.BorderThickness = new Thickness(2, 2, 2, 2);
            Grid.SetRow(obramówka, 2);
            Grid.SetColumn(obramówka, 0);
            Grid.SetColumnSpan(obramówka, 10);
            Grid.SetRowSpan(obramówka, 22);
            grid.Children.Add(obramówka);

            for (int i = kolumny + 2; i < 16; i++)
            {
                for (int j = 8; j < 12; j++)
                {
                    kontrolne[i, j] = new Label();
                    kontrolne[i, j].Background = brak;
                    kontrolne[i, j].BorderThickness = siatka;
                    Grid.SetRow(kontrolne[i, j], j);
                    Grid.SetColumn(kontrolne[i, j], i);
                    grid.Children.Add(kontrolne[i, j]);
                }
            }
            Label nast = new Label();
            nast.Background = brak;
            nast.BorderBrush = Brushes.Black;
            nast.BorderThickness = new Thickness(2, 2, 2, 2);
            Grid.SetRow(nast, 8);
            Grid.SetColumn(nast, 12);
            Grid.SetColumnSpan(nast, 4);
            Grid.SetRowSpan(nast, 4);
            grid.Children.Add(nast);

            Siatka(grid, true);
            //stworzenie pierwszego i drugiego tetromino; umieszczenie pierwszego tetromino na planszy
            tetromino = new Tetromino();
            kolejneTetromino = new Tetromino();
            UmiescTetromino();
            UmiescNastepne();
        }
        public void Siatka(Grid grid, bool x)
        {
            if (x)
            {
                siatka = new Thickness(1, 1, 1, 1);
            }
            else
            {
                siatka = new Thickness(0, 0, 0, 0);
            }

            for (int i = 0; i < kolumny; i++)
            {
                for (int j = 2; j < wiersze; j++)
                {
                    kontrolne[i, j].BorderBrush = szary;
                    kontrolne[i, j].BorderThickness = siatka;
                }
            }
            
            /**/
            for (int i = kolumny + 2; i < 16; i++)
            {
                for (int j = 8; j < 12; j++)
                {
                    kontrolne[i, j].BorderBrush = szary;
                    kontrolne[i, j].BorderThickness = siatka;
                }
            }
        }
        public bool CzyPorazka()
        {
            return czyPrzegrana;
        }
        public int WezPunkty()
        {
            return punkty;
        }
        public int WezWypelnienia()
        {
            return wypelnienia;
        }
        //zmienia kolor kafelków na kolor aktywnego tetromino
        private void UmiescNastepne()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = kolejneTetromino.wezKsztalt();
            Brush kolor = kolejneTetromino.wezKolor();
            foreach (Point p in ksztalt)
            {
                kontrolne[(int)(p.X) + (14 - 1), (int)(p.Y) + 10].Background = kolor;
            }
        }
        private void UsunNastepne()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            Brush kolor = tetromino.wezKolor();
            foreach (Point p in ksztalt)
            {
                kontrolne[(int)(p.X + pozycja.X) + (14 - 1), (int)(p.Y + pozycja.Y) + 10].Background = brak;
            }
        }
        private void UmiescTetromino()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            Brush kolor = tetromino.wezKolor();
            foreach (Point p in ksztalt)
            {
                kontrolne[(int)(p.X + pozycja.X) + (5 - 1), (int)(p.Y + pozycja.Y) + 1].Background = kolor;
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    kontrolne[j, i].Background = brak;
                }
            }
        }
        //usuwa kolor z kafelków na obecnie aktywnego tetromino
        private void UsunTetromino()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            Brush kolor = tetromino.wezKolor();
            foreach (Point p in ksztalt)
            {
                kontrolne[(int)(p.X + pozycja.X) + (5 - 1), (int)(p.Y + pozycja.Y) + 1].Background = brak;
            }
        }
        //sprawdza, czy wszystkie kafelki w wierszu mają przypisany inny kolor niż domyślny; przypisanie punktów
        
        public void SprawdzWiersze()
        {
            bool zapelnione;
            for (int i = wiersze - 1; i > 0; i--)
            {
                zapelnione = true;
                for (int j = 0; j < kolumny; j++)
                {
                    if (kontrolne[j, i].Background == brak)
                    {
                        zapelnione = false;
                    }
                }
                if (zapelnione)
                {
                    UsunWiersz(i);
                    punkty += 200;
                    wypelnienia += 1;
                }
            }
            
        }
        //usuwa pełen wiersz przemieszczając wszystkie kafelki nad nim w dół
        private void UsunWiersz(int wiersz)
        {
            for (int i = wiersz; i > 2; i--)
            {
                for (int j = 0; j < kolumny; j++)
                {
                    kontrolne[j, i].Background = kontrolne[j, i - 1].Background;
                }
            }
            SprawdzWiersze();
        }
        //sprawdzenie, czy tetromino wykonać dana czynność; wykonanie jej jeśli jest to możliwe
        public void TetrominoPrawo()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            bool ruch = true;
            UsunTetromino();
            foreach (Point p in ksztalt)
            {
                if (((int)(p.X + pozycja.X) + 5) >= kolumny)
                {
                    ruch = false;
                }
                else if (kontrolne[((int)(p.X + pozycja.X) + 5), (int)(p.Y + pozycja.Y) + 1].Background != brak)
                {
                    ruch = false;
                }
            }
            if (ruch)
            {
                tetromino.Prawo();
                UmiescTetromino();
            }
            else
            {
                UmiescTetromino();
            }
        }
        public void TetrominoLewo()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            bool ruch = true;
            UsunTetromino();
            foreach (Point p in ksztalt)
            {
                if (((int)(p.X + pozycja.X) + 5 - 2) < 0)
                {
                    ruch = false;
                }
                else if (kontrolne[((int)(p.X + pozycja.X) + 5 - 2), (int)(p.Y + pozycja.Y) + 1].Background != brak)
                {
                    ruch = false;
                }
            }
            if (ruch)
            {
                tetromino.Lewo();
                UmiescTetromino();
            }
            else
            {
                UmiescTetromino();
            }
        }
        public void TetrominoDol()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            bool ruch = true;
            UsunTetromino();
            foreach (Point p in ksztalt)
            {
                if (((int)(p.Y + pozycja.Y) + 2) >= wiersze)
                {
                    ruch = false;
                }
                else if (kontrolne[((int)(p.X + pozycja.X) + (5 - 1)), (int)(p.Y + pozycja.Y) + 2].Background != brak)
                {
                    ruch = false;
                }
            }
            if (ruch)
            {
                tetromino.Dol();
                UmiescTetromino();
            }
            else
            {
                tetromino.UstawKolor();
                UmiescTetromino();
                SprawdzWiersze();
                tetromino = kolejneTetromino;
                czyPrzegrana = Przegrana();
                UsunNastepne();
                kolejneTetromino = new Tetromino();
            }
            UmiescNastepne();
        }
        public void TetrominoObrot()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            Point[] pom = new Point[4];
            bool ruch = true;
            ksztalt.CopyTo(pom, 0);
            UsunTetromino();
            for (int i = 0; i < pom.Length; i++)
            {
                double x = pom[i].X;
                pom[i].X = pom[i].Y * -1;
                pom[i].Y = x;
                if (((int)((pom[i].Y + pozycja.Y) + 2)) >= wiersze)
                {
                    ruch = false;
                }
                else if (((int)(pom[i].X + pozycja.X) + (5 - 1)) < 0)
                {
                    ruch = false;
                }
                else if (((int)(pom[i].X + pozycja.X) + (5 - 1)) >= kolumny)
                {
                    ruch = false;
                }
                else if (((int)(pom[i].X + pozycja.X) + (5 - 1)) >= wiersze)
                {
                    ruch = false;
                }
                else if (kontrolne[((int)(pom[i].X + pozycja.X) + (5 - 1)), (int)(pom[i].Y + pozycja.Y) + 2].Background != brak)//obrót przy prawym boku(?)
                {
                    ruch = false;
                }
            }
            if (ruch)
            {
                tetromino.Gora();
                UmiescTetromino();
            }
            else
            {
                UmiescTetromino();
            }
        }
        private bool Przegrana()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            foreach (Point p in ksztalt)
            {
                if (kontrolne[(int)(p.X + pozycja.X) + (5 - 1), (int)(p.Y + pozycja.Y) + 2].Background != brak)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
