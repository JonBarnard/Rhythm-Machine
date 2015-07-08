using System;
using System.Collections.Generic;
using System.Linq;
using FYP_Music_Theory.Utilities;

namespace FYP_Music_Theory.Domain
{
    public sealed class Question
    {
        private readonly Phrase correctPhrase;
        private readonly List<Phrase> displayedPhrases;

        public Question(IEnumerable<Phrase> allPhrases, Difficulty questionDifficulty)
        {
            IShuffler<Phrase> phraseShuffler = new FisherYatesShuffle<Phrase>();

            IEnumerable<Phrase> phrasesThatMatchDifficulty = allPhrases.Where(phrase => questionDifficulty.HasFlag(phrase.Difficulty));

            IEnumerable<Phrase> shuffledPhrases = phraseShuffler.Shuffle(phrasesThatMatchDifficulty, 4);

            displayedPhrases = shuffledPhrases.ToList();

            Random randomPhraseGenerator = new Random();

            int randomPhraseIndex = randomPhraseGenerator.Next(0, displayedPhrases.Count());

            correctPhrase = displayedPhrases[randomPhraseIndex];
        }

        public List<Phrase> DisplayedPhrases
        {
            get { return displayedPhrases; }
        }

        public Phrase CorrectPhrase
        {
            get { return correctPhrase; }
        }

        public int Attempts { get; private set; }

        public bool IsSelectedPhraseCorrect(int selectedPhraseTag)
        {
            Phrase shuffledPhrase = displayedPhrases[selectedPhraseTag - 1];

            return shuffledPhrase.Equals(correctPhrase);
        }

        public void NewAttempt()
        {
            Attempts++;
        }
    }
}