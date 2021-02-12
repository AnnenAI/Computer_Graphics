using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public class Dizering
    {
        private int N;
        private PictureBox PB;
        private Color[] inpColor;
        private MColor DesColor;
        private Color[] buf;

        public class MColor{
            public Color[] Col;
            public int Index;
            public MColor(int size) {
                Col=new Color[size];
                Index = 0;
            }
            public void Add(Color[] temp)
            {
                for (int i = 0; i < temp.Length; i++)
                    Col[Index++] = temp[i];
            }
        }


        public Dizering(Color[] Colors, PictureBox inpPB)
        {
            inpColor = (Color[])Colors.Clone();
            PB = inpPB;
        }

        private void FComb(int ind, int begin)
        {
            for (int i = begin; i < inpColor.Length; i++)
            {
                buf[ind] = inpColor[i];
                if (ind + 1 < N * N) FComb(ind + 1, i);
                else DesColor.Add(buf);
            }
        }
        double Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }

        public Dizering(Color[] Colors, PictureBox inpPB,int n)
        {
            int N2 = n * n;
            N = n;
            buf = new Color[N2];
            inpColor = (Color[])Colors.Clone();
            double size = Factorial(inpColor.Length-1+(n*n))/(Factorial(inpColor.Length - 1) * Factorial(n * n));
            DesColor = new MColor((int)size*N2);
            FComb(0, 0);
            PB = inpPB;
        }

        private Color[] FindMinWay(Color C1)
        {
            int N2 = N * N;
            Color[] res = new Color[N2];
            int index = 0;
            double Min = 255;
            for (int i = 0; i < DesColor.Index; i += N2)
            {
                int R = 0, G = 0, B = 0;
                for (int j = i; j < i + N2; j++)
                {
                    R += DesColor.Col[j].R; G += DesColor.Col[j].G; B += DesColor.Col[j].B;
                }
                double temp = Math.Sqrt(0.22 * Math.Pow((R / N2) - C1.R, 2) + 0.71 * Math.Pow((G / N2) - C1.G, 2) + 0.07 * Math.Pow((B / N2) - C1.B, 2));
                if (temp < Min) { Min = temp; index = i; }
            }
            for (int i = 0; i < N2; i++)
                res[i] = DesColor.Col[index++];
        return res;
        }

        internal Bitmap DitherSquare()
        {
            var bmp = PB.Image as Bitmap;
            int N2 = N * N;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmpData.Height;
            int Stride = Image.GetPixelFormatSize(bmp.PixelFormat) / 8 * bmp.Width;
            Bitmap bmpRes = new Bitmap(bmp.Width * N, bmp.Height * N);
            var Nrect = new Rectangle(0, 0, bmpRes.Width, bmpRes.Height);
            BitmapData bmpDataRes = bmpRes.LockBits(Nrect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            bmp.UnlockBits(bmpData);
            int w = 0,h=0;
            byte[] Value = new byte[bmpDataRes.Stride*bmpDataRes.Height];
            for (int i = 0; i < bytes - 3; i += 3)
            {
                Color[] dot = FindMinWay(Color.FromArgb(rgbValues[i + 2], rgbValues[i + 1], rgbValues[i]));
                for (int j = 0; j < N; j++)
                    for (int k = 0; k < N; k++)
                    {
                        int pos = h+w;
                        Value[2 + k*3 +pos+ j * bmpDataRes.Stride] = dot[j*N+k].R;
                        Value[1 + k*3 + pos + j * bmpDataRes.Stride] = dot[j*N+k].G;
                        Value[k*3 + pos+ j * bmpDataRes.Stride] = dot[j*N+k].B;
                    }
                w+=3*N;
                if (w % (Stride*N) == 0) { i += bmpData.Stride - Stride; w = 0; h+= bmpDataRes.Stride *N; };
            }
            
            ptr = bmpDataRes.Scan0;
            Marshal.Copy(Value, 0, ptr, bmpDataRes.Height*bmpDataRes.Stride);
            bmpRes.UnlockBits(bmpDataRes);
            PB.Invalidate();
            return bmpRes;
        }

        public Bitmap DitherFS()
        {
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            int Stride = Image.GetPixelFormatSize(bmp.PixelFormat) / 8 * bmp.Width;
            IntPtr ptr = bmpData.Scan0;
            int k=0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - 3; i += 3)
            {
                k++;
                Color dot = FindColor(Color.FromArgb(Value[i + 2], Value[i + 1], Value[i]));
                double[] DK = { (Value[i + 2] - dot.R) * (3.0 / 8.0),
                                (Value[i + 1] - dot.G) * (3.0 / 8.0),
                                (Value[i] - dot.B) * (3.0 / 8.0),
                                (Value[i + 2] - dot.R) / 4.0,
                                (Value[i + 1] - dot.G) / 4.0,
                                (Value[i] - dot.B) / 4.0};
                Value[i + 2] = dot.R; Value[i + 1] = dot.G; Value[i] = dot.B;
                if (k % bmpData.Width != 0)
                {
                    if (Value[i + 5] + DK[0] < 0) Value[i + 5] = 0;
                    else if (Value[i + 5] + DK[0] > 255) Value[i + 5] = 255;
                    else Value[i + 5] = Convert.ToByte(Value[i + 5] + DK[0]);
                    if (Value[i + 4] + DK[1] < 0) Value[i + 4] = 0;
                    else if (Value[i + 4] + DK[1] > 255) Value[i + 4] = 255;
                    else Value[i + 4] = Convert.ToByte(Value[i + 4] + DK[1]);
                    if (Value[i + 3] + DK[2] < 0) Value[i + 3] = 0;
                    else if (Value[i + 3] + DK[2] > 255) Value[i + 3] = 255;
                    else Value[i + 3] = Convert.ToByte(Value[i + 3] + DK[2]);
                }
                if (i + Stride < bytes-3)
                {
                    if (Value[i + 2 + Stride] + DK[0] < 0) Value[i + 2 + Stride] = 0;
                    else if (Value[i + 2 + Stride] + DK[0] > 255) Value[i + 2 + Stride] = 255;
                    else Value[i + 2 + Stride] = Convert.ToByte(Value[i + 2 + Stride] + DK[0]);
                    if (Value[i + 1 + Stride] + DK[1] < 0) Value[i + 1 + Stride] = 0;
                    else if (Value[i + 1 + Stride] + DK[1] > 255) Value[i + 1 + Stride] = 255;
                    else Value[i + 1 + Stride] = Convert.ToByte(Value[i + 1 + Stride] + DK[1]);
                    if (Value[i + Stride] + DK[2] < 0) Value[i + Stride] = 0;
                    else if (Value[i + Stride] + DK[2] > 255) Value[i + Stride] = 255;
                    else Value[i + Stride] = Convert.ToByte(Value[i + Stride] + DK[2]);
                }
                if (i + Stride < bytes-3 && k % bmpData.Width != 0)
                {
                    if (Value[i + 5 + Stride] + DK[3] < 0) Value[i + 5 + Stride] = 0;
                    else if (Value[i + 5 + Stride] + DK[3] > 255) Value[i + 5 + Stride] = 255;
                    else Value[i + 5 + Stride] = Convert.ToByte(Value[i + 5 + Stride] + DK[3]);
                    if (Value[i + 4 + Stride] + DK[4] < 0) Value[i + 4 + Stride] = 0;
                    else if (Value[i + 4 + Stride] + DK[4] > 255) Value[i + 4 + Stride] = 255;
                    else Value[i + 4 + Stride] = Convert.ToByte(Value[i + 4 + Stride] + DK[4]);
                    if (Value[i + 3 + Stride] + DK[5] < 0) Value[i + 3 + Stride] = 0;
                    else if (Value[i + 3 + Stride] + DK[5] > 255) Value[i + 3 + Stride] = 255;
                    else Value[i + 3 + Stride] = Convert.ToByte(Value[i + 3 + Stride] + DK[5]);
                }
                
                if (k % bmpData.Width == 0 && k != 0) { i += bmpData.Stride - Stride; k = 0; };
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        private Color FindColor(Color Inp)
        {
            int index = 0;
            double Min = 255;
            for (int i = 0; i < inpColor.Length; i++)
            {
                double temp = Math.Sqrt(0.22 * Math.Pow((Inp.R - inpColor[i].R), 2) + 0.71 * Math.Pow((Inp.G - inpColor[i].G), 2) + 0.07 * Math.Pow((Inp.B - inpColor[i].B), 2));
                if (temp < Min) { Min = temp; index = i; }
            }
            return inpColor[index];
        }

       
    }
}
