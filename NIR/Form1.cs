using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Audio;
using System.Numerics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Media;


namespace NIR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public const int SIZE_BLOCK = 1024;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        SoundPlayer playOld = new SoundPlayer("Test1.wav");
        SoundPlayer playNewer = new SoundPlayer("NewWav.wav");

        private void doMagic_Click(object sender, EventArgs e)
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

        private void playOriginal_Click(object sender, EventArgs e)
        {
            playOld.Play();
        }

        private void playNew_Click(object sender, EventArgs e)
        {
            playNewer.Play();
        }
    }
}
