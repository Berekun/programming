using DAM;

namespace ImageFilters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                string path = "C:\\Users\\danberinf\\Desktop\\Images\\test.png";
                DAM.Image img=new DAM.Image();
                img.Config(800,600,new DAM.RGBA(1.0,0.0,0.0,1.0));
                img.Save(path);
            }

            {
                int x, y, width, heigth;
                x = 30;
                y = 20;
                heigth = 500;
                width = 100;
                string path = "C:\\Users\\danberinf\\Desktop\\Images\\test.png";
                DAM.RGBA color = new RGBA(4.0, 6.0, 8.0, 1.0);
                DAM.Image img2 = new DAM.Image();
                img2.Config(800, 600, new DAM.RGBA(1.0, 0.0, 0.0, 1.0));
                img2 = Paintools.FillRectangle(img2, x, y, width, heigth, color);
                img2.Save(path);
            }
        }
    }
}