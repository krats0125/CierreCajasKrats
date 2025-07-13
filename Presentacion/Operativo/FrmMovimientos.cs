

using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using Guna.UI2.WinForms.Suite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace CierreDeCajas.Presentacion.Sistema
{
    public partial class FrmMovimientos : Form
    {
        Principal ppal = null;
        CONEXION Conexion = new CONEXION();
        public int idCaja;

        private bool test = false;
        const string mesDiaPrueba = "0702";


        public FrmMovimientos(Principal ppal)
        {
            this.ppal = ppal;

            InitializeComponent();
           this.idCaja=ppal.idCaja;

        }

        private void Movimientos_Load(object sender, EventArgs e)
        {
            // se llenan los comboBox
            ListaMovimientos();
            listarConceptos();
            ListarMediosPago();
            ListaMoviemintosRepetidos();

        }
        #region Traer Transferencias
        public List<TrasferenciaP> traerTransferencias2()
        {
            List<TrasferenciaP> transferencias = new List<TrasferenciaP>();
            string fecha = ppal.Fecha.ToString("yyyy-MM-dd");

            try
            {
              

                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionRibisoft()))
                {
                    conexion.Open();
                    string consulta = $@"select fp.Valor,f.Fecha, fp.MedioPago,f.Numero
                                      from Facturas1 f inner join formaspago fp 
                                      on f.Numero=fp.Numero 
									  left join Notas_CxC1 nc
									  on f.Numero=nc.Factura
                                      where fp.MedioPago='0405' AND f.IdUsuario=@IdUsuario and CONVERT(date,f.FechaCreacion)=@Fecha AND (nc.Total <> fp.Valor OR nc.Numero IS NULL);";
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                        //cmd.Parameters.AddWithValue("@Fecha",fechaformateada);
                        cmd.Parameters.AddWithValue("@Fecha",fecha);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                TrasferenciaP transferencia = new TrasferenciaP
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
                                    MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
                                    Factura = reader["Numero"].ToString()
                                };
                                transferencias.Add(transferencia);
                            }
                        }
                    }
                }
          
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer transferencias: " + ex.Message);
            }
            return transferencias;
        }

        public List<TrasferenciaP> traerTransferencias()
        {
            List<TrasferenciaP> transferencias = new List<TrasferenciaP>();
            
            string fecha = ppal.Fecha.ToString("yyyy-MM-dd");

            string mes = DateTime.Now.ToString("MM");
            string dia = DateTime.Now.ToString("dd");

            string mesDia = mes + dia;

            string tablaMesVenta = "Ventae" + mes;

            if (test)
            {
                mesDia = mesDiaPrueba;
            
            }

          
            try
            {


                using (OdbcConnection conexion = new OdbcConnection(Conexion.ConexionVisualFoxPro()))
                {
                    conexion.Open();
                    string consulta = $@"select ve.fpago as MedioPago, ve.total_fpago as Valor, v.estado, ve.id_usuario,ve.fecha,allTrim(v.terminal) + v.mmdd + v.conse as factura from ventacam ve
                                         inner join {tablaMesVenta} v on ve.terminal = v.terminal and ve.mmdd = v.mmdd and ve.conse = v.conse
                                         where ve.fpago = 'TR' and ve.id_usuario = '{ppal.idUsuario}' and ve.mmdd = '{mesDia}' and v.estado <> 'ANULADA'";
                    consulta = consulta.Trim();
                    
                    using (OdbcCommand cmd = new OdbcCommand(consulta, conexion))
                    {
                        
                        using (OdbcDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                TrasferenciaP transferencia = new TrasferenciaP
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
                                    MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
                                    Factura = reader["Factura"].ToString()
                                };
                                transferencias.Add(transferencia);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer transferencias: " + ex.Message);
            }
            return transferencias;
        }
        private static bool TransferenciasInsertados = false;

        public bool InsetarTransferencias(List<TrasferenciaP> transferencias)
        {
            if (TransferenciasInsertados)
            {
                return false;
            }

            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    conexion.Open();

                    foreach (var Transferencias in transferencias)
                    {

                        string consultaVerificacion = @"
                                                      SELECT COUNT(*) FROM MovimientoCaja
                                                      WHERE IdCierre = @IdCierre AND Valor = @Valor AND Fecha = @Fecha AND IdMedioPago = @IdMedioPago AND Factura = @Factura";

                        using (SqlCommand cmdVerificar = new SqlCommand(consultaVerificacion, conexion))
                        {
                            cmdVerificar.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmdVerificar.Parameters.AddWithValue("@Valor", Transferencias.Valor);
                            cmdVerificar.Parameters.AddWithValue("@Fecha", Transferencias.Fecha);
                            cmdVerificar.Parameters.AddWithValue("@IdMedioPago", 4);
                            cmdVerificar.Parameters.AddWithValue("@Factura", Transferencias.Factura);

                            int count = (int)cmdVerificar.ExecuteScalar();

                            if (count > 0)
                            {
                                continue; // Si ya existe, saltar esta inserción
                            }
                        }
                        using (SqlCommand cmd = new SqlCommand("InsertarTransferencia", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", ppal.idCaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                            cmd.Parameters.AddWithValue("@IdConcepto", 4);
                            cmd.Parameters.AddWithValue("@Valor", Transferencias.Valor);
                            cmd.Parameters.AddWithValue("@IdMedioPago", 4);
                            cmd.Parameters.AddWithValue("@Fecha", Transferencias.Fecha);
                            cmd.Parameters.AddWithValue("@Factura", Transferencias.Factura);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                ListaMovimientos();
                respuesta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar las transferencias: " + ex.Message);
                respuesta = false;
            }

            TransferenciasInsertados = true; // Marcar que ya se insertaron los bonos
            return respuesta;
        }
        #endregion

        #region Traer Rappi
        public List<VentasRappi> traerVentasRappi2()
        {
            List<VentasRappi> rappi = new List<VentasRappi>();
            string fecha = ppal.Fecha.ToString("yyyy-MM-dd");

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionRibisoft()))
                {
                    conexion.Open();
                    string consulta = $@"select f.IdUsuario,fp.Valor,f.Fecha, fp.MedioPago,f.Numero
										  from Facturas1 f inner join formaspago fp
										  on f.Numero=fp.Numero
										  left join Notas_CxC1 nc
										  on f.Numero=nc.Factura
										  where fp.MedioPago='07' AND f.IdUsuario=@IdUsuario and CONVERT(date,f.FechaCreacion)=@Fecha AND (nc.Total <> fp.Valor OR nc.Numero IS NULL);";
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                        //cmd.Parameters.AddWithValue("@Fecha",fechaformateada);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                VentasRappi ventaRappi = new VentasRappi
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
                                    MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
                                    Factura = reader["Numero"].ToString()
                                };
                                rappi.Add(ventaRappi);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer las ventas de Rappi: " + ex.Message);
            }
            return rappi;
        }
        public List<VentasRappi> traerVentasRappi()
        {
            List<VentasRappi> rappi = new List<VentasRappi>();
            string fecha = ppal.Fecha.ToString("yyyy-MM-dd");

            string mes = DateTime.Now.ToString("MM");
            string dia = DateTime.Now.ToString("dd");

            string mesDia = mes + dia;

            string tablaMesVenta = "Ventae" + mes;

            if (test)
            {
                mesDia = mesDiaPrueba;
            }

            try
            {
                using (OdbcConnection conexion = new OdbcConnection(Conexion.ConexionVisualFoxPro()))
                {
                    conexion.Open();
                    string consulta = $@"select ve.fpago as MedioPago, ve.total_fpago as Valor, v.estado, ve.id_usuario,ve.fecha,allTrim(v.terminal) + v.mmdd + v.conse as factura from ventacam ve
                                         inner join {tablaMesVenta} v on ve.terminal = v.terminal and ve.mmdd = v.mmdd and ve.conse = v.conse
                                         where ve.fpago = 'RA' and ve.id_usuario = '{ppal.idUsuario}' and ve.mmdd = '{mesDia}' and v.estado <> 'ANULADA'";

                    consulta = consulta.Trim();

                    using (OdbcCommand cmd = new OdbcCommand(consulta, conexion))
                    {
                       
                        using (OdbcDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                VentasRappi ventaRappi = new VentasRappi
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   
                                    MedioDePago = reader["MedioPago"].ToString(),
                                    Factura = reader["Factura"].ToString()
                                };
                                rappi.Add(ventaRappi);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer las ventas de Rappi: " + ex.Message);
            }
            return rappi;
        }
        private static bool VentasRappiInsertados = false;

        public bool InsetarVentasRappi(List<VentasRappi> ventasRappi)
        {
            if (VentasRappiInsertados)
            {
                return false;
            }

            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    conexion.Open();

                    foreach (var Rappi in ventasRappi)
                    {

                        string consultaVerificacion = @"
                                                      SELECT COUNT(*) FROM MovimientoCaja
                                                      WHERE IdCierre = @IdCierre AND Valor = @Valor AND Fecha = @Fecha AND IdMedioPago = @IdMedioPago AND Factura = @Factura";

                        using (SqlCommand cmdVerificar = new SqlCommand(consultaVerificacion, conexion))
                        {
                            cmdVerificar.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmdVerificar.Parameters.AddWithValue("@Valor", Rappi.Valor);
                            cmdVerificar.Parameters.AddWithValue("@Fecha", Rappi.Fecha);
                            cmdVerificar.Parameters.AddWithValue("@IdMedioPago", 13);
                            cmdVerificar.Parameters.AddWithValue("@Factura", Rappi.Factura);

                            int count = (int)cmdVerificar.ExecuteScalar();

                            if (count > 0)
                            {
                                continue; // Si ya existe, saltar esta inserción
                            }

                        }
                        using (SqlCommand cmd = new SqlCommand("InsertarVentasRappi", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", ppal.idCaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                            cmd.Parameters.AddWithValue("@IdConcepto", 18);
                            cmd.Parameters.AddWithValue("@Valor", Rappi.Valor);
                            cmd.Parameters.AddWithValue("@IdMedioPago", 13);
                            cmd.Parameters.AddWithValue("@Fecha", Rappi.Fecha);
                            cmd.Parameters.AddWithValue("@Factura", Rappi.Factura);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                ListaMovimientos();
                respuesta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar las ventas de Rappi: " + ex.Message);
                respuesta = false;
            }

            VentasRappiInsertados = true;
            return respuesta;
        }
        
        //public void EliminarDevolucionesRappi()
        //{
        //    List<Devoluciones> devolucionesList = new List<Devoluciones>();
        //    string fecha = ppal.Fecha.ToString("yyyy-MM-dd");

        //    try
        //    {
        //        using (SqlConnection conexionDevoluciones = new SqlConnection(Conexion.ConexionRibisoft()))
        //        {
        //            conexionDevoluciones.Open();

        //            string consultaVerificacionDevolucion = @"
        //                    SELECT f.Numero, nc.total
        //                    FROM Notas_CxC1 nc
        //                    INNER JOIN Facturas1 f ON f.Numero = nc.Factura
        //                    INNER JOIN FormasPago fp ON f.Numero = fp.Numero
        //                    WHERE fp.MedioPago = '07' 
        //                    AND f.IdUsuario = @IdUsuario
        //                    AND CONVERT(date, f.FechaCreacion) = @Fecha";

        //            using (SqlCommand cmdVerificacionDevolucion = new SqlCommand(consultaVerificacionDevolucion, conexionDevoluciones))
        //            {
        //                cmdVerificacionDevolucion.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
        //                cmdVerificacionDevolucion.Parameters.AddWithValue("@Fecha", fecha);

        //                using (SqlDataReader reader = cmdVerificacionDevolucion.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        Devoluciones devolucion = new Devoluciones
        //                        {
        //                            FacturaDevolucion = reader["Numero"].ToString(),
        //                            valorMovimientoDevuelto = Convert.ToDecimal(reader["Valor"])
        //                        };
        //                        devolucionesList.Add(devolucion);
        //                    }
        //                }
        //            }
        //        }

        //        using (SqlConnection conexionCaja = new SqlConnection(Conexion.ConexionCierreCaja()))
        //        {
        //            conexionCaja.Open();

        //            foreach (var devolucion in devolucionesList)
        //            {
        //                string consultaVerificacionMovimiento = @"SELECT Valor, Factura
        //                FROM MovimientoCaja
        //                WHERE IdUsuario = @IdUsuario AND IdCierre = @IdCierre AND IdMedioPago = 13 AND Factura = @Factura";

        //                using (SqlCommand cmdVerificacionMovimiento = new SqlCommand(consultaVerificacionMovimiento, conexionCaja))
        //                {
        //                    cmdVerificacionMovimiento.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
        //                    cmdVerificacionMovimiento.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
        //                    cmdVerificacionMovimiento.Parameters.AddWithValue("@Factura", devolucion.FacturaDevolucion);

        //                    using (SqlDataReader reader = cmdVerificacionMovimiento.ExecuteReader())
        //                    {
        //                        if (reader.Read())
        //                        {
        //                            decimal valorMovimiento = Convert.ToDecimal(reader["Valor"]);
        //                            string facturaMovimiento = reader["Factura"].ToString();
        //                            reader.Close();
        //                            if (devolucion.valorMovimientoDevuelto == valorMovimiento && devolucion.FacturaDevolucion == facturaMovimiento)
        //                            {
        //                                string eliminarMovimientoCaja = @"DELETE FROM MovimientoCaja
        //                                                                  WHERE IdCierre = @IdCierre
        //                                                                  AND Factura = @Factura";

        //                                using (SqlCommand cmdEliminar = new SqlCommand(eliminarMovimientoCaja, conexionCaja))
        //                                {
        //                                    cmdEliminar.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
        //                                    cmdEliminar.Parameters.AddWithValue("@Factura", devolucion.FacturaDevolucion);
        //                                    cmdEliminar.ExecuteNonQuery();

        //                                }
        //                            }
        //                            else
        //                            {
        //                                string actualizarMovimientoCaja = @"UPDATE MovimientoCaja
        //                                                                    SET Valor = @NuevoValor
        //                                                                    WHERE IdCierre = @IdCierre
        //                                                                    AND Factura = @Factura";

        //                                using (SqlCommand cmdActualizar = new SqlCommand(actualizarMovimientoCaja, conexionCaja))
        //                                {
        //                                    cmdActualizar.Parameters.AddWithValue("@NuevoValor", devolucion.valorMovimientoDevuelto);
        //                                    cmdActualizar.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
        //                                    cmdActualizar.Parameters.AddWithValue("@Factura", devolucion.FacturaDevolucion);

        //                                    cmdActualizar.ExecuteNonQuery();
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            reader.Close();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al eliminar las devoluciones de Rappi: " + ex.Message);
        //    }
        //}
        #endregion

        #region Traer Datafonos
        public List<Datafonos> traerDatafonos2()
        {
            List<Datafonos> Datafonos = new List<Datafonos>();
            string fechaformateada= ppal.Fecha.ToString("yyyy-MM-dd");
            try
            {


                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionRibisoft()))
                {
                    conexion.Open();
                    string consulta = $@"select fp.Valor,f.Fecha, fp.MedioPago,f.Numero 
                                       from Facturas1 f inner join formaspago fp 
									   on f.Numero=fp.Numero 
									   left join Notas_CxC1 nc
									  on f.Numero=nc.Factura
                                       where fp.MedioPago='0401' AND f.IdUsuario=@IdUsuario and CONVERT(date,f.FechaCreacion)=@Fecha AND (nc.Total <> fp.Valor OR nc.Numero IS NULL)";
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                        //cmd.Parameters.AddWithValue("@Fecha",fechaformateada);
                        cmd.Parameters.AddWithValue("@Fecha",fechaformateada);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Datafonos datafonos = new Datafonos
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
                                    MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
                                    Factura = reader["Numero"].ToString()
                                };
                                Datafonos.Add(datafonos);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer datafonos: " + ex.Message);
            }
            return Datafonos;
        }

        public List<Datafonos> traerDatafonos()
        {
            List<Datafonos> Datafonos = new List<Datafonos>();

            string fecha = ppal.Fecha.ToString("yyyy-MM-dd");

            string mes = DateTime.Now.ToString("MM");
            string dia = DateTime.Now.ToString("dd");

            string mesDia = mes + dia;

            string tablaMesVenta = "Ventae" + mes;

            if (test)
            {
                mesDia = mesDiaPrueba;
            }


           
            try
            {


                using (OdbcConnection conexion = new OdbcConnection(Conexion.ConexionVisualFoxPro()))
                {
                    conexion.Open();
                    string consulta = $@"select ve.fpago as MedioPago, ve.total_fpago as Valor, v.estado, ve.id_usuario,ve.fecha,allTrim(v.terminal) + v.mmdd + v.conse as factura from ventacam ve
                                         inner join {tablaMesVenta} v on ve.terminal = v.terminal and ve.mmdd = v.mmdd and ve.conse = v.conse
                                         where (ve.fpago = 'TD' or ve.fpago = 'TC') and ve.id_usuario = '{ppal.idUsuario}' and ve.mmdd = '{mesDia}' and v.estado <> 'ANULADA'";
                    using (OdbcCommand cmd = new OdbcCommand(consulta, conexion))
                    {
                        using (OdbcDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
                                Datafonos datafonos = new Datafonos
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
                                    MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
                                    Factura = reader["Factura"].ToString()
                                };
                                Datafonos.Add(datafonos);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer datafonos: " + ex.Message);
            }
            return Datafonos;
        }

        private static bool DatafonosInsertados = false;

        public bool InsertarDatafonos(List<Datafonos> datafonos)
        {
            if (DatafonosInsertados)
            {
                return false;
            }

            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    conexion.Open();

                    foreach (var Datafonos in datafonos)
                    {

                        string consultaVerificacion = @"
                                                      SELECT COUNT(*) FROM MovimientoCaja
                                                      WHERE IdCierre = @IdCierre AND Valor = @Valor AND Fecha = @Fecha AND IdMedioPago = @IdMedioPago AND Factura = @Factura";

                        using (SqlCommand cmdVerificar = new SqlCommand(consultaVerificacion, conexion))
                        {
                            cmdVerificar.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmdVerificar.Parameters.AddWithValue("@Valor", Datafonos.Valor);
                            cmdVerificar.Parameters.AddWithValue("@Fecha", Datafonos.Fecha);
                            cmdVerificar.Parameters.AddWithValue("@IdMedioPago", 2);
                            cmdVerificar.Parameters.AddWithValue("@Factura", Datafonos.Factura);

                            int count = (int)cmdVerificar.ExecuteScalar();

                            if (count > 0)
                            {
                                continue; // Si ya existe, saltar esta inserción
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("InsertarDatafono", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", ppal.idCaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                            cmd.Parameters.AddWithValue("@IdConcepto", 5);
                            cmd.Parameters.AddWithValue("@Valor", Datafonos.Valor   );
                            cmd.Parameters.AddWithValue("@IdMedioPago", 2);
                            cmd.Parameters.AddWithValue("@Fecha", Datafonos.Fecha);
                            cmd.Parameters.AddWithValue("@Factura", Datafonos.Factura);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                ListaMovimientos();
                respuesta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar los Datafonos: " + ex.Message);
                respuesta = false;
            }

            DatafonosInsertados = true; // Marcar que ya se insertaron los bonos
            return respuesta;
        }
        #endregion

        #region Traer bonos
        public List<BonoAlcaldia> traerBonos2()
        {
            List<BonoAlcaldia> BonosAlcaldia = new List<BonoAlcaldia>();
            string fechaformateada= ppal.Fecha.ToString("yyyy-MM-dd");
            try
            {


                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionRibisoft()))
                {
                    conexion.Open();
                    string consulta = $@"select fp.Valor,f.Fecha, fp.MedioPago,f.Numero
                                      from Facturas1 f inner join formaspago fp 
                                      on f.Numero=fp.Numero
									  left join Notas_CxC1 nc
									  on f.Numero=nc.Factura
                                      where fp.MedioPago='0501' AND f.IdUsuario=@IdUsuario and CONVERT(date,f.FechaCreacion)=@Fecha AND (nc.Total <> fp.Valor OR nc.Numero IS NULL);";
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                        cmd.Parameters.AddWithValue("@Fecha",fechaformateada);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                BonoAlcaldia bonosalcaldia = new BonoAlcaldia
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
                                    MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
                                    Factura = reader["Numero"].ToString()
                                };
                                BonosAlcaldia.Add(bonosalcaldia);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer datafonos: " + ex.Message);
            }
            return BonosAlcaldia;
        }
        public List<BonoAlcaldia> traerBonos()
        {
            List<BonoAlcaldia> BonosAlcaldia = new List<BonoAlcaldia>();
            string fechaformateada = ppal.Fecha.ToString("yyyy-MM-dd");

            string mes = DateTime.Now.ToString("MM");
            string dia = DateTime.Now.ToString("dd");

            string mesDia = mes + dia;

            string tablaMesVenta = "Ventae" + mes;

            if (test)
            {
                mesDia = mesDiaPrueba;
            }

            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionVisualFoxPro()))
                {
                    conexion.Open();
                    string consulta = $@"select ve.fpago as MedioPago, ve.total_fpago as Valor, v.estado, ve.id_usuario,ve.fecha,allTrim(v.terminal) + v.mmdd + v.conse as factura from ventacam ve
                                         inner join {tablaMesVenta} v on ve.terminal = v.terminal and ve.mmdd = v.mmdd and ve.conse = v.conse
                                         where ve.fpago = 'BO' and ve.id_usuario = '{ppal.idUsuario}' and ve.mmdd = '{mesDia}' and v.estado <> 'ANULADA'";

                    consulta = consulta.Trim();

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                BonoAlcaldia bonosalcaldia = new BonoAlcaldia
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
                                    MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
                                    Factura = reader["Factura"].ToString()
                                };
                                BonosAlcaldia.Add(bonosalcaldia);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer datafonos: " + ex.Message);
            }
            return BonosAlcaldia;
        }

        private static bool BonosInsertados = false;
        public bool InsertarBonoAlcadia(List<BonoAlcaldia> bonos)
        {
            if (BonosInsertados)
            {
                return false;
            }

            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    conexion.Open();

                    foreach (var Bonos in bonos)
                    {

                        string consultaVerificacion = @"
                                                      SELECT COUNT(*) FROM MovimientoCaja
                                                      WHERE IdCierre = @IdCierre AND Valor = @Valor AND Fecha = @Fecha AND IdMedioPago = @IdMedioPago AND Factura = @Factura";

                        using (SqlCommand cmdVerificar = new SqlCommand(consultaVerificacion, conexion))
                        {
                            cmdVerificar.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmdVerificar.Parameters.AddWithValue("@Valor", Bonos.Valor);
                            cmdVerificar.Parameters.AddWithValue("@Fecha", Bonos.Fecha);
                            cmdVerificar.Parameters.AddWithValue("@IdMedioPago", 7);
                            cmdVerificar.Parameters.AddWithValue("@Factura", Bonos.Factura);

                            int count = (int)cmdVerificar.ExecuteScalar();

                            if (count > 0)
                            {
                                continue; // Si ya existe, saltar esta inserción
                            }
                        }

                        // Insertar el bono si no existe
                        using (SqlCommand cmd = new SqlCommand("InsertarBonos", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", ppal.idCaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                            cmd.Parameters.AddWithValue("@IdConcepto", 16);
                            cmd.Parameters.AddWithValue("@Valor", Bonos.Valor);
                            cmd.Parameters.AddWithValue("@IdMedioPago", 7);
                            cmd.Parameters.AddWithValue("@Fecha", Bonos.Fecha);
                            cmd.Parameters.AddWithValue("@Factura", Bonos.Factura);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                ListaMovimientos();
                respuesta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar los bonos: " + ex.Message);
                respuesta = false;
            }

            BonosInsertados = true; // Marcar que ya se insertaron los bonos
            return respuesta;
        }
        #endregion

        public void ListaMovimientos()
        {
            string consulta = $@"Select MC.IdMovimiento, CM.Concepto AS CONCEPTO, MC.Descripcion AS DESCRIPCION, FORMAT(MC.Valor, 'N0', 'es-CO') AS VALOR, MP.Descripcion AS 'MEDIO DE PAGO', MC.fecha AS FECHA
            from MovimientoCaja MC 
            inner join ConceptoMovimiento CM ON MC.IdConcepto= CM.Id 
            inner join MediosDePago MP ON MC.IdMedioPago=MP.IdMedioPago
            where MC.IdUsuario='{ppal.idUsuario}' and MC.IdCierre= {ppal.idCierre}
			ORDER BY MP.Descripcion";

            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvMovimientos.DataSource = lista;
            dgvMovimientos.Refresh();
            dgvMovimientos.Columns[0].Visible = false;

        }

        public void ListaMoviemintosRepetidos()
        {
            string consulta = $@"SELECT MC.Descripcion AS DESCRIPCION, MC.Valor AS VALOR
                                FROM 
                                    MovimientoCaja MC
                                INNER JOIN 
                                    ConceptoMovimiento CM ON MC.IdConcepto = CM.Id
                                INNER JOIN 
                                    MediosDePago MP ON MC.IdMedioPago = MP.IdMedioPago
                                WHERE EXISTS (SELECT 1
                                        FROM MovimientoCaja MC2 
                                        WHERE 
                                            MC.Descripcion = MC2.Descripcion 
                                            AND MC.Valor = MC2.Valor
                                            AND MC2.IdMovimiento <> MC.IdMovimiento
                                			and CM.Concepto='Cobro super caja'
                                			AND MC2.Fecha=MC.Fecha
                                    ) and MC.IdUsuario='{ppal.idUsuario}' and MC.IdCierre= {ppal.idCierre}
                                ORDER BY MC.Descripcion, MC.Valor;";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvRepetidos.DataSource = lista;
            dgvRepetidos.Refresh();
        }

        private void ListarMediosPago()
        {
            DataTable listaMediosPago = new SentenciaSqlServer().TraerDatos("select IdMedioPago,Descripcion from MediosDePago", Conexion.ConexionCierreCaja());
            cbMediodepago.DataSource = listaMediosPago;
            cbMediodepago.DisplayMember = "Descripcion";
            cbMediodepago.ValueMember = "IdMedioPago";

        }

        private void listarConceptos()
        {
            DataTable listaConceptos = new SentenciaSqlServer().TraerDatos("Select Id, Concepto from ConceptoMovimiento where activo=1 and id!=9 and id!=19", Conexion.ConexionCierreCaja());
            cbConceptos.DataSource = listaConceptos;
            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";

        }

        

        private void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Al seleccionar una fila se muestra en los campos para ser editada o elimianda
                DataGridViewRow fila = dgvMovimientos.Rows[e.RowIndex];

                lbIdMovimiento.Text = fila.Cells["IdMovimiento"].Value.ToString();
                string Concepto = fila.Cells["Concepto"].Value.ToString();
                cbConceptos.SelectedIndex = cbConceptos.FindStringExact(Concepto);
                txtValor.Text = fila.Cells["Valor"].Value.ToString();
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
                string medioPago = fila.Cells["MEDIO DE PAGO"].Value.ToString();
                cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact(medioPago);
                btnGuarda.Enabled = false;
            }
        }

        private void limpiarcampos()
        {
           
            txtValor.Text = "";
            lbIdMovimiento.Text = "";
            txtDescripcion.Text = "";
        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            cbConceptos.Focus();
        }

        public void Enfoque()
        {
            cbConceptos.Focus();
            cbConceptos.Select();
            
        }

        private void FrmMovimientos_Activated(object sender, EventArgs e)
        {
            //cbConceptos.Focus();
        }

        private void FrmMovimientos_Shown(object sender, EventArgs e)
        {
            this.Activate();
            cbConceptos.Focus();
        }

        private void cbConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbConceptos.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cbConceptos.SelectedItem;
                string concepto = drv["Concepto"].ToString();

                if (concepto == "Cobro super caja")
                {
                    lbTipoCobro.Visible = true;
                    cbTipodecobro.Visible = true;
                }
                else
                {
                    lbTipoCobro.Visible = false;
                    cbTipodecobro.Visible = false;
                }
                if (concepto == "Prestamo Caja" || concepto == "Control" || concepto == "Menuda" || concepto == "Pago" || concepto == "Cierre PTM" || concepto == "Cierre PAC" || concepto == "Saldo clientes" || concepto == "Devoluciones no registradas" || concepto == "Faltante base" )
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Efectivo");
                    cbMediodepago.Enabled = false;

                }
                else if (concepto == "Credito")
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Credito");
                    cbMediodepago.Enabled = false;
                }
                else if (concepto == "Bonos alcaldía ")
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Bonos Alcaldia");
                    cbMediodepago.Enabled = false;
                }
                else if(concepto== "Consumo interno")
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Consumo interno");
                }
                else if(concepto== "Consumo propietario")
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Consumo Propietarios");
                }
                else if (concepto == "Ventas Rappi")
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Ventas Rappi");
                }

                else
                {
                    cbMediodepago.Enabled = true;
                    cbMediodepago.SelectedIndex = -1;
                }
         


            }

        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            if (cbMediodepago.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un medio de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Movimiento oMovimiento = new Movimiento();

            oMovimiento.IdCaja = ppal.idCaja;
            oMovimiento.IdUsuario = ppal.idUsuario;
            oMovimiento.IdCierre = ppal.idCierre;
            oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
            oMovimiento.Descripcion = txtDescripcion.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

            bool seInserto = new MovimientosRepository().Insertar(oMovimiento);
            if (seInserto)
            {
                MessageBox.Show("Se ha insertado el movimiento correctamente.");
                limpiarcampos();
                //if (cbConceptos.SelectedItem != null)
                //{
                //    DataRowView drv = (DataRowView)cbConceptos.SelectedItem;
                //    string concepto = drv["Concepto"].ToString();
                //    if (concepto == "Prestamo Caja" || concepto == "Control" || concepto == "Menuda" || concepto == "Pago" || concepto == "Cierre PTM" || concepto == "Cierre PAC" || concepto == "Saldo clientes" || concepto == "Devoluciones no registradas" || concepto == "Faltante base" || concepto == "Consumo interno" || concepto == "Consumo propietario")
                //    {
                //        cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Efectivo");

                //    }
                //}
                ListaMovimientos();
                ListaMoviemintosRepetidos();
                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();
                bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }
                frm.CargarCierreVentas();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error insertando el movimiento.");
            }

        }

        private void btnEdita_Click(object sender, EventArgs e)
        {
            try
            {
                Movimiento oMovimiento = new Movimiento();
                oMovimiento.IdMovimiento = Convert.ToInt64(lbIdMovimiento.Text);
                oMovimiento.IdCaja = ppal.idCaja;
                oMovimiento.IdUsuario = ppal.idUsuario;
                oMovimiento.IdCierre = ppal.idCierre;
                oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
                oMovimiento.Descripcion = txtDescripcion.Text;
                oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
                oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

                bool seActualizo = new MovimientosRepository().Editar(oMovimiento);
                if (seActualizo)
                {
                    MessageBox.Show("Se actualizo el movimiento correctamente");
                    limpiarcampos();
                    btnGuarda.Enabled = true;
                    FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();

                    ListaMovimientos();//Muestra en el dgv los movimientos
                    ListaMoviemintosRepetidos();
                    frm.CargarSumatorias();//Carga los movimientos al panel de medios de pago(cierre de caja)
                    frm.CitarPanelesMovimientos();

                    bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);//Actualiza el panel del cierre de caja
                    if (!actualizacionExitosa)
                    {
                        MessageBox.Show("Hubo un error actualizando el cierre de caja");
                    }
                    frm.CargarCierreVentas();
                }
                else
                {
                    MessageBox.Show("Hubo un error al actualizar el movimiento");
                    return;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Por favor seleccione el campo que desea editar");
            }

          
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            try
            {
                Movimiento oMovimiento = new Movimiento();


            if (MessageBox.Show("¿Está seguro de que desea eliminar este movimiento?", "Confirmar eliminación",
            MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                oMovimiento.IdMovimiento = Convert.ToInt64(lbIdMovimiento.Text);

                bool seElimino = new MovimientosRepository().Eliminar(oMovimiento);


                if (seElimino)
                {
                    MessageBox.Show("El movimiento se ha elimando correctamente");
                    FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();

                    ListaMovimientos();
                    ListaMoviemintosRepetidos();
                    frm.CargarSumatorias();
                    frm.CitarPanelesMovimientos();

                    bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);//Actualiza el panel del cierre de caja
                    if (!actualizacionExitosa)
                    {
                        MessageBox.Show("Hubo un error actualizando el cierre de caja");
                    }
                    frm.CargarCierreVentas();
                    limpiarcampos();
                    btnGuarda.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el movimiento. Por favor, inténtelo de nuevo.");
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Por favor seleccione el campo que desea eliminar");
            }

        }

        private void cbConceptos_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtValor.Focus();
            }
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            DataRowView drv = (DataRowView)cbConceptos.SelectedItem;
            string concepto = drv["Concepto"].ToString();
            if (concepto == "Datafonos" || concepto == "Transferencia")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    cbMediodepago.Focus();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtDescripcion.Focus();
                }
            }
            
          
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGuarda.Focus();
            }

        }

        private void btnGuarda_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    e.SuppressKeyPress = true;
            //    Movimiento oMovimiento = new Movimiento();

            //    oMovimiento.IdCaja = ppal.idCaja;
            //    oMovimiento.IdUsuario = ppal.idUsuario;
            //    oMovimiento.IdCierre = ppal.idCierre;
            //    oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
            //    oMovimiento.Descripcion = txtDescripcion.Text;
            //    oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            //    oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

            //    bool seInserto = new MovimientosRepository().Insertar(oMovimiento);
            //    if (seInserto)
            //    {
            //        MessageBox.Show("Se ha insertado el movimiento correctamente.");
            //        limpiarcampos();
            //        ListaMovimientos();
            //        FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
            //        frm.CargarSumatorias();
            //        frm.CitarPanelesMovimientos();
            //        bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);
            //        if (!actualizacionExitosa)
            //        {
            //            MessageBox.Show("Hubo un error actualizando el cierre de caja");
            //        }
            //        frm.CargarCierreVentas();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Ha ocurrido un error insertando el movimiento.");
            //    }

            //}
        }

        private void cbMediodepago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDescripcion.Focus();
               
            }
        }
    }
}
