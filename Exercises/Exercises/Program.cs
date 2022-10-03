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
        }
    }
}   