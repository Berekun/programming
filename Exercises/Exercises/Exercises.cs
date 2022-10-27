using System;
using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Exercises
{
    internal class Exercises
    {
        public static bool GetEveOrOdd(double a)
        {
            return ((a % 2) == 0);
        }

        public static bool IsPrime(int number)
        {
            double solution = 0;

            for (int i = 2; i < number; i++)
            {
                solution = number % i;

                if (solution == 0)
                {
                    return false;
                }
            }

            return true;
        }


        public static double GetCircleArea(double radio)
        {
            double pi = Math.PI;

            return pi * (radio * radio);
        }

        public static double GetRectangleArea(double height, double weight)
        {
            return weight * height;
        }

        public static double GetDistance2points(double x1, double x2, double y1, double y2)
        {
            double firstparent = (x2 - x1) * (x2 - x1);
            double secondparent = (y2 - y1) * (y2 - y1);
            double sume = firstparent + secondparent;
            double sqrt = Math.Sqrt(sume);

            return sqrt;
        }

        public static string Getsequecyofprime(int number)
        {
            string result = "";
            for (int i = 1; i < number; i++)
            {
                if (IsPrime(i))
                    result += i + ",";
            }

            return result;
        }

        public static string GenerateFibonacci(int n)
        {
            if (n <= 0)
                return "Nan";
            string result = "0,1";
            int r1 = 0, r2 = 1; int alter = 0;
            for (int i = 0; r1 < n && r2 < n; i++)
            {

                if (alter == 0)
                {
                    r1 = r1 + r2;
                    result += "," + r1;
                    alter = 1;
                }
                else
                {
                    r2 = r2 + r1;
                    result += "," + r2;
                    alter = 0;
                }
            }

            return result;
        }

        public static string Getscript(string word)
        {
            string result = "";

            for (int i = 0; i < word.Length; i++)
            {
                char c = word[i];
                if (i == word.Length - 1)
                    result += c;
                else
                    result += c + "-";
            }

            return result;
        }

        public static bool Getlether(char mychar, char start, char finish)
        {
            if ((start <= mychar) && (mychar <= finish))
            {
                return true;
            }

            return false;
        }

        public static bool IsEmail(string word)
        {
            int length = word.Length;
            int length2 = word.Length;
            string result1 = "";
            string result2 = "";

            for (int z = 0; z < length2; z++)
            {
                char error = word[length2 - 1];
                length2--;

                if ((error == '@') || (error == '.'))
                {
                    for (int x = 0; x < length2; x++)
                    {
                        error = word[length2 - 1];
                        length2--;

                        if (error == '.')

                            return false;

                        if (error == '@')
                        {
                            for (int y = 0; y < length2; y++)
                            {
                                error = word[length2 - 1];
                                length2--;

                                if (error == '@')

                                    return false;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < word.Length; i++)
            {
                char c = word[length - 1];
                length--;
                result1 += c;
                if ((c == '.') && (result1.Length > 1))
                {
                    for (int a = 0; a < word.Length; a++)
                    {
                        c = word[length - 1];
                        length--;
                        result2 += c;
                        if ((c == '@') && (result2.Length > 1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static int GetNumberOf(string word,char character)
        {
            int numerof = 0;
            int length = word.Length;
            char c;

            for (int i = 0; i < word.Length; i++)
            {
                c = word[length - 1];
                length--;

                if (c == character)
                    numerof++;

            }

            // si word es algo que tiene algo parecido a length
            // y puedo hacer word[i]

            // foreach (char c in word)
            // {
            //      if (c == character)
            //              count++;
            // {


            return numerof;
        }

        public static int ContaintwoDots(string word)
        {
            int length = word.Length;
            char c;

            // si el && dentro de un if la primera condicion deja de leer la siguiente

            for (int i = 0; i < word.Length; i++)
            {
                c = word[length - 1];
                length--;

                if (c == '.' && word[length + 1] == '.')

                    return 2;
            }


            return 1;

        }

        public static int ContainsNotValidCharacters(string word)
        {
            int numerofinvalid = 0;
            int length = word.Length;
            char c;

            for(int i = 0; i < word.Length; i++)
            {
                c = word[length - 1];
                length--;

                if ((c != '.') && (c != '@'))
                {
                    if ((c < '0') || ('9' < c))
                    {
                        if ((c < 'A') || ('z' < c))
                        {
                            numerofinvalid++;
                        }

                        if ((c < 'a') && ('A' < c))
                        {
                            numerofinvalid++;
                        }
                    }
                }
            }
            return numerofinvalid;
        }

        public static bool IsEmail2(string mail)
        {
            if(GetNumberOf(mail,'@') != 1)
                return false;
            if (ContaintwoDots(mail) != 1)
                return false;
            if (ContainsNotValidCharacters(mail) != 0)
                return false;
            if (mail[0] == '@' || mail[0] == '.')
                return false;

            // return posDot > posArroba; esto devolveria true o false

                return true;
        }

        public static double GetDistance(Vector3D a,Vector3D b)
        {
            double solution;
            double x = b.x - a.x;
            double y = b.y - a.y;
            double z = b.z - a.z;
            solution = Math.Sqrt((x * x) + (y * y) + (z * z));

            return solution;
        }

        public static double GetModule(Vector3D a)
        {
            double x = a.x;
            double z = a.z;
            double y = a.y;

            return  Math.Sqrt(x*x+y*y+z*z);
        }

        public static int CalculateProductory(int number)
        {
            int result = 1;

            if(number <= 0)
            {
                return 0;
            }

            for(int i = 2; i <= number; i++)
            {
                result = result * i;
            }

            return result;
        }

        public static int GetMCD(int min, int max)
        {
            for (int i = min; i >= 1; i--)
            {
                if ((min % i == 0) && (max % i == 0))
                {
                    return i;
                }
            }

            return 1;
        }

        public static double GetSaturate(double valor,double min,double max)
        {
            if (valor <= min)
                return min;
            if (valor >= max)
                return max;
            return valor;
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

        public static double Interpolate(double value,double max,double min)
        {
            if ((value < 0) || (1 < value))
                return double.NaN;
            double result = min + (max - min) * value;
            return result;
        }

        public static double Ulerp(double value,double max,double min)
        {
            double dist = max - min;
            double result = (value - min) / dist;
            return result;
        }

        public static (double, double) GetMaxandMind(double a,double b)
        {
            if (a > b)
                return (a, b);
            else
                return (b,a);
        }

        public static double Funtion1(double a,double b)
        {
            if (a > b)
                return -1;
            else if (b > a)
                return 1;
            return 0;
        }

        public static string GetNumberofBinary(int a)
        {
            string result = "";
            while (a > 0)
            {
                result += a % 2;
                a = a / 2;
            }

            return result;
        }

        public static char GetMayusc(char a)
        {
            if ('a' < a && 'z' < a)
            return (char)(a - 'a' + 'A');

            return a;
        }

        public static double GetMediaofTemperature(double a,double b)
        {
            double result = a + b;
            return result / 2;
        }

        public static string GetMorse(string a)
        {
            string result = "";
            for(int i = 0; i < a.Length; i++)
            {
                char upper = GetMayusc(a[i]);
                char c = a[i];
                if (c == 'a')
                    result += ".-";
                if (c == 'b')
                    result += "-...";
                if (c == 'c')
                    result += " -.-.";
            }

            return result;
        }

        public static void GetTableOfMultiply(int a)
        {
            for(int i= 0; i <= 12; i++)
            {
                string result = "";
                Console.Write(result += a * i + ",");
            }
        }

        public static string GetNnumbersOfThisSequency(int a)
        {
            string result = "-1,";
            int number = -1;
            for (int i = 0; i < a - 1;i++)
            {  
                if (number > 0)
                {
                    number = (number * (-1)) * 2;
                    result += number + ",";
                }
                else if (number < 0)
                {
                    number = (number * (-1)) * 2;
                    result += number + ",";
                }
            }

            return result;

        }

        public static int GetSumeOfPrimary(int num1,int num2)
        {
            for(int i = 0; i < num1; i++)
            {

            }

            return num1;
        }

        public static int MinorDivisible(int a)
        {
            for(int i = 2; i <= a; i++)
            {
                int result = a % i;
                if (result == 0)
                    return i;
            }

            return a;
        }

        public static string GetDescomprimed(int a)
        {
            string result = "";

            while (a > 1)
            {
                result += MinorDivisible(a) + "-";
                a = a / MinorDivisible(a);
            }
            return result;
        }

        public static string GetSecuencyofCollatz(int a)
        {
            string result = "" + a;
            while (a != 1)
            {
                if (a % a == 0)
                {
                    a = a / 2;
                    result += "," + a;
                }
                    
                if (a % a != 0)
                {
                    a = a * 3 + 1;
                    result += "," + a;
                }
            }

            return result;
        }   

        public static (int num,int denom) GetSimplificedDivision(int num,int denom)
        {
            int max = GetMinor(num,denom) / 2;
            for(int i = 2; i < max; i++)
            {
                if (num % i == 0 && denom % i == 0)
                {
                    num = num / i;
                    denom = denom / i;
                    i = 1;
                }
            }

            return (num,denom);
        }

        public static int GetMinor(int a,int b)
        {
            if (a < b)
                return a;
            return b;
        }
    }
}
