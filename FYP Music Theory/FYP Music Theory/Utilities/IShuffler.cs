using System.Collections.Generic;

namespace FYP_Music_Theory.Utilities
{
    /// <summary>
    /// A Collection shuffler.
    /// </summary>
    /// <typeparam name="T">The collection items.</typeparam>
    public interface IShuffler<T>
    {
        /// <summary>
        /// Shuffle an collection with n-elements.
        /// </summary>
        /// <param name="collection">The collection to shuffle.</param>
        /// <param name="itemCount">How many items to return.</param>
        /// <returns>A shuffled collection with n-items.</returns>
        IEnumerable<T> Shuffle(IEnumerable<T> collection, int itemCount);
    }
}