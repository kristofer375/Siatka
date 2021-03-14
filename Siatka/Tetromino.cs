using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Siatka
{
    public class Tetromino
    {
        private Point polozenie;
        private Point[] ksztalt;
        private Brush kolor;
        private bool czyObrot;

        public Tetromino()
        {
            polozenie = new Point(0, 0);
            kolor = LosowyKolor();
            ksztalt = LosowyKsztalt();
        }
        public Point wezPolozenie()
        {
            return polozenie;
        }
        public void UstawKolor()
        {
             kolor = Brushes.Green;
        }
        public Brush wezKolor()
        {
            return kolor;
        }
        public Point[] wezKsztalt()
        {
            return ksztalt;
        }
        private Point[] LosowyKsztalt()
        {
            Random random = new Random();
            switch (random.Next(0, 7))
            {
                case 0:
                    czyObrot = true;
                    return new Point[] { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, -1) }; //T
                case 1:
                    czyObrot = true;
                    return new Point[] { new Point(-1, 0), new Point(0, 0), new Point(0, -1), new Point(1, -1) }; //Z
                case 2:
                    czyObrot = true;
                    return new Point[] { new Point(0, 0), new Point(-1, -1), new Point(0, -1), new Point(1, 0) }; //S
                case 3:
                    czyObrot = true;
                    return new Point[] { new Point(-1, -1), new Point(-1, 0), new Point(0, 0), new Point(1, 0) }; //L
                case 4:
                    czyObrot = true;
                    return new Point[] { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(1, -1) }; //J
                case 5:
                    czyObrot = true;
                    return new Point[] { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(2, 0) }; //I
                case 6:
                    czyObrot = false;
                    return new Point[] { new Point(0, 0), new Point(1, 0), new Point(0, -1), new Point(1, -1) }; //O
                default:
                    return null;
            }
        }
        private Brush LosowyKolor()
        {
            Random random = new Random();
            switch (random.Next(0, /*6*/1))
            {
                case 0:
                    return Brushes.Red;
                case 1:
                    return Brushes.Blue;
                case 2:
                    return Brushes.Green;
                case 3:
                    return Brushes.Yellow;
                case 4:
                    return Brushes.Purple;
                case 5:
                    return Brushes.Brown;
                default:
                    return null;
            }
        }
        public void Lewo()
        {
            polozenie.X -= 1;
        }
        public void Prawo()
        {
            polozenie.X += 1;
        }
        public void Dol()
        {
            polozenie.Y += 1;
        }
        public void Gora()
        {
            if (czyObrot)
            {
                for (int i = 0; i < ksztalt.Length; i++)
                {
                    double x = ksztalt[i].X;
                    ksztalt[i].X = ksztalt[i].Y * -1;
                    ksztalt[i].Y = x;
                }
            }
        }
    }
}
