using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Persona
    {

        public string Identificacion { get; set; }

        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public decimal Pulsacion { get; set; }
        public void CalcularPulsacion()
        {
            if (Sexo.Equals("F") || Sexo.Equals("f"))
            {
                Pulsacion = (220 - Edad) / 10;
            }
            else
            {
                Pulsacion = (210 - Edad) / 10;
            }
        }
    }
}
