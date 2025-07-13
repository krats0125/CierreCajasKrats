using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion;
using CierreDeCajas.Presentacion.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
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
            listarCajeros();

        }

        private void listarcaja()
        {
            try
            {
                string sql = "select IdCaja,NombreCaja from Caja where Activo=1";
                DataTable listaCaja = new SentenciaSqlServer().TraerDatos(sql, Cn.ConexionCierreCaja());

                cbCaja.DataSource = listaCaja;
                cbCaja.DisplayMember = "NombreCaja";
                cbCaja.ValueMember = "IdCaja";
            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo conectar con la la red, revisa la conexion de la carpeta");
            }
           
        }
        
        private void listarCajeros2()
        {
            string sql = "select IdUsuario, Descripcion,Grupo from Usuarios where Estado=1 and grupo!=5 order by IdUsuario";
            DataTable listaCajero = new SentenciaSqlServer().TraerDatos(sql, Cn.ConexionRibisoft());
            cbUsuario.DataSource = listaCajero;
            cbUsuario.DisplayMember = "IdUsuario";
            cbUsuario.ValueMember = "IdUsuario";
        }

        private void listarCajeros()
        {
            try
            {
                string sql = "select id_usuario as IdUsuario, nombre,2 as grupo from Usuarios order by nombre asc";
                DataTable listaCajero = new SentenciasSqlODBC().TraerDatos(sql, Cn.ConexionVisualFoxPro());
                cbUsuario.DataSource = listaCajero;
                cbUsuario.DisplayMember = "nombre";
                cbUsuario.ValueMember = "idusuario";
            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo conectar con la la red, revisa la conexion de la carpeta");
            }
           
        }
        //private void listarCajeros()
        //{
        //    string sql = "select IdUsuario, Descripcion,Grupo from Usuario";
        //    DataTable listaCajero = new SentenciaSqlServer().TraerDatos(sql, Cn.ConexionCierreCaja());
        //    cbUsuario.DataSource = listaCajero;
        //    cbUsuario.DisplayMember = "IdUsuario";
        //    cbUsuario.ValueMember = "IdUsuario";
        //}

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (cbUsuario.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cbUsuario.SelectedItem;
                int grupo = Convert.ToInt32(drv["Grupo"].ToString());

        
                if (grupo == 1)
                {
        
                    idUsuario = cbUsuario.SelectedValue.ToString();
                    NombreUsuario = cbUsuario.Text.ToString();
                    string contraseña = txtContraseña.Text;
                    string contraseñaCorrecta = obtenerContraseña();
                    if (contraseña.Equals(contraseñaCorrecta))
                    {
                        if (rbSuperCaja.Checked)
                        {
                            idUsuario = cbUsuario.SelectedValue.ToString();
                            NombreUsuario = cbUsuario.Text.ToString();
                            idCierre = InsertarEnCierreSuperCaja();
                            this.Visible = false;
                            FrmSuperCaja frmsupercaja = new FrmSuperCaja(this);
                            frmsupercaja.Show();
                         
                        }
                        else if(rbAdministrativo.Checked)
                        {
                           
                            this.Visible = false;
                            FrmAdministrativo frmadmin = new FrmAdministrativo(this);
                            frmadmin.Show();

                        }
                        else if(rbCierreCaja.Checked)
                        {
                            lbCaja.Visible = true;
                            cbCaja.Visible = true;
                            idCaja = Convert.ToInt32(cbCaja.SelectedValue.ToString());
                            idUsuario = cbUsuario.SelectedValue.ToString();
                            Caja = cbCaja.Text.ToString();
                            NombreUsuario = cbUsuario.Text.ToString();
                            idCierre = InsertarEnCierre();
                            DialogResult result = MessageBox.Show("¿Desea abrir un cierre de caja?","Confirmación",MessageBoxButtons.YesNo);
                            if(result==DialogResult.Yes)
                            {
                                Principal frm = new Principal(this);
                                frm.Show();
                                this.Visible = false;
                            }
                           
                        }
                    }
                else
                {

                    MessageBox.Show("Contraseña incorrecta.");
                    txtContraseña.Clear();

                }

            }
                else if (grupo == 2)
                {
                    if(rbCierreCaja.Checked)
                    {
                        idCaja = Convert.ToInt32(cbCaja.SelectedValue.ToString());
                        idUsuario = cbUsuario.SelectedValue.ToString().Trim();
                        Caja = cbCaja.Text.ToString();
                        NombreUsuario = cbUsuario.Text.ToString();
                        idCierre = InsertarEnCierre();
                        Principal frm = new Principal(this);
                        frm.Show();
                        this.Visible = false;
                    }
                    else if(rbSuperCaja.Checked)
                    {
                        idUsuario = cbUsuario.SelectedValue.ToString();
                        NombreUsuario = cbUsuario.Text.ToString();
                        idCierre = InsertarEnCierreSuperCaja();
                        this.Visible = false;
                        FrmSuperCaja frmsupercaja = new FrmSuperCaja(this);
                        frmsupercaja.Show();
                    }
                    
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

        private int InsertarEnCierreSuperCaja()
        {

            try
            {
                using (SqlConnection conexion = new SqlConnection(Cn.ConexionCierreCaja()))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("ValidarCierreSuperCaja", conexion))
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
                int grupo = Convert.ToInt32(drv["Grupo"].ToString());

                if (grupo == 1)
                {
                    lbCaja.Visible = false;
                    cbCaja.Visible = false;
                    lbContraseña.Visible = true;
                    txtContraseña.Visible = true;
                    rbSuperCaja.Visible = true;
                    rbAdministrativo.Visible = true;
                    rbCierreCaja.Checked = false;
                    btnEntrar.Location = new Point(99, 380);
                }
                else
                {
                    lbCaja.Location = new Point(115, 233);
                    cbCaja.Location = new Point(107, 273);
                    lbCaja.Visible = true;
                    cbCaja.Visible = true;
                    lbContraseña.Visible = false;
                    txtContraseña.Visible = false;
                    rbSuperCaja.Visible = true;
                    rbAdministrativo.Visible = false;
                    rbCierreCaja.Focus();
                    btnEntrar.Location = new Point(99, 350);
                }
                Console.WriteLine($"btnEntrar Location: {btnEntrar.Location}"); // Depuración
            }

        }
        private string obtenerContraseña2()
        {
            using (SqlConnection conexion = new SqlConnection(Cn.ConexionRibisoft()))
            {
                conexion.Open();
                string sql = "select contraseña from Usuarios where IdUsuario=@IdUsuario";
                using (SqlCommand cmd = new SqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null)
                    {
                        return resultado.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        private string obtenerContraseña()
        {
            using (OdbcConnection conexion = new OdbcConnection(Cn.ConexionVisualFoxPro()))
            {
                conexion.Open();
                string sql = "select contraseña from claves where id_usuario = @IdUsuario";
                using (OdbcCommand cmd = new OdbcCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null)
                    {
                        return resultado.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }




        private void cbCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
               
                        idCaja = Convert.ToInt32(cbCaja.SelectedValue.ToString());
                        idUsuario = cbUsuario.SelectedValue.ToString();
                        Caja = cbCaja.Text.ToString();
                        NombreUsuario = cbUsuario.Text.ToString();
                        idCierre = InsertarEnCierre();
                        Principal frm = new Principal(this);
                        frm.Show();
                        this.Visible = false;  
                
            }
        }

    

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               e.SuppressKeyPress = true;
                rbSuperCaja.Focus();
            }

        }

        private void rbSuperCaja_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (cbUsuario.SelectedItem != null)
            //    {
            //        DataRowView drv = (DataRowView)cbUsuario.SelectedItem;
            //        int grupo = Convert.ToInt32(drv["Grupo"].ToString());


            //        if (grupo == 1)
            //        {

            //            idUsuario = cbUsuario.SelectedValue.ToString();
            //            NombreUsuario = cbUsuario.Text.ToString();
            //            string contraseña = txtContraseña.Text;
            //            string contraseñaCorrecta = obtenerContraseña();
            //            if (contraseña.Equals(contraseñaCorrecta))
            //            {
            //                if (rbSuperCaja.Checked)
            //                {

            //                    idUsuario = cbUsuario.SelectedValue.ToString();
            //                    NombreUsuario = cbUsuario.Text.ToString();
            //                    idCierre = InsertarEnCierreSuperCaja();
            //                    this.Visible = false;
            //                    FrmSuperCaja frmsupercaja = new FrmSuperCaja(this);
            //                    frmsupercaja.Show();
            //                }
            //                else
            //                {
            //                    this.Visible = false;
            //                    FrmAdministrativo frmadmin = new FrmAdministrativo(this);
            //                    frmadmin.Show();
            //                }

            //            }
            //            else
            //            {

            //                MessageBox.Show("Contraseña incorrecta.");
            //                txtContraseña.Clear();

            //            }

            //        }
            //        else if (grupo == 2)
            //        {
            //            idCaja = Convert.ToInt32(cbCaja.SelectedValue.ToString());
            //            idUsuario = cbUsuario.SelectedValue.ToString();
            //            Caja = cbCaja.Text.ToString();
            //            NombreUsuario = cbUsuario.Text.ToString();
            //            idCierre = InsertarEnCierre();
            //            Principal frm = new Principal(this);
            //            frm.Show();
            //            this.Visible = false;
            //        }

            //    }

            //}
            //else if (e.KeyCode == Keys.Right)
            //{
            //    // Mover el foco al control siguiente
            //    this.SelectNextControl(rbAdministrativo, true, true, true, true);
            //    e.SuppressKeyPress = true; // Evitar la acción predeterminada
            //}

        }

        private void rbCierresCajeros_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (cbUsuario.SelectedItem != null)
            //    {
            //        DataRowView drv = (DataRowView)cbUsuario.SelectedItem;
            //        int grupo = Convert.ToInt32(drv["Grupo"].ToString());


            //        if (grupo == 1)
            //        {

            //            idUsuario = cbUsuario.SelectedValue.ToString();
            //            NombreUsuario = cbUsuario.Text.ToString();
            //            string contraseña = txtContraseña.Text;
            //            string contraseñaCorrecta = obtenerContraseña();
            //            if (contraseña.Equals(contraseñaCorrecta))
            //            {
            //                if (rbSuperCaja.Checked)
            //                {

            //                    idUsuario = cbUsuario.SelectedValue.ToString();
            //                    NombreUsuario = cbUsuario.Text.ToString();
            //                    idCierre = InsertarEnCierreSuperCaja();
            //                    this.Visible = false;
            //                    FrmSuperCaja frmsupercaja = new FrmSuperCaja(this);
            //                    frmsupercaja.Show();
            //                }
            //                else
            //                {
            //                    this.Visible = false;
            //                    FrmAdministrativo frmadmin = new FrmAdministrativo(this);
            //                    frmadmin.Show();
            //                }

            //            }
            //            else
            //            {

            //                MessageBox.Show("Contraseña incorrecta.");
            //                txtContraseña.Clear();

            //            }

            //        }
            //        else if (grupo == 2)
            //        {
            //            idCaja = Convert.ToInt32(cbCaja.SelectedValue.ToString());
            //            idUsuario = cbUsuario.SelectedValue.ToString();
            //            Caja = cbCaja.Text.ToString();
            //            NombreUsuario = cbUsuario.Text.ToString();
            //            idCierre = InsertarEnCierre();
            //            Principal frm = new Principal(this);
            //            frm.Show();
            //            this.Visible = false;
            //        }

            //    }

            //}
        }

        private void rbCierreCaja_CheckedChanged(object sender, EventArgs e)
        {
            if(rbCierreCaja.Checked)
            {
                if (cbUsuario.SelectedItem != null)
                {
                    DataRowView drv = (DataRowView)cbUsuario.SelectedItem;
                    int grupo = Convert.ToInt32(drv["Grupo"].ToString());


                    if (grupo == 1)
                    {
                        lbCaja.Location = new Point(23, 369);
                        cbCaja.Location = new Point(126, 369);
                        lbCaja.Visible = true;
                        cbCaja.Visible = true;
                        btnEntrar.Location = new Point(101, 420);
                    }
                    if(grupo==2)
                    {
                        lbCaja.Visible = true;
                        cbCaja.Visible = true;
                        btnEntrar.Location = new Point(99, 350);
                    }
                            
                }
            }
            else
            {
                lbCaja.Visible = false;
                cbCaja.Visible = false;
                btnEntrar.Location = new Point(106,383);
            }
          
        }

        private void rbSuperCaja_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUsuario.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cbUsuario.SelectedItem;
                int grupo = Convert.ToInt32(drv["Grupo"].ToString());


                if (grupo == 2)
                {
                    btnEntrar.Location = new Point(99, 350);
                }
            }
        }
    }
}

