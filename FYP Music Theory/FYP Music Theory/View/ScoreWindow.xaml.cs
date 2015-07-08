using System;
using System.Windows;
using FYP_Music_Theory.Domain;

namespace FYP_Music_Theory.View
{
    /// <summary>
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow
    {
        public ScoreWindow(IGame game)
        {
            int goldCount = 0;
            int silverCount = 0;
            int duckCount = 0;

            InitializeComponent();

            foreach (int attempts in game.QuestionAttempts)
            {
                if (attempts == 1)
                {
                    goldCount++;
                }
                if (attempts == 2)
                {
                    silverCount++;
                }
                if (attempts > 2)
                {
                    duckCount++;
                }
            }

            GoldCoinCount.Content = goldCount + "x";
            SilverCoinCount.Content = silverCount + "x";
            DuckCount.Content = duckCount + "x";   
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PlayAgainClicked(object sender, RoutedEventArgs e)
        {
            OnNewGameRequested();
        }

        public event EventHandler NewGameRequested;

        private void OnNewGameRequested()
        {
            var handler = NewGameRequested;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}