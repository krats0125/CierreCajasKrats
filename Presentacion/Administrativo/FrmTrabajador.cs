using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmTrabajador : Form
    {
        CONEXION cn=new CONEXION();
      
       
        public FrmTrabajador()
        {
            InitializeComponent();
        }

        private void FrmCreacionCajero_Load(object sender, EventArgs e)
        {
            listaroles();
            listatrabajadores();
        }

        public void listaroles()
        {
            string sql = "SELECT idRol,Rol from Rol";
            DataTable lista = new SentenciaSqlServer().TraerDatos(sql,cn.ConexionCierreCaja());
            cbRol.DataSource = lista;
            cbRol.DisplayMember = "Rol";
            cbRol.ValueMember = "IdRol";
        }

        public void listatrabajadores()
        {
            string sql = "select U.IdUsuario, U.Nombre, R.Rol,U.Activo from Usuario U inner join Rol R on U.IdRol=R.IdRol";
            DataTable lista = new SentenciaSqlServer().TraerDatos(sql, cn.ConexionCierreCaja());
            dgvTrabajador.DataSource = lista;

        }

     

     

        private void dgvTrabajador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                    txtUsuario.Enabled = false;
            
                    DataGridViewRow fila = dgvTrabajador.Rows[e.RowIndex];

             
                    txtUsuario.Text = fila.Cells["IdUsuario"].Value.ToString();
                    txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                    string rol = fila.Cells["Rol"].Value.ToString(); 
                    cbRol.SelectedIndex = cbRol.FindStringExact(rol); 
                    btnGuardar.Enabled = false;
        }

        private void limpiarcampos()
        {
            txtUsuario.Text = "";
            txtNombre.Text = "";
            cbRol.SelectedIndex = -1;
            
        }

      
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Trabajador oTrabajador = new Trabajador();

            oTrabajador.IdUsuario = txtUsuario.Text;
            oTrabajador.Nombre = txtNombre.Text;
            oTrabajador.IdRol = Convert.ToInt32(cbRol.SelectedValue.ToString());
            oTrabajador.Activo = true;

            bool seInserto = new TrabajadoresRepository().Insertar(oTrabajador);

            if (seInserto)
            {
                MessageBox.Show("Se ha ingresado el trabajador correctamente");
                listatrabajadores();
                limpiarcampos();
            }
            else
            {
                MessageBox.Show("No se ha podido ingresar el trabajador");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Trabajador oTrabajador = new Trabajador();

            oTrabajador.IdUsuario = txtUsuario.Text;
            oTrabajador.Nombre = txtNombre.Text;
            oTrabajador.IdRol = Convert.ToInt32(cbRol.SelectedValue.ToString());
            oTrabajador.Activo = true;

            bool seActualizo = new TrabajadoresRepository().Actualizar(oTrabajador);

            if (seActualizo)
            {
                MessageBox.Show("Se actualizo el trabajador correctamente");
                listatrabajadores();
                limpiarcampos();
            }
            else
            {
                MessageBox.Show("Hubo un error al actualizar el trabajador");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Trabajador oTrabajador = new Trabajador();


            if (MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Confirmar eliminación",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                oTrabajador.IdUsuario = txtUsuario.Text;
                bool seElimino = new TrabajadoresRepository().Eliminar(oTrabajador);

                if (seElimino)
                {
                    MessageBox.Show("Usuario eliminado correctamente");
                    listatrabajadores();
                    limpiarcampos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario. Por favor, inténtelo de nuevo.");
                }
            }
        }
    }
}
