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

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmMenudaAdmin : Form
    {
        FrmDetalleReporte frmDr = null;
        private DateTime fechaApertura;
        public decimal valorentregado = 0;
        Menuda oMenuda = new Menuda();
        public FrmMenudaAdmin(FrmDetalleReporte frmDr,DateTime fechaApertura)
        {
            InitializeComponent();
            this.frmDr = frmDr;
            this.fechaApertura=fechaApertura;
        }


        private void FrmMenudaAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                Menuda oMenuda = new Menuda();
                oMenuda.IdCierre = frmDr.IdCierre;
                MenudaRepository menuda = new MenudaRepository();

                if (menuda.CargarDenominaciones(oMenuda))
                {
                    MostrarDenominaciones(oMenuda);
                    calcularTotalEntregado();
                }
                else
                {

                    InicializarDineroEntregado();
                
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el formulario: {ex.Message}");
            }
        }

        private void MostrarDenominaciones(Menuda oMenuda)
        {
            txB100.Text = oMenuda.Billete_100.ToString();
            txB50.Text = oMenuda.Billete_50.ToString();
            txB20.Text = oMenuda.Billete_20.ToString();
            txB10.Text = oMenuda.Billete_10.ToString();
            txB5.Text = oMenuda.Billete_5.ToString();
            txB2.Text = oMenuda.Billete_2.ToString();
            txB1.Text = oMenuda.Billete_1.ToString();
            txM1000.Text = oMenuda.Moneda_1000.ToString();
            txM500.Text = oMenuda.Moneda_500.ToString();
            txM200.Text = oMenuda.Moneda_200.ToString();
            txM100.Text = oMenuda.Moneda_100.ToString();
            txM50.Text = oMenuda.Moneda_50.ToString();

            int total = 0;
            total += TryConvertToInt(txB100.Text) * 100000;
            total += TryConvertToInt(txB50.Text) * 50000;
            total += TryConvertToInt(txB20.Text) * 20000;
            total += TryConvertToInt(txB10.Text) * 10000;
            total += TryConvertToInt(txB5.Text) * 5000;
            total += TryConvertToInt(txB2.Text) * 2000;
            total += TryConvertToInt(txB1.Text) * 1000;
            total += TryConvertToInt(txM1000.Text) * 1000;
            total += TryConvertToInt(txM500.Text) * 500;
            total += TryConvertToInt(txM200.Text) * 200;
            total += TryConvertToInt(txM100.Text) * 100;
            total += TryConvertToInt(txM50.Text) * 50;
            mostrarTotales();
        }
        private void mostrarTotales()
        {
            txtTotal100000.Text = (TryConvertToInt(txB100.Text) * 100000).ToString("C0");
            txtTotal50000.Text = (TryConvertToInt(txB50.Text) * 50000).ToString("C0");
            txtTotal20000.Text = (TryConvertToInt(txB20.Text) * 20000).ToString("C0");
            txtTotal10000.Text = (TryConvertToInt(txB10.Text) * 10000).ToString("C0");
            txtTotal5000.Text = (TryConvertToInt(txB5.Text) * 5000).ToString("C0");
            txtTotal2000.Text = (TryConvertToInt(txB2.Text) * 2000).ToString("C0");
            txtTotalB1000.Text = (TryConvertToInt(txB1.Text) * 1000).ToString("C0");
            txtTotal1000.Text = (TryConvertToInt(txM1000.Text) * 1000).ToString("C0");
            txtTotal500.Text = (TryConvertToInt(txM500.Text) * 500).ToString("C0");
            txtTotal200.Text = (TryConvertToInt(txM200.Text) * 200).ToString("C0");
            txtTotal100.Text = (TryConvertToInt(txM100.Text) * 100).ToString("C0");
            txtTotal50.Text = (TryConvertToInt(txM50.Text) * 50).ToString("C0");

            calcularTotalEntregado();
        }
        private void InicializarDineroEntregado()
        {
            txB100.Text = "0";
            txB50.Text = "0";
            txB20.Text = "0";
            txB10.Text = "0";
            txB5.Text = "0";
            txB2.Text = "0";
            txB1.Text = "0";
            txM1000.Text = "0";
            txM500.Text = "0";
            txM200.Text = "0";
            txM100.Text = "0";
            txM50.Text = "0";

            txtTotal100000.Text = "0";
            txtTotal50000.Text = "0";
            txtTotal20000.Text = "0";
            txtTotal10000.Text = "0";
            txtTotal5000.Text = "0";
            txtTotal2000.Text = "0";
            txtTotalB1000.Text = "0";
            txtTotal1000.Text = "0";
            txtTotal500.Text = "0";
            txtTotal200.Text = "0";
            txtTotal100.Text = "0";
            txtTotal50.Text = "0";

            calcularTotalEntregado();
        }

        //Metodo general para aumentar la cantidad de billetes por medio del boton +
        private void aumentar(TextBox tx, TextBox txtotal, int denominacion)
        {
            int cantidad = 0;
            if (!int.TryParse(tx.Text, out cantidad))
            {
                cantidad = 0;
            }

            cantidad++;
            tx.Text = cantidad.ToString();

            int total = cantidad * denominacion;
            txtotal.Text = total.ToString();
            calcularTotalEntregado();

        }
        //Metodo general para disminuir la cantidad de billetes por medio del boton -
        private void Disminuir(TextBox tx, TextBox txtotal, int denominacion)
        {
            int cantidad = 0;
            if (!int.TryParse(tx.Text, out cantidad))
            {
                cantidad = 0;
            }
            cantidad--;
            tx.Text = cantidad.ToString();

            int total = cantidad * denominacion;
            txtotal.Text = total.ToString();
            calcularTotalEntregado();
        }
        private int TryConvertToInt(string input)
        {
            int result = 0;
            int.TryParse(input, out result);
            return result;
        }
        public void calcularTotalEntregado()
        {
            int total = 0;
            int totalB100 = TryConvertToInt(txB100.Text) * 100000;
            int totalB50 = TryConvertToInt(txB50.Text) * 50000;
            int totalB20 = TryConvertToInt(txB20.Text) * 20000;
            int totalB10 = TryConvertToInt(txB10.Text) * 10000;
            int totalB5 = TryConvertToInt(txB5.Text) * 5000;
            int totalB2 = TryConvertToInt(txB2.Text) * 2000;
            int totalB1 = TryConvertToInt(txB1.Text) * 1000;
            int totalM1000 = TryConvertToInt(txM1000.Text) * 1000;
            int totalM500 = TryConvertToInt(txM500.Text) * 500;
            int totalM200 = TryConvertToInt(txM200.Text) * 200;
            int totalM100 = TryConvertToInt(txM100.Text) * 100;
            int totalM50 = TryConvertToInt(txM50.Text) * 50;

            total += totalB100 + totalB50 + totalB20 + totalB10 + totalB5 + totalB2 + totalB1 +
             totalM1000 + totalM500 + totalM200 + totalM100 + totalM50;

            lbValorTotalEntregado.Text = total.ToString("C0");
            txtTotal100000.Text = totalB100.ToString("C0");
            txtTotal50000.Text = totalB50.ToString("C0");
            txtTotal20000.Text = totalB20.ToString("C0");
            txtTotal10000.Text = totalB10.ToString("C0");
            txtTotal5000.Text = totalB5.ToString("C0");
            txtTotal2000.Text = totalB2.ToString("C0");
            txtTotalB1000.Text = totalB1.ToString("C0");
            txtTotal1000.Text = totalM1000.ToString("C0");
            txtTotal500.Text = totalM500.ToString("C0");
            txtTotal200.Text = totalM200.ToString("C0");
            txtTotal100.Text = totalM100.ToString("C0");
            txtTotal50.Text = totalM50.ToString("C0");

            valorentregado = total;

            actualizardenominacion();

        }

        #region suma con boton
        private void btSumaB100_Click(object sender, EventArgs e)
        {
            aumentar(txB100, txtTotal100000, 100000);
        }

        private void btSumaB50_Click(object sender, EventArgs e)
        {
            aumentar(txB50, txtTotal50000, 50000);
        }

        private void btSumaB20_Click(object sender, EventArgs e)
        {
            aumentar(txB20, txtTotal20000, 20000);
        }

        private void btSumaB10_Click(object sender, EventArgs e)
        {
            aumentar(txB10, txtTotal10000, 10000);
        }

        private void btSumaB5_Click(object sender, EventArgs e)
        {
            aumentar(txB5, txtTotal5000, 5000);
        }

        private void btSumaB2_Click(object sender, EventArgs e)
        {
            aumentar(txB2, txtTotal2000, 2000);
        }

        private void btSumaB1_Click(object sender, EventArgs e)
        {
            aumentar(txB1, txtTotalB1000, 1000);
        }

        private void btSumaM1000_Click(object sender, EventArgs e)
        {
            aumentar(txM1000, txtTotal1000, 1000);
        }

        private void btSumaM500_Click(object sender, EventArgs e)
        {
            aumentar(txM500, txtTotal500, 500);
        }

        private void btSumaM200_Click(object sender, EventArgs e)
        {
            aumentar(txM200, txtTotal200, 200);
        }

        private void btSumaM100_Click(object sender, EventArgs e)
        {
            aumentar(txM100, txtTotal100, 100);
        }

        private void btSumaM50_Click(object sender, EventArgs e)
        {
            aumentar(txM50, txtTotal50, 50);
        }
        #endregion

        #region resta con boton
        private void btRestaB100_Click(object sender, EventArgs e)
        {
            Disminuir(txB100, txtTotal100000, 100000);
        }

        private void btRestaB50_Click(object sender, EventArgs e)
        {
            Disminuir(txB50, txtTotal50000, 50000);
        }

        private void btRestaB20_Click(object sender, EventArgs e)
        {
            Disminuir(txB20, txtTotal20000, 20000);
        }

        private void btRestaB10_Click(object sender, EventArgs e)
        {
            Disminuir(txB10, txtTotal10000, 10000);
        }

        private void btRestaB5_Click(object sender, EventArgs e)
        {
            Disminuir(txB5, txtTotal5000, 5000);
        }

        private void btRestaB2_Click(object sender, EventArgs e)
        {
            Disminuir(txB2, txtTotal2000, 2000);
        }

        private void btRestaB1_Click(object sender, EventArgs e)
        {
            Disminuir(txB1, txtTotalB1000, 1000);
        }

        private void btRestaM1000_Click(object sender, EventArgs e)
        {
            Disminuir(txM1000, txtTotal1000, 1000);
        }

        private void btRestaM500_Click(object sender, EventArgs e)
        {
            Disminuir(txM500, txtTotal500, 500);
        }

        private void btRestaM200_Click(object sender, EventArgs e)
        {
            Disminuir(txM200, txtTotal200, 200);
        }

        private void btRestaM100_Click(object sender, EventArgs e)
        {
            Disminuir(txM100, txtTotal100, 100);
        }

        private void btRestaM50_Click(object sender, EventArgs e)
        {
            Disminuir(txM50, txtTotal50, 50);
        }


        #endregion

        public void actualizardenominacion()
        {
            try
            {
                oMenuda.IdUsuario = frmDr.IdUsuario;
                oMenuda.IdCierre = frmDr.IdCierre;
                oMenuda.Billete_100 = TryConvertToInt(txB100.Text);
                oMenuda.Billete_50 = TryConvertToInt(txB50.Text);
                oMenuda.Billete_20 = TryConvertToInt(txB20.Text);
                oMenuda.Billete_10 = TryConvertToInt(txB10.Text);
                oMenuda.Billete_5 = TryConvertToInt(txB5.Text);
                oMenuda.Billete_2 = TryConvertToInt(txB2.Text);
                oMenuda.Billete_1 = TryConvertToInt(txB1.Text);
                oMenuda.Moneda_1000 = TryConvertToInt(txM1000.Text);
                oMenuda.Moneda_500 = TryConvertToInt(txM500.Text);
                oMenuda.Moneda_200 = TryConvertToInt(txM200.Text);
                oMenuda.Moneda_100 = TryConvertToInt(txM100.Text);
                oMenuda.Moneda_50 = TryConvertToInt(txM50.Text);

                string valorx = lbValorTotalEntregado.Text.Replace("$", "").Replace(".", "");
                valorentregado = Convert.ToDecimal(valorx);

                // Guardar en la base de datos
            
                bool seInserto = new MenudaRepository().insertar(oMenuda);

                if (seInserto)
                {
                    FrmDetalleReporte frm = new InstanciasRepository().InstanciaFrmDetalle();
                    if (frm != null)
                    {

                        bool actualizacionExitosa = new DetalleReporteRepository(frmDr,fechaApertura).ActualizarCierre(frmDr.IdCierre);
                        if (!actualizacionExitosa)
                        {
                            MessageBox.Show("Hubo un error actualizando el cierre de caja");
                        }
                        frm.CargarCierreVentas();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el formato de entrada: " + ex.Message);
            }

        }

        private void lbValorTotalEntregado_Click(object sender, EventArgs e)
        {
        }
        //Metodo para actualizar la cantidad sin necesidad de tocar los botones + -
        private void ActualizarTotalManual(TextBox tx, TextBox txtotal, int denominacion)
        {
            int cantidad = 0;

            if (!int.TryParse(tx.Text, out cantidad))
            {
                cantidad = 0;
            }


            int total = cantidad * denominacion;
            txtotal.Text = total.ToString();


            calcularTotalEntregado();
        
        }
        #region ActualizartotalentregadoEnter
        private void txB100_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB100, txtTotal100000, 100000);

                e.SuppressKeyPress = true;
                txB50.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txB100, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB100, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }
        private void txB50_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB50, txtTotal50000, 50000);

                e.SuppressKeyPress = true;
                txB20.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txB50, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB50, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txB20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB20, txtTotal20000, 20000);

                e.SuppressKeyPress = true;
                txB10.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txB20, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB20, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }

        }

        private void txB10_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB10, txtTotal10000, 10000);

                e.SuppressKeyPress = true;
                txB5.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txB10, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB10, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txB5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB5, txtTotal5000, 5000);

                e.SuppressKeyPress = true;
                txB2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txB5, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB5, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txB2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB2, txtTotal2000, 2000);

                e.SuppressKeyPress = true;
                txB1.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txB2, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB2, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txB1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB1, txtTotalB1000, 1000);

                e.SuppressKeyPress = true;
                txM1000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txB1, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB1, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txM1000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM1000, txtTotal1000, 1000);

                e.SuppressKeyPress = true;
                txM500.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txM1000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM1000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txM500_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM500, txtTotal500, 500);

                e.SuppressKeyPress = true;
                txM200.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txM500, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM500, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txM200_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM200, txtTotal200, 200);

                e.SuppressKeyPress = true;
                txM100.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txM200, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM200, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txM100_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM100, txtTotal100, 100);

                e.SuppressKeyPress = true;
                txM50.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txM100, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM100, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txM50_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM50, txtTotal50, 50);

                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txM50, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM50, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }
        #endregion


    }
}
