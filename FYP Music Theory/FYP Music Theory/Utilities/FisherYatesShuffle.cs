using System;
using System.Collections.Generic;
using System.Linq;

namespace FYP_Music_Theory.Utilities
{
    /// <summary>
    /// Implementation of a Fisher-Yates shuffle.
    /// See http://www.dotnetperls.com/fisher-yates-shuffle
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public sealed class FisherYatesShuffle<T> : IShuffler<T>
    {
        private readonly Random random = new Random();

        /// <summary>
        /// Shuffle a collection using the Fisher-Yates algorithm.
        /// </summary>
        /// <param name="collection">The collection to shuffle.</param>
        /// <param name="itemCount">How many items to return.</param>
        /// <returns></returns>
        public IEnumerable<T> Shuffle(IEnumerable<T> collection, int itemCount)
        {
            List<T> shuffledPhrases = new List<T>(collection);

            int n = shuffledPhrases.Count;
            for (int i = 0; i < n; i++)
            {
                // NextDouble returns a random number between 0 and 1.
                // ... It is equivalent to Math.random() in Java.
                int r = i + (int) (random.NextDouble()*(n - i));
                T t = shuffledPhrases[r];
                shuffledPhrases[r] = shuffledPhrases[i];
                shuffledPhrases[i] = t;
            }

            return shuffledPhrases.Take(itemCount);
        }
    }
}