namespace Carrera
{
    internal class Simulation
    {

        public static List<Racer> GetRacers()
        {
            List<Racer> racers = new List<Racer>();

            Racer a = new Racer();
            a.name = "Nacho";
            a.position = 0.0;
            a.type = Utils.Getrandomtype();

            Racer b = new Racer();
            b.name = "Pablo";
            b.position = 0.0;
            b.type = Utils.Getrandomtype();

            Racer c = new Racer();
            c.name = "Daniel";
            c.position = 0.0;
            c.type = Utils.Getrandomtype();

            Racer d = new Racer();
            d.name = "Nico";
            d.position = 0.0;
            d.type = Utils.Getrandomtype();

            racers.Add(a);
            racers.Add(b);
            racers.Add(c);
            racers.Add(d);

            return racers;
        }
        public static void MoveRacers(List<Racer> list)
        {

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Advanceposition();
            }
        }

        public static Racer GetWinner(List<Racer> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].position >= 1000)
                    return list[i];

            }

            return null;
        }


    }
}
