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
                string resourceName = "CookieClicker.Songs.click1.mp3"; // Assurez-vous que ce nom est correct
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (resourceStream == null)
                        throw new Exception("Audio resource not found.");

                    // Créer un fichier temporaire pour stocker le son
                    string tempFile = Path.GetTempFileName() + ".flac";

                    using (FileStream fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                    {
                        resourceStream.CopyTo(fileStream);
                    }

                    // Lire et jouer le fichier audio temporaire
                    waveOut = new WaveOutEvent();
                    audioFileReader = new MediaFoundationReader(tempFile);

                    waveOut.Init(audioFileReader);
                    waveOut.Play();

                    // Nettoyer le fichier temporaire après la lecture
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