using System;
using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;

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
    }
}
