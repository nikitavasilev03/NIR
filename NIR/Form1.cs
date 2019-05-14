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

namespace NIR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            double[] data = WaveReader.GetWAVasDoubleArray("Test1.wav");
            Complex[] cData = new Complex[data.Length];
            for (int i = 0; i < cData.Length; i++)
                cData[i] = new Complex(data[i], 0);
            Complex[][] ccData = FFT.GetBlocks(cData, 16);
            Complex[] fData = FFT.fft(ccData[0]);
            Complex[] rData = FFT.nfft(fData);

            chart1.ChartAreas.Add(new ChartArea("Default"));

            int[] xData = new int[rData.Length];
            double[] yData = new double[rData.Length];
            for (int i = 0; i < xData.Length; i++)
            {
                xData[i] = (i + 1);
                yData[i] = fData[i].Magnitude;
            }

            // Добавим линию, и назначим ее в ранее созданную область "Default"
            chart1.Series.Add(new Series("StepaP1di"));
            chart1.Series["StepaP1di"].ChartArea = "Default";
            chart1.Series["StepaP1di"].ChartType = SeriesChartType.Column;
            chart1.Series["StepaP1di"].Color = Color.Pink;
            chart1.Series["StepaP1di"].Points.DataBindXY(xData, yData);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
