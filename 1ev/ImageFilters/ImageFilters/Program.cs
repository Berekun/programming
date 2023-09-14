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
            string path = "D:\\Documentos\\Deberes de Dani\\Programacion\\programminga\\programming\\3ev\\TinyRpg\\TinyRpg\\resources\\map\\";
            string in_path = path + "world.png";
            string out_path = path + "world2.png";
            Image img = new Image();
            img.Load(in_path);
            Paintools.Changecolor(img);
            img.Save(out_path);
        }

        static void Test3()
        {
            string path = "D:\\Documentos\\Deberes de Dani\\Programacion\\programminga\\programming\\3ev\\TinyRpg\\TinyRpg\\resources\\map\\";
            string in_path = path + "world.png";
            string out_path = path + "world2.png";
            Image img = new Image();
            img.Load(in_path);
            Paintools.MultiplyPixel(img,1,0,0,1);
            img.Save(out_path);
        }

        static void Test4()
        {
            string path = "C:\\Users\\danberinf\\Desktop\\Images\\";
            string in_path = path + "foto_asta.jpg";
            string out_path = path + "invert3.jpg";
            Image img = new Image();
            img.Load(in_path);
            Paintools.InvertImage(img);
            img.Save(out_path);
        }

        static void Test5()
        {
            double hueIncrement = 0.3;
            string path = "D:\\Documentos\\Deberes de Dani\\Programacion\\programming\\Images\\";
            string in_path = path + "Joseph.png";
            string out_path = path + "changed.png";
            Image img = new Image();
            Image img2 = new Image();
            img.Load(in_path);
            img2.Config(img.Width, img.Height);
            Paintools.RotateHue(img, img2, hueIncrement);
            img2.Save(out_path);
        }
        static void Test6()
        {
            double hueIncrement = 0.75;
            string path = "D:\\Documentos\\Deberes de Dani\\Programacion\\programming\\Images\\";
            string in_path = path + "Joseph.png";
            string out_path = path + "changed2.png";
            Image img = new Image();
            Image img2 = new Image();
            img.Load(in_path);
            img2.Config(img.Width, img.Height);
            Paintools.ChageEspecificHue(img, img2, hueIncrement,0.4,0.6);
            img2.Save(out_path);
        }

        static void Test7()
        {
            string path = "C:\\Users\\danberinf\\Desktop\\Images\\";
            string in_path = path + "arcoiris.jpg";
            string out_path = path + "Discretized.jpg";
            Image img = new Image();
            Image img2 = new Image();
            img.Load(in_path);
            img2.Config(img.Width, img.Height);
            Paintools.Discretize(img, img2,0.9,0.1);
            img2.Save(out_path);
        }



        static void Test8()
        {
            string path = "C:\\Users\\danberinf\\Desktop\\Images\\";
            string in_path = path + "cocherojo.jpg";
            string out_path = path + "blued.jpg";
            Image img = new Image();
            Image img2 = new Image();
            img.Load(in_path);
            img2.Config(img.Width, img.Height);
            Paintools.Blur(img,img2);
            img2.Save(out_path);
        }

        static void Main(string[] args)
        {
            Test3();
        }



    }
}