using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FYP_Music_Theory.Domain
{
    public class Phrase
    {
        private readonly List<Audio> audios;
        private readonly Image image;

        public Phrase(Image image, List<Audio> audios, Difficulty difficulty)
        {
            this.image = image;
            this.audios = audios;
            Difficulty = difficulty;
        }

        /// <summary>
        /// The Phrase you#re keybo'ard is fucked up..
        /// </summary>
        public Image Image
        {
            get { return image; }
        }

        public Difficulty Difficulty { get; private set; }

        public Audio GetAudio(int bpm)
        {
            return audios.FirstOrDefault(audio => audio.Bpm.Equals(bpm));
        }
    }
}