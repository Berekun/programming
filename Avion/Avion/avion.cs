using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avion
{
    public class avion
    {
        private float _altura;
        private float _velocidad; // Velocidad del avion
        private float _combustible;
        private int _orientacion;

        public float Altura
        {
            get
            {
                return _altura;
            }
        }

        public int Orientacion
        {
            get
            {
                return _orientacion;
            }
        }

        public float Combustible
        {
            get
            {
                return _combustible;
            }
        }
        public avion(float altura, float velocidad, float combustible, int orientacion)
        {
            _altura = altura;
            _velocidad = velocidad;
            _combustible = combustible;
            _orientacion = orientacion;
        }

        public void Virar(int grados)
        {
            _orientacion = (_orientacion + grados) % 360;
            ConsumirFuel(grados * 0.1f);
        }

        private void ConsumirFuel(float litros)
        {
            _combustible = _combustible - litros;

            if(_combustible < 0)
            {
                _combustible = 0;
            }
        }

        //Metodos que sirve para ascender los metros indicados

        public void AscenderMetros(float metros) //M son los metros
        {
            _altura = _altura + metros;
            ConsumirFuel(metros * 0.3f);
        }

        //Metodos que sirve para descender los metros indicados

        public void DescenderMetros(float metros)
        {
            _altura = _altura - metros;

            if(_altura < 0)
            {
                _altura = 0;
            }
        }

    }
}
