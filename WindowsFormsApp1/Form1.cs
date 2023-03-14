using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {

        private Graphics graph;
        private bool[,] marks;
        int gen = 0; 
        int col; 
        int row;
        public Form1()
        {
            InitializeComponent();
        }

        int clicks = 0; 

        private void btstart_Click(object sender, EventArgs e)
        {
            if (clicks % 2 == 1)
            {
                btnew.Enabled = false;
                timer1.Start(); 
            }
            else
            {
                timer1.Stop(); 
                btnew.Enabled = true; 
            }
            clicks++; 
        }

        private void btnew_Click(object sender, EventArgs e)
        {
            graph = CreateGraphics();
            graph.Clear(Color.White);
            gen = 1; 
            label1.Text = $"Generation {gen}";
            clicks = 0;
            col = pictureBox1.Width; 
            row = pictureBox1.Height;
            marks = new bool[col, row];
            Random rand = new Random();
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    marks[i, j] = rand.Next(0, 100) > 90; 
                }
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(pictureBox1.Image);
            timer1.Start(); 
        }

        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var c = (x + i + col) % col;
                    var r = (y + j + row) % row;
                    var isSelfChecking = c == x && r == y;
                    var hasLife = marks[c, r];
                    if (hasLife && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graph.Clear(Color.White);
            var newmarks = new bool[col, row];
            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);
                    var hasLife = marks[x, y];

                    if (!hasLife && neighboursCount == 3)
                    {
                        newmarks[x, y] = true;
                    }
                    else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                    {
                        newmarks[x, y] = false;
                    }
                    else
                    {
                        newmarks[x, y] = marks[x, y];
                    }
                    if (hasLife)
                    {
                        graph.FillRectangle(Brushes.Green, x * 2, y * 2, 1, 1);
                    }
                }
            }
            marks = newmarks;
            pictureBox1.Refresh();
            gen++; 
            label1.Text = $"Generation {gen}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
