using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Windows;
using FYP_Music_Theory.Domain;
using FYP_Music_Theory.Utilities;
using FYP_Music_Theory.View;
using SplashScreen = FYP_Music_Theory.View.SplashScreen;

namespace FYP_Music_Theory
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            ResourceSet resourceSet = FYP_Music_Theory.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            PhraseMaker phraseMaker = new PhraseMaker(resourceSet);

            SplashScreen splashScreen = new SplashScreen(phraseMaker);

            splashScreen.Show();

            IEnumerable<Phrase> loadedPhrases = await phraseMaker.CreatePhrasesAsync();

            MainWindow mainWindow = new MainWindow(loadedPhrases);

            mainWindow.Show();
            splashScreen.Close();

            base.OnStartup(e);
        }
    }
}