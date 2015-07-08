using System.Windows;
using FYP_Music_Theory.Utilities;

namespace FYP_Music_Theory.View
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen
    {
        public SplashScreen(IProgressable contentToLoad)
        {
            InitializeComponent();

            contentToLoad.ItemLoaded += OnItemLoaded;

            PhraseLoadingProgress.Maximum = contentToLoad.TotalItems;
            TotalAssets.Text = contentToLoad.TotalItems.ToString();
            Asset.Text = "0";
        }

        void OnItemLoaded(object sender, int e)
        {
            Application.Current.Dispatcher.Invoke(() => UpdateProgress(e));
        }

        private void UpdateProgress(int e)
        {
            PhraseLoadingProgress.Value = e;
            Asset.Text = e.ToString();
        }
    }
}