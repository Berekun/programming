namespace MontyHall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int win = 0;
            Show show = new Show();
            Console.WriteLine("Has ganado: " + show.Execute(100) + " veces");
        }
    }
}