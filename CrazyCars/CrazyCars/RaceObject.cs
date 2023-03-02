using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CrazyCars
{
    public enum ObjectType
    {
        ROCK, CHARCO, BOMB, TROGLODYTECAR, WOODCAR, GLAMOURCAR, PIERECAR
    }

    public abstract class RaceObject
    {
        private string _name;
        private double _position;
        protected ObjectType _type;
        protected bool _isEnable = true;
        protected int _turnsDisable = 0;

        public string Name => _name;
        public double Position => _position;

        public RaceObject(string name,double position)
        {
            _name = name;
            _position = position;
        }

        public RaceObject()
        {

        }

        public abstract ObjectType GetObjectType();

        public abstract bool IsEnable();

        public void Disable(int turns)
        {
            _turnsDisable += turns;
        }

        public virtual void Simulate(IRace race)
        {

        }

        public virtual void Move(double position)
        {
            _position += position;
        }

        public virtual void VisitDriver(Visitor visit)
        {

        }

    }
}
