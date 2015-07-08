using System;

namespace FYP_Music_Theory.AudioPlayer
{
    public interface IAudioPlayer : IDisposable
    {
        void Play(byte[] audio);
    }
}