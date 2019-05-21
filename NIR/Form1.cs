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
        public const int SIZE_BLOCK = 1024;
        public const string OUT_WAV_FILE = "newwav.wav";

        private SoundPlayer playNew; //= new SoundPlayer("Test1.wav");
        private SoundPlayer playOriginal; //= new SoundPlayer("NewWav.wav");
        private string origenalFile;

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
                origenalFile = tbPathFileOriginal.Text;
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
            Convertor.Convert(origenalFile, OUT_WAV_FILE, size_block, chageVal);
            playNew = new SoundPlayer(OUT_WAV_FILE);
            groupBox4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (playNew != null)
                playNew.Play();
        }
    }
}
