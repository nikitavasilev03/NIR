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
        public const string OUT_WAV_FILE = "newwav.wav";

        private SoundPlayer playNew;
        private SoundPlayer playOriginal; 
        private string originalFile;

        private delegate double windowFunc(double i, double framesize);
        private windowFunc winFunc = WindowFunc.Rectangle; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bSwitchFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                tbPathFileOriginal.Text = openFileDialog1.FileName;
        }

        private void bOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                playOriginal = new SoundPlayer(tbPathFileOriginal.Text);
                groupBox2.Enabled = true;
                originalFile = tbPathFileOriginal.Text;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void playOriginal_Click(object sender, EventArgs e)
        {
            if (playOriginal != null)
                playOriginal.Play();
            else
                MessageBox.Show("Файл не открыт!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(comboBox1.Text, out int size_block))
            {
                MessageBox.Show("Неверный ввод данных!");
                return;
            }
            if (!double.TryParse(textBox2.Text, out double chageVal))
                chageVal = 0;

            double[] data = Wave.Read(originalFile, out byte[] header);
            Complex[] c_data = new Complex[data.Length];
            for (int i = 0; i < c_data.Length; i++)
                c_data[i] = new Complex(data[i], 0);
            Complex[][] c_data_bloks = FFT.GetBlocks(c_data, size_block);
            for (int k = 0; k < c_data_bloks.Length; k++)
            {
                Complex[] input_data = c_data_bloks[k];
                FFT.fft(ref input_data);
                if (checkBox1.Checked)
                    for (int i = 0; i < input_data.Length; i++)
                        input_data[i] /= input_data.Length;
                for (int i = 0; i < input_data.Length; i++)
                    input_data[i] *= winFunc(i, input_data.Length);

            }

            var out_block = c_data_bloks[0];
            double[] indexes = new double[out_block.Length];
            double[] values = new double[out_block.Length];
            for (int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = i + 1;
                values[i] = out_block[i].Magnitude;
            }

            chart1.Series.Clear();
            chart1.Series.Add(new Series("BlockFFT"));
            chart1.Series["BlockFFT"].ChartArea = "Default";
            chart1.Series["BlockFFT"].ChartType = SeriesChartType.Column;
            chart1.Series["BlockFFT"].Points.DataBindXY(indexes, values);

            for (int k = 0; k < c_data_bloks.Length; k++)
            {
                Complex[] input_data = c_data_bloks[k];
                FFT.fft(ref input_data, true);
            }

            Wave.Record(OUT_WAV_FILE, header, FFT.RemoveImaginaryToDouble(FFT.UnionBlocks(c_data_bloks)));

            playNew = new SoundPlayer(OUT_WAV_FILE);
            groupBox4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (playNew != null)
                playNew.Play();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) // прямоугольник
        {
            if (radioButton1.Checked)
                winFunc = WindowFunc.Rectangle;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) // гаусс
        {
            if (radioButton2.Checked)
                winFunc = WindowFunc.Gausse;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) // химмниг
        {
            if (radioButton3.Checked)
                winFunc = WindowFunc.Hamming;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) // ханн
        {
            if (radioButton4.Checked)
                winFunc = WindowFunc.Hann;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e) // блэкман
        {
            if (radioButton5.Checked)
                winFunc =  WindowFunc.BlackmannHarris;
        }
    }
}
