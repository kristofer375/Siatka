using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Siatka
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        Random random = new Random();
        int x = 0;
        int lol = 0;
        bool isStart;
        List<Rectangle> klocki = new List<Rectangle>();
        int counter = 0;
        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Start();
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                Grid.SetColumn(klocki[counter], 1);
            }
            else if (e.Key == Key.Right)
            {
                Grid.SetColumn(klocki[counter], 0);
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (isStart && lol < 16)
            {
                Grid.SetRow(klocki[counter], lol++);
            }
            else if (isStart)
            {
                lol = 0;
                counter++;
                isStart = false;
            }
        }

        public class Board
        {
            private int rows;
            private int cols;
            private int score;
            private Label[,] BlockContols;
            public Board(Grid TetrisGrid)
            {
                rows = TetrisGrid.RowDefinitions.Count;
                cols = TetrisGrid.ColumnDefinitions.Count;
            }
        }


        private void OnClick(object sender, RoutedEventArgs e)
        {
            //if (counter > 0)
            //    W_Dół(0);
            klocki.Add(new Rectangle());
            grid1.Children.Add(klocki[counter]);
            Grid.SetColumn(klocki[counter], x/*random.Next(0, 6)*/);
            Grid.SetColumnSpan(klocki[counter], 3);
            switch (Siatka.SelectedIndex)
            {
                case 1:
                    klocki[counter].Fill = Brushes.Blue;
                    break;
                case 2:
                    klocki[counter].Fill = Brushes.Green;
                    break;
                case 3:
                    klocki[counter].Fill = Brushes.Yellow;
                    break;
                case 4:
                    klocki[counter].Fill = Brushes.Purple;
                    break;
                case 5:
                    klocki[counter].Fill = Brushes.Brown;
                    break;
                default:
                    klocki[counter].Fill = Brushes.Red;
                    break;
            }
            border.BorderBrush = Brushes.Black;
            isStart = true;
        }
        public void W_Dół(int plus)
        {
            while (plus < 16)
            {
                //Thread.Sleep(1000);
                Grid.SetRow(klocki[0], plus++);
            }
        }
        public void W_Dół(int counter, int plus)
        {
            if (counter < 16)
            {
                if (counter - 1 > 0)
                    W_Dół(counter - 1, plus + 1);
                Grid.SetRow(klocki[counter - 1], plus + 1);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && x > 0)
            {
                Grid.SetColumn(klocki[counter], --x);
            }
            else if (e.Key == Key.Right && x < 6)
            {
                Grid.SetColumn(klocki[counter], ++x);
            }
        }
    }
}
