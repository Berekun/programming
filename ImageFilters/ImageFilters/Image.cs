
using System.Drawing;

namespace DAM
{
    public struct RGBA
    {
        public double r, g, b, a;

        public RGBA(double R, double G, double B, double A)
        {
            r = R;
            g = G;
            b = B;
            a = A;
        }
         
        public int IntR
        {
            get
            {
                int result = (int)(r * 255);
                if (result > 255)
                    return 255;
                if (result < 0)
                    return 0;
                return result;
            }
        }

        public int IntG
        {
            get
            {
                int result = (int)(g * 255);
                if (result > 255)
                    return 255;
                if (result < 0)
                    return 0;
                return result;
            }
        }

        public int IntB
        {
            get
            {
                int result = (int)(b * 255);
                if (result > 255)
                    return 255;
                if (result < 0)
                    return 0;
                return result;
            }
        }

        public int IntA
        {
            get
            {
                int result = (int)(a * 255);
                if (result > 255)
                    return 255;
                if (result < 0)
                    return 0;
                return result;
            }
        }

        public static RGBA operator + (RGBA a, RGBA b)
        {
            return new RGBA(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        }

        public static RGBA operator * (RGBA a, double b)
        {
            return new RGBA(a.r + b, a.g + b, a.b + b, a.a + b);
        }

        public static RGBA operator * (RGBA a, RGBA b)
        {
            return new RGBA(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        }

        public static RGBA operator -(RGBA a)
        {
            return new RGBA(-a.r, -a.g, -a.b, -a.a);
        }
        public static RGBA operator !(RGBA a)
        {
            return new RGBA(1.0 - a.r, 1.0 - a.g, 1.0 - a.b, 1.0 - a.a);
        }

        public HSLA ToHSL()
        {
            HSLA result = new HSLA(0, 0, 0, 0);
            double max = System.Math.Max(System.Math.Max(r, g), b);
            double min = System.Math.Min(System.Math.Min(r, g), b);
            result.h = result.s = result.l = (max + min) / 2;
            if (max == min)
                result.h = result.s = 0;
            else
            {
                double d = max - min;
                result.s = (result.l > 0.5) ? d / (2 - max - min) : d / (max + min);
                if (max == r)
                    result.h = (g - b) / d + (g < b ? 6 : 0);
                else if (max == g)
                    result.h = (b - r) / d + 2;
                else if (max == b)
                    result.h = (r - g) / d + 4;
                result.h /= 6;
            }
            result.a = a;
            return result;
        }
    }


    public struct HSLA
    {
        public double h, s, l, a;

        public HSLA(double H, double S, double L, double A)
        {
            h = H;
            s = S;
            l = L;
            a = A;
        }

        public RGBA ToRGBA()
        {
            RGBA result = new RGBA(0, 0, 0, 0);
            if (0 == s)
                result.r = result.g = result.b = l; // achromatic         
            else
            {
                double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                double p = 2 * l - q;
                result.r = hue2rgb(p, q, h + 1.0 / 3);
                result.g = hue2rgb(p, q, h);
                result.b = hue2rgb(p, q, h - 1.0 / 3);
            }
            result.a = a;
            return result;
        }

        private static double hue2rgb(double p, double q, double t)
        {
            if (t < 0)
                t += 1;
            if (t > 1)
                t -= 1;
            if (t < 1.0 / 6)
                return p + (q - p) * 6 * t;
            if (t < 1.0 / 2)
                return q;
            if (t < 2.0 / 3)
                return p + (q - p) * (2.0 / 3 - t) * 6;
            return p;
        }
    }

    public class Image
    {
        private int width;
        private int height;
        private RGBA[] pixels;

        public int Width { get { return width; } }
        public int Height { get { return height; } }


        public Image()
        {
        }

        public void Config(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.pixels = new RGBA[width * height];
        }

        public void Config(int width, int height, RGBA value)
        {
            this.width = width;
            this.height = height;
            this.pixels = new RGBA[width * height];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = value;
        }

        public RGBA GetPixelAt(int x, int y)
        {
            if (pixels == null || pixels.Length == 0)
                return new RGBA(0.0, 0.0, 0.0, 0.0);
            if (x < 0)
                return new RGBA(0.0, 0.0, 0.0, 0.0);
            if (x >= width)
                return new RGBA(0.0, 0.0, 0.0, 0.0);
            if (y < 0)
                return new RGBA(0.0, 0.0, 0.0, 0.0);
            if (y >= height)
                return new RGBA(0.0, 0.0, 0.0, 0.0);
            return pixels[y * width + x];
        }

        public RGBA GetPixelAtNearest(int x, int y)
        {
            if (pixels == null || pixels.Length == 0)
                return new RGBA(0.0, 0.0, 0.0, 0.0);
            if (x < 0)
                x = 0;
            else if (x >= width)
                x = width - 1;
            if (y < 0)
                y = 0;
            else if (y >= height)
                y = height - 1;
            return pixels[y * width + x];
        }

        public void SetPixel(int x, int y, RGBA value)
        {
            if (x < 0)
                return;
            else if (x >= width)
                return;
            if (y < 0)
                return;
            else if (y >= height)
                return;
            pixels[y * width + x] = value;
        }

        public void Save(string path)
        {
            using (Bitmap bm = new Bitmap(Width, Height))
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        var p = GetPixelAt(x, y);
                        Color c = Color.FromArgb(p.IntA, p.IntR, p.IntG, p.IntB);
                        bm.SetPixel(x, y, c);
                    }
                }
                bm.Save(path);
            }
        }

        public void Load(string url)
        {
            using (Bitmap bm = new Bitmap(url, false))
            {
                Config(bm.Width, bm.Height);
                for (int x = 0; x < bm.Width; x++)
                {
                    for (int y = 0; y < bm.Height; y++)
                    {
                        var p = bm.GetPixel(x, y);
                        RGBA c = new RGBA(p.R / 255.0, p.G / 255.0, p.B / 255.0, p.A / 255.0);
                        SetPixel(x, y, c);
                    }
                }
            }
        }

    }
}

