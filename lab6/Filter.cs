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
    public class Filter
    {
        private int N;
        private PictureBox PB;
        public Filter(int size, PictureBox inpPB) {
            N = size;
            PB = inpPB;
        }

        struct ColorInt {
            public double I;
            public Color RGB;
        }

        private class Region
        {
            public double[] I;
            public int R,G,B;
            public double D;
            private int size;
            public ColorInt CI;
            private int N;

            public Region(int n) {
                size = 0;
                N = n;
                I = new double[N * N]; 
                D = 0;
                R=0; G= 0; B = 0;
            }
            public void Add(int iR,int iG, int iB) {
                R += iR; G += iG; B += iB;
                I[size++]= 0.22 * iR + 0.71 * iG + 0.07 * iB;
                if (size == N * N) {
                    CI.I = 0;
                    for (int i = 0; i < N * N; i++)
                        CI.I += I[i];
                    CI.I /= N * N;
                    CI.RGB = Color.FromArgb(R / (N * N), G / (N * N), B / (N * N));
                    for (int i = 0; i < N * N; i++)
                        D += Math.Pow(I[i] - CI.I,2);
                }
            }
        }

        public Bitmap AverageArithmetic() {
            int N3 = N * 3;
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] CRes = Average(RGB);
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
                if ((i + (N3) - 3) % bmpData.Stride == 0) i += N3 - 6;
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap AverageGeometric()
        {
            int N3 = N * 3;
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] CRes = AverageMul(RGB);
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
                if ((i + (N3) - 3) % bmpData.Stride == 0) i += N3 - 6;
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap Gauss(double S)
        {
            int N3 = N * 3;
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] CRes = GaussCalc(RGB, S);
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
                if ((i + (N3) - 3) % bmpData.Stride == 0) i += N3 - 6;
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap AverageDot()
        {
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            ColorInt[] CArrRes;
            Color CRes;
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                CArrRes = SortIntensive(RGB);
                CRes = Color.FromArgb((CArrRes[N * N - 1].RGB.R + CArrRes[0].RGB.R) / 2, (CArrRes[N * N - 1].RGB.G + CArrRes[0].RGB.G) / 2, (CArrRes[N * N - 1].RGB.B + CArrRes[0].RGB.B) / 2);
                Value[i + Center + 2] = CRes.R; Value[i + Center + 1] = CRes.G; Value[i + Center] = CRes.B;
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap TrancationAverage(int D)
        {
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            ColorInt[] CRes;
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N * 3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                CRes = SortIntensive(RGB);
                Color Res = ControledAverage(CRes, D);
                Value[i + Center + 2] = Res.R; Value[i + Center + 1] = Res.G; Value[i + Center] = Res.B;
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap ContrHarmonic(int Q)
        {
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N * 3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] CRes = Sum(RGB);
                if (CRes[0] != 0) CRes[0] = Convert.ToInt32(Math.Pow(CRes[0], Q + 1) / Math.Pow(CRes[0], Q)) / (N * N);
                if (CRes[1] != 0) CRes[1] = Convert.ToInt32(Math.Pow(CRes[1], Q + 1) / Math.Pow(CRes[1], Q)) / (N * N);
                if (CRes[2] != 0) CRes[2] = Convert.ToInt32(Math.Pow(CRes[2], Q + 1) / Math.Pow(CRes[2], Q)) / (N * N);
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
                if ((i + (N3) - 3) % bmpData.Stride == 0) i += N3 - 6;
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap Median()
        {
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            ColorInt CRes;
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N * 3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                CRes = SortIntensive(RGB)[N * N / 2];
                Value[i + Center + 2] = Convert.ToByte(CRes.RGB.R); Value[i + Center + 1] = Convert.ToByte(CRes.RGB.G); Value[i + Center] = Convert.ToByte(CRes.RGB.B);
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap UnderlineOutlines(int y)
        {
            double[] H1 = { -0.1111, -0.1111, -0.1111, -0.1111, 1.8888, -0.1111, -0.1111, -0.1111, -0.1111 };
            double[] H2 = { 0, -1.0, 0, -1.0, 5.0, -1.0, 0, -1.0, 0 };
            double[] H3 = { -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0, -2.0, -2.0, -1.0, -1.0, -2.0, 32.0, -2.0, -1.0, -1.0, -2.0, -2.0, -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0 };
            double[] H;
            if (y == 1) H = H1;
            else if (y == 2) H = H2;
            else H = H3;
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N * 3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] G = MultiplicationMatrix(RGB, H); int[] CRes = Sum(G);
                CRes = Min(CRes);
                CRes[0] += 127; CRes[1] += 127; CRes[2] += 127;
                if (CRes[0] > 255) CRes[0] = 255; if (CRes[0] < 0) CRes[0] = 0;
                if (CRes[1] > 255) CRes[1] = 255; if (CRes[1] < 0) CRes[1] = 0;
                if (CRes[2] > 255) CRes[2] = 255; if (CRes[2] < 0) CRes[2] = 0;
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap Outlines()
        {
            int[] H = { 0, -1, 0, -1, 4, -1, 0, -1, 0 };
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N * 3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] G = MultiplicationMatrix(RGB, H); int[] CRes = Sum(G);
                CRes = Min(CRes);
                CRes[0] += 127; CRes[1] += 127; CRes[2] += 127;
                if (CRes[0] > 255) CRes[0] = 255; if (CRes[0] < 0) CRes[0] = 0;
                if (CRes[1] > 255) CRes[1] = 255; if (CRes[1] < 0) CRes[1] = 0;
                if (CRes[2] > 255) CRes[2] = 255; if (CRes[2] < 0) CRes[2] = 0;
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap MinMax(int key)
        {
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            ColorInt CRes;
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                if (key == 0) CRes = SortIntensive(RGB)[0];
                else CRes = SortIntensive(RGB)[N * N - 1];
                Value[i + Center + 2] = Convert.ToByte(CRes.RGB.R); Value[i + Center + 1] = Convert.ToByte(CRes.RGB.G); Value[i + Center] = Convert.ToByte(CRes.RGB.B);
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        public Bitmap Sobel()
        {
            int[] Hx = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
            int[] Hy = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int[] CRes = new int[3];
                int index = 0;
                int[] RGB = new int[N * N3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] Gx = MultiplicationMatrix(RGB, Hx);
                int[] Gy = MultiplicationMatrix(RGB, Hy);
                Gx = Sum(Gx); Gy = Sum(Gy);
                CRes[0] = (int)Math.Sqrt(Gx[0] * Gx[0] + Gy[0] * Gy[0]);
                CRes[1] = (int)Math.Sqrt(Gx[1] * Gx[1] + Gy[1] * Gy[1]);
                CRes[2] = (int)Math.Sqrt(Gx[2] * Gx[2] + Gy[2] * Gy[2]);
                CRes = Min(CRes);
                if (CRes[0] > 255) CRes[0] = 255; if (CRes[0] < 0) CRes[0] = 0;
                if (CRes[1] > 255) CRes[1] = 255; if (CRes[1] < 0) CRes[1] = 0;
                if (CRes[2] > 255) CRes[2] = 255; if (CRes[2] < 0) CRes[2] = 0;
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }
        public Bitmap Laplas()
        {
            int[] H1 = { 1, 1, 1, 1, -8, 1, 1, 1, 1 };
            int[] H2 = { 2, -1, 2, -1, -4, -1, 2, -1, 2 };
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] G = MultiplicationMatrix(RGB, H1); int[] CRes = Sum(G);
                CRes = Min(CRes);
                if (CRes[0] > 255) CRes[0] = 255; if (CRes[0] < 0) CRes[0] = 0;
                if (CRes[1] > 255) CRes[1] = 255; if (CRes[1] < 0) CRes[1] = 0;
                if (CRes[2] > 255) CRes[2] = 255; if (CRes[2] < 0) CRes[2] = 0;
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }


        public Bitmap Kuvahare()
        {
            int N3 = N * 3;
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                Color CRes = FindRegionColor(rgbValues,i+Center,bmpData.Stride);
                Value[i + Center + 2] = CRes.R; Value[i + Center + 1] = CRes.G; Value[i + Center] = CRes.B;
                if ((i + (N3) - 3) % bmpData.Stride == 0) i += N3 - 6;
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        private Color FindRegionColor(byte[] Arr, int index, int Stride)
        {
            int R = N / 2;
            if (N % 2 != 0) R++; 
            Region[] R4 = new Region[4];
            R4[0] = new Region(R);
            R4[1] = new Region(R);
            R4[2] = new Region(R);
            R4[3] = new Region(R);
            for (int i = 0; i < R; i++)
                for (int j = 0; j < R * 3; j += 3)
                {
                    R4[0].Add(Arr[index + 2 - Stride * i - j], Arr[index + 1 - Stride * i - j], Arr[index - Stride * i - j]);
                    R4[1].Add(Arr[index + 2 - Stride * i + j], Arr[index + 1 - Stride * i + j], Arr[index - Stride * i + j]);
                    R4[2].Add(Arr[index + 2 + Stride * i - j], Arr[index + 1 + Stride * i - j], Arr[index + Stride * i - j]);
                    R4[3].Add(Arr[index + 2 + Stride * i + j], Arr[index + 1 + Stride * i + j], Arr[index + Stride * i + j]);
                }
            double min = R4[0].D;
            Color Res = R4[0].CI.RGB;
            for (int i = 1; i < 4; i++)
            {
                if (R4[i].D < min)
                {
                    min = R4[i].D;
                    Res = R4[i].CI.RGB;
                }
            }
            return Res;
        }

        public Bitmap Stamping()
        {
            int[] H = { 0, -1, 0, 1, 0, -1 ,0, 1,0 };
            var bmp = PB.Image as Bitmap;
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int N3 = N * 3;
            int Center = N / 2 * bmpData.Stride + N3 / 2 - 1;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] Value = (byte[])rgbValues.Clone();
            for (int i = 0; i < bytes - bmpData.Stride * (N - 1) - N3 + 3; i += 3)
            {
                int index = 0;
                int[] RGB = new int[N * N * 3];
                for (int j = 0; j < N; j++)
                    for (int k = i; k < i + N3; k++)
                        RGB[index++] = rgbValues[k + j * bmpData.Stride];
                int[] G = MultiplicationMatrix(RGB, H); int[] CRes = Sum(G);
                CRes = Min(CRes);
                CRes[0] += 127; CRes[1] += 127; CRes[2] += 127;
                if (CRes[0] > 255) CRes[0] = 255; if (CRes[0] < 0) CRes[0] = 0;
                if (CRes[1] > 255) CRes[1] = 255; if (CRes[1] < 0) CRes[1] = 0;
                if (CRes[2] > 255) CRes[2] = 255; if (CRes[2] < 0) CRes[2] = 0;
                Value[i + Center + 2] = Convert.ToByte(CRes[0]); Value[i + Center + 1] = Convert.ToByte(CRes[1]); Value[i + Center] = Convert.ToByte(CRes[2]);
            }
            Marshal.Copy(Value, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            PB.Invalidate();
            return bmp;
        }

        private int[] MultiplicationMatrix(int[] mas,int[] Matrix) {
            int[] temp = new int[mas.Length];
            for (int i = 0; i < N * N * 3; i += 3)
            {
                temp[i] = mas[i] * Matrix[i/3];
                temp[i + 1] = mas[i + 1] * Matrix[i / 3];
                temp[i+2] = mas[i + 2]*Matrix[i / 3];
            }
            return temp;
        }

        private int[] MultiplicationMatrix(int[] mas, double[] Matrix)
        {
            int[] temp = new int[mas.Length];
            for (int i = 0; i < N * N * 3; i += 3)
            {
                temp[i] = Convert.ToInt32(mas[i] * Matrix[i / 3]);
                temp[i + 1] = Convert.ToInt32(mas[i + 1] * Matrix[i / 3]);
                temp[i + 2] = Convert.ToInt32(mas[i + 2] * Matrix[i / 3]);
            }
            return temp;
        }
        private int[] Min(int []mas) {
            if (mas[0] < mas[1] && mas[0] < mas[2]) { mas[1] = mas[0]; mas[2] = mas[0]; }
            if (mas[1] < mas[0] && mas[1] < mas[2]) { mas[0] = mas[1]; mas[2] = mas[1]; }
            else { mas[0] = mas[2]; mas[1] = mas[2]; }
            return mas;
        }

        private int[] Average(int[] mas)
        {
            double[] temp = { 0, 0, 0 };
            int[] value = new int[3];
            for (int i = 0; i < N * N * 3; i += 3)
            {
                temp[0] += mas[i + 2];
                temp[1] += mas[i + 1];
                temp[2] += mas[i];
            }
            value[0] = Convert.ToInt32(temp[0] / (N * N));
            value[1] = Convert.ToInt32(temp[1] / (N * N));
            value[2] = Convert.ToInt32(temp[2] / (N * N));
            return value;
        }

        private Color ControledAverage(ColorInt[] CI,int D)
        {
            int Ln = N*N-(int)Math.Truncate(D/2.0)*2;
            double[] temp = { 0, 0, 0 };
            int[] value = new int[3];
            for (int i = D/2; i < D/2+Ln; i++)
            {
                temp[0] += CI[i].RGB.R;
                temp[1] += CI[i].RGB.G;
                temp[2] += CI[i].RGB.B;
            }
            value[0] = Convert.ToInt32(temp[0] / Ln);
            value[1] = Convert.ToInt32(temp[1] / Ln);
            value[2] = Convert.ToInt32(temp[2] / Ln);
            return Color.FromArgb(value[0], value[1], value[2]);
        }

        private int[] GaussCalc(int[] mas, double S)
        {
            int R = N / 2;
            double sum = 0;
            double[,] A = new double[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    A[i, j] = Math.Exp(-((i - R) * (i - R) + (j - R) * (j - R)) / (2 * S * S));
                    sum += A[i, j];
                }
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    A[i, j] /= sum;
            double[] temp = { 0, 0, 0 };
            int[] value = new int[3];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N * 3; j += 3)
                {
                    temp[0] += mas[i * N * 3 + j + 2] * A[i, j / 3];
                    temp[1] += mas[i * N * 3 + j + 1] * A[i, j / 3];
                    temp[2] += mas[i * N * 3 + j] * A[i, j / 3];
                }
            value[0] = Convert.ToInt32(Math.Round(temp[0]));
            value[1] = Convert.ToInt32(Math.Round(temp[1]));
            value[2] = Convert.ToInt32(Math.Round(temp[2]));
            return value;
        }

        private int[] AverageMul(int[] mas)
        {
            double[] temp = { 1, 1, 1 };
            int[] value = new int[3];
            for (int i = 0; i < N * N * 3; i += 3)
            {
                temp[0] *= mas[i + 2];
                temp[1] *= mas[i + 1];
                temp[2] *= mas[i];
            }
            value[0] = Convert.ToInt32(Math.Pow(temp[0],1.0/(N * N)));
            value[1] = Convert.ToInt32(Math.Pow(temp[1], 1.0 / (N * N)));
            value[2] = Convert.ToInt32(Math.Pow(temp[2], 1.0 / (N * N)));
            return value;
        }

        private int[] Sum(int[] mas)
        {
            int[] res = { 0, 0, 0 };
            for (int i = 0; i < N * N * 3; i += 3)
            {
                res[0] += mas[i + 2];
                res[1] += mas[i + 1];
                res[2] += mas[i];
            }
            return res;
        }

        private ColorInt[] SortIntensive(int[] mas)
        {
            ColorInt[] CI=new ColorInt[N*N];
            int k = 0;
            for (int i = 0; i < N * N * 3; i += 3)
            {
                CI[k].I = (0.22 * mas[i + 2]) + (0.71 * mas[i + 1]) + (0.07 * mas[i]);
                CI[k++].RGB = Color.FromArgb(mas[i + 2], mas[i + 1], mas[i]);
            }
            CI = BubbleSort(CI);
            return CI;
        }

        private ColorInt[] BubbleSort(ColorInt[] A)
        {
            for (int i = 0; i < A.Length - 1; i++)
                for (int j = i + 1; j < A.Length; j++)
                    if (A[i].I > A[j].I)
                    {
                        ColorInt temp = A[i];
                        A[i] = A[j]; A[j] = temp;
                    }
            return A;
        }
    }
}
