using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
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
    public partial class FrmNotasSuperCaja : Form
    {
        public string nota;
        CONEXION cn = new CONEXION();
        FrmSuperCaja supercaja = null;
        public FrmNotasSuperCaja(FrmSuperCaja supercaja)
        {
            InitializeComponent();
            this.supercaja = supercaja;
        }
        public bool Insertar(Nota oNota)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"Insert into NovedadesSuperCaja(IdCierre,Descripcion)
                                 values(@IdCierre,@Descripcion)";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {

                        cmd.Parameters.AddWithValue("@IdCierre", oNota.IdCierre);
                        cmd.Parameters.AddWithValue("@Descripcion", oNota.Descripcion);

                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }


                }

            }
            catch (Exception ex)
            {
                return respuesta;
            }

            return respuesta;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            nota = txtNotas.Text;
            this.DialogResult = DialogResult.OK;

            Nota oNota = new Nota();
            oNota.IdCierre = supercaja.idCierre;
            oNota.Descripcion = nota;

            bool seInserto = Insertar(oNota);
        }
    }
}
