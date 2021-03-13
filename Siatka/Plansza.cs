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
        //przypisanie wartości z istniejącego grida
        public Plansza(Grid grid)
        {
            kolumny = grid.ColumnDefinitions.Count;
            wiersze = grid.RowDefinitions.Count;
            punkty = 0;
            wypelnienia = 0;
            kontrolne = new Label[kolumny, wiersze];
            czyPrzegrana = false;
            //wypełnienie grida siatką
            for (int i = 0; i < kolumny; i++)
            {
                for (int j = 0; j < wiersze; j++)
                {
                    kontrolne[i, j] = new Label();
                    kontrolne[i, j].Background = brak;
                    kontrolne[i, j].BorderBrush = szary;
                    kontrolne[i, j].BorderThickness = new System.Windows.Thickness(1, 1, 1, 1);
                    Grid.SetRow(kontrolne[i, j], j);
                    Grid.SetColumn(kontrolne[i, j], i);
                    grid.Children.Add(kontrolne[i, j]);
                }
            }
            //stworzenie pierwszego i drugiego tetromino; umieszczenie pierwszego tetromino na planszy
            tetromino = new Tetromino();
            kolejneTetromino = new Tetromino();
            UmiescTetromino();
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
        private void UmiescTetromino()
        {
            Point pozycja = tetromino.wezPolozenie();
            Point[] ksztalt = tetromino.wezKsztalt();
            Brush kolor = tetromino.wezKolor();
            foreach (Point p in ksztalt)
            {
                kontrolne[(int)(p.X + pozycja.X) + ((kolumny / 2) - 1), (int)(p.Y + pozycja.Y) + 1].Background = kolor;
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
                kontrolne[(int)(p.X + pozycja.X) + ((kolumny / 2) - 1), (int)(p.Y + pozycja.Y) + 1].Background = brak;
            }
        }
        //sprawdza, czy wszystkie kafelki w wierszu mają przypisany inny kolor niż domyślny; przypisanie punktów
        private void SprawdzWiersze()
        {
            bool zapelnione = false;
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
                if (((int)(p.X + pozycja.X) + (kolumny / 2)) >= kolumny)
                {
                    ruch = false;
                }
                else if (kontrolne[((int)(p.X + pozycja.X) + (kolumny / 2)), (int)(p.Y + pozycja.Y) + 1].Background != brak)
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
                if (((int)(p.X + pozycja.X) + (kolumny / 2) - 2) < 0)
                {
                    ruch = false;
                }
                else if (kontrolne[((int)(p.X + pozycja.X) + (kolumny / 2) - 2), (int)(p.Y + pozycja.Y) + 1].Background != brak)
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
                else if (kontrolne[((int)(p.X + pozycja.X) + ((kolumny / 2) - 1)), (int)(p.Y + pozycja.Y) + 2].Background != brak)
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
                UmiescTetromino();
                SprawdzWiersze();
                tetromino = kolejneTetromino;
                czyPrzegrana = Przegrana();
                kolejneTetromino = new Tetromino();
            }
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
                else if (((int)(pom[i].X + pozycja.X) + ((kolumny / 2) - 1)) < 0)
                {
                    ruch = false;
                }
                else if (((int)(pom[i].X + pozycja.X) + ((kolumny / 2) - 1)) >= kolumny)
                {
                    ruch = false;
                }
                else if (((int)(pom[i].X + pozycja.X) + ((kolumny / 2) - 1)) >= wiersze)
                {
                    ruch = false;
                }
                else if (kontrolne[((int)(pom[i].X + pozycja.X) + ((kolumny / 2) -1)), (int)(pom[i].Y + pozycja.Y) + 2].Background != brak)//obrót przy prawym boku(?)
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
                if (kontrolne[(int)(p.X + pozycja.X) + ((kolumny / 2) - 1), (int)(p.Y + pozycja.Y) + 2].Background != brak)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
