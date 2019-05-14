using System;
using System.Numerics;

using static PL.Console.Out; // Подключение команды Print, для консольного вывода

namespace Audio
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] data = WaveReader.GetWAVasDoubleArray("Test1.wav");
            Complex[] cData = new Complex[data.Length];
            for (int i = 0; i < cData.Length; i++)
                cData[i] = new Complex(data[i], 0);
            Complex[][] ccData = FFT.GetBlocks(cData, 1024);
            Complex[] fData = FFT.fft(ccData[0]);
            Complex[] rData = FFT.offt(fData);
        }
    }
}