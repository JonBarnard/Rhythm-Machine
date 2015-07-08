using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FYP_Music_Theory.AudioPlayer;
using FYP_Music_Theory.Domain;

namespace FYP_Music_Theory.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const int TotalQuestions = 10;
        private readonly IAudioPlayer audioPlayer = new Mp3Player();
        private readonly Bank bank = new Bank();
        private readonly List<Image> images;
        private IGame game;
        private int playbackBpm;
        private readonly IEnumerable<Phrase> phrasesToUse; 

        public MainWindow(IEnumerable<Phrase> loadedPhrases)
        {
            phrasesToUse = loadedPhrases;

            CreateNewGame();

            InitializeComponent();

            UpdateSilverDisplay();

            images = new List<Image> {Image1, Image2, Image3, Image4};
        }

        private void CreateNewGame()
        {
            game = new Game(CurrentSelectedGameDifficulty(), phrasesToUse);
        }

        private void ImageClicked(object sender, RoutedEventArgs e)
        {
            if (game.HasQuestion)
            {
                Button imageClicked = (Button) e.Source;
                string imageTag = imageClicked.Tag.ToString();

                bool isCorrect = game.IsPhraseCorrectForQuestion(imageTag);

                // IAnswerHandler answerHandler = AnswerHandlerFactory.GetAnswerHandler(isCorrect);

                // answerHandler.HandleAnswer();

                if (isCorrect)
                {
                    if (game.AnsweredQuestionCount < TotalQuestions)
                    {
                        imageClicked.Background = new SolidColorBrush(Colors.LimeGreen);
                        MessageBox.Show("Correct, press Ok for next question!");
                        game.NewQuestion(images);
                    }
                    else
                    {
                        HandleGameFinish();
                    }
                }
                else
                {
                    imageClicked.Background = new SolidColorBrush(Colors.DarkRed);
                    MessageBox.Show("Wrong, press Ok to try again...");
                }

                imageClicked.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void HandleGameFinish()
        {
            CoinGenerator coinGenerator = new CoinGenerator();
            coinGenerator.AddGameScoreToBank(bank, game.QuestionAttempts);
            UpdateSilverDisplay();

            ScoreWindow scoreWindow = new ScoreWindow(game);
            scoreWindow.ShowDialog();

            CreateNewGame();
        }

        private void UpdateSilverDisplay()
        {
            SilverCoinCount.Content = string.Format("{0} x", bank.Silver);
        }

        private void StartClicked(object sender, RoutedEventArgs e)
        {
            game.NewQuestion(images);
        }

        private void OnPlayPhraseClicked(object sender, RoutedEventArgs e)
        {
            if (game.HasQuestion)
            {
                Audio correctAudio = game.CorrectAudio(playbackBpm);

                audioPlayer.Play(correctAudio.AudioData);
            }
            else
            {
                MessageBox.Show("Please load game first");
            }
        }

        private void Close_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void OnDifficultyChanged(object sender, RoutedEventArgs e)
        {
            Difficulty newGameDifficulty = CurrentSelectedGameDifficulty();

            game.Difficulty = newGameDifficulty;
        }

        private Difficulty CurrentSelectedGameDifficulty()
        {
            Difficulty difficultySelected = Difficulty.None;

            if (EasyCheckBox != null && EasyCheckBox.IsChecked != null && EasyCheckBox.IsChecked.Value)
            {
                difficultySelected |= Difficulty.Easy;
            }

            if (MediumCheckBox != null && MediumCheckBox.IsChecked != null && MediumCheckBox.IsChecked.Value)
            {
                difficultySelected |= Difficulty.Medium;
            }

            if (HardCheckBox != null && HardCheckBox.IsChecked != null && HardCheckBox.IsChecked.Value)
            {
                difficultySelected |= Difficulty.Hard;
            }

            return difficultySelected;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton buttonChanged = sender as RadioButton;

            if (buttonChanged != null)
            {
                int newPlaybackBpm = int.Parse(buttonChanged.Tag.ToString());

                playbackBpm = newPlaybackBpm;
            }
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}