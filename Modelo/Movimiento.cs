using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Modelo
{
    public class Movimiento
    {
        public int IdMovimiento { get; set; }

        public int IdCaja { get; set; }
        public int IdCierre { get; set; }
        public string IdUsuario { get; set; }
        public int IdConcepto { get; set; }
        public decimal Valor { get; set; }

        public string Descripcion { get; set; }
        public int IdMedioPago { get; set; }

        public DateTime Fecha { get; set; }

    }
}
