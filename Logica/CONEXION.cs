using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Logica
{
    public class CONEXION
    {
               
        public string ConexionRibisoft()
        {
            //string servidor = @"SERVIDOR-PC\SQLEXPRESS";
            string servidor = @"SERVIDOR-PC";
            string NombreBaseDatos = "Administrativo";                         
            string usuario = "sa";
            string contraseña = "abcd.1234";
            string cadena = $"Data Source={servidor};Initial Catalog={NombreBaseDatos};User ID={usuario};Password={contraseña};";
            return cadena;
        }

        public string ConexionVisualFoxPro()
        {
            string ruta = @"Z:\Delfin\DbfRed\";

            string conexion = "Driver={Driver para o Microsoft Visual FoxPro};SourceType=DBF;SourceDB=" + ruta +
                ";Exclusive=NO";

            return conexion;

        }

        //public string ConexionCierreCaja()
        //{
        //    string servidor = @"SERVIDOR-PC\SQLEXPRESS";
        //    string NombreBaseDatos = "CierresCaja";
        //    string usuario = "sa";
        //    string contraseña = "abcd.1234";
        //    string cadena = $"Data Source={servidor};Initial Catalog={NombreBaseDatos};User ID={usuario};Password={contraseña};";

        //    return cadena;

        //    //string cadena = $"Data Source=localhost;Initial Catalog=CierresCaja;Integrated Security=True;";
        //    //return cadena;


        //}
        public string ConexionCierreCaja()
        {
            string servidor = @"tcp:carlosdbprojects.database.windows.net";
            string NombreBaseDatos = "CierresCaja";
            string usuario = "krats0125";
            string contraseña = "Valentinag11";
            string cadena = $"Data Source={servidor};Initial Catalog={NombreBaseDatos};User ID={usuario};Password={contraseña};";
            return cadena;
            //string cadena = $"Data Source=localhost;Initial Catalog=CierresCaja;Integrated Security=True;";
            //return cadena;


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
        //public string Conexionlabodegadenacho()
        //{
        //    string servidor = @"SERVIDOR-PC";
        //    string NombreBaseDatos = "LABODEGADENACHO";
        //    string usuario = "sa";
        //    string contraseña = "abcd.1234";
        //    string cadena = $"Data Source={servidor};Initial Catalog={NombreBaseDatos};User ID={usuario};Password={contraseña};";

        //    return cadena;

        //}
        public string Conexionlabodegadenacho()
        {
            string servidor = @"tcp:carlosdbprojects.database.windows.net";
            string NombreBaseDatos = "LABODEGADENACHO";
            string usuario = "krats0125";
            string contraseña = "Valentinag11";
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
