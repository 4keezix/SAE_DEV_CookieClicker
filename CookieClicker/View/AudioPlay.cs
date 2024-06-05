using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using NAudio.Wave;
using System;
namespace CookieClicker.View
{
    public class AudioPlay
    {

        
            private static IWavePlayer waveOut;
            private static WaveStream audioFileReader;

            public static void PlayClickSound()
            {
                string resourceName = "CookieClicker.Songs.click1.mp3"; 
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
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
        public static void BuyingSongs()
        {
            string resourceName = "CookieClicker.Songs.buy1.mp3";
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
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

        public static void SellingSongs()
        {
            string resourceName = "CookieClicker.Songs.sell1.mp3";
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
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