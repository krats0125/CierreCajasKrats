﻿using CierreDeCajas.Presentacion;
using CierreDeCajas.Presentacion.Administrativo;
using CierreDeCajas.Presentacion.Operativo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CierreDeCajas
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmLogin());
            Application.Run(new FrmFacturas());
        }
    }
}
