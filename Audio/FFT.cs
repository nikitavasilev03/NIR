using System;
using System.Numerics;
using System.Collections.Generic;

namespace Audio
{
    public class FFT
    {
        public static void fft(ref Complex[] data, bool invert = false)
        {
            int n = data.Length;
            if (n == 1) return;

            Complex[] a0 = new Complex[n / 2];
            Complex[] a1 = new Complex[n / 2];
            for (int i = 0, j = 0; i < n; i += 2, ++j)
            {
                a0[j] = data[i];
                a1[j] = data[i + 1];
            }
            fft(ref a0, invert);
            fft(ref a1, invert);

            double ang = 2 * Math.PI / n * (invert ? -1 : 1);
            Complex w = new Complex(1, 0);  
            Complex wn = new Complex(Math.Cos(ang), Math.Sin(ang));
            for (int i = 0; i < n / 2; ++i)
            {
                data[i] = a0[i] + w * a1[i];
                data[i + n / 2] = a0[i] - w * a1[i];
                if (invert)
                {
                    data[i] /= 2;
                    data[i + n / 2] /= 2;
                }
                w *= wn;
            }
        }

        public static Complex[][] GetBlocks(Complex[] data, int size_block)
        {
            int n = data.Length / size_block;
            if (n % size_block > 0)
                n++;
            Complex[][] blocks = new Complex[n][];
            for (int i = 0; i < n; i++)
            {
                Complex[] block = new Complex[size_block];
                for (int j = 0; j < size_block; j++)
                {
                    if (i * size_block + j >= data.Length)
                        block[j] = new Complex(0, 0);
                    else
                        block[j] = data[i * size_block + j];
                }
                blocks[i] = block;
            }
            return blocks;
        }

        public static Complex[] UnionBlocks(Complex[][] blocks)
        {
            List<Complex> data = new List<Complex>();
            foreach (var block in blocks)
                foreach (var item in block)
                    data.Add(item);
            return data.ToArray();
        }

        public static Complex[] RemoveImaginary(Complex[] data)
        {
            Complex[] outData = new Complex[data.Length];
            for (int i = 0; i < data.Length; i++)
                outData[i] = new Complex(data[i].Real, 0);
            return outData;
        }

        public static double[] RemoveImaginaryToDouble(Complex[] data)
        {
            double[] outData = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                outData[i] = data[i].Real;
            return outData;
        }
    }
}