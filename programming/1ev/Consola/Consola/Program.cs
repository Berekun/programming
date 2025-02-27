namespace Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char minLetra = Char.MinValue;
            char maxLetra = Char.MaxValue;
            int numMayusculas = 0;
            while (true)
            {
                //Leo una letra
                Console.WriteLine("Introduce una letra. Pulsa 0 si quieres salir: ");
                char letraAux = Console.ReadKey().KeyChar;
                Console.WriteLine("");
                Console.WriteLine("-------");
                if (letraAux == '0')
                    break;
                //almaceno los menores y mayores.
                if (minLetra < letraAux)
                    minLetra = letraAux;
                if (maxLetra > letraAux)
                    maxLetra = letraAux;
                //Si la letra es mayusculas la contabiliza
                //
                if ((letraAux >= 'A') && (letraAux <= 'Z'))
                    numMayusculas++;
            }
            //Escribe el resultado
            Console.WriteLine("el Char menor es : " + maxLetra);
            Console.WriteLine("el Char mayor es : " + minLetra);
            Console.WriteLine("Hay " + numMayusculas + " letras mayusculas ");
            Console.ReadKey();
        }
    }

}
    
