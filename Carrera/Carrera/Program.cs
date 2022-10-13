namespace Carrera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Racer winner = null;
            List<Racer> list = Simulation.GetRacers();

            while(winner==null)
            {
                Simulation.MoveRacers(list);
                winner = Simulation.GetWinner(list);
            }

            Console.WriteLine(winner.name);
        }
    }
}