using CierreDeCajas.Presentacion;
using CierreDeCajas.Presentacion.Administrativo;
using CierreDeCajas.Presentacion.AdminSuperCaja;
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
        public FrmResumenSuperCaja InstanciaFrmCierreSuperCaja()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmResumenSuperCaja)
                {
                    return (FrmResumenSuperCaja)form; // Retornar la instancia si está abierta
                }
            }
            return null; // Retornar null si no se encuentra ninguna instancia
        }
        public FrmSuperCaja InstanciaFrmSuperCaja()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmSuperCaja)
                {
                    return (FrmSuperCaja)form; // Retornar la instancia si está abierta
                }
            }
            return null; // Retornar null si no se encuentra ninguna instancia
        }

        public FrmResumenSuperCajaAdmin InstanciaFrmSuperCajaAdmin()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmResumenSuperCajaAdmin)
                {
                    return (FrmResumenSuperCajaAdmin)form; // Retornar la instancia si está abierta
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
        public FrmMenudaSuperCaja InstanciaFrmMenudaSuperCaja()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmMenudaSuperCaja)
                {
                    return (FrmMenudaSuperCaja)form; // Retornar la instancia si está abierta
                }
            }
            return null; // Retornar null si no se encuentra ninguna instancia
        }
        public FrmMenudaSuperCajaAdmin InstanciaFrmMenudaSuperCajaAdmin()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmMenudaSuperCajaAdmin)
                {
                    return (FrmMenudaSuperCajaAdmin)form; // Retornar la instancia si está abierta
                }
            }
            return null; // Retornar null si no se encuentra ninguna instancia
        }

        public FrmDetalleReporte InstanciaFrmDetalle()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmDetalleReporte)
                {
                    return (FrmDetalleReporte)form; // Retornar la instancia si está abierta
                }
            }
            return null; // Retornar null si no se encuentra ninguna instancia
        }

        public FrmMenudaAdmin InstanciaFrmMenudaAdmin()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmMenudaAdmin)
                {
                    return (FrmMenudaAdmin)form; // Retornar la instancia si está abierta
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
