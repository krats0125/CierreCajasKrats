using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Logica
{
    public class MenudaRepository
    {
        CONEXION cn=new CONEXION();

        //METODO PARA GUARDAR LA MENUDA
        public bool insertar(Menuda oMenuda)
        {
            bool respuesta = false;


            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_InsertarDenominaciones", conexion))
                    {
                        cmd.CommandType=CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", oMenuda.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdCierre", oMenuda.IdCierre);
                        cmd.Parameters.AddWithValue("@Billete_100", oMenuda.Billete_100);
                        cmd.Parameters.AddWithValue("@Billete_50", oMenuda.Billete_50);
                        cmd.Parameters.AddWithValue("@Billete_20", oMenuda.Billete_20);
                        cmd.Parameters.AddWithValue("@Billete_10", oMenuda.Billete_10);
                        cmd.Parameters.AddWithValue("@Billete_5", oMenuda.Billete_5);
                        cmd.Parameters.AddWithValue("@Billete_2", oMenuda.Billete_2);
                        cmd.Parameters.AddWithValue("@Billete_1", oMenuda.Billete_1);
                        cmd.Parameters.AddWithValue("@Moneda_1000", oMenuda.Moneda_1000);
                        cmd.Parameters.AddWithValue("@Moneda_500", oMenuda.Moneda_500);
                        cmd.Parameters.AddWithValue("@Moneda_200", oMenuda.Moneda_200);
                        cmd.Parameters.AddWithValue("@Moneda_100", oMenuda.Moneda_100);
                        cmd.Parameters.AddWithValue("@Moneda_50", oMenuda.Moneda_50);

                        cmd.ExecuteNonQuery();
                        return respuesta;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar: " + ex.Message);
                return respuesta;
            }
          
        
        }


        public bool CargarDenominaciones(Menuda oMenuda)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_CargarDenominaciones", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCierre", oMenuda.IdCierre);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                oMenuda.Billete_100 = Convert.ToInt32(reader["Billete_100"]);
                                oMenuda.Billete_50 = Convert.ToInt32(reader["Billete_50"]);
                                oMenuda.Billete_20 = Convert.ToInt32(reader["Billete_20"]);
                                oMenuda.Billete_10 = Convert.ToInt32(reader["Billete_10"]);
                                oMenuda.Billete_5 = Convert.ToInt32(reader["Billete_5"]);
                                oMenuda.Billete_2 = Convert.ToInt32(reader["Billete_2"]);
                                oMenuda.Billete_1 = Convert.ToInt32(reader["Billete_1"]);
                                oMenuda.Moneda_1000 = Convert.ToInt32(reader["Moneda_1000"]);
                                oMenuda.Moneda_500 = Convert.ToInt32(reader["Moneda_500"]);
                                oMenuda.Moneda_200 = Convert.ToInt32(reader["Moneda_200"]);
                                oMenuda.Moneda_100 = Convert.ToInt32(reader["Moneda_100"]);
                                oMenuda.Moneda_50 = Convert.ToInt32(reader["Moneda_50"]);
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar denominaciones: {ex.Message}");
                return false;
            }
        }





    }
}
