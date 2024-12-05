using NAudio.Wave;

namespace Maze
{
    internal class AudioPlayer
    {
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;

        public void PlaySound(string filePath)
        {
            Thread playThread = new Thread(() =>
            {
                waveOutDevice = new WaveOutEvent();
                audioFileReader = new AudioFileReader(filePath);
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
                while (waveOutDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            });

            playThread.Start();
        }

        public void Stop()
        {
            waveOutDevice?.Stop();
            waveOutDevice?.Dispose();
            audioFileReader?.Dispose();
        }
    }
}