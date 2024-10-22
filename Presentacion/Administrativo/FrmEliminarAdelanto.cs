using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
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

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmEliminarAdelanto : Form
    {
        CONEXION cn = new CONEXION();
        public FrmEliminarAdelanto()
        {
            InitializeComponent();
            listarprestamos();  
        }

        public void listarprestamos()
        {
            string consulta = $@"SELECT 
                              PRESTAMOS_MENSAJEROS2.Fecha AS Fecha, 
                              MENSAJEROS.nombre AS Nombre, 
                              PRESTAMOS_MENSAJEROS2.Valor AS Valor, 
                              PRESTAMOS_MENSAJEROS2.Concepto AS Concepto, 
                              PRESTAMOS_MENSAJEROS2.Observacion AS Observacion
                              FROM 
                                  MENSAJEROS 
                              INNER JOIN 
                                  PRESTAMOS_MENSAJEROS2 ON MENSAJEROS.IdTrabajador = PRESTAMOS_MENSAJEROS2.IdTrabajador
                              UNION ALL
                              SELECT 
                                  PRESTAMOS2.Fecha AS Fecha, 
                                  TRABAJADORES.nombre AS Nombre, 
                                  PRESTAMOS2.Valor AS Valor, 
                                  PRESTAMOS2.Concepto AS Concepto, 
                                  PRESTAMOS2.Observacion AS Observacion
                              FROM 
                                  PRESTAMOS2 
                              INNER JOIN 
                             TRABAJADORES ON PRESTAMOS2.IdTrabajador = TRABAJADORES.IdTrabajador;";


            DataTable lista = new SentenciaSqlOLEDB().TraerDatos(consulta, cn.ConexionDbInterna());
            dgvAdelantos.DataSource = lista;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre != null)
            {
                string sql = $@"SELECT 
                             PRESTAMOS_MENSAJEROS2.Fecha AS Fecha, 
                             MENSAJEROS.nombre AS Nombre, 
                             PRESTAMOS_MENSAJEROS2.Valor AS Valor, 
                             PRESTAMOS_MENSAJEROS2.Concepto AS Concepto, 
                             PRESTAMOS_MENSAJEROS2.Observacion AS Observacion
                             FROM 
                             MENSAJEROS 
                             INNER JOIN 
                             PRESTAMOS_MENSAJEROS2 ON MENSAJEROS.IdTrabajador = PRESTAMOS_MENSAJEROS2.IdTrabajador
                             WHERE 
                             MENSAJEROS.nombre LIKE '%{txtNombre.Text}%'
                         
                             UNION ALL
                         
                             SELECT 
                             PRESTAMOS2.Fecha AS Fecha, 
                             TRABAJADORES.nombre AS Nombre, 
                             PRESTAMOS2.Valor AS Valor, 
                             PRESTAMOS2.Concepto AS Concepto, 
                             PRESTAMOS2.Observacion AS Observacion
                             FROM 
                             PRESTAMOS2 
                             INNER JOIN 
                             TRABAJADORES ON PRESTAMOS2.IdTrabajador = TRABAJADORES.IdTrabajador
                             WHERE 
                             TRABAJADORES.nombre LIKE '%{txtNombre.Text}%';";

                DataTable lista = new SentenciaSqlServer().TraerDatos(sql, cn.ConexionCierreCaja());
                dgvAdelantos.DataSource = lista;
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            string sql = $@"
                         SELECT 
                             PRESTAMOS_MENSAJEROS2.Fecha AS Fecha, 
                             MENSAJEROS.nombre AS Nombre, 
                             PRESTAMOS_MENSAJEROS2.Valor AS Valor, 
                             PRESTAMOS_MENSAJEROS2.Concepto AS Concepto, 
                             PRESTAMOS_MENSAJEROS2.Observacion AS Observacion
                         FROM 
                             MENSAJEROS 
                         INNER JOIN 
                             PRESTAMOS_MENSAJEROS2 ON MENSAJEROS.IdTrabajador = PRESTAMOS_MENSAJEROS2.IdTrabajador
                         WHERE 
                             PRESTAMOS_MENSAJEROS2.Fecha = @fecha
                         
                         UNION ALL
                         
                         SELECT 
                             PRESTAMOS2.Fecha AS Fecha, 
                             TRABAJADORES.nombre AS Nombre, 
                             PRESTAMOS2.Valor AS Valor, 
                             PRESTAMOS2.Concepto AS Concepto, 
                             PRESTAMOS2.Observacion AS Observacion
                         FROM 
                             PRESTAMOS2 
                         INNER JOIN 
                             TRABAJADORES ON PRESTAMOS2.IdTrabajador = TRABAJADORES.IdTrabajador
                         WHERE 
                             PRESTAMOS2.Fecha = @fecha;";

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);

                        conexion.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable lista = new DataTable();
                        da.Fill(lista);

                        dgvAdelantos.DataSource = lista;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAdelantos.SelectedRows.Count > 0)
            {
                int idPrestamo = Convert.ToInt32(dgvAdelantos.SelectedRows[0].Cells["iDPrestamo"].Value);

 
                DialogResult dialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    bool eliminado = new PrestamosRepository().Eliminar();
                    if (eliminado)
                    {
                        MessageBox.Show("Registro eliminado correctamente.");
                        listarprestamos();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el registro.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona un registro para eliminar.");
            }
        }
    }
}
