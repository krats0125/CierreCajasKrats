using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
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
        }

        public void listaroles()
        {
            string sql = "SELECT idRol,Rol from Rol";
            DataTable lista = new SentenciaSqlServer().TraerDatos(sql,cn.ConexionCierreCaja());
            cbRol.DataSource = lista;
            cbRol.DisplayMember = "Rol";
            cbRol.ValueMember = "IdRol";
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

                    limpiarcampos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario. Por favor, inténtelo de nuevo.");
                }
            }
        }

        private void rbMensajero_CheckedChanged(object sender, EventArgs e)
        {
            string consulta = @"SELECT MENSAJEROS.IdTrabajador AS DOCUMENTO, MENSAJEROS.nombre AS NOMBRE, MENSAJEROS.estado AS ESTADO
            FROM MENSAJEROS
            WHERE (((MENSAJEROS.estado)=True));";
            DataTable lista = new SentenciaSqlOLEDB().TraerDatos(consulta, cn.ConexionDbInterna());
            dgvTrabajadores.DataSource = lista;

        }

        private void rbTrabajadores_CheckedChanged(object sender, EventArgs e)
        {
            string consulta = @"SELECT TRABAJADORES.IdTrabajador AS DOCUMENTO, TRABAJADORES.nombre AS NOMBRE, TRABAJADORES.estado AS ESTADO
            FROM TRABAJADORES
            WHERE (((TRABAJADORES.estado)=True));";
            DataTable lista = new SentenciaSqlOLEDB().TraerDatos(consulta, cn.ConexionDbInterna());
            dgvTrabajadores.DataSource = lista;

        }

        private void lbxTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
          
                // Obtener la fila seleccionada del DataTable
                DataGridViewRow filaSeleccionada = dgvTrabajadores.SelectedRows[0];

                // Asignar los valores a los TextBoxes u otros controles
                txtNombre.Text = filaSeleccionada.Cells["nombre"].ToString();
                txtUsuario.Text = filaSeleccionada.Cells["IdTrabajador"].ToString();
                bool estado = Convert.ToBoolean(filaSeleccionada.Cells["estado"]);
                if (estado)
                {
                    rbActivo.Checked = true;
                }
                else
                {
                    rbInactivo.Checked = true;
                }
        }
    }
}
