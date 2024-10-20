using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CierreDeCajas.Logica.Utilitarios
{
    public class SentenciaSqlOLEDB
    {
        public bool EjecutarSQL(string sentenciaSQL, string CadenaConexion)
        {
            bool respuesta = false;

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
                {
                    conexion.Open();

                    using (OleDbCommand cmd = new OleDbCommand(sentenciaSQL, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        respuesta = true;

                    }
                }
            }
            catch (Exception)
            {

                return respuesta;
            }

            return respuesta;
        }

        public DataTable TraerDatos(string sentenciaSQL, string CadenaConexion)
        {
            DataTable dt = new DataTable();

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
                {
                    conexion.Open();

                    using (OleDbDataAdapter da = new OleDbDataAdapter(sentenciaSQL, conexion))
                    {
                        da.Fill(dt);

                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

                //return dt;
                MessageBox.Show("Error al traer datos: " + ex.Message);
            }

            return dt;
        }

     

    }
}
