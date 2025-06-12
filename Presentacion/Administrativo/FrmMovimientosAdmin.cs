using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmMovimientosAdmin : Form
    {
        CONEXION Conexion = new CONEXION();
        FrmDetalleReporte frmDr = null;
        private DateTime fechaApertura;
        Principal ppal;

        private void FrmMovimientosAdmin_Load(object sender, EventArgs e)
        {
            ListarMediosPago();
            listarConceptos();
            ListaMovimientos();
            ListaMoviemintosRepetidos();
        }

        public FrmMovimientosAdmin(FrmDetalleReporte frmDr,DateTime fechaApertura)
        {
            InitializeComponent();
            this.frmDr = frmDr;
            this.fechaApertura=fechaApertura;
           
        }

        #region Transferencias
        public List<TrasferenciaP> traerTransferencias()
        {
            List<TrasferenciaP> transferencias = new List<TrasferenciaP>();
            string fecha = fechaApertura.ToString("yyyy-MM-dd");

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
                                      where fp.MedioPago='0405'  AND f.IdUsuario=@IdUsuario and CONVERT(date,f.FechaCreacion)=@Fecha AND (nc.Total <> fp.Valor OR nc.Numero IS NULL);";
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
                        //cmd.Parameters.AddWithValue("@Fecha",fechaformateada);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);

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
        private static bool TransferenciasInsertadas = false;
        public bool InsetarTransferencias(List<TrasferenciaP> transferencias)
        {
            if (TransferenciasInsertadas)
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
                        // Verificar si ya existe
                        string consultaVerificacion = @"
                    SELECT COUNT(*) FROM MovimientoCaja
                    WHERE IdCierre = @IdCierre AND Valor = @Valor AND Fecha = @Fecha AND IdMedioPago = @IdMedioPago AND Factura = @Factura";

                        using (SqlCommand cmdVerificar = new SqlCommand(consultaVerificacion, conexion))
                        {
                            cmdVerificar.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                            cmdVerificar.Parameters.AddWithValue("@Valor", Transferencias.Valor);
                            cmdVerificar.Parameters.AddWithValue("@Fecha", Transferencias.Fecha);
                            cmdVerificar.Parameters.AddWithValue("@IdMedioPago", 2);
                            cmdVerificar.Parameters.AddWithValue("@Factura", Transferencias.Factura);

                            int count = (int)cmdVerificar.ExecuteScalar();


                            if (count > 0)
                            {
                                continue; // Si ya existe, saltar esta inserción
                            }
                        }

                        // Insertar si no existe
                        using (SqlCommand cmd = new SqlCommand("InsertarTransferencia", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", frmDr.idcaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
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

            TransferenciasInsertadas = true; // Marcar que ya se insertaron las transferencias
            return respuesta;
        }

 
        #endregion

        #region Rappi
        public List<VentasRappi> traerVentasRappi()
        {
            List<VentasRappi> rappi = new List<VentasRappi>();
            string fecha = fechaApertura.ToString("yyyy-MM-dd");

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
                        cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
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
        private static bool VentasRappiInsertados = false;

        public bool InsertarVentasRappi(List<VentasRappi> ventasRappi)
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
                            cmdVerificar.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
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
                            cmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", frmDr.idcaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
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
        //    string fecha = fechaApertura.ToString("yyyy-MM-dd");

        //    try
        //    {
        //        using (SqlConnection conexionDevoluciones = new SqlConnection(Conexion.ConexionRibisoft()))
        //        {
        //            conexionDevoluciones.Open();

        //            string consultaVerificacionDevolucion = @"
        //                   SELECT f.Numero, nc.total
        //                   FROM Notas_CxC1 nc
        //                   INNER JOIN Facturas1 f ON f.Numero = nc.Factura
        //                   INNER JOIN FormasPago fp ON f.Numero = fp.Numero
        //                   WHERE fp.MedioPago = '07' 
        //                   AND f.IdUsuario = @IdUsuario
        //                   AND CONVERT(date, f.FechaCreacion) = @Fecha";

        //            using (SqlCommand cmdVerificacionDevolucion = new SqlCommand(consultaVerificacionDevolucion, conexionDevoluciones))
        //            {
        //                cmdVerificacionDevolucion.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
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
        //                    cmdVerificacionMovimiento.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
        //                    cmdVerificacionMovimiento.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
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
        //                                    cmdEliminar.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
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
        //                                    cmdActualizar.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
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

        #region Datafonos
        public List<Datafonos> traerDatafonos()
        {
            List<Datafonos> Datafonos = new List<Datafonos>();
            string fechaformateada = fechaApertura.ToString("yyyy-MM-dd");
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
                                       where fp.MedioPago='0401' AND f.IdUsuario=@IdUsuario and CONVERT(date,f.FechaCreacion)=@Fecha AND (nc.Total <> fp.Valor OR nc.Numero IS NULL);";
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
                        //cmd.Parameters.AddWithValue("@Fecha",fechaformateada);
                        cmd.Parameters.AddWithValue("@Fecha", fechaformateada);

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
                            cmdVerificar.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
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
                            cmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", frmDr.idcaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
                            cmd.Parameters.AddWithValue("@IdConcepto", 5);
                            cmd.Parameters.AddWithValue("@Valor", Datafonos.Valor);
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

        #region Bonos
        public List<BonoAlcaldia> traerBonos()
        {
            List<BonoAlcaldia> BonosAlcaldia = new List<BonoAlcaldia>();
            string fechaformateada = fechaApertura.ToString("yyyy-MM-dd");
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
                        cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
                        cmd.Parameters.AddWithValue("@Fecha", fechaformateada);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                BonoAlcaldia bonoalcaldia = new BonoAlcaldia
                                {
                                    Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
                                    MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
                                    Factura = reader["Numero"].ToString()
                                };
                                BonosAlcaldia.Add(bonoalcaldia);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al traer los bonos: " + ex.Message);
            }
            return BonosAlcaldia;
        }
        private static bool bonosInsertados = false;


        public bool InsertarBonoAlcadia(List<BonoAlcaldia> bonos)
        {
           
            if (bonosInsertados)
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
                                                        WHERE IdCierre = @IdCierre AND Valor = @Valor AND Fecha = @Fecha 
                                                        AND IdMedioPago = @IdMedioPago AND Factura = @Factura";

                        using (SqlCommand cmdVerificar = new SqlCommand(consultaVerificacion, conexion))
                        {
                            cmdVerificar.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
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
                            cmd.Parameters.AddWithValue("@IdCierre", frmDr.IdCierre);
                            cmd.Parameters.AddWithValue("@IdCaja", frmDr.idcaja);
                            cmd.Parameters.AddWithValue("@IdUsuario", frmDr.IdUsuario);
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

            bonosInsertados = true; // Marcar que ya se insertaron los bonos
            return respuesta;
        }
        #endregion

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
                                    ) and MC.IdUsuario='{frmDr.IdUsuario}' and MC.IdCierre= {frmDr.IdCierre}
                                ORDER BY MC.Descripcion, MC.Valor;";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvRepetidos.DataSource = lista;
            dgvRepetidos.Refresh();
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            Movimiento oMovimiento = new Movimiento();
            if (cbMediodepago.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un medio de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            oMovimiento.IdUsuario = frmDr.IdUsuario;
            oMovimiento.IdCaja= frmDr.idcaja;
            oMovimiento.IdCierre = frmDr.IdCierre;
            oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
            oMovimiento.Descripcion = txtDescripcion.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

            bool seInserto = new MovimientosRepository().Insertar(oMovimiento);
            if (seInserto)
            {
                MessageBox.Show("Se ha insertado el movimiento correctamente.");
                limpiarcampos();
                ListaMovimientos();
                ListaMoviemintosRepetidos();
                FrmDetalleReporte frm = new InstanciasRepository().InstanciaFrmDetalle();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();
                bool actualizacionExitosa = new DetalleReporteRepository(frmDr,fechaApertura).ActualizarCierre(frmDr.IdCierre);
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
                if (concepto == "Prestamo Caja" || concepto == "Control" || concepto == "Menuda" || concepto == "Pago" || concepto == "Cierre PTM" || concepto == "Cierre PAC" || concepto == "Saldo clientes" || concepto == "Devoluciones no registradas" || concepto == "Faltante base" || concepto == "Consumo interno" || concepto == "Consumo propietario" || concepto == "Prestamo trabajadores")
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

        private void cbConceptos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtValor.Focus();
            }
        }

        private void cbMediodepago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDescripcion.Focus();
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

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;


                if (cbTipodecobro.Visible)
                {
                    cbTipodecobro.Focus();
                }
                else
                {
                    cbMediodepago.Focus();
                }

            }

        }

        private void cbCajero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGuarda.Focus();
            }
        }

        private void btnGuarda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Movimiento oMovimiento = new Movimiento();

                oMovimiento.IdUsuario = frmDr.IdUsuario;
                oMovimiento.IdCaja = frmDr.idcaja;
                oMovimiento.IdCierre = frmDr.IdCierre;
                oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
                oMovimiento.Descripcion = txtDescripcion.Text;
                oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
                oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

                bool seInserto = new MovimientosRepository().Insertar(oMovimiento);
                if (seInserto)
                {
                    MessageBox.Show("Se ha insertado el movimiento correctamente.");
                    limpiarcampos();
                    ListaMovimientos();
                    ListaMoviemintosRepetidos();
                    FrmDetalleReporte frm = new InstanciasRepository().InstanciaFrmDetalle();
                    frm.CargarSumatorias();
                    frm.CitarPanelesMovimientos();
                    bool actualizacionExitosa = new DetalleReporteRepository(frmDr, fechaApertura).ActualizarCierre(frmDr.IdCierre);
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
        }

        private void btnEdita_Click(object sender, EventArgs e)
        {
            try
            {
                Movimiento oMovimiento = new Movimiento();
                oMovimiento.IdMovimiento = Convert.ToInt64(lbIdMovimiento.Text);
                oMovimiento.IdUsuario = frmDr.IdUsuario;
                oMovimiento.IdCaja = frmDr.idcaja;
                oMovimiento.IdCierre = frmDr.IdCierre;
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
                    FrmDetalleReporte frm = new InstanciasRepository().InstanciaFrmDetalle();

                    ListaMovimientos();//Muestra en el dgv los movimientos
                    ListaMoviemintosRepetidos();
                    frm.CargarSumatorias();//Carga los movimientos al panel de medios de pago(cierre de caja)
                    frm.CitarPanelesMovimientos();

                    bool actualizacionExitosa = new DetalleReporteRepository(frmDr, fechaApertura).ActualizarCierre(frmDr.IdCierre);//Actualiza el panel del cierre de caja
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
                        FrmDetalleReporte frm = new InstanciasRepository().InstanciaFrmDetalle();

                        ListaMovimientos();
                        ListaMoviemintosRepetidos();
                        frm.CargarSumatorias();
                        frm.CitarPanelesMovimientos();

                        bool actualizacionExitosa = new DetalleReporteRepository(frmDr, fechaApertura).ActualizarCierre(frmDr.IdCierre);//Actualiza el panel del cierre de caja
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
        public void ListaMovimientos()
        {
            string consulta = $@"SELECT 
                                   MC.IdMovimiento, 
                                   CM.Concepto AS CONCEPTO, 
                                   MC.Descripcion AS DESCRIPCION, 
                                   FORMAT(MC.Valor, 'N0', 'es-CO') AS VALOR, 
                                   MP.Descripcion AS 'MEDIO DE PAGO', 
                                   MC.fecha AS FECHA
                               FROM 
                                   MovimientoCaja MC 
                               INNER JOIN 
                                   ConceptoMovimiento CM ON MC.IdConcepto= CM.Id 
                               INNER JOIN 
                                   MediosDePago MP ON MC.IdMedioPago=MP.IdMedioPago
                               WHERE 
                                   MC.IdUsuario='{frmDr.IdUsuario}' 
                                   AND MC.IdCierre= {frmDr.IdCierre}
                               ORDER BY 
                                   MP.Descripcion";

            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvMovimientos.DataSource = lista;
            dgvMovimientos.Refresh();
            dgvMovimientos.Columns[0].Visible = false;


        }

        private void limpiarcampos()
        {

            txtValor.Text = "";
            lbIdMovimiento.Text = "";
            txtDescripcion.Text = "";
        }

        private void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
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
    }
    
}
