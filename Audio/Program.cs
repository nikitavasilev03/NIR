using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

using static PL.Console.Out;

namespace Audio
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (WaveFileReader reader = new WaveFileReader("Test1.wav"))
            //{
            //    do
            //    {
            //        byte x = reader.Re
            //    } while
            //}
            Print(prepare("Test1.wav"));
        }
        public static double[] prepare(string wavePath)

        {
            double[] data;
            byte[] wave;
            System.IO.FileStream WaveFile = System.IO.File.OpenRead(wavePath);
            wave = new byte[WaveFile.Length];
            data = new double[(wave.Length - 44) / 4];//shifting the headers out of the PCM data;
            WaveFile.Read(wave, 0, Convert.ToInt32(WaveFile.Length));//read the wave file into the wave variable

                                                                     /***********Converting and PCM accounting***************/
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (BitConverter.ToInt32(wave, 44 + i * 4)) / 4294967296.0;
            }

            return data;
        }
    }
}