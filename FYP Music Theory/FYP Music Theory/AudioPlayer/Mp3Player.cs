using System;
using System.IO;
using System.Windows;
using NAudio.Wave;

namespace FYP_Music_Theory.AudioPlayer
{
    public sealed class Mp3Player : IAudioPlayer
    {
        private WaveStream mainOutputStream;
        private IWavePlayer waveOut;

        public void Play(byte[] mediacontent)
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Paused)
                {
                    waveOut.Play();
                    return;
                }

                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    CloseWaveOut();
                }
            }

            // we are in a stopped state
            // TODO: only re-initialise if necessary

            try
            {
                CreateWaveOut();
            }
            catch (Exception driverCreateException)
            {
                MessageBox.Show(String.Format("{0}", driverCreateException.Message));
                return;
            }

            try
            {
                mainOutputStream = CreateInputStream(mediacontent);
            }
            catch (Exception createException)
            {
                MessageBox.Show(String.Format("{0}", createException.Message), "Error Loading Sound Byte[]");
                return;
            }

            try
            {
                waveOut.Init(mainOutputStream);
            }
            catch (Exception initException)
            {
                MessageBox.Show(String.Format("{0}", initException.Message), "Error Initializing Output");
                return;
            }

            waveOut.Play();
        }

        public void Dispose()
        {
            CloseWaveOut();
        }

        private void CloseWaveOut()
        {
            // TODO: Dispose!
        }

        private void CreateWaveOut()
        {
            CloseWaveOut();
            const int Latency = 150;
            waveOut = new DirectSoundOut(Latency);
        }

        private static WaveStream CreateInputStream(byte[] mediacontent)
        {
            var memoryStream = new MemoryStream(mediacontent);
            var mp3Reader = new Mp3FileReader(memoryStream);

            var inputStream = new WaveChannel32(mp3Reader);

            return inputStream;
        }
    }
}