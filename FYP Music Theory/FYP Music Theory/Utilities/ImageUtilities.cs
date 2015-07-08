using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FYP_Music_Theory.Utilities
{
    internal static class ImageUtilities
    {
        public static ImageSource ImageToImageSource(Image image)
        {
            MemoryStream memoryStream = new MemoryStream();

            image.Save(memoryStream, ImageFormat.Png);

            memoryStream.Position = 0;

            BitmapImage imageSource = new BitmapImage();

            imageSource.BeginInit();

            imageSource.StreamSource = memoryStream;

            imageSource.EndInit();

            return imageSource;
        }
    }
}