using System;

namespace FYP_Music_Theory.Utilities
{
    /// <summary>
    /// Something that can be progressed (i.e. a loader) 
    /// </summary>
    public interface IProgressable
    {
        /// <summary>
        /// The total items to be loaded.
        /// </summary>
        int TotalItems { get; }

        /// <summary>
        /// Fires when an an item has been loaded with its item position.
        /// </summary>
        event EventHandler<int> ItemLoaded;
    }
}