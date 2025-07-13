using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CierreDeCajas.Presentacion.Sistema;

namespace CierreDeCajas.Logica
{
    public class TransaccionesRepository
    {
        private FrmMovimientos frmMvt;

        // Creaccion de codigo utilitario para hallar las transacciones.
        //private void cargarTransacciones(string MedioDePago)
        //{

        //    DateTime Fecha = DateTime.Now;
        //    var transaccionesCargadas = TraerTransacciones(MedioDePago, Fecha);

        //    bool insercionExitosa = InsetarTransacciones(transaccionesCargadas);

        //    if (insercionExitosa)
        //    {


        //        frmMvt.ListaMovimientos();

        //        FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
        //        frm.CargarSumatorias();
        //        frm.CitarPanelesMovimientos();

        //        bool actualizacionExitosa = new CierreCajaRepository(this).ActualizarCierre(idCierre);
        //        if (!actualizacionExitosa)
        //        {
        //            MessageBox.Show("Hubo un error actualizando el cierre de caja");
        //        }

        //        frm.CargarCierreVentas();
        //    }

        //}

        //public List<MovimientoCaja> TraerTransacciones(string formaPago, DateTime fecha)
        //{
        //    List<TrasferenciaP> transferencias = new List<TrasferenciaP>();
        //    string fecha = ppal.Fecha.ToString("yyyy-MM-dd");

        //    try
        //    {


        //        using (OdbcConnection conexion = new OdbcConnection(Conexion.ConexionVisualFoxPro()))
        //        {
        //            conexion.Open();
        //            string consulta = $@"select ve.fpago as MedioPago, ve.total_fpago as Valor, v.estado, ve.id_usuario,ve.fecha,allTrim(v.terminal) + v.mmdd + v.conse as factura from ventacam ve
        //                         inner join ventae07 v on ve.terminal = v.terminal and ve.mmdd = v.mmdd and ve.conse = v.conse
        //                         where ve.fpago = 'EF' and ve.id_usuario = '1040748591' and ve.mmdd = '0702' and v.estado <> 'ANULADA'";

        //            consulta = consulta.Trim();
        //            using (OdbcCommand cmd = new OdbcCommand(consulta, conexion))
        //            {
        //                cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
        //                //cmd.Parameters.AddWithValue("@Fecha",fechaformateada);
        //                cmd.Parameters.AddWithValue("@Fecha", fecha);

        //                using (OdbcDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {

        //                        TrasferenciaP transferencia = new TrasferenciaP
        //                        {
        //                            Valor = Convert.ToDecimal(reader["Valor"]),           //GetDecimal(0),
        //                            Fecha = Convert.ToDateTime(reader["Fecha"]),   //reader.GetDateTime(1),
        //                            MedioDePago = reader["MedioPago"].ToString(),//reader.GetString(2)
        //                            Factura = reader["Numero"].ToString()
        //                        };
        //                        transferencias.Add(transferencia);
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al traer transferencias: " + ex.Message);
        //    }
        //    return transferencias;
        //}

        //public bool InsetarTransacciones(List<MovimientoCaja> transacciones)
        //{
        //    if (TransferenciasInsertados)
        //    {
        //        return false;
        //    }

        //    bool respuesta = false;
        //    try
        //    {
        //        using (SqlConnection conexion = new SqlConnection(Conexion.ConexionCierreCaja()))
        //        {
        //            conexion.Open();

        //            foreach (var Transaccion in transacciones)
        //            {

        //                string consultaVerificacion = @"
        //                                      SELECT COUNT(*) FROM MovimientoCaja
        //                                      WHERE IdCierre = @IdCierre AND Valor = @Valor AND Fecha = @Fecha AND IdMedioPago = @IdMedioPago AND Factura = @Factura";

        //                using (SqlCommand cmdVerificar = new SqlCommand(consultaVerificacion, conexion))
        //                {
        //                    cmdVerificar.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
        //                    cmdVerificar.Parameters.AddWithValue("@Valor", Transferencias.Valor);
        //                    cmdVerificar.Parameters.AddWithValue("@Fecha", Transferencias.Fecha);
        //                    cmdVerificar.Parameters.AddWithValue("@IdMedioPago", 4);
        //                    cmdVerificar.Parameters.AddWithValue("@Factura", Transferencias.Factura);

        //                    int count = (int)cmdVerificar.ExecuteScalar();

        //                    if (count > 0)
        //                    {
        //                        continue; // Si ya existe, saltar esta inserción
        //                    }
        //                }
        //                using (SqlCommand cmd = new SqlCommand("InsertarTransferencia", conexion))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
        //                    cmd.Parameters.AddWithValue("@IdCaja", ppal.idCaja);
        //                    cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
        //                    cmd.Parameters.AddWithValue("@IdConcepto", 4);
        //                    cmd.Parameters.AddWithValue("@Valor", Transferencias.Valor);
        //                    cmd.Parameters.AddWithValue("@IdMedioPago", 4);
        //                    cmd.Parameters.AddWithValue("@Fecha", Transferencias.Fecha);
        //                    cmd.Parameters.AddWithValue("@Factura", Transferencias.Factura);

        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        ListaMovimientos();
        //        respuesta = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al insertar las transferencias: " + ex.Message);
        //        respuesta = false;
        //    }

        //    TransferenciasInsertados = true; // Marcar que ya se insertaron los bonos
        //    return respuesta;
        //}
    }
}
