using System;
using System.IO;
using System.Reflection;
using NAudio.Wave;

namespace CookieClicker.View
{
    public class AudioPlay
    {
        private static IWavePlayer? waveOut;
        private static WaveStream? audioFileReader;
        private static LoopStream? loopStream;

        public static void PlayClickSound()
        {
            PlaySound("CookieClicker.Songs.click1.mp3");
        }

        public static void BuyingSongs()
        {
            PlaySound("CookieClicker.Songs.buy1.mp3");
        }

        public static void SellingSongs()
        {
            PlaySound("CookieClicker.Songs.sell1.mp3");
        }

        public static void PlayGoldenCookieSound()
        {
            PlaySound("CookieClicker.Songs.GoldenSound.mp3");
        }

        public static void PlayBackgroundMusic()
        {
            string resourceName = "CookieClicker.Songs.BackgroundMusic.mp3";
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream? resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                    throw new Exception("Audio resource not found.");

                string tempFile = Path.GetTempFileName() + ".mp3";

                using (FileStream fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }

                waveOut = new WaveOutEvent();
                audioFileReader = new MediaFoundationReader(tempFile);
                loopStream = new LoopStream(audioFileReader);

                waveOut.Init(loopStream);
                waveOut.Volume = 0.1f;
                waveOut.Play();
            }
        }

        public static void StopBackgroundMusic()
        {
            waveOut?.Stop();
            audioFileReader?.Dispose();
            waveOut?.Dispose();
        }

        private static void PlaySound(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream? resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                    throw new Exception("Audio resource not found.");

                string tempFile = Path.GetTempFileName() + ".mp3";

                using (FileStream fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }

                waveOut = new WaveOutEvent();
                audioFileReader = new MediaFoundationReader(tempFile);

                waveOut.Init(audioFileReader);
                waveOut.Play();

                waveOut.PlaybackStopped += (s, e) =>
                {
                    audioFileReader.Dispose();
                    waveOut.Dispose();
                    File.Delete(tempFile);
                };
            }
        }
    }
}
