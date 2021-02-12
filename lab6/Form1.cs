using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        public Form1(){
            InitializeComponent();
        }
        private int Click;
        private int Index { get; set; }

        private void фильтр3х3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.AverageArithmetic();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр5х5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(5, pictureBox1);
                pictureBox2.Image = NFilter.AverageArithmetic();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр7х7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(7, pictureBox1);
                pictureBox2.Image = NFilter.AverageArithmetic();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр3х3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label1.Text = "Q=";
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                panel1.Visible = true;
                textBox1.Clear();
                Index = 3;
            }
        }

        private void фильтр5х5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label1.Text = "Q=";
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                textBox1.Clear();
                panel1.Visible = true;
                Index = 5;
            }
        }

        private void фильтр7х7ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label1.Text = "Q=";
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                panel1.Visible = true;
                textBox1.Clear();
                Index = 7;
            }
        }

        private void фильтр3х3ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.Median();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр5х5ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(5, pictureBox1);
                pictureBox2.Image = NFilter.Median();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр7х7ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(7, pictureBox1);
                pictureBox2.Image = NFilter.Median();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр3х3ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.MinMax(1);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр5х5ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(5, pictureBox1);
                pictureBox2.Image = NFilter.MinMax(1);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр7х7ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(7, pictureBox1);
                pictureBox2.Image = NFilter.MinMax(1);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр3х3ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.MinMax(0);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр5х5ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(7, pictureBox1);
                pictureBox2.Image = NFilter.MinMax(0);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр7х7ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(7, pictureBox1);
                pictureBox2.Image = NFilter.MinMax(0);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтрЛапласаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.Laplas();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                if (openFileDialog1.FileName != null)
                    pictureBox1.Load(openFileDialog1.FileName);
        }

        private void фильтрСобеляToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.Sobel();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.Stamping();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (label1.Text == "Si="&& double.TryParse(textBox1.Text, out double SI))
                {
                    Filter NFilter = new Filter(Index, pictureBox1);
                    pictureBox2.Image = NFilter.Gauss(SI);
                    pictureBox1.Load(openFileDialog1.FileName);
                }
                if (label1.Text == "Q="&&int.TryParse(textBox1.Text, out int Q))
                {
                    Filter NFilter = new Filter(Index, pictureBox1);
                    pictureBox2.Image = NFilter.ContrHarmonic(Q);
                    pictureBox1.Load(openFileDialog1.FileName);
                }
            if (label1.Text == "D=" && int.TryParse(textBox1.Text, out int D))
            {
                if (D >= 0 && D < Index*Index)
                {
                    Filter NFilter = new Filter(Index, pictureBox1);
                    pictureBox2.Image = NFilter.TrancationAverage(D);
                    pictureBox1.Load(openFileDialog1.FileName);
                }
                else MessageBox.Show("D == [0,"+(Index*Index-1)+"]");
            }
            if (label1.Text == "N=" && int.TryParse(textBox1.Text, out int N))
            {
                if (Index==0)
                {
                    if (N > 3)
                    {
                        Filter NFilter = new Filter(N, pictureBox1);
                        pictureBox2.Image = NFilter.Kuvahare();
                        pictureBox1.Load(openFileDialog1.FileName);
                    }
                    else MessageBox.Show("N должно быть больше 3");
                }
                else if (Index == 1)
                {
                    if (N >= 3)
                    {
                        label1.Text = "Si=";
                        textBox1.Clear();
                        Index = N;
                    }
                    else MessageBox.Show("N должно быть больше 2");
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Left=140;
            foreach (Control X in panel2.Controls)
                Left += 30;
            Panel Cls = new Panel();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                Cls.BackColor = colorDialog1.Color;
            Cls.Size = new Size(25,25);
            Cls.Name="panel7";
            Cls.BorderStyle = BorderStyle.FixedSingle;
            Cls.Location = new Point(Left,5);
            panel2.Controls.Add(Cls);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else if (panel2.Controls.Count < 3) MessageBox.Show("Нужно хотя бы 2 цвета");
            else
            {
                int i = 0;
                Color[] Colors = new Color[panel2.Controls.Count - 2];
                foreach (Control X in panel2.Controls)
                    if (X is Panel)
                    {
                        Colors[i++] = X.BackColor;
                    }
                Dizering Dizer = new Dizering(Colors, pictureBox1);
                pictureBox2.Image = Dizer.DitherFS();
                pictureBox1.Load(openFileDialog1.FileName);
                panel1.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                dataGridView1.Visible = false;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null) MessageBox.Show("Нет картинки для сохранения");
            else
            {
                saveFileDialog1.FileName = Path.GetFileName(openFileDialog1.FileName);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    if (saveFileDialog1.FileName != null)
                        pictureBox2.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int Left = 150;
            foreach (Control X in panel3.Controls)
                Left += 30;
            Panel Cls = new Panel();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                Cls.BackColor = colorDialog1.Color;
            Cls.Size = new Size(25, 25);
            Cls.Name = "panel6";
            Cls.BorderStyle = BorderStyle.FixedSingle;
            Cls.Location = new Point(Left, 5);
            panel3.Controls.Add(Cls);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else if (panel3.Controls.Count < 5) MessageBox.Show("Нужно хотя бы 2 цвета");
            else
            {
                int i = 0;
                Color[] Colors = new Color[panel3.Controls.Count - 4];
                foreach (Control X in panel3.Controls)
                    if (X is Panel)
                    {
                        Colors[i++] = X.BackColor;
                    }
                Dizering Dizer = new Dizering(Colors, pictureBox1, (int)numericUpDown1.Value);
                pictureBox2.Image = Dizer.DitherSquare();
                pictureBox1.Load(openFileDialog1.FileName);
                panel1.Visible = false;
                panel2.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                dataGridView1.Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int X0) && int.TryParse(textBox3.Text, out int Y0)
                && int.TryParse(textBox4.Text, out int XE) && int.TryParse(textBox5.Text, out int YE)) {
                dataGridView1.Visible = true;
                LineBrez line = new LineBrez();
                line.Bresenham4Line(pictureBox1,dataGridView1,X0,Y0,XE,YE);
            }
        }
     
        private void button8_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox6.Text, out int X0) && int.TryParse(textBox7.Text, out int Y0)
                && int.TryParse(textBox8.Text, out int XE) && int.TryParse(textBox9.Text, out int YE)){
                dataGridView1.Visible = true;
                LineBrez line = new LineBrez();
                line.Bresenham8Line(pictureBox1, dataGridView1, X0, Y0, XE, YE);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 1;
            dataGridView1.Rows.Clear();
            Click = 0;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                Click++;
                if (Click == 3) { Click = 1; pictureBox1.Refresh(); }
                Graphics g = pictureBox1.CreateGraphics();
                Brush B = new SolidBrush(Color.Red);
                g.FillRectangle(B, e.X - 1, e.Y - 1, 3, 3);
                if (Click % 2 == 0)
                {
                    textBox4.Text = Convert.ToString(e.X);
                    textBox5.Text = Convert.ToString(e.Y);
                    textBox8.Text = Convert.ToString(e.X);
                    textBox9.Text = Convert.ToString(e.Y);
                }
                else
                {
                    textBox2.Text = Convert.ToString(e.X);
                    textBox3.Text = Convert.ToString(e.Y);
                    textBox6.Text = Convert.ToString(e.X);
                    textBox7.Text = Convert.ToString(e.Y);
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox8.Clear();
                    textBox9.Clear();
                }
            }
        }

        private void фToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.AverageGeometric();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр5х5ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(5, pictureBox1);
                pictureBox2.Image = NFilter.AverageGeometric();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр7х7ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(7, pictureBox1);
                pictureBox2.Image = NFilter.AverageGeometric();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр3х3ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.AverageDot();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр5х5ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(5, pictureBox1);
                pictureBox2.Image = NFilter.AverageDot();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр7х7ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(7, pictureBox1);
                pictureBox2.Image = NFilter.AverageDot();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void фильтр3х3ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label1.Text = "D=";
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                panel1.Visible = true;
                textBox1.Clear();
                Index = 3;
            }
        }

        private void фильтр5х5ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label1.Text = "D=";
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                panel1.Visible = true;
                textBox1.Clear();
                Index = 5;
            }
        }

        private void фильтр7х7ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label1.Text = "D=";
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                panel1.Visible = true;
                textBox1.Clear();
                Index = 7;
            }
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear();
            textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox9.Clear();
            pictureBox1.Refresh();
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 1;
            dataGridView1.Rows.Clear();
            while (panel2.Controls.Count > 2 || panel3.Controls.Count > 4)
            {
                panel2.Controls.RemoveByKey("panel7");
                panel3.Controls.RemoveByKey("panel6");
            }
        }

        private void дизерингФлойдаСтейнбергаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
        }

        private void дизерингToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
        }

        private void алгоритмБрезенхэма4ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel5.Visible = false;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            dataGridView1.Visible = false;
        }

        private void алгоритмБрезенхэма8ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            dataGridView1.Visible = false;
        }

        private void фильтрКувахареToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label1.Text = "N=";
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                panel1.Visible = true;
                textBox1.Clear();
                Index = 0;
            }
        }

        private void гаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label1.Text = "N=";
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                panel1.Visible = true;
                textBox1.Clear();
                Index = 1;
            }
        }

        private void выделениеКонтураToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.Outlines();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void маска1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.UnderlineOutlines(1);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void маска2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.UnderlineOutlines(2);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void маска3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.UnderlineOutlines(3);
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void акварелизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            dataGridView1.Visible = false;
            if (pictureBox1.Image == null) MessageBox.Show("Картинка не загружена");
            else
            {
                Filter NFilter = new Filter(3, pictureBox1);
                pictureBox2.Image = NFilter.Median();
                Filter Akv = new Filter(3, pictureBox2);
                pictureBox2.Image = Akv.Laplas();
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }
    }
}
