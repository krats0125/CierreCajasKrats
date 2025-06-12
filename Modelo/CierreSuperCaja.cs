using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Modelo
{
    public class CierreSuperCaja
    {
        public string IdUsuario { get; set; }
        public int IdCierre { get; set; }

        public decimal TotalEfectivoSistema { get; set; }
        public decimal DiferenciaEfectivo { get; set; }
        public decimal Diferencia { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalDatafono { get; set; }
        public decimal TotalDatafonoSistema { get; set; }
        public decimal DiferenciaDatafono { get; set; }
        public decimal TotalMovimientosCaja { get; set; }
        public decimal TotalLiquidado { get; set; }
        public decimal TotalCobrado { get; set; }
        public decimal EntregaUltimoEfectivo { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
    }
}
