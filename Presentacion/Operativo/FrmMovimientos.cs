    using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
  
        }
        public List<TrasferenciaP> traerTransferencias()
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
                                      where fp.MedioPago='0405' and f.IdUsuario=@IdUsuario and f.fecha=@Fecha";
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
                            cmdVerificar.Parameters.AddWithValue("@IdMedioPago", 2);
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
                Console.WriteLine("Error al insertar los Datafonos: " + ex.Message);
                respuesta = false;
            }

            TransferenciasInsertados = true; // Marcar que ya se insertaron los bonos
            return respuesta;
        }

        public List<Datafonos> traerDatafonos()
        {
            List<Datafonos> Datafonos = new List<Datafonos>();
            string fechaformateada= ppal.Fecha.ToString("yyyy-MM-dd");
            try
            {


                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionRibisoft()))
                {
                    conexion.Open();
                    string consulta = $@"select fp.Valor,f.Fecha, fp.MedioPago,f.Numero 
                                       from Facturas1 f inner join formaspago fp on f.Numero=fp.Numero 
                                       where fp.MedioPago='0401' and f.IdUsuario=@IdUsuario and f.fecha=@Fecha";
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


        public List<BonoAlcaldia> traerBonos()
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
                                      where fp.MedioPago='0501' and f.IdUsuario=@IdUsuario and f.fecha=@Fecha";
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

        public void ListaMovimientos()
        {
            string consulta = $@"Select MC.IdMovimiento, CM.Concepto AS CONCEPTO, MC.Descripcion AS DESCRIPCION, MC.Valor AS VALOR, MP.Descripcion AS 'MEDIO DE PAGO', MC.fecha AS FECHA
            from MovimientoCaja MC 
            inner join ConceptoMovimiento CM ON MC.IdConcepto= CM.Id 
            inner join MediosDePago MP ON MC.IdMedioPago=MP.IdMedioPago
            where MC.IdUsuario='{ppal.idUsuario}' and MC.IdCierre= {ppal.idCierre}
			ORDER BY CM.Concepto";

            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvMovimientos.DataSource = lista;
            dgvMovimientos.Refresh();
            dgvMovimientos.Columns[0].Visible = false;


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
            DataTable listaConceptos = new SentenciaSqlServer().TraerDatos("Select Id, Concepto from ConceptoMovimiento where activo=1", Conexion.ConexionCierreCaja());
            cbConceptos.DataSource = listaConceptos;
            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";

        }

        

        private void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Al seleccionar una fila se muestra en los campos para ser editada o elimianda
            DataGridViewRow fila= dgvMovimientos.Rows[e.RowIndex];

            lbIdMovimiento.Text=fila.Cells["IdMovimiento"].Value.ToString();
            string Concepto=fila.Cells["Concepto"].Value.ToString();
            cbConceptos.SelectedIndex=cbConceptos.FindStringExact(Concepto);
            txtValor.Text = fila.Cells["Valor"].Value.ToString();
            txtDescripcion.Text=fila.Cells["Descripcion"].Value.ToString();
            string medioPago = fila.Cells["MEDIO DE PAGO"].Value.ToString();
            cbMediodepago.SelectedIndex=cbMediodepago.FindStringExact(medioPago);
            btnGuarda.Enabled=false;
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
                if(concepto== "Prestamo Caja" || concepto== "Control" || concepto== "Menuda" || concepto== "Pago" || concepto== "Cierre PTM" || concepto== "Cierre PAC" || concepto== "Saldo clientes"|| concepto == "Devoluciones no registradas" || concepto == "Faltante base" || concepto == "Consumo interno" || concepto == "Consumo propietario")
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Efectivo");

                }


            }

        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
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
                if (cbConceptos.SelectedItem != null)
                {
                    DataRowView drv = (DataRowView)cbConceptos.SelectedItem;
                    string concepto = drv["Concepto"].ToString();
                    if (concepto == "Prestamo Caja" || concepto == "Control" || concepto == "Menuda" || concepto == "Pago" || concepto == "Cierre PTM" || concepto == "Cierre PAC" || concepto == "Saldo clientes" || concepto == "Devoluciones no registradas" || concepto == "Faltante base" || concepto == "Consumo interno" || concepto == "Consumo propietario")
                    {
                        cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Efectivo");

                    }
                }
                ListaMovimientos();
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
                oMovimiento.IdMovimiento = Convert.ToInt32(lbIdMovimiento.Text);
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
                oMovimiento.IdMovimiento = Convert.ToInt16(lbIdMovimiento.Text);

                bool seElimino = new MovimientosRepository().Eliminar(oMovimiento);


                if (seElimino)
                {
                    MessageBox.Show("El movimiento se ha elimando correctamente");
                    FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();

                    ListaMovimientos();
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
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbMediodepago.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(cbMediodepago, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
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
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
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
                    ListaMovimientos();
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
        }

        private void cbMediodepago_KeyDown(object sender, KeyEventArgs e)
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
                    txtDescripcion.Focus();
                }
            }
        }

        private void cbTipodecobro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDescripcion.Focus();
            }
        }
    }
}
