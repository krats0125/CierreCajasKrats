using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Modelo
{
    public class Domicilio
    {
        public string Direccion { get; set; }
        public decimal Valor { get; set; }
        public int MedioDePago { get; set; }
    }
}
