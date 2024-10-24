using CierreDeCajas.Presentacion;
using System.Windows.Forms;

namespace CierreDeCajas.Logica
{
    public class InstanciasRepository
    {
        public FrmCierreCaja InstanciaFrmCierredeCaja()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmCierreCaja)
                {
                    return (FrmCierreCaja)form; // Retornar la instancia si está abierta
                }
            }
            return null; // Retornar null si no se encuentra ninguna instancia
        }

        public FrmMenuda InstanciaFrmMenuda()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmMenuda)
                {
                    return (FrmMenuda)form; // Retornar la instancia si está abierta
                }
            }
            return null; // Retornar null si no se encuentra ninguna instancia
        }

        //public FrmVentas InstanciaFrmVentas()
        //{
        //    foreach (Form form in Application.OpenForms)
        //    {
        //        if (form is FrmVentas)
        //        {
        //            return (FrmVentas)form; // Retornar la instancia si está abierta
        //        }
        //    }
        //    return null; // Retornar null si no se encuentra ninguna instancia
        //}
    }
}
