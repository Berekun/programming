namespace Examen1
{
    internal class Program
    {
        // Nota: 2
        public static double Funcion1(int a, int b)
        {
            // mmm, no sé por qué has puesto esto
            if (a < 0 || b < 0)
                return 0;
            return (a + 1) / b;
        }

        // Nota: 4
        public static double Funcion2(int a, int b, int c, int d, int e, int x)
        {
            int x4 = x * x * x * x;
            int x3 = x * x * x;
            int x2 = x * x;

            return (a * x4) + (b * x3) + (c * x2) + d * x + e;
        }

        // Nota: 4
        public static double Funcion3(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j)
        {
            int primero = GetMinor(a, b);
            int segundo = GetMinor(c, d);
            int tercero = GetMinor(e, f);
            int cuarto = GetMinor(g, h);
            int quinto = GetMinor(i, j);

            int pys = GetMinor(primero, segundo);
            int tyc = GetMinor(tercero, cuarto);
            int tycq = GetMinor(tyc, quinto);

            int final = GetMinor(pys, tycq);

            return final;
        }

        public static double GetMinor(int a, int b)
        {
            if (a < b)
                return a;
            return b;
        }

        public static int GetMajor(int a, int b)
        {
            if (a > b)
                return a;
            return b;
        }

        // Esta función tiene bugs
        public static int GetCentral(int a, int b, int c)
        {
            if ((a > b && a < c) || (a < b && a > c))
                return a;
            else if ((b > a && b < c) || (b < a && b > c))
                return b;
            else
                return c;
        }

        // Nota: 4
        public static int Funcion4(int a, int b, int c)
        {
            int max = GetMajor(c, GetMajor(a, b));
            int min = GetMinor(c, GetMinor(a, b));
            int central = GetCentral(c, GetCentral(a, b));

            int distanceafter = central - min;
            int distancebefore = max - central;

            return GetMajor(distanceafter, distancebefore);
        }

        // Esta será la función 5, no?
        // Nota: 4
        public static int Funcion4(int a)
        {
            int digit = 0;

            while (a > 1)
            {
                a = a / 10;
                digit++;
            }

            return digit;
        }

        // Nota: 4
        public static int Funcion6(int n)
        {
            if (n == 1)
                return n;
            int nalcuadrado = n * n;
            return nalcuadrado + Funcion6(n - 1);
        }

        // Nota: 4
        public static (int, int) Funcion7(string word)
        {
            int firstvocal = 0;
            int secondvocal = 0;

            for (int i = 0; i < word.Length; i++)
            {
                // Tenías que sacarme eso a una función, a la próxima te bajo nota
                if (word[i] == 'a' || word[i] == 'e' || word[i] == 'i' || word[i] == 'o' || word[i] == 'u')
                {
                    firstvocal = i;
                    break;
                }

                if (word[i] == 'A' || word[i] == 'E' || word[i] == 'I' || word[i] == 'O' || word[i] == 'U')
                {
                    firstvocal = i;
                    break;
                }

            }

            for (int i = word.Length; i >= 0; i--)
            {
                if (word[i] == 'a' || word[i] == 'e' || word[i] == 'i' || word[i] == 'o' || word[i] == 'u')
                {
                    secondvocal = i;
                    break;
                }

                if (word[i] == 'A' || word[i] == 'E' || word[i] == 'I' || word[i] == 'O' || word[i] == 'U')
                {
                    secondvocal = i;
                    break;
                }

            }

            return (firstvocal, secondvocal);


        }

        public static bool IsPrime(int n)
        {
            int divisible = 0;

            for (int i = 1; i < n; i++)
            {
                if (n % i == 0)
                    // aquí pondría un break o un return
                    divisible = 1;
            }

            if (divisible == 1)
                return true;
            return false;
        }

        // Nota: 4
        public static int Funcion8(int n)
        {
            int numberofprime = 0;

            if (IsPrime(n) == false)
                return -1;

            for (int i = 2; i <= n; i++)
            {
                if (IsPrime(i) == true)
                    numberofprime++;
            }

            return numberofprime;



        }

        public enum MachineStatus
        {
            PREPARADA,PROCESANDO,EJECUTANDO,TERMINADO
        }

        // Nota: 3, aquí si que te quito nota
        public static MachineStatus Funcion9(MachineStatus status,bool x)
        {
            switch (status)
            {
                   case status == MachineStatus.PREPARADA:
                    if (x == true)
                    {
                        status == MachineStatus.PROCESANDO;
                        break;
                    }

                    case status == MachineStatus.PROCESANDO:
                    if (x == true)
                    {
                        status == MachineStatus.EJECUTANDO;
                        break;
                    }

                    case status == MachineStatus.EJECUTANDO:
                    if (x == true)
                    {
                        status == MachineStatus.TERMINADO;
                        break;
                    }

                    case status == MachineStatus.TERMINADO:
                    if (x == true)
                    {
                        status == MachineStatus.PREPARADA;
                        break;
                    }

                default:

                    break;
            }

            return status;
        }

        // Nota: 4
        public static int Funcion10(int n)
        {
            int result = 0;

            for(int i = 1; i < n; i++)
            {
                if (n % i == 0)
                    result += i;
            }

            return result;
        }

    }   
}
