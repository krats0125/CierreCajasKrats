using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Presentacion;
using CierreDeCajas.Presentacion.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas
{
    public partial class FrmLogin : Form
    {
        CONEXION Cn = new CONEXION();


        public int idCaja = 0;
        public string Caja = null;
        public string idUsuario = null;
        public string NombreUsuario = null;
        public int idCierre = 0;



        public FrmLogin()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            listarcaja();
            listarcajero();

        }

        private void listarcaja()
        {
            string sql = "select IdCaja,NombreCaja from Caja where Activo=1";
            DataTable listaCaja = new SentenciaSqlServer().TraerDatos(sql, Cn.ConexionCierreCaja());

            cbCaja.DataSource = listaCaja;
            cbCaja.DisplayMember = "NombreCaja";
            cbCaja.ValueMember = "IdCaja";
        }

        private void listarcajero()
        {
            string sql = "select IdUsuario,Nombre from Usuario where activo=1";
            DataTable listaCajero = new SentenciaSqlServer().TraerDatos(sql, Cn.ConexionCierreCaja());
            cbUsuario.DataSource = listaCajero;
            cbUsuario.DisplayMember = "Nombre";
            cbUsuario.ValueMember = "IdUsuario";
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (cbUsuario.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cbUsuario.SelectedItem;
                string usuario = drv["IdUsuario"].ToString();

                if (usuario == "ADMIN")
                {
                    idUsuario = cbUsuario.SelectedValue.ToString();
                    NombreUsuario = cbUsuario.Text.ToString();
                    string contraseña = txtContraseña.Text;
                    string contraseñaCorreta = "abc123";
                    if (contraseña.Equals(contraseñaCorreta))
                    {
                        this.Visible = false;
                        FrmAdministrativo frmadmin = new FrmAdministrativo(this);
                        frmadmin.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta.");
                    }

                }
                else
                {
                    idCaja = Convert.ToInt16(cbCaja.SelectedValue.ToString());
                    idUsuario = cbUsuario.SelectedValue.ToString();
                    Caja = cbCaja.Text.ToString();
                    NombreUsuario = cbUsuario.Text.ToString();
                    idCierre = InsertarEnCierre();
                    Principal frm = new Principal(this);
                    frm.Show();
                    this.Visible = false;
                }

            }
         }

            private int InsertarEnCierre()
            {

                try
                {
                    using (SqlConnection conexion = new SqlConnection(Cn.ConexionCierreCaja()))
                    {
                        conexion.Open();

                        using (SqlCommand cmd = new SqlCommand("ValidarCierrePorUsuario", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@IdUsuario", idUsuario));
                            // Ejecutar el procedimiento almacenado y obtener el resultado
                            object result = cmd.ExecuteScalar();

                            // Verificar si el resultado no es nulo y convertirlo a int
                            if (result != null && int.TryParse(result.ToString(), out int nuevoIdCierre))
                            {
                                idCierre = nuevoIdCierre;
                            }


                        }
                        conexion.Close();
                    }
                    return idCierre;
                }
                catch (Exception)
                {


                    return 0;

                }


            }

            private void cbUsuario_SelectedIndexChanged(object sender, EventArgs e)
            {

                if (cbUsuario.SelectedItem != null)
                {
                    DataRowView drv = (DataRowView)cbUsuario.SelectedItem;
                    string usuario = drv["IdUsuario"].ToString();

                    if (usuario == "ADMIN")
                    {
                        lbCaja.Visible = false;
                        cbCaja.Visible = false;
                        lbContraseña.Visible = true;
                        txtContraseña.Visible = true;

                    }
                    else
                    {
                        lbCaja.Visible = true;
                        cbCaja.Visible = true;
                        lbContraseña.Visible = false;
                        txtContraseña.Visible = false;
                    }
                }
            }

        }
    }

