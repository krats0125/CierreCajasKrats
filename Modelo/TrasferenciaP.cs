using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Modelo
{
    public class TrasferenciaP
    {
        public string Factura { get; set; }
        public decimal Valor { get; set; }
        public DateTime Fecha { get; set; }
        public string MedioDePago { get; set; }
    }
}
