﻿using DAM;
using System;

namespace ImageFilters
{
    internal class Paintools
    {
        public static void FillRectangle(Image image,int x, int y,int width, int height,RGBA color)
        {
            for(int h=y;h <= height;h++)
            {
                for (int w=x;w <= width;w++)
                {
                    image.SetPixel(h,w,color);
                }
            }
        }

        public static void Changecolor(Image image)
        {
            for(int h= 0; h <= image.Height;h++)
            {
                for (int w= 0;w <=image.Width;w++)
                {
                    RGBA value = image.GetPixelAt(w,h);
                    double values = (value.r + value.g + value.b) /3;
                    value.r = values;
                    value.g = values;
                    value.b = values;
                    value.a = 1.0;

                    //RGBA grey = new RGBA(values, values, values, 1.0);

                    image.SetPixel(w,h,value);
                }
            }
        }

        public static void MultiplyPixel(Image image,double r,double g,double b,double a)
        {
            for (int h = 0; h <= image.Height; h++)
            {
                for (int w = 0; w <= image.Width; w++)
                {
                    RGBA value = image.GetPixelAt(w, h);
                    value.r = value.r* r;
                    value.g = value.g* g;
                    value.b = value.b* b;
                    value.a = a;    
                    image.SetPixel(w, h, value);
                }
            }
        }

        public static void InvertImage(Image img)
        {
            int max_height = img.Height;

            for (int h = 0; h <= img.Height/2; h++, max_height--)
            {
                for (int w = 0; w <= img.Width; w++)
                {
                    RGBA firstpixel = img.GetPixelAt(w, h);
                    RGBA lastpixel = img.GetPixelAt(w, max_height);

                    img.SetPixel(w, max_height, firstpixel);

                    img.SetPixel(w, h, lastpixel);
                }
            }
        }

        public static double GetCircular(double value, double min, double max)
        {
            double dist = (max - min);

            while (value > max)
            {
                value -= dist;

            }

            while (value < min)
            {
                value += dist;
            }

            return value;
        }

        public static void RotateHue(Image img,Image img2, double hueIncrement)
        {
            for(int h = 0; h < img.Height; h++)
            {
                for(int w = 0; w < img.Width; w++)
                {
                    RGBA rgba = img.GetPixelAt(w, h);
                    HSLA hsla = rgba.ToHSL();
                    hsla.h = Paintools.GetCircular(hueIncrement,0,1);
                    rgba = hsla.ToRGBA();
                    img2.SetPixel(w, h,rgba);

                }
            }
        }

        public static void ChageEspecificHue(Image img,Image img2, double hueIncrement, double min,double max)
        {
            int red = 0;
            for (int h= 0; h < img.Height; h++)
            {
                for(int w= 0; w < img.Width; w++)
                {
                    RGBA rgba = img.GetPixelAt(w, h);
                    HSLA hsla = rgba.ToHSL();
                    if ((hsla.h < min) || (max < hsla.h))
                    {
                        hsla.h = GetCircular(hueIncrement, 0, 1);
                        rgba = hsla.ToRGBA();
                        img2.SetPixel(w, h, rgba);
                        red++;
                    }
                    else if((hsla.h > min)&&(max > hsla.h)&&(red == 0))
                    {
                        hsla.h = GetCircular(hueIncrement, 0, 1);
                        rgba = hsla.ToRGBA();
                        img2.SetPixel(w, h, rgba);
                    }
                    else
                    {
                        img2.SetPixel(w, h, rgba);
                    }

                }
            }
        }
    }
}
