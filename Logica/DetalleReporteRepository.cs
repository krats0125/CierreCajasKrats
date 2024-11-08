using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion;
using CierreDeCajas.Presentacion.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Logica
{
    public class DetalleReporteRepository
    {
        CierreCaja oCierreCaja = new CierreCaja();
        FrmDetalleReporte frmDr = null;
        FrmReportes frmD=null;
        CONEXION conexion = new CONEXION();
        decimal valorVentas = 0;
        decimal entregaUltimoEfectivo = 0;
        string novedades;
        private DateTime fechaApertura;

        public DetalleReporteRepository(FrmDetalleReporte frmDr,DateTime fechaApertura )
        {
            this.frmDr = frmDr;
            this.fechaApertura = fechaApertura;
            CargarValorUltimoEfectivo();
            CargarValorVentas();
            CargarNovedades();
        }

        public decimal ActualizarVentas(string IdUsuario)
        {
            decimal ventasNuevas = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.ConexionRibisoft()))
                {
                    cn.Open();
                    string sql = $@"	SELECT 
                             SUM(F1.total) AS VENTAS,
                             ISNULL(
                                 (SELECT SUM(NC.total) 
                                  FROM Notas_CxC1 NC 
                                  WHERE NC.IdUsuario = @IdUsuario
                                  AND CAST(NC.Fecha AS DATE) =CAST(@Fecha AS DATE)), 0) AS DEVOLUCIONES,
                             SUM(F1.total) - 
                             ISNULL(
                                 (SELECT SUM(NC.total) 
                                  FROM Notas_CxC1 NC 
                                  WHERE NC.IdUsuario = @IdUsuario
                                  AND CAST(NC.Fecha AS DATE) = CAST(@Fecha AS DATE)), 0) AS VentasTotales
                         FROM 
                             Facturas1 F1
                         WHERE 
                             F1.IdUsuario = @IdUsuario 
                             AND CAST(F1.FechaCreacion AS DATE) = CAST(@Fecha AS DATE)";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
                        cmd.Parameters.AddWithValue("@Fecha", fechaApertura);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                ventasNuevas = Convert.ToDecimal(dr["VentasTotales"]);
                                if (ventasNuevas > 0)
                                {
                                    valorVentas = ventasNuevas;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return valorVentas;
        }

        public decimal CargarValorVentas()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = "SELECT ValorVentas FROM CierreCaja WHERE IdCierre = @IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            valorVentas = Convert.ToDecimal(result);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor de ventas guardado: {ex.Message}");
            }
            return valorVentas;
        }


        public string CargarNovedades()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = "select descripcion from novedades where IdCierre=@IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            novedades =Convert.ToString(result);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor de ventas guardado: {ex.Message}");
            }
            return novedades;
        }
        public decimal CargarValorUltimoEfectivo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = "SELECT EntregaUltimoEfectivo FROM CierreCaja WHERE IdCierre = @IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            entregaUltimoEfectivo = Convert.ToDecimal(result);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor del ultimo efectivo guardado: {ex.Message}");
            }
            return entregaUltimoEfectivo;
        }

        public bool ActualizarCierre(int IdCierre)
        {

            FrmMenudaAdmin Frm = new InstanciasRepository().InstanciaFrmMenudaAdmin();

            if (Frm != null)
            {
                entregaUltimoEfectivo = Frm.valorentregado;
                CargarValorVentas();
            }

            bool respuesta = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.ConexionCierreCaja()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_ActualizarCierreCaja", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCierre", IdCierre);
                        cmd.Parameters.AddWithValue("@EntregaUltimoEfectivo", entregaUltimoEfectivo);
                        cmd.Parameters.AddWithValue("@ValorVentas", valorVentas);

                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return respuesta;
            }

            return respuesta;
        }

        public CierreCaja listar(int IdCierre, out string mensaje)
        {
            mensaje = string.Empty;
            CierreCaja oCierreCaja = new CierreCaja();
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = @"select *
                                   from CierreCaja
                                   where IdCierre=@IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {

                        cmd.Parameters.AddWithValue("@IdCierre", IdCierre);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oCierreCaja.TotalEfectivo = Convert.ToDecimal(dr["TotalEfectivo"].ToString());
                                oCierreCaja.ValorVentas = Convert.ToDecimal(dr["ValorVentas"].ToString());
                                oCierreCaja.Diferencia = Convert.ToDecimal(dr["Diferencia"].ToString());
                                oCierreCaja.TotalMovimientosCaja = Convert.ToDecimal(dr["TotalMovimientosCaja"].ToString());
                                oCierreCaja.EntregaUltimoEfectivo = Convert.ToDecimal(dr["EntregaUltimoEfectivo"].ToString());
                                oCierreCaja.TotalDatafono = Convert.ToDecimal(dr["TotalDatafono"].ToString());
                                oCierreCaja.TotalTransferencia = Convert.ToDecimal(dr["TotalTransferencia"].ToString());
                                oCierreCaja.TotalLiquidado = Convert.ToDecimal(dr["TotalLiquidado"].ToString());

                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "hubo un error : " + ex.Message;
                return oCierreCaja;
            }

            return oCierreCaja;
        }

        public bool LeerExcel(List<string> rutasArchivos)
        {
            try
            {
                List<Domicilio> domicilios = new List<Domicilio>();

                foreach (string rutaArchivo in rutasArchivos)
                {
                    string nombreArchivo = Path.GetFileName(rutaArchivo).Trim();

                    using (var reader = new StreamReader(rutaArchivo))
                    {
                        string encabezados = reader.ReadLine(); // Leer la primera línea (encabezados)

                        while (!reader.EndOfStream)
                        {
                            var linea = reader.ReadLine();
                            var valores = linea.Split(',');

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

                            // Lógica para agregar Domicilio según datos obtenidos
                            if (idMedioPago == 0)
                            {
                                if (dato1 != 0)
                                {
                                    domicilios.Add(new Domicilio { Direccion = direccion, Valor = dato1, MedioDePago = 11 });
                                }
                                if (dato2 != 0)
                                {
                                    domicilios.Add(new Domicilio { Direccion = direccion, Valor = dato2, MedioDePago = 3 });
                                }
                                if (dato3 != 0)
                                {
                                    domicilios.Add(new Domicilio { Direccion = direccion, Valor = dato3, MedioDePago = 5 });
                                }
                            }
                            else
                            {
                                domicilios.Add(new Domicilio { Direccion = direccion, Valor = valor, MedioDePago = idMedioPago });
                            }
                        }
                    }
                }

                // Insertar todos los domicilios en la base de datos en una sola operación
                InsertarDomiciliosEnBaseDeDatos(domicilios);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer archivos CSV: " + ex.Message);
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
                    return 11;

                default:
                    return 0;
            }
        }
        public bool InsertarDomiciliosEnBaseDeDatos(List<Domicilio> domicilios)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Se eliminaran los archivos anteriores.¿Desea continuar?",
                    "Confirmación de eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection cn = new SqlConnection(conexion.ConexionCierreCaja()))
                    {
                        cn.Open();
                        string consulta = "Delete from MovimientoCaja Where IdCierre=@IdCierre and IdConcepto=10";
                        using (SqlCommand Eliminarcmd = new SqlCommand(consulta, cn))
                        {
                            Eliminarcmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                            Eliminarcmd.ExecuteNonQuery();
                        }

                        foreach (var domicilio in domicilios)
                        {
                            string query = "INSERT INTO MovimientoCaja (IdCierre,IdUsuario, IdConcepto, Valor, Descripcion, IdMedioPago) " +
                                           "VALUES (@IdCierre,@IdUsuario, @IdConcepto, @Valor, @Descripcion, @IdMedioPago)";

                            using (SqlCommand cmd = new SqlCommand(query, cn))
                            {
                                cmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                                cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
                                cmd.Parameters.AddWithValue("@IdConcepto", 10);
                                cmd.Parameters.AddWithValue("@Valor", domicilio.Valor);
                                cmd.Parameters.AddWithValue("@Descripcion", domicilio.Direccion);
                                cmd.Parameters.AddWithValue("@IdMedioPago", domicilio.MedioDePago);

                                cmd.ExecuteNonQuery();
                            }
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
