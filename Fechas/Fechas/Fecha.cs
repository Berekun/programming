using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fechas
{
    internal class Fecha
    {
        public int dia;
        public int mes;
        public int anyo;

        //TODO validar los valores introducidos
        /// <summary>
        /// Constructor de Fecha sin parámetros
        /// Se establecen los valores a 1
        /// </summary>
        public Fecha()
        {
            dia = 1;
            mes = 1;
            anyo = 1;
        }

        /// <summary>
        /// Constructor de Fecha con 3 parámetros
        /// Si algún parámetro no es correcto se establece a 1
        /// </summary>
        /// <param name="dia">Dia</param>
        /// <param name="mes">Mes</param>
        /// <param name="anyo">Anyo (entre 1 y 2500)</param>
        public Fecha(int dia, int mes, int anyo)
        {
            int ultimoDiaMes;

            if (anyo >= 1 && anyo <= 2500)
                this.anyo = anyo;
            else
                this.anyo = 1;

            if (mes >= 1 && mes <= 12)
                this.mes = mes;
            else
                this.mes = 1;

            if (this.mes == 1 || this.mes == 3 || this.mes == 5 || this.mes == 7 || this.mes == 8 || this.mes == 10 || this.mes == 12)
                ultimoDiaMes = 31;
            else if (this.mes == 4 || this.mes == 6 || this.mes == 9 || this.mes == 11)
                ultimoDiaMes = 30;
            else if (EsBisiesto())
                ultimoDiaMes = 29;
            else
                ultimoDiaMes = 28;

            if (dia >= 1 && dia <= ultimoDiaMes)
                this.dia = dia;
            else
                this.dia = 1;
        }

        public bool EsBisiesto()
        {
            if ((anyo % 400 == 0) || ((anyo % 4 == 0) && (anyo % 100 != 0)))
                return true;
            return false;
        }


        /// <summary>
        /// Devuelve un string con la fecha en formato dia/mes/anyo
        /// </summary>
        /// <remarks> la palabra clave override indica que se  sobreescribe el metodo ToString</remarks>
        /// <returns>un string con la fecha en formato dia/mes/anyo</returns>
        public override string ToString()
        {
            return dia + "/" + mes + "/" + anyo;
        }
    }
}
