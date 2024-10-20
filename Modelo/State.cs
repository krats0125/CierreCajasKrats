using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CierreDeCajas.Modelo
{
    public class State
    {
        public Dictionary<string, string> TextBoxValues { get; set; }
        public string TotalEntregado { get; set; }
        public string TotalCaja { get; set; }

        public State()
        {
            TextBoxValues = new Dictionary<string, string>();
        }
    }
}
