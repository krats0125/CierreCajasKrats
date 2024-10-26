using CierreDeCajas.Modelo;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using CierreDeCajas.Presentacion;


namespace CierreDeCajas.Logica
{
    public class DomicilioRepository
    {
        CONEXION cn = new CONEXION();
        Principal ppal = null;
        public DomicilioRepository(Principal principal)
        {
            ppal = principal;
        }
        public bool LeerExcel(string rutaArchivo)
        {
            try
            {
                List<Domicilio> domicilios = new List<Domicilio>();
                string nombreArchivo = Path.GetFileName(rutaArchivo);

                // Leer el archivo CSV
                using (var reader = new StreamReader(rutaArchivo))
                {
                    // Leer la primera línea (encabezados), si existe
                    string encabezados = reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var valores = linea.Split(','); // Asumiendo que los campos están separados por comas
                        decimal dato1 = 0, dato2 = 0, dato3 = 0, valor = 0;
                        string direccion = string.Empty;
                        string medioDePago = string.Empty;
                        int idMedioPago = 0;

                        if (nombreArchivo.StartsWith("cuadreCajaMensajeros"))
                        {
                            dato1 = Convert.ToDecimal(valores[7]);
                            dato2 = Convert.ToDecimal(valores[8]);
                            dato3 = Convert.ToDecimal(valores[9]);
                            direccion = valores[2];
                            medioDePago = valores[5];
                            valor = Convert.ToDecimal(valores[11]);

                        }
                        else if (nombreArchivo.StartsWith("pedidosDetallado")) 
                        {
                            dato1 = Convert.ToDecimal(valores[6]);
                            dato2 = Convert.ToDecimal(valores[7]);
                            dato3 = Convert.ToDecimal(valores[8]);
                            direccion = valores[2];
                            medioDePago = valores[4];
                            valor = Convert.ToDecimal(valores[10]);
                        }
                        idMedioPago = MapearMedioDePago(medioDePago);

                        if (idMedioPago==0)
                         {
                            if(dato1!=0)
                            {
                                Domicilio domicilio = new Domicilio
                                {
                                    Direccion = direccion,
                                    Valor = dato1,
                                    MedioDePago = 1,
                                };
                                domicilios.Add(domicilio);

                            }
                            if (dato2 != 0)
                            {
                                Domicilio domicilio = new Domicilio
                                {
                                    Direccion = direccion,
                                    Valor = dato2,
                                    MedioDePago = 3,
                                };
                                domicilios.Add(domicilio);

                            }
                            if (dato3 != 0)
                            {
                                Domicilio domicilio = new Domicilio
                                {
                                    Direccion = direccion,
                                    Valor = dato3,
                                    MedioDePago = 5,
                                };
                                domicilios.Add(domicilio);

                            }

                        }
                        else
                        {
                            Domicilio domicilio = new Domicilio
                            {
                                Direccion = direccion,
                                Valor = valor,
                                MedioDePago = idMedioPago,
                            };
                            domicilios.Add(domicilio);
                        }


                        
                    }
                }

                InsertarDomiciliosEnBaseDeDatos(domicilios); 

                return true;
            }
            catch (Exception ex)
            {
                // Manejar el error (puedes agregar logging o manejarlo de otra forma)
                Console.WriteLine("Error al leer el archivo CSV: " + ex.Message);
                return false;
            }
        }

       

        public int MapearMedioDePago(string descripcionExcel)
        {
            switch (descripcionExcel.ToLower())
            {
                case "transferencia":
                    return 5;
                case "qr":
                    return 6; 

                case "datafono":
                    return 3; 

                case "efectivo":
                    return 1;

                default:
                    return 0;
            }
        }
        public bool InsertarDomiciliosEnBaseDeDatos(List<Domicilio> domicilios)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();

                    foreach (var domicilio in domicilios)
                    {
                        string query = "INSERT INTO MovimientoCaja (IdCierre,IdCaja, IdUsuario, IdConcepto, Valor, Descripcion, IdMedioPago) " +
                                       "VALUES (@IdCierre,@IdCaja, @IdUsuario, @IdConcepto, @Valor, @Descripcion, @IdMedioPago)";

                        using (SqlCommand cmd = new SqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", ppal.idCaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                            cmd.Parameters.AddWithValue("@IdConcepto", 10); 
                            cmd.Parameters.AddWithValue("@Valor", domicilio.Valor);
                            cmd.Parameters.AddWithValue("@Descripcion", domicilio.Direccion);
                            cmd.Parameters.AddWithValue("@IdMedioPago", domicilio.MedioDePago);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar domicilios en la base de datos: " + ex.Message);
                return false;
            }

        }
    }
}
