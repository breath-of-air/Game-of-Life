using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int resolution;
        private GameEngine gameEngine;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            if (timer1.Enabled)
                return;

            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            bStart.Enabled = false;
            resolution = (int)nudResolution.Value;


            gameEngine = new GameEngine
                (
                    rows: pictureBox1.Height / resolution,
                    cols: pictureBox1.Width / resolution,
                    density: (int)nudDensity.Minimum + (int)nudDensity.Maximum - (int)nudDensity.Value
                );


            Text = $"Generation {gameEngine.CurrentGeneration}";

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            timer1.Start();
        }

        private void StopGame()
        {
            if (!timer1.Enabled)
                return;
            else
                timer1.Stop();

            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
            bStart.Enabled = true;
        }

        private void ContinueGame()
        {
            if (timer1.Enabled)
                return;

            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            bStart.Enabled = true;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            timer1.Start();
        }

        private void DrawGeneration()
        {
            graphics.Clear(Color.Black);

            var field = gameEngine.GetCurrentGeneration();

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if(field[x,y])
                    graphics.FillRectangle(Brushes.Crimson, x * resolution, y * resolution, resolution - 1, resolution - 1);
                }
            }

            pictureBox1.Refresh();
            Text = $"Generation {gameEngine.CurrentGeneration}";

            gameEngine.NextGeneration();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawGeneration();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
            DrawGeneration();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }
        private void bContinue_Click(object sender, EventArgs e)
        {
            ContinueGame();
            DrawGeneration();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;

            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;

                gameEngine.AddCell(x, y);
            }

            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;

                gameEngine.RemoveCell(x, y);
            }
        }

    }
}
