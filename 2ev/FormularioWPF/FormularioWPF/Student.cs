using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormularioWPF
{
    internal class Student
    {
        private string _name;
        private string _description;
        private int _age;

        public string Name => _name;
        public string Description => _description;
        public int Age => _age;
    }
}
