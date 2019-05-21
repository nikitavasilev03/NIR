using System;
using System.Numerics;
using Audio;

namespace NIR
{
    public class Convertor
    {
        public static void Convert(string path, string out_path, int size_block, double chageVal)
        {
            double[] data = Wave.Read(path, out byte[] header);
            Complex[] c_data = new Complex[data.Length];
            for (int i = 0; i < c_data.Length; i++)
                c_data[i] = new Complex(data[i], 0);
            Complex[][] c_data_bloks = FFT.GetBlocks(c_data, size_block);
            for (int k = 0; k < c_data_bloks.Length; k++)
            {
                Complex[] input_data = c_data_bloks[k];
                FFT.fft(ref input_data);
                for (int i = 0; i < input_data.Length; i++)
                    input_data[i] += chageVal;
                FFT.fft(ref input_data, true);
            }
            Wave.Record(out_path, header, FFT.RemoveImaginaryToDouble(FFT.UnionBlocks(c_data_bloks)));
        }
    }
}
