using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Logica.Utilitarios
{
    public class TransferenciasRepository
    {
        CONEXION conexion = new CONEXION();

        public DataTable ExportarTransferencias(int idCierre)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.ConexionCierreCaja()))
                {
                    string consulta = @"select IdUsuario as Cajero,format(Valor,'N0','es-CO') as Valor
                                      from MovimientoCaja 
                                      where IdCierre=@idCierre and IdMedioPago in (4,5,6) 
                                      order by IdUsuario";
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, cn))
                    {
                        cmd.Parameters.AddWithValue("@idCierre", idCierre);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }

    }
}
