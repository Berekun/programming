using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenAutosLocos
{
    public abstract class Obstacle : RaceObject
    {
        public Obstacle(string name, double position, ObjectType type) : base(name, position)
        {
            _type = type;
        }

        public override ObjectType GetObjectType()
        {
            return _type;
        }
    }
}
