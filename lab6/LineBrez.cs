using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public class LineBrez
    {
        private void DrawLine(PictureBox PB, DataGridView DG, int[,] Line,int size,Color cl) {
            DG.Rows.Clear();
            DG.RowCount=size;
            DG.ColumnCount = 1;
            Graphics g = PB.CreateGraphics();
            Brush B = new SolidBrush(cl);
            for (int i = 0; i < size; i++)
                DG.Rows[i].Cells[0].Value = "{"+Convert.ToString(Line[0,i])+";"+ Convert.ToString(Line[1, i])+"}";
            for (int i = 0; i < size; i++)
                g.FillRectangle(B, Line[0, i], Line[1, i], 1, 1);
            g.Dispose();
        }

        public void Bresenham4Line(PictureBox PB,DataGridView DG, int x1, int y1, int x2, int y2)
        {
            int size = 0;
            int[,] Line = new int[2, 2000];
            int ix, iy,e;
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            if (x1 < x2) ix = 1;
            else ix = -1;
            if (y1 < y2) iy = 1;
            else iy = -1;
            e = 0;
            for (int i = 0; i < dx + dy; i++)
            {
                Line[0, size] = x1; Line[1, size++] = y1;
                int e1 = e + dy; int e2 = e - dx;
                if (Math.Abs(e1) < Math.Abs(e2))
                { x1 += ix; e = e1; }
                else { y1 += iy; e = e2; }
            }
            DrawLine(PB,DG, Line, size, Color.Blue);
        }

        public void Bresenham8Line(PictureBox PB, DataGridView DG, int x0, int y0, int x1, int y1)
        {
            int size=0;
            int[,] Line = new int[2, 2000];
            int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
            int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
            int sx = (x1 >= x0) ? (1) : (-1);
            int sy = (y1 >= y0) ? (1) : (-1);
            if (dy < dx)
            {
                int d = (dy << 1) - dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                Line[0, size] = x0;
                Line[1, size++] = y0;
                int x = x0 + sx;
                int y = y0;
                for (int i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    Line[0, size] = x;
                    Line[1, size++] = y;
                    x++;
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                Line[0, size] = x0;
                Line[1, size++] = y0;
                int x = x0;
                int y = y0 + sy;
                for (int i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    Line[0, size] = x;
                    Line[1, size++] = y;
                    y++;
                }
            }
            DrawLine(PB,DG,Line, size, Color.Green);
        }
    }
}
