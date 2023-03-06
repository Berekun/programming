namespace ExamenAutosLocos
{
    public enum ObjectType
    {
        ROCK, CHARCO, BOMB, GLAMOUR, TROGLO, WOOD, PIERRE
    }

    public abstract class RaceObject
    {
        private string _name;
        private double _position;
        protected ObjectType _type;
        protected int _disableturns = 0;

        public string Name => _name;
        public double Position => _position;

        public RaceObject(string name, double position)
        {
            _name = name;
            _position = position;
        }

        public void Displace(double newPosition)
        {
            _position += newPosition;
        }

        public virtual void DisplacePlus(double newPosition)
        {
            _position += newPosition;
        }

        public abstract ObjectType GetObjectType();

        public virtual bool IsEnabled()
        {
            return true;
        }
        public void Disable(int turns)
        {
            _disableturns += turns;
        }

        public abstract void Simulate(IRace race);

        public List<RaceObject> ObjectNear(IRace race, RaceObject raceobject, double min, double max)
        {
            List<RaceObject> objectsNear = new List<RaceObject>();
            for (int i = 0; i < race.GetObjectCount(); i++)
            {
                if (race.GetObjectAt(i) is Car)
                {
                    if (raceobject.Position >= race.GetObjectAt(i).Position - min || raceobject.Position <= race.GetObjectAt(i).Position + max)
                        objectsNear.Add(race.GetObjectAt(i));
                }
            }
            return objectsNear;
        }



    }
}
