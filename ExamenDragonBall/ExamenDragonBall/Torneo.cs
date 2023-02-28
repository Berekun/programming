using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDragonBall
{
    internal class Torneo : ITorneo
    {
        List<Persona> personas = new List<Persona>();
        private string[] NombresParticipantes = { "Paquito", "Juanito", "Pepito", "Ricardo", "Goku", "Vegeta" };

        public void CreateParticipants()
        {

            for (int i = 0; i < 16; i++)
            {
                int random = (int)Utils.GetRandom(0, 4);
                switch (random)
                {
                    case 0:
                        Humano human = new Humano(NombresParticipantes[(int)Utils.GetRandom(0, 5.99)]);
                        personas.Add(human);
                        break;
                    case 1:
                        Saiyan saiyan = new Saiyan(NombresParticipantes[(int)Utils.GetRandom(0, 5.99)]);
                        personas.Add(saiyan);
                        break;
                    case 2:
                        SuperSaiyan supersaiyan = new SuperSaiyan(NombresParticipantes[(int)Utils.GetRandom(0, 5.99)]);
                        personas.Add(supersaiyan);
                        break;
                    case 3:
                        Namekiano namekiano = new Namekiano(NombresParticipantes[(int)Utils.GetRandom(0, 5.99)]);
                        personas.Add(namekiano);
                        break;
                    default: break;
                }
            }
        }
        public void Init()
        {
            CreateParticipants();
        }
        public List<string> Execute()
        {
            Init();
            string winner = null;
            List<string> names = new List<string>();

            for (int i = 0; winner == null; i++)
            {
                if (personas.Count != 1) i = 0;
                if (personas.Count == 1)
                {
                    winner = personas[0].Name;
                    names.Add(winner);
                    break;
                }
                Persona luchador1 = personas[i];
                Persona luchador2 = personas[i + 1];

                while (true)
                {
                    luchador1.Atacar(luchador2);
                    if (luchador2.Energy >= 0)
                    {
                        personas.Remove(luchador2);
                        break;
                    }
                    if (luchador1.Energy >= 0)
                    {
                        personas.Remove(luchador1);
                        break;
                    }
                }
            }
            return names;
        }

        public void Visit()
        {
            throw new NotImplementedException();
        }
    }
}
