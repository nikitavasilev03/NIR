using System;
using System.IO;
using System.Numerics;
using System.Collections.Generic;

using static PL.Console.Out; // Подключение команды Print, для консольного вывода

namespace Audio
{
    class Program
    {
        public const int SIZE_BLOCK = 1024;

        static void Main(string[] args)
        {
            double[] data = Wave.Read("Test1.wav", out byte[] header);
            Complex[] c_data = new Complex[data.Length];
            for (int i = 0; i < c_data.Length; i++)
                c_data[i] = new Complex(data[i], 0);
            Complex[][] c_data_bloks = FFT.GetBlocks(c_data, SIZE_BLOCK);

            for (int k = 0; k < c_data_bloks.Length; k++)
            {
                Complex[] input_data = c_data_bloks[k];
                FFT.fft(ref input_data);
                for (int i = 0; i < input_data.Length; i++)
                    input_data[i] += 0.4;
                FFT.fft(ref input_data, true);
            }

            Wave.Record("NewWav.wav", header, FFT.RemoveImaginaryToDouble(FFT.UnionBlocks(c_data_bloks)));
        }

    }
}