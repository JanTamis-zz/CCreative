using NAudio.Wave.SampleProviders;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using static CCreative.Math;

namespace CCreative
{
    public class PAudio
    {
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;
        
        public PAudio(string filePath)
        {
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(filePath);

            waveOutDevice.Init(audioFileReader);
        }

        public void Play()
        {
            waveOutDevice.Play();
        }

        public void Pause()
        {
            waveOutDevice.Pause();
        }

        public void Stop()
        {
            waveOutDevice.Stop();
        }

        public void Cue(double seconds)
        {
            Stop();
            audioFileReader.CurrentTime = TimeSpan.FromSeconds(seconds);
        }

        public void Amp(double volume)
        {
            audioFileReader.Volume = (float)volume;
        }

        public TimeSpan Duration()
        {
            return audioFileReader.TotalTime;
        }
    }

    public class WhiteNoise
    {

        WhiteNoise()
        {
            
        }
    }
}
