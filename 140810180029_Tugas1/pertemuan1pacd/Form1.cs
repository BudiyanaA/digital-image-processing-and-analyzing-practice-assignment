using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pertemuan1pacd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.bmp) | *.jpg; *.bmp";
            if (open.ShowDialog() == DialogResult.OK) 
            {
                Bitmap gambar = new Bitmap(open.FileName);
                pictureBox1.Image = new Bitmap(gambar, new Size(200,200));
            }
        }

        Bitmap bmp;
        private void button1_Click(object sender, EventArgs e)
        {
            // Menentukan pixel yang mau dilihat RGB-nya
            int x = Convert.ToInt16(textBox1.Text);
            int y = Convert.ToInt16(textBox2.Text);

            // memuat gambar dari picture box
            bmp = (Bitmap)pictureBox1.Image;

            //Mengambil nilai warna dari pixel yang ditentukan
            int r = bmp.GetPixel(x, y).R;
            int g = bmp.GetPixel(x, y).G;
            int b = bmp.GetPixel(x, y).B;

            //Menampilkan warna tiap pixel pada textbox
            textBox3.Text = r.ToString();
            textBox4.Text = g.ToString();
            textBox5.Text = b.ToString();

        }

        Bitmap bmpAsli, bmpHasil;
        private void button2_Click(object sender, EventArgs e)
        {
            int k = Convert.ToInt16(textBox6.Text);
            int i, j;
            int nilaiR, nilaiG, nilaiB;
            bmpAsli = (Bitmap)pictureBox1.Image;
            int baris = bmpAsli.Width;
            int kolom = bmpAsli.Height;
            bmpHasil = new Bitmap(baris, kolom);
            Cursor = Cursors.WaitCursor;
            for (i = 0; i < baris; i++) 
            {
                for(j = 0; j<kolom; j++) 
                {
                    nilaiR = bmpAsli.GetPixel(i,j).R + k;
                    nilaiG = bmpAsli.GetPixel(i, j).G + k;
                    nilaiB = bmpAsli.GetPixel(i, j).B + k;
                    if (nilaiR > 255) nilaiR = 255;
                    if (nilaiG > 255) nilaiG = 255;
                    if (nilaiB > 255) nilaiB = 255;
                    if (nilaiR < 0) nilaiR = 0;
                    if (nilaiG < 0) nilaiG = 0;
                    if (nilaiB < 0) nilaiB = 0;
                    bmpHasil.SetPixel(i, j, Color.FromArgb(nilaiR, nilaiG, nilaiB));
                }
            }
            pictureBox2.Image = bmpHasil;
            Cursor = Cursors.Default;
        }
 
        private void button3_Click(object sender, EventArgs e)
        {
            int i, j;
            int nilaiR, nilaiG, nilaiB;
            bmpAsli = (Bitmap)pictureBox1.Image;
            int baris = bmpAsli.Width;
            int kolom = bmpAsli.Height;
            bmpHasil = new Bitmap(baris, kolom);
            Cursor = Cursors.WaitCursor;
            for (i = 0; i < baris; i++)
            {
                for (j = 0; j < kolom; j++)
                {
                    nilaiR = 255 - bmpAsli.GetPixel(i, j).R;
                    nilaiG = 255 - bmpAsli.GetPixel(i, j).G;
                    nilaiB = 255 - bmpAsli.GetPixel(i, j).B;
                    bmpHasil.SetPixel(i, j, Color.FromArgb(nilaiR, nilaiG, nilaiB));
                }
            }
            pictureBox2.Image = bmpHasil;
            Cursor = Cursors.Default;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i, j;
            int nilaiR, nilaiG, nilaiB, grayscale;
            bmpAsli = (Bitmap)pictureBox1.Image;
            int baris = bmpAsli.Width;
            int kolom = bmpAsli.Height;
            bmpHasil = new Bitmap(baris, kolom);
            Cursor = Cursors.WaitCursor;
            for (i = 0; i < baris; i++)
            {
                for (j = 0; j < kolom; j++)
                {
                    nilaiR = bmpAsli.GetPixel(i, j).R;
                    nilaiG = bmpAsli.GetPixel(i, j).G;
                    nilaiB = bmpAsli.GetPixel(i, j).B;
                    grayscale = (nilaiR + nilaiG + nilaiB)/3;
                    bmpHasil.SetPixel(i, j, Color.FromArgb(grayscale, grayscale, grayscale));
                }
            }
            pictureBox2.Image = bmpHasil;
            Cursor = Cursors.Default;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i, j;
            int nilaiR, nilaiG, nilaiB, grayscale;
            bmpAsli = (Bitmap)pictureBox1.Image;
            int baris = bmpAsli.Width;
            int kolom = bmpAsli.Height;
            bmpHasil = new Bitmap(baris, kolom);
            Cursor = Cursors.WaitCursor;
            for (i = 0; i < baris; i++)
            {
                for (j = 0; j < kolom; j++)
                {
                    nilaiR = (int) (bmpAsli.GetPixel(i, j).R * .299);
                    nilaiG = (int) (bmpAsli.GetPixel(i, j).G * .587);
                    nilaiB = (int) (bmpAsli.GetPixel(i, j).B * .114);
                    grayscale = nilaiR + nilaiG + nilaiB;
                    bmpHasil.SetPixel(i, j, Color.FromArgb(grayscale, grayscale, grayscale));
                }
            }
            pictureBox2.Image = bmpHasil;
            Cursor = Cursors.Default;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int t = Convert.ToInt16(textBox7.Text);
            if (t > 255) t = 255;
            if (t < 0) t = 0;

            int i, j;
            int nilaiR, nilaiG, nilaiB;
            bmpAsli = (Bitmap)pictureBox1.Image;
            int baris = bmpAsli.Width;
            int kolom = bmpAsli.Height;
            bmpHasil = new Bitmap(baris, kolom);
            Cursor = Cursors.WaitCursor;
            for (i = 0; i < baris; i++)
            {
                for (j = 0; j < kolom; j++)
                {
                    nilaiR = bmpAsli.GetPixel(i, j).R;
                    nilaiG = bmpAsli.GetPixel(i, j).G;
                    nilaiB = bmpAsli.GetPixel(i, j).B;

                    nilaiR = nilaiR > t ? 255 : 0;
                    nilaiG = nilaiG > t ? 255 : 0;
                    nilaiB = nilaiB > t ? 255 : 0;
                    bmpHasil.SetPixel(i, j, Color.FromArgb(nilaiR, nilaiG, nilaiB));
                }
            }
            pictureBox2.Image = bmpHasil;
            Cursor = Cursors.Default;
        }
    }
}
