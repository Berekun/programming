namespace ExamenDragonBall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Utils.GetRandom(0, 3));
            Torneo torneo = new Torneo();
            torneo.Execute();
        }
    }
}