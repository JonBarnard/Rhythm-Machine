using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Threading.Tasks;
using FYP_Music_Theory.Domain;

namespace FYP_Music_Theory.Utilities
{
    public sealed class PhraseMaker: IProgressable
    {
        private readonly ResourceSet resourceSet;

        public PhraseMaker(ResourceSet resourceSet)
        {
            this.resourceSet = resourceSet;
            TotalItems = GetPhraseCount();
        }

        public int TotalItems { get; private set; }

        public event EventHandler<int> ItemLoaded;

        public Task<IEnumerable<Phrase>> CreatePhrasesAsync()
        {
            return Task.Factory.StartNew(() => CreatePhrases());
        }

        private IEnumerable<Phrase> CreatePhrases()
        {
            int loadedPhrases = 0;

            List<Phrase> phrases = new List<Phrase>();

            foreach (DictionaryEntry possibleImageResource in resourceSet)
            {
                var imageFileName = (string) possibleImageResource.Key;

                if (imageFileName != "duck")
                {
                    if (imageFileName != "silverstar")
                    {
                        if (imageFileName != "goldstar")
                        {
                            if (IsAnImageFile(imageFileName))
                            {
                                List<Audio> phraseAudios = new List<Audio>();

                                // The resource is an image
                                foreach (DictionaryEntry possibleAudioResource in resourceSet)
                                {
                                    var audioFileName = (string) possibleAudioResource.Key;

                                    // Find all audio files that are related to this image
                                    if (IsAnAudioFile(audioFileName))
                                    {
                                        // We found an audio file.. Now, is it related to the image?
                                        if (DoesAudioFileBelongToImage(imageFileName, audioFileName))
                                        {
                                            // We found an audio file that belongs to the image. Add it to the audio files list.
                                            Audio audio = CreateAudio(audioFileName, (byte[]) possibleAudioResource.Value);

                                            phraseAudios.Add(audio);
                                        }
                                    }
                                }


                                // Search for audio files has finished. Create the Phrase object with the audios.
                                Image phraseImage = (Image) possibleImageResource.Value;
                                Phrase phrase = CreatePhrase(imageFileName, phraseImage, phraseAudios);
                                phrases.Add(phrase);

                                loadedPhrases++;

                                EventUtility.SafeFireEvent(ItemLoaded, this, loadedPhrases);
                            }
                        }
                    }
                }
            }

            return phrases;
        }

        private int GetPhraseCount()
        {
            int imageCount = 0;

            foreach (DictionaryEntry possibleImageResource in resourceSet)
            {
                var imageFileName = (string) possibleImageResource.Key;

                if (imageFileName != "duck" && imageFileName != "silverstar" && imageFileName != "goldstar")
                {
                    if (IsAnImageFile(imageFileName))
                    {
                        imageCount++;
                    }
                }
            }

            return imageCount;
        }

        private static bool IsAnImageFile(string fileName)
        {
            return !DoesFileNameContainBpm(fileName);
        }

        private static bool IsAnAudioFile(string fileName)
        {
            return DoesFileNameContainBpm(fileName);
        }

        private static bool DoesFileNameContainBpm(string fileName)
        {
            try
            {
                var components = fileName.Split('_');
                string bpmFilenameComponent = components[1];
                int bpm;
                return int.TryParse(bpmFilenameComponent, out bpm);
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        private static bool DoesAudioFileBelongToImage(string imageFileName, string audioFileName)
        {
            var imageComponents = imageFileName.Split('_');
            var audioComponents = audioFileName.Split('_');
            return audioComponents[0].Equals(imageComponents[0]);
        }

        private static Audio CreateAudio(string audioFileName, byte[] audioData)
        {
            var bpmComponents = audioFileName.Split('_');
            int bpm;
            int.TryParse(bpmComponents[1], out bpm);
            return new Audio(audioData, bpm);
        }

        private static Phrase CreatePhrase(string imageFileName, Image phraseImage, List<Audio> phraseAudios)
        {
            var components = imageFileName.Split('_');
            string phraseComponent = components[1];
            Difficulty phraseDifficulty;
            bool didParseSuccessfully = Enum.TryParse(phraseComponent, out phraseDifficulty);

            if (!didParseSuccessfully)
            {
                throw new Exception("Difficulty could not be parsed.");
            }

            return new Phrase(phraseImage, phraseAudios, phraseDifficulty);
        }
    }
}