using DAM;

namespace ImageFilters
{
    internal class Program
    {

        static void Test1()
        {
            {
                string path = "C:\\Users\\danberinf\\Desktop\\Images\\test.png";
                Image img = new Image();
                img.Config(800, 600, new DAM.RGBA(1.0, 0.0, 0.0, 1.0));
                img.Save(path);
            }

            {
                int x, y, width, heigth;
                x = 30;
                y = 20;
                heigth = 500;
                width = 100;
                string path = "C:\\Users\\danberinf\\Desktop\\Images\\test1.png";
                RGBA color = new RGBA(4.0, 6.0, 8.0, 1.0);
                Image img2 = new Image();
                img2.Config(800, 600, new RGBA(1.0, 0.0, 0.0, 1.0));
                Paintools.FillRectangle(img2, x, y, width, heigth, color);
                Paintools.FillRectangle(img2, x, y, width, heigth, color);
                img2.Save(path);
            }
        }

        static void Test2()
        {
            string path = "C:\\Users\\danberinf\\Desktop\\Images\\";
            string in_path = path + "foto_asta.jpg";
            string out_path = path + "bw.jpg";
            Image img = new Image();
            img.Load(in_path);
            Paintools.Changecolor(img);
            img.Save(out_path);
        }

        static void Test3()
        {
            string path = "C:\\Users\\danberinf\\Desktop\\Images\\";
            string in_path = path + "foto_asta.jpg";
            string out_path = path + "multiply.jpg";
            Image img = new Image();
            img.Load(in_path);
            Paintools.MultiplyPixel(img,0,0,1,1);
            img.Save(out_path);
        }
        static void Main(string[] args)
        {
            Test1();
            Test2();
            Test3();
        }
    }
}