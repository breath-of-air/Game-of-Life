using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Game_of_Life
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int resolution;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            resolution = (int)nudResolution.Value;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            graphics.FillRectangle(Brushes.Crimson, 0, 0, resolution, resolution);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {

        }
    }
}
