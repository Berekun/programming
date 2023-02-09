namespace Listas
{
    internal class Program
    {
        public static int GetMajor(List<int> list)
        {
            int major = list[0];
            if(list == null)
                return 0;
            if(list.Count == 0)
                return 0;
            for(int i = 1; i < list.Count; i++)
            {
                if (list[i] > major)
                    major = list[i];
            }

            return major;
        }

        public static int IndexOf1(List<int> list, int number)
        {
            int position = -1;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i] == number)
                    position = i;
            }

            return position;
        }

        //public static bool IsNumberInList(List<int> list, int number)
        //{
            //return IndexOf(list, number) >= 0;
        //}

        public static List<int> Sort1(List<int> list)
        {
            int minor = int.MaxValue;
            int position = 0;
            int i = 0;
            List<int> result = new List<int>();

            while(list.Count > 0)
            {
                if (list[i] < minor)
                {
                    minor = list[i];
                    position = i;
                }
                if (i == list.Count - 1)
                {
                    result.Add(minor);
                    list.RemoveAt(position);
                    i = 0;
                    minor = int.MaxValue;
                    continue;
                }
                    
                i++;
            }
            return result;

        }

        public static bool HaveANumber(int[] array, int number)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == number)
                    return true;
            }

            return false;
        }

        public static bool HaveANumberDouble(double[] array, double number)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == number)
                    return true;
            }

            return false;
        }

        public static bool HaveANumberFloat(float[] array, float number)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == number)
                    return true;
            }

            return false;
        }

        public static bool HaveACharacter(char[] array, char letter)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == letter)
                    return true;
            }

            return false;
        }

        public static bool HaveAString(string[] array, string word)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == word)
                    return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            /*/List<int> list = new List<int>();
            list.Add(3);
            list.Add(5);
            list.Add(-1);
            list.Add(0);
            list.Add(7);
            list.Add(-4);
            list.Add(4);
            list.Add(-2);

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            list.Add(5);
            list.Add(4);
            list.Add(3);
            list.Add(2);
            list.Add(1);/*/

            //var ll = Sort(list);
            //list = list;

            int[] array = new int[6];
            array[0] = 2;
            array[1] = 5;
            array[2] = -3;
            array[3] = 7;
            array[4] = -7;
            array[5] = 10;

            int[] array2 = { 1, 2, 3, 5 };

            /*/
            double[] array1 = new double[4];
            array1[0] = 2.3;
            array1[1] = 5.7;
            array1[2] = -3.5;
            array1[3] = 7.1;
            array1[4] = 10.3;

            float[] array2 = new float[4];
            array2[0] = 2.3f;
            array2[1] = 5.7f;
            array2[2] = -3.5f;
            array2[3] = 7.1f;
            array2[4] = 10.3f;

            char[] array3 = new char[4];
            array3[0] = 'a';
            array3[1] = 'b';
            array3[2] = 'c';
            array3[3] = 'd';
            array3[4] = 'e';

            string[] array4 = new string[4];
            array4[0] = "hola";
            array4[1] = "nacho";
            array4[2] = "berekun";
            array4[3] = "pabloskio";
            array4[4] = "nicogay";/*/

            Comparator<int> comp = (a , b) =>
            {
                if (a < b)
                    return 0;
                return 1;
            };

            Comparator<int> equal = (a, b) =>
            {
                if (a == b)
                    return 0;
                return 1;
            };

            //Sort(array,comp);
            //IndexOf(array, 4, equal);
            //BinarySearch(array, -7, equal, comp);
            RemoveAT(array, 2);
        }

        public delegate int Comparator<T>(T a, T b);

        //Deberes Navidad

        public static T[] Sort<T>(T[] array, Comparator<T> comp)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (comp(array[i], array[j]) == 0)
                    {
                        T aux = array[i];             //
                        array[i] = array[j];          //SWAP
                        array[j] = aux;               //
                    }
                }
            }
            return array;
        }

        public static int IndexOf<T>(T[] array, T value, Comparator<T> equal)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (equal(array[i], value) == 0)
                    return i;
            }
            return -1;
        }

        public static int BinarySearch<T>(T[] array, T value, Comparator<T> equal, Comparator<T> comp)
        {
            int min = 0;
            int max = array.Length - 1;

            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (equal(value, array[mid]) == 0)
                {
                    return mid;
                }
                if (comp(value, array[mid]) == 0)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return 0;
        }

        public static T[] RemoveAT<T>(T[] array,int index)
        {
            T[] result = new T[array.Length - 1];

            for(int i = 0; i < array.Length; i++)
            {
                if (i != index && i < index)
                {
                    result[i] = array[i];
                }
                else if(i != index && i > index)
                {
                    result[i-1] = array[i];
                }
                else
                {
                    result[i] = array[++i];
                }
            }

            return result;
        }


    }
}