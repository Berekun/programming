using DAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    internal class Paintools
    {
        public static DAM.Image FillRectangle(DAM.Image image,int x, int y,int width, int height,RGBA color)
        {
            for(int h=y;h <= height;h++)
            {
                for (int w=x;w <= width;w++)
                {
                    image.SetPixel(h,w,color);
                }
            }

            return image;
        }
    }
}
