using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Logica
{
    public class CONEXION
    {
        

       // public string CadenaRibisoft { get; set; }
       // public string CadenaCierreCajas { get; set; }
       // public string CadenaBdInterna { get; set; }

        //public CONEXION()
        //{
        //    // generar consulta a un archivo plano [.txt] para que se pueda configurar fuera de la compilación.

        //}



        public string ConexionRibisoft()
        {
            string servidor = @"SERVIDOR-PC\SQLEXPRESS";
            string NombreBaseDatos = "Administrativo";                         
            string usuario = "sa";
            string contraseña = "   ";
            string cadena = $"Data Source={servidor};Initial Catalog={NombreBaseDatos};User ID={usuario};Password={contraseña};";

            return cadena;

        }

        public string ConexionCierreCaja()
        {
            //string servidor = @"SERVIDOR-PC\SQLEXPRESS";
            //string NombreBaseDatos = "CierresCaja";
            //string usuario = "sa";
            //string contraseña = "abcd.1234";
            //string cadena = $"Data Source={servidor};Initial Catalog={NombreBaseDatos};User ID={usuario};Password={contraseña};";

            //return cadena;

            string cadena = $"Data Source=localhost;Initial Catalog=CierresCaja;Integrated Security=True;";
            return cadena;


        }

        public string Conexionkrats()
        {
            string servidor = @"SERVIDOR-PC\SQLEXPRESS";
            string NombreBaseDatos = "CierresCaja";
            string usuario = "sa";
            string contraseña = "abcd.1234";
            string cadena = $"Data Source={servidor};Initial Catalog={NombreBaseDatos};User ID={usuario};Password={contraseña};";

            return cadena;

        }

        public string ConexionDbInterna()
        {
            // codigo para tomar cadena de conexion de un archivo plano

            //string direccionFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conexionXpos.txt");
            //string originalFilePath = LeerDireccion(direccionFilePath);

            string cadena = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=Z:\APP BODEGA DE NACHO\LABODEGADENACHO.accdb; ";
            //string cadena = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=Z:\APP BODEGA DE NACHO\LABODEGADENACHO.accdb; ";   // cadena de conexion para los computadores del call center
            return cadena;
        }



    }
}
