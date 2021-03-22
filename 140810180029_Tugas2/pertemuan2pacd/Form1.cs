using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pertemuan2pacd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            saveImageToolStripMenuItem.Enabled = false;
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.bmp) | *.jpg; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Bitmap gambar = new Bitmap(open.FileName);
                pictureBox1.Image = new Bitmap(gambar, new Size(200, 200));
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            string resultMessage = "File save {0}. {1}";

            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Image Files(*.jpg; *.bmp) | *.jpg; *.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap drawImage = (Bitmap)pictureBox2.Image;
                    drawImage.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            
                isSuccess = true;
                resultMessage = string.Format(resultMessage, "succeeded", string.Empty);
            }
            catch (Exception errorSave)
            {
                resultMessage = string.Format(resultMessage, "failed", errorSave.Message);
            }
            finally
            {
                MessageBox.Show(resultMessage, isSuccess ? "Success" : "Error");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Bitmap bmpAsli, bmpHasil;

        private void button1_Click(object sender, EventArgs e)
        {
            bmpAsli = (Bitmap)pictureBox1.Image;
            int baris = bmpAsli.Width;
            int kolom = bmpAsli.Height;
            bmpHasil = new Bitmap(baris, kolom);

            Cursor = Cursors.WaitCursor;
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            System.Drawing.Imaging.BitmapData bmData = bmpAsli.LockBits(new Rectangle(0, 0, bmpAsli.Width, bmpAsli.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
                int stopAddress = (int)p + bmData.Stride * bmData.Height;
                while ((int)p != stopAddress)
                {
                    p[0] = (byte)(.299 * p[2] + .587 * p[1] + .114 * p[0]);
                    p[1] = p[0];
                    p[2] = p[0];
                    p += 3;
                }
            }
            bmpAsli.UnlockBits(bmData);
            pictureBox2.Image = bmpAsli;
            saveImageToolStripMenuItem.Enabled = true;
            Cursor = Cursors.Default;
            sw.Stop();
            label2.Text = sw.Elapsed.TotalMilliseconds.ToString() + " ms";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bmpAsli = (Bitmap)pictureBox1.Image;
            int baris = bmpAsli.Width;
            int kolom = bmpAsli.Height;
            bmpHasil = new Bitmap(baris, kolom);

            Cursor = Cursors.WaitCursor;
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            System.Drawing.Imaging.BitmapData data = bmpAsli.LockBits(new Rectangle(0, 0, bmpAsli.Width, bmpAsli.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int nOffset = data.Stride - data.Width * 3, nVal, nBrightness = 50;
            int nWidth = data.Width * 3;
            unsafe
            {
                byte* ptr = (byte*)(data.Scan0);
                for (int y = 0; y < data.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nVal = (int)(ptr[0] + nBrightness);
                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;
                        ptr[0] = (byte)nVal;
                        ++ptr;
                    }
                    ptr += nOffset;
                }
            }

            bmpAsli.UnlockBits(data);
            pictureBox2.Image = bmpAsli;
            saveImageToolStripMenuItem.Enabled = true;
            Cursor = Cursors.Default;
            sw.Stop();
            label2.Text = sw.Elapsed.TotalMilliseconds.ToString() + " ms";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bmpAsli = (Bitmap)pictureBox1.Image;
            int baris = bmpAsli.Width;
            int kolom = bmpAsli.Height;
            bmpHasil = new Bitmap(baris, kolom);

            Cursor = Cursors.WaitCursor;
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            System.Drawing.Imaging.BitmapData bmData = bmpAsli.LockBits(new Rectangle(0, 0, bmpAsli.Width, bmpAsli.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
                int stopAddress = (int)p + bmData.Stride * bmData.Height;
                while ((int)p != stopAddress)
                {
                    p[0] = (byte)(255 - p[0]);
                    p[1] = (byte)(255 - p[1]);
                    p[2] = (byte)(255 - p[2]);
                    p += 3;
                }
            }
            bmpAsli.UnlockBits(bmData);
            pictureBox2.Image = bmpAsli;
            saveImageToolStripMenuItem.Enabled = true;
            Cursor = Cursors.Default;
            sw.Stop();
            label2.Text = sw.Elapsed.TotalMilliseconds.ToString() + " ms";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int t = Convert.ToInt16(textBox1.Text);
            if (t > 255)
            {
                MessageBox.Show("Thresholding value > 255", "Error");
            }
            else if (t < 0)
            {
                MessageBox.Show("Thresholding value < 0", "Error");
            }
            else
            {
                bmpAsli = (Bitmap)pictureBox1.Image;
                int baris = bmpAsli.Width;
                int kolom = bmpAsli.Height;
                bmpHasil = new Bitmap(baris, kolom);

                Cursor = Cursors.WaitCursor;
                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                System.Drawing.Imaging.BitmapData bmData = bmpAsli.LockBits(new Rectangle(0, 0, bmpAsli.Width, bmpAsli.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
                    int stopAddress = (int)p + bmData.Stride * bmData.Height;
                    while ((int)p != stopAddress)
                    {
                        p[0] = (byte)(p[0] > t ? 255 : 0);
                        p[1] = (byte)(p[1] > t ? 255 : 0);
                        p[2] = (byte)(p[2] > t ? 255 : 0);
                        p += 3;
                    }
                }
                bmpAsli.UnlockBits(bmData);
                pictureBox2.Image = bmpAsli;
                saveImageToolStripMenuItem.Enabled = true;
                Cursor = Cursors.Default;
                sw.Stop();
                label2.Text = sw.Elapsed.TotalMilliseconds.ToString() + " ms";
            }
        }

    }
}
