namespace Exercises
{
    internal class Program
    {
        static void Main(string[] args)
        
        {
            int number;
            number = 33;
            bool solution;

            solution = Exercises.IsPrime(number);
            Console.WriteLine(solution);

            Console.WriteLine("---------------");

            {
                double radio, x;
                radio = 8;

                x = Exercises.GetCircleArea(radio);
                Console.WriteLine(x);
            }

            Console.WriteLine("---------------");

            {
                double height, weight, x;
                height = 7.2;
                weight = 5.3;

                x = Exercises.GetRectangleArea(height,weight);
                Console.WriteLine(x);
            }

            Console.WriteLine("---------------");

            {
                double x1, x2, y1, y2, x;
                x1 = 1;
                x2 = 4;
                y1 = 8;
                y2 = 15;

                x = Exercises.GetDistance2points(x1, x2, y1, y2);
                Console.WriteLine(x);
            }

            Console.WriteLine("---------------");

            {
                int a;
                string x;
                a = 34;

                x = Exercises.Getsequecyofprime(a);
                Console.WriteLine(x);
            }

            Console.WriteLine("---------------");

            {
                int n;
                string x;
                n = 344;

                x = Exercises.GenerateFibonacci(n);
                Console.WriteLine(x);
            }

            Console.WriteLine("---------------");

            {
                string n;
                string x;
                n = "Hola";

                x = Exercises.Getscript(n);
                Console.WriteLine(x);
            }

            Console.WriteLine("---------------");

            {
                char mylether;
                char start;
                char finish;
                bool x;

                mylether = 'f';
                start = 'a';
                finish = 'p';

                x = Exercises.Getlether(mylether,start,finish);
                Console.WriteLine(x);
            }

            Console.WriteLine("---------------");

            {
                string word;
                bool x;

                word = "holaaa@gmail.es";

                x = Exercises.IsEmail(word);
                Console.WriteLine(x);
            }

            Console.WriteLine("---------------");

            {
                string word;
                bool x;

                word = "sexo@gmail.com";

                x = Exercises.IsEmail2(word);
                Console.WriteLine(x);

            }

            Console.WriteLine("---------------");

            {
                Vector3D a = new Vector3D();
                Vector3D b = new Vector3D();

                a.x = 4;
                b.x = 7;

                a.y = 6;
                b.y = 9;

                a.z = 10;
                b.z = 11;

                double x;

                x = Exercises.GetDistance(a, b);
                Console.WriteLine(x);

            }

            Console.WriteLine("---------------"); 

            {
                Vector3D a = new Vector3D();

                a.x = 8;
                a.y= 9;
                a.z = 10;

                double x;

                x = Exercises.GetModule(a);
                Console.WriteLine(x);
            }



        }
    }
}   