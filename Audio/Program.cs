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
            byte[] buffer;
            using (WaveFileReader reader = new WaveFileReader("Test1.wav"))
            {
                buffer = new byte[reader.Length];
                reader.Read(buffer, 0, buffer.Length);
                reader.Close();
            }
            double[] dataAplituds = readAmplitudeValues(buffer);
            prepare("Test1.wav");
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

        public static double[] readAmplitudeValues(byte[] buffer, bool isBigEndian = true)
        {
            int MSB, LSB; // старший и младший байты
            double[] data = new double[buffer.Length / 2];

            for (int i = 0; i < data.Length; i++)
            {
                if (isBigEndian) // задает порядок байтов во входном сигнале
                {
                    // первым байтом будет MSB
                    MSB = buffer[2 * i];
                    // вторым байтом будет LSB
                    LSB = buffer[2 * i + 1];
                }
                else
                {
                    // наоборот
                    LSB = buffer[2 * i];
                    MSB = buffer[2 * i + 1];
                }
                // склеиваем два байта, чтобы получить 16-битное вещественное число
                // все значения делятся на максимально возможное - 2^15
                data[i] = ((MSB << 8) | LSB) / 32768.0;
            }
            return data;
        }
    }
}