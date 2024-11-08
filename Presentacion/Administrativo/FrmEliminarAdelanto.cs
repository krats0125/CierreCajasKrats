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
            string consulta =$@"SELECT 
                          PM.IdPrestamo,
                          PM.Fecha AS FECHA, 
                          M.nombre AS NOMBRE, 
                          PM.Valor AS VALOR, 
                          PM.Concepto AS CONCEPTO, 
                          PM.Observacion AS OBSERVACIONES
                          FROM 
                              MENSAJEROS M
                          INNER JOIN 
                              PRESTAMOS_MENSAJEROS PM ON M.IdTrabajador = PM.IdTrabajador
							  WHERE PM.PAGADO=0
                          UNION ALL
                          SELECT 
                              P.IdPrestamo,
                              P.Fecha AS FECHA, 
                              T.nombre AS NOMBRE, 
                              P.Valor AS VALOR, 
                              P.Concepto AS CONCEPTO, 
                              P.Observacion AS OBSERVACIONES
                          FROM 
                              PRESTAMOS P
                          INNER JOIN 
                              TRABAJADORES T ON P.IdTrabajador = T.IdTrabajador
							  WHERE P.Pagado=0";


            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, cn.Conexionlabodegadenacho());
            dgvAdelantos.DataSource = lista;
            dgvAdelantos.Columns[0].Visible = false;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre != null)
            {
                string consulta = $@"SELECT 
                          PRESTAMOS_MENSAJEROS.IdPrestamo,
                          PRESTAMOS_MENSAJEROS.Fecha AS FECHA, 
                          MENSAJEROS.nombre AS NOMBRE, 
                          PRESTAMOS_MENSAJEROS.Valor AS VALOR, 
                          PRESTAMOS_MENSAJEROS.Concepto AS CONCEPTO, 
                          PRESTAMOS_MENSAJEROS.Observacion AS OBSERVACIONES
                          FROM 
                              MENSAJEROS 
                          INNER JOIN 
                              PRESTAMOS_MENSAJEROS ON MENSAJEROS.IdTrabajador = PRESTAMOS_MENSAJEROS.IdTrabajador
                             WHERE 
                             MENSAJEROS.nombre LIKE '%{txtNombre.Text}%'
                         
                          UNION ALL
                          SELECT 
                              PRESTAMOS.IdPrestamo,
                              PRESTAMOS.Fecha AS FECHA, 
                              TRABAJADORES.nombre AS NOMBRE, 
                              PRESTAMOS.Valor AS VALOR, 
                              PRESTAMOS.Concepto AS CONCEPTO, 
                              PRESTAMOS.Observacion AS OBSERVACIONES
                          FROM 
                              PRESTAMOS 
                          INNER JOIN 
                              TRABAJADORES ON PRESTAMOS.IdTrabajador = TRABAJADORES.IdTrabajador
                              WHERE 
                             TRABAJADORES.nombre LIKE '%{txtNombre.Text}%';";
            
                DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, cn.Conexionlabodegadenacho());
                dgvAdelantos.DataSource = lista;
            }
        }

        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Prestamo oPrestamo = new Prestamo();
            if (dgvAdelantos.SelectedRows.Count > 0)
            {
                int idPrestamo = Convert.ToInt32(dgvAdelantos.SelectedRows[0].Cells["IdPrestamo"].Value);

 
                DialogResult dialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    oPrestamo.IdPrestamo = idPrestamo;
                    bool seElimino = new PrestamosRepository().Eliminar(oPrestamo);
                    if (seElimino)
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
