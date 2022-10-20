using DAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

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


    }
}
