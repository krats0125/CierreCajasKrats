using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Logica
{
    public class ConceptoAdelantoRepository
    {
        CONEXION cn=new CONEXION();
        public bool Insertar(ConceptoAdelanto oConceptoAdelanto,bool Mensajero,bool Trabajador,bool ambos)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"insert into ConceptoAdelanto(Concepto,Descripcion,Permiso)
                    values(@Concepto,@Descripcion,@Permiso)";

                    
                     
                        if(Mensajero)
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, conexion))
                            {
                                cmd.Parameters.AddWithValue("@Concepto", oConceptoAdelanto.Concepto);
                                cmd.Parameters.AddWithValue("@Descripcion", oConceptoAdelanto.Descripcion);
                                cmd.Parameters.AddWithValue("@Permiso", 2); // Permiso para mensajero
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else if(Trabajador)
                        {
                        using (SqlCommand cmd = new SqlCommand(sql, conexion))
                        {
                            cmd.Parameters.AddWithValue("@Concepto", oConceptoAdelanto.Concepto);
                            cmd.Parameters.AddWithValue("@Descripcion", oConceptoAdelanto.Descripcion);
                            cmd.Parameters.AddWithValue("@Permiso", 3); // Permiso para trabajador
                            cmd.ExecuteNonQuery();
                        }
                        
                        }
                        else if(ambos)
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, conexion))
                            {
                                cmd.Parameters.AddWithValue("@Concepto", oConceptoAdelanto.Concepto);
                                cmd.Parameters.AddWithValue("@Descripcion", oConceptoAdelanto.Descripcion);
                                cmd.Parameters.AddWithValue("@Permiso", 1);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        
                        respuesta = true;
                }
                
            }
            catch (Exception ex)
            {
                return respuesta;

            }
            return respuesta;
        }

    }
}
