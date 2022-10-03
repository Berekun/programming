﻿using System;
using System.CodeDom.Compiler;
using System.Net.Mail;
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

        public static double GetDistance2points(double x1,double x2, double y1, double y2)
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
            for (int i=1; i < number; i++)
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
            for (int i = 0; r1<n && r2 <n;i++)
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

            for(int i = 0; i < word.Length; i++)
            {
                char c = word[i];
                if (i == word.Length - 1)
                    result += c;
                else
                    result += c + "-";
            }

            return result;
        }

        public static bool Getlether(char mychar,char start,char finish)
        {








        }
    }
}
