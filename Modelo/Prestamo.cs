using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Modelo
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public DateTime Fecha { get; set; }
        public string IdTrabajador { get; set; }
        public string IdMensajero { get; set; }
        public decimal Valor { get; set; }
        public string Concepto { get; set; }
        public string Caja { get; set; }
        public string Cajero { get; set; }
        public string Observacion { get; set; }
        public int IdMovimiento { get; set; }

    }
}
