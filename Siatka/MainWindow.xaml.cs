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
        Plansza plansza;
        Txt txt = new Txt();
        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);

            txt.Odczyt();

            plansza = new Plansza(grid1);
            //Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            punkty.Content = plansza.WezPunkty().ToString("0000000000");
            wypelnienia.Content = plansza.WezWypelnienia().ToString("0000000000");
            plansza.TetrominoDol();
            if (plansza.CzyPorazka())
            {
                gameTimer.Stop();
                txt.Dodaj(wypelnienia.Content.ToString());
                txt.Zapisz();
            }
        }
        private void Start()
        {
            grid1.Children.Clear();
            plansza = new Plansza(grid1);
            gameTimer.Start();
        }
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && gameTimer.IsEnabled)
            {
                plansza.TetrominoLewo();
            }
            else if (e.Key == Key.Right && gameTimer.IsEnabled)
            {
                plansza.TetrominoPrawo();
            }
            else if (e.Key == Key.Up && gameTimer.IsEnabled)
            {
                plansza.TetrominoObrot();
            }
            else if (e.Key == Key.Down && gameTimer.IsEnabled)
            {
                plansza.TetrominoDol();
            }
            else if (e.Key == Key.F1)
            {
                Start();
            }
            else if (e.Key == Key.F2)
            {
                if (gameTimer.IsEnabled)
                {
                    gameTimer.Stop();
                }
                else
                {
                    gameTimer.Start();
                }
            }
            else if (e.Key == Key.F3)
            {
                plansza.Siatka(grid1, true);
            }
            else if (e.Key == Key.F4)
            {
                plansza.Siatka(grid1, false);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (gameTimer.IsEnabled)
            {
                gameTimer.Stop();
            }
            else
            {
                gameTimer.Start();
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            plansza.Siatka(grid1, true);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            plansza.Siatka(grid1, false);
        }
    }
}
