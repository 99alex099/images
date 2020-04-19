using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private Bitmap img;
        private String filename;
        public int[,] pixels;
        public int[,] shadpixels;

        private bool isLoadedShadow = false;


        public Form1()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.Series[0].IsValueShownAsLabel = false;
                Bitmap img = new Bitmap(pictureBox1.Image);

                pictureBox1.Image = SetPixels(pixels);

                DrawGistogr(1, new int[2] { 0, 1 }, GetCountOfEachElement(pixels, new int[2] { 0, 1 }));
                chart1.Series[0].IsValueShownAsLabel = true;
            }
            catch
            {
                MessageBox.Show("Error!", "Attention!");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                isLoadedShadow = false;
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.Filter = "Image Files(*.BMP;*.JPG)|*BMP;*.JPG|All files (*.*)|*.*";
                fileDialog.ShowDialog();
                string[] result = fileDialog.FileNames;

                filename = fileDialog.FileName;

                img = new Bitmap((Bitmap)Image.FromFile(fileDialog.FileName));
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pixels = new int[img.Height, img.Width];

                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        if (img.GetPixel(j, i) == Color.FromArgb(255, 255, 255))
                        {
                            pixels[i, j] = 0;
                        }
                        else
                        {
                            pixels[i, j] = 1;
                        }
                    }
                }
                dataGridView1.ColumnCount = img.Width;
                dataGridView1.RowCount = img.Height;

                dataGridView2.ColumnCount = img.Width;
                dataGridView2.RowCount = img.Height;

                dataGridView3.ColumnCount = img.Width;
                dataGridView3.RowCount = img.Height;

                dataGridView4.ColumnCount = img.Width;
                dataGridView4.RowCount = img.Height;

                Color zero = Color.Black;
                Color first = Color.White;

                pictureBox1.Image = img;


                SetDataToStringGrid(dataGridView1, pixels);

                saveToolStripMenuItem.Enabled = true;
                saveBinaryImageToolStripMenuItem.Enabled = true;
                chart1.Series[0].Points.Clear();

                DrawGistogr(1, new int[2] { 0, 1 }, GetCountOfEachElement(pixels, new int[2] { 0, 1 }));
            }
            catch
            {
                MessageBox.Show("The image was not loaded", "Attention!");
            }

            for (int i = 1; i < dataGridView1.RowCount + 1; i++)
            {
                dataGridView1.Rows[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView1.ColumnCount + 1; i++)
            {
                dataGridView1.Columns[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView2.RowCount + 1; i++)
            {
                dataGridView2.Rows[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView2.ColumnCount + 1; i++)
            {
                dataGridView2.Columns[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView3.RowCount + 1; i++)
            {
                dataGridView3.Rows[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView3.ColumnCount + 1; i++)
            {
                dataGridView3.Columns[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView4.RowCount + 1; i++)
            {
                dataGridView4.Rows[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView4.ColumnCount + 1; i++)
            {
                dataGridView4.Columns[i - 1].HeaderCell.Value = Convert.ToString(i);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int[] distarr = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                shadpixels = new int[dataGridView1.RowCount, dataGridView1.ColumnCount];
                for (int i = 0; i < pixels.GetLength(0); i++)
                {
                    for (int j = 0; j < pixels.GetLength(1); j++)
                    {
                        int i1 = i, j1 = j;
                        int b = pixels[i, j];
                        while (pixels[i1, j1] == b)
                        {

                            if (i1 == dataGridView1.ColumnCount - 1)
                            {
                                distarr[0] = 0;
                                break;
                            }
                            else
                            {
                                distarr[0]++;
                                i1++;
                            }
                        }
                        i1 = i;
                        j1 = j;
                        while (pixels[i1, j1] == b)
                        {
                            if (j1 == dataGridView1.RowCount - 1)
                            {
                                distarr[1] = 0;
                                break;
                            }
                            else
                            {
                                distarr[1]++;
                                j1++;
                            }
                        }
                        i1 = i;
                        j1 = j;
                        while (pixels[i1, j1] == b)
                        {
                            if (i1 == dataGridView1.ColumnCount - 1 || j1 == dataGridView1.RowCount - 1)
                            {
                                distarr[2] = 0;
                                break;
                            }
                            else
                            {
                                distarr[2]++;
                                i1++;
                                j1++;
                            }
                        }
                        i1 = i;
                        j1 = j;
                        while (pixels[i1, j1] == b)
                        {
                            if (i1 == 0 || j1 == dataGridView1.RowCount - 1)
                            {
                                distarr[3] = 0;
                                break;
                            }
                            else
                            {
                                distarr[3]++;
                                i1--;
                                j1++;
                            }
                        }

                        i1 = i;
                        j1 = j;

                        while (pixels[i1, j1] == b)
                        {
                            if (i1 == 0)
                            {
                                distarr[4] = 0;
                                break;
                            }
                            else
                            {
                                distarr[4]++;
                                i1--;
                            }
                        }

                        i1 = i;
                        j1 = j;

                        while (pixels[i1, j1] == b)
                        {
                            if (i1 == 0 || j1 == 0)
                            {
                                distarr[5] = 0;
                                break;
                            }
                            else
                            {
                                distarr[5]++;
                                i1--;
                                j1--;
                            }
                        }

                        i1 = i;
                        j1 = j;

                        while (pixels[i1, j1] == b)
                        {
                            if (j1 == 0)
                            {
                                distarr[6] = 0;
                                break;
                            }
                            else
                            {
                                distarr[6]++;
                                j1--;
                            }
                        }

                        i1 = i;
                        j1 = j;

                        while (pixels[i1, j1] == b)
                        {
                            if (j1 == 0 || i1 == dataGridView1.ColumnCount - 1)
                            {
                                distarr[7] = 0;
                                break;
                            }
                            else
                            {
                                distarr[7]++;
                                j1--;
                                i1++;
                            }
                        }

                        shadpixels[i, j] = SetSignToElement(pixels[i, j], FindMin(distarr));

                        for (int i2 = 0; i2 < distarr.Length; i2++)
                        {
                            distarr[i2] = 0;
                        }
                    }
                }

                SetDataToStringGrid(dataGridView2, shadpixels);

                HashSet<int> de = GetDifElements(shadpixels);
                int c = de.Count;
                int[] difElem = de.ToArray();
                int[] gistogr = FormGistogramma(c);
                int c1 = gistogr.Length;
                ReverseArray(gistogr);
                SortArray(difElem);
                img = MakeImageShadow(difElem, gistogr, shadpixels);
                pictureBox2.Image = img;

                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                saveShadowImageToolStripMenuItem.Enabled = true;
                chart2.Series[0].Points.Clear();

                DrawGistogr(2, difElem, GetCountOfEachElement(shadpixels, difElem));
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex), "Attention!");
            }
        }

        private int FindMin(int[] array)
        {
            int min = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                {
                    min = array[i];
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0)
                {
                    continue;
                }
                else if (array[i] < min)
                {
                    min = array[i];
                }
            }

            return min;
        }

        private int SetSignToElement(int matrixElement, int arrayElement)
        {
            if (matrixElement == 0)
            {
                return -arrayElement;
            }

            else
            {
                return arrayElement;
            }
        }
        private int[] FormGistogramma(int differentPxCount)
        {
            int[] gist = new int[differentPxCount + 1];

            double step = 255.0 / (differentPxCount);

            for (int i = 0; i < differentPxCount + 1; i++)
            {
                gist[i] = (int)Math.Round(i * step);
            }
            return gist;
        }

        private HashSet<int> GetDifElements(int[,] array)
        {
            HashSet<int> difElements = new HashSet<int>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    difElements.Add(array[i, j]);
                }
            }
            return difElements;
        }

        private Bitmap MakeImageShadow(int[] difElem, int[] gistogr, int[,] shadpix)
        {
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    for (int y = 0; y < difElem.Length; y++)
                    {
                        if (shadpix[i, j] == difElem[y])
                        {
                            img.SetPixel(j, i, Color.FromArgb(gistogr[y], gistogr[y], gistogr[y]));
                            dataGridView3.Rows[i].Cells[j].Value = "(" + gistogr[y] + "," + gistogr[y] + "," + gistogr[y] + ")";
                            break;
                        }
                    }
                }
            }
            return img;
        }

        private void ReverseArray(int[] array)
        {
            for (int i = 0, j = array.Length - 1; i < j; i++, j--)
            {
                int t = array[i];
                array[i] = array[j];
                array[j] = t;
            }
        }

        private void SortArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                    }
                }
            }
        }

        private Bitmap SetBinOnBitmap(Color zero, Color first, int[,] array, Bitmap imgf)
        {
            //Bitmap img = new Bitmap(imgf);
            Bitmap img = imgf;
            int n = 0;
            int w = img.Size.Width;
            int h = img.Size.Height;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (Convert.ToByte(array[i, j]) == Convert.ToByte(1)) img.SetPixel(j, i, zero);
                    else img.SetPixel(j, i, first);
                    n++;
                }
            }
            return img;
        }

        private void saveBinaryImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(sfd.FileName);
            }
        }

        private void saveShadowImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image.Save(sfd.FileName);
            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private int DistanceToOppositePix(int x, int y)
        {

            int dist = 0;


            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 && j != 0)
                    {
                        int tempDist = CalculateDist(x, y, i, j);

                        if (dist == 0 || tempDist < dist)
                        {
                            if (tempDist != 0)
                            {
                                dist = tempDist;
                            }
                        }
                    }
                }
            }
            return dist;
        }

        private int CalculateDist(int x, int y, int dx, int dy)
        {
            int elem = Convert.ToInt32(dataGridView1.Rows[x].Cells[y].Value);

            int i = x;
            int j = y;

            while (true)
            {
                x += dx;
                y += dy;

                if (x >= dataGridView1.RowCount || x < 0
                    || y >= dataGridView1.ColumnCount || y < 0)
                {
                    return 0;
                }

                int tempElem = Convert.ToInt32(dataGridView1.Rows[x].Cells[y].Value);

                if (elem == 0 && tempElem == 1
                    || elem == 1 && tempElem == 0)
                {
                    int dist = -1;

                    if (Math.Abs(i - x) <= Math.Abs(j - y))
                    {
                        dist = Math.Abs(j - y);
                    }
                    else
                    {
                        dist = Math.Abs(i - x);
                    }

                    return elem == 1 ? dist : -dist;
                }
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            shadpixels = new int[dataGridView1.RowCount, dataGridView1.ColumnCount];

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    shadpixels[i, j] = DistanceToOppositePix(i, j);
                }
            }


            dataGridView2.RowCount = dataGridView1.RowCount;
            dataGridView2.ColumnCount = dataGridView1.ColumnCount;

            /*for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = shadpixels[i, j];
                }
            }*/
            SetDataToStringGrid(dataGridView2, shadpixels);

            HashSet<int> de = GetDifElements(shadpixels);
            int c = de.Count;
            int[] difElem = de.ToArray();
            int[] gistogr = FormGistogramma(c);
            int c1 = gistogr.Length;
            ReverseArray(gistogr);
            SortArray(difElem);
            img = MakeImageShadow(difElem, gistogr, shadpixels);
            pictureBox2.Image = img;

            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            saveShadowImageToolStripMenuItem.Enabled = true;
            chart2.Series[0].Points.Clear();
            DrawGistogr(2, difElem, GetCountOfEachElement(shadpixels, difElem));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DrawGistogr(int chartSelector, int[] xValues, int[] yValues)
        {
            switch (chartSelector)
            {
                case 1:
                    {
                        for (int i = 0; i < xValues.Length; i++)
                        {
                            chart1.Series[0].Points.AddXY(xValues[i], yValues[i]);
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < xValues.Length; i++)
                        {
                            chart2.Series[0].Points.AddXY(xValues[i], yValues[i]);
                        }
                    }
                    break;

            }
        }

        private int[] GetCountOfEachElement(int[,] matr, int[] arrayOfDifElem)
        {
            int[] countOfEachElem = new int[arrayOfDifElem.Length];

            for (int i = 0; i < countOfEachElem.Length; i++)
            {
                countOfEachElem[i] = 0;
            }

            for (int n = 0; n < arrayOfDifElem.Length; n++)
            {
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        if (arrayOfDifElem[n] == matr[i, j])
                        {
                            countOfEachElem[n]++;
                        }
                    }
                }
            }


            return countOfEachElem;
        }

        public double[,] GetAvg(int[,]shadpixels)
        {
            double[,] avg = new double[shadpixels.GetLength(0), shadpixels.GetLength(1)];
            int[,] binpixels = new int[shadpixels.GetLength(0), shadpixels.GetLength(1)];
            binpixels = shadpixels;

            for (int i = 0; i < shadpixels.GetLength(0); i++)
            {
                for (int j = 0; j < shadpixels.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                             Math.Round(avg[i,j] = (shadpixels[i + 1, j] + shadpixels[i + 1, j + 1] + shadpixels[i, j + 1]) / 3,2);
                        }

                        else if (j == shadpixels.GetLength(1) - 1)
                        {
                            Math.Round(avg[i, j] = (shadpixels[i + 1, j] + shadpixels[i + 1, j - 1] + shadpixels[i, j - 1]) / 3,2);
                        }

                        else
                        {
                            Math.Round(avg[i, j] = (shadpixels[i, j - 1] + shadpixels[i + 1, j - 1] + shadpixels[i + 1, j] + shadpixels[i + 1, j + 1] + shadpixels[i, j + 1]) / 5,2);
                        }
                    }

                    else if (i == shadpixels.GetLength(0) - 1)
                    {
                        if (j == 0)
                        {
                            Math.Round(avg[i, j] = (shadpixels[i - 1, j] + shadpixels[i - 1, j + 1] + shadpixels[i, j + 1]) / 3,2);
                        }

                        else if (j == shadpixels.GetLength(1) - 1)
                        {
                            Math.Round(avg[i, j] = (shadpixels[i - 1, j] + shadpixels[i - 1, j - 1] + shadpixels[i, j - 1]) / 3,2);
                        }

                        else
                        {
                            Math.Round(avg[i, j] = (shadpixels[i, j - 1] + shadpixels[i - 1, j - 1] + shadpixels[i - 1, j] + shadpixels[i - 1, j + 1] + shadpixels[i, j + 1]) / 5,2);
                        }
                    }

                    else
                    {
                        if(j == 0)
                        {
                            Math.Round(avg[i, j] = (shadpixels[i - 1, j] + shadpixels[i - 1, j + 1] + shadpixels[i, j + 1] + shadpixels[i + 1, j + 1] + shadpixels[i + 1, j]) / 5,2);
                        }
                        else if(j == shadpixels.GetLength(1) - 1)
                        {
                            Math.Round(avg[i, j] = (shadpixels[i - 1, j] + shadpixels[i - 1, j - 1] + shadpixels[i, j - 1] + shadpixels[i + 1, j - 1] + shadpixels[i + 1, j]) / 5,2);
                        }

                        else
                        {
                            Math.Round(avg[i, j] = (shadpixels[i - 1, j + 1] + shadpixels[i - 1, j] + shadpixels[i - 1, j - 1] + shadpixels[i, j - 1] + shadpixels[i + 1, j - 1] + shadpixels[i + 1, j] + shadpixels[i + 1, j + 1] + shadpixels[i, j + 1]) / 8,2);
                        }
                    }

                }
            }
            SetDataToStringGrid(dataGridView3, avg);
            return avg;
        }

        private int[,] ChangePixelToBinInLocalBin(int[,] shadpx,double[,] avg)
        {
            int[,] px = new int[img.Height, img.Width];

            for (int i = 0; i < shadpx.GetLength(0); i++)
            {
                for (int j = 0; j < shadpx.GetLength(1); j++)
                {
                    if (shadpx[i, j] >= avg[i, j])
                    {
                        px[i, j] = 0;
                    }
                    else
                    {
                        px[i, j] = 1;
                    }
                }
            }
                
            return px;
        }

        private int[,] ChangePixelToBinInGlobalBin(int[,] shadpx,int bordValue)
        {
            int[,] px = new int[img.Height, img.Width];
            for (int i = 0; i < shadpx.GetLength(0); i++)
            {
                for (int j = 0; j < shadpx.GetLength(1); j++)
                {
                    if (shadpx[i,j] < bordValue)
                    {
                        px[i, j] = 0;
                    }
                    else
                    {
                        px[i, j] = 1;
                    }
                }
            }

            return px;
        }

        public Bitmap SetPixels(int[,]pixels)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (pixels[i,j] == 1)
                    {
                        img.SetPixel(j, i, Color.Black);
                    }
                    else
                    {
                        img.SetPixel(j, i, Color.White);
                    }
                }
            }
            return img;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(isLoadedShadow)
            {
                img = new Bitmap((Bitmap)pictureBox1.Image);
            }
            else
            {
                img = new Bitmap((Bitmap)pictureBox2.Image);
            }

            if (!checkBox1.Checked)
            {
                int[,] colorpx = GetColorsSeparately(GetShadowColors());
                int[,] pixels = ChangePixelToBinInLocalBin(colorpx, GetAvg(colorpx));
                SetDataToStringGrid(dataGridView4, pixels);
                SetDataToStringGrid(dataGridView1, colorpx);
                pictureBox2.Image = SetPixels(pixels);

                HashSet<int> de = GetDifElements(colorpx);
                int[] difElem = de.ToArray();
                DrawGistogr(1, difElem, GetCountOfEachElement(colorpx, difElem));

                de = GetDifElements(pixels);
                difElem = de.ToArray();
                DrawGistogr(2, difElem, GetCountOfEachElement(pixels, difElem));
            }
            else
            {
                try
                {
                    int[,] colorpx = GetColorsSeparately(GetShadowColors());
                    int bordValue = Convert.ToInt32(textBox1.Text);
                    int[,] pixels = ChangePixelToBinInGlobalBin(colorpx, bordValue);
                    SetDataToStringGrid(dataGridView4, pixels);
                    SetDataToStringGrid(dataGridView1, colorpx);
                    pictureBox2.Image = SetPixels(pixels);

                    HashSet<int> de = GetDifElements(colorpx);
                    int[] difElem = de.ToArray();
                    DrawGistogr(1, difElem, GetCountOfEachElement(colorpx, difElem));

                    de = GetDifElements(pixels);
                    difElem = de.ToArray();
                    DrawGistogr(2, difElem, GetCountOfEachElement(pixels, difElem));

                }
                catch(Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex), "Attention!");
                }
            }

        }

         

        public void SetDataToStringGrid<T>(DataGridView dgv, T[,] matr)
        {
            for(int i = 0; i < dgv.RowCount; i++)
            {
                for(int j = 0; j < dgv.ColumnCount; j++)
                {
                    dgv.Rows[i].Cells[j].Value = matr[i, j];
                }
            }
        }

        private void loadShadowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image Files(*.BMP;*.JPG)|*BMP;*.JPG|All files (*.*)|*.*";
            fileDialog.ShowDialog();
            string[] result = fileDialog.FileNames;

            filename = fileDialog.FileName;

            img = new Bitmap((Bitmap)Image.FromFile(fileDialog.FileName));
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img;

            Color[,] color = GetShadowColors();

            dataGridView1.ColumnCount = img.Width;
            dataGridView1.RowCount = img.Height;

            dataGridView2.ColumnCount = img.Width;
            dataGridView2.RowCount = img.Height;

            dataGridView3.ColumnCount = img.Width;
            dataGridView3.RowCount = img.Height;

            dataGridView4.ColumnCount = img.Width;
            dataGridView4.RowCount = img.Height;

            isLoadedShadow = true;

            for (int i = 1; i < dataGridView1.RowCount + 1; i++)
            {
                dataGridView1.Rows[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView1.ColumnCount + 1; i++)
            {
                dataGridView1.Columns[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView2.RowCount + 1; i++)
            {
                dataGridView2.Rows[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView2.ColumnCount + 1; i++)
            {
                dataGridView2.Columns[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView3.RowCount + 1; i++)
            {
                dataGridView3.Rows[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView3.ColumnCount + 1; i++)
            {
                dataGridView3.Columns[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView4.RowCount + 1; i++)
            {
                dataGridView4.Rows[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

            for (int i = 1; i < dataGridView4.ColumnCount + 1; i++)
            {
                dataGridView4.Columns[i - 1].HeaderCell.Value = Convert.ToString(i);
            }

        }

        private Color[,] GetShadowColors()
        {
            Color[,] color = new Color[img.Height, img.Width];

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {

                    color[i, j] = img.GetPixel(j, i);
                }
            }

            return color;
        }

        private int[,] GetColorsSeparately(Color[,] color)
        {
            int[,] colorpx = new int[color.GetLength(0), color.GetLength(1)];

            for (int i = 0; i < color.GetLength(0); i++)
            {
                for (int j = 0; j < color.GetLength(1); j++)
                {
                    colorpx[i, j] = color[i, j].R;
                }
            }

            return colorpx;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
        }
    }
}

