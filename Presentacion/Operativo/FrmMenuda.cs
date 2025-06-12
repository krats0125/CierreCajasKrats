using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CierreDeCajas.Presentacion
{
    public partial class FrmMenuda : Form
    {
        Principal ppal = null;
        public decimal valorentregado=0;
        private Menuda oMenuda;
        public FrmMenuda(Principal ppal)
        {
            this.ppal = ppal;
            InitializeComponent();
            this.oMenuda = new Menuda();
        }

       

        //Metodo general para aumentar la cantidad de billetes por medio del boton +
        private void aumentar(TextBox tx, TextBox txtotal, decimal denominacion)
        {
            decimal cantidad = 0;
            if (!decimal.TryParse(tx.Text, out cantidad))
            {
                cantidad = 0;
            }

            cantidad++;
            tx.Text = cantidad.ToString();

            decimal total = cantidad * denominacion;
            txtotal.Text = total.ToString();
            calcularTotalEntregado();
            calcularTotalCaja();

        }
        //Metodo general para disminuir la cantidad de billetes por medio del boton -
        private void Disminuir(TextBox tx, TextBox txtotal, decimal denominacion)
        {
            decimal cantidad = 0;
            if (!decimal.TryParse(tx.Text, out cantidad) || cantidad <= 0)
            {
                cantidad = 0;
                MessageBox.Show("Ingrese un valor válido y no negativo.");
                return;
            }
            cantidad--;
            tx.Text = cantidad.ToString();

            decimal total = cantidad * denominacion;
            txtotal.Text = total.ToString();
            calcularTotalEntregado();
            calcularTotalCaja();
        }

        #region Funciones de suma
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



        private void btmas100000_Click(object sender, EventArgs e)
        {
            aumentar(txt100000, txtTotalB100, 100000);
        }



        private void btmas50000_Click(object sender, EventArgs e)
        {
            aumentar(txt50000, txtTotalB50, 50000);
        }



        private void btmas20000_Click(object sender, EventArgs e)
        {
            aumentar(txt20000, txtTotalB20, 20000);
        }



        private void btmas10000_Click(object sender, EventArgs e)
        {
            aumentar(txt10000, txtTotalB10, 20000);
        }



        private void btmas5000_Click(object sender, EventArgs e)
        {
            aumentar(txt5000, txtTotalB5, 5000);
        }



        private void btmas2000_Click(object sender, EventArgs e)
        {
            aumentar(txt2000, txtTotalB2, 2000);
        }

        private void btMasB1000_Click(object sender, EventArgs e)
        {
            aumentar(txtB1000, txtTotalB1, 1000);
        }

        private void btmas1000_Click(object sender, EventArgs e)
        {
            aumentar(txt1000, txtTotalM1, 1000);
        }



        private void btmas500_Click(object sender, EventArgs e)
        {
            aumentar(txt500, txtTotalM500, 500);
        }



        private void btmas200_Click(object sender, EventArgs e)
        {
            aumentar(txt200, txtTotalM200, 200);
        }



        private void btmas100_Click(object sender, EventArgs e)
        {
            aumentar(txt100, txtTotalM100, 100);
        }



        private void btmas50_Click(object sender, EventArgs e)
        {
            aumentar(txt50, txtTotalM50, 50);
        }
        #endregion

        #region Funciones de resta
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
        private void btmenos100000_Click(object sender, EventArgs e)
        {
            Disminuir(txt100000, txtTotalB100, 100000);
        }
        private void btMenos50000_Click(object sender, EventArgs e)
        {
            Disminuir(txt50000, txtTotalB50, 50000);
        }
        private void btmenos5000_Click(object sender, EventArgs e)
        {
            Disminuir(txt5000, txtTotalB5, 5000);
        }
        private void btmenos20000_Click(object sender, EventArgs e)
        {
            Disminuir(txt20000, txtTotalB20, 20000);
        }
        private void btMenos10000_Click(object sender, EventArgs e)
        {
            Disminuir(txt10000, txtTotalB10, 10000);
        }
        private void btmenos2000_Click(object sender, EventArgs e)
        {
            Disminuir(txt2000, txtTotalB2, 2000);
        }
        private void btMenosB1000_Click(object sender, EventArgs e)
        {
            Disminuir(txtB1000, txtTotalB1, 1000);
        }
        private void btmenos1000_Click(object sender, EventArgs e)
        {
            Disminuir(txt1000, txtTotalM1, 1000);

        }
        private void btmenos500_Click(object sender, EventArgs e)
        {
            Disminuir(txt500, txtTotalM500, 500);
        }
        private void btmenos200_Click(object sender, EventArgs e)
        {
            Disminuir(txt200, txtTotalM200, 200);
        }
        private void btmenos100_Click(object sender, EventArgs e)
        {
            Disminuir(txt100, txtTotalM100, 100);
        }
        private void btmenos50_Click(object sender, EventArgs e)
        {
            Disminuir(txt50, txtTotalM50, 50);
        }
        #endregion


     
        private void FrmMenuda_Load(object sender, EventArgs e)
        {
            try
            {
                oMenuda.IdCierre = ppal.idCierre;
                MenudaRepository menuda = new MenudaRepository();

                if (menuda.CargarDenominaciones(oMenuda))
                {
                    MostrarDenominaciones(oMenuda);
                    calcularTotalEntregado();
                    InicializarEnCaja();
                }
                else
                {

                    InicializarDineroEntregado();
                    InicializarEnCaja();
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

            decimal total = 0;
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

            txtTotal100000.Text= "0";
            txtTotal50000.Text ="0";
            txtTotal20000.Text ="0";
            txtTotal10000.Text ="0";
            txtTotal5000.Text = "0";
            txtTotal2000.Text = "0";
            txtTotalB1000.Text ="0";
            txtTotal1000.Text = "0";
            txtTotal500.Text =  "0";
            txtTotal200.Text =  "0";
            txtTotal100.Text =  "0";
            txtTotal50.Text = "0";

            calcularTotalEntregado();
        }
        
        private void InicializarEnCaja()
        {
            txt100000.Text = "0";
            txt50000.Text = "0";
            txt20000.Text = "0";
            txt10000.Text = "0";
            txt5000.Text = "0";
            txt2000.Text = "0";
            txtB1000.Text = "0";
            txt1000.Text = "0";
            txt500.Text = "0";
            txt200.Text = "0";
            txt100.Text = "0";
            txt50.Text = "0";

            txtTotalB100.Text = "0";
            txtTotalB50.Text = "0";
            txtTotalB20.Text = "0";
            txtTotalB10.Text = "0";
            txtTotalB5.Text = "0";
            txtTotalB2.Text = "0";
            txtTotalB1.Text = "0";
            txtTotalM1.Text = "0";
            txtTotalM500.Text = "0";
            txtTotalM200.Text = "0";
            txtTotalM100.Text = "0";
            txtTotalM50.Text = "0";
        }


        public void calcularTotalEntregado()
        {
            decimal total = 0;
            decimal totalB100 = TryConvertToInt(txB100.Text) * 100000;
            decimal totalB50 = TryConvertToInt(txB50.Text) * 50000;
            decimal totalB20 = TryConvertToInt(txB20.Text) * 20000;
            decimal totalB10 = TryConvertToInt(txB10.Text) * 10000;
            decimal totalB5 = TryConvertToInt(txB5.Text) * 5000;
            decimal totalB2 = TryConvertToInt(txB2.Text) * 2000;
            decimal totalB1 = TryConvertToInt(txB1.Text) * 1000;
            decimal totalM1000 = TryConvertToInt(txM1000.Text) * 1000;
            decimal totalM500 = TryConvertToInt(txM500.Text) * 500;
            decimal totalM200 = TryConvertToInt(txM200.Text) * 200;
            decimal totalM100 = TryConvertToInt(txM100.Text) * 100;
            decimal totalM50 = TryConvertToInt(txM50.Text) * 50;

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

        }


        private void calcularTotalCaja()
        {
            decimal total = 0;
            decimal valor;

            decimal totalB100 = TryConvertToInt(txt100000.Text) * 100000;
            decimal totalB50 = TryConvertToInt(txt50000.Text) * 50000;
            decimal totalB20 = TryConvertToInt(txt20000.Text) * 20000;
            decimal totalB10 = TryConvertToInt(txt10000.Text) * 10000;
            decimal totalB5 = TryConvertToInt(txt5000.Text) * 5000;
            decimal totalB2 = TryConvertToInt(txt2000.Text) * 2000;
            decimal totalB1 = TryConvertToInt(txtB1000.Text) * 1000;
            decimal totalM1000 = TryConvertToInt(txt1000.Text) * 1000;
            decimal totalM500 = TryConvertToInt(txt500.Text) * 500;
            decimal totalM200 = TryConvertToInt(txt200.Text) * 200;
            decimal totalM100 = TryConvertToInt(txt100.Text) * 100;
            decimal totalM50 = TryConvertToInt(txt50.Text) * 50;

            total += totalB100 + totalB50 + totalB20 + totalB10 + totalB5 + totalB2 + totalB1 +
            totalM1000 + totalM500 + totalM200 + totalM100 + totalM50;

            lbValorTotalCaja.Text =total.ToString("C0");
            txtTotalB100.Text = totalB100.ToString("C0");
            txtTotalB50.Text = totalB50.ToString("C0");
            txtTotalB20.Text = totalB20.ToString("C0");
            txtTotalB10.Text = totalB10.ToString("C0");
            txtTotalB5.Text = totalB5.ToString("C0");
            txtTotalB2.Text = totalB2.ToString("C0");
            txtTotalB1.Text = totalB1.ToString("C0");
            txtTotalM1.Text = totalM1000.ToString("C0");
            txtTotalM500.Text= totalM500.ToString("C0");
            txtTotalM200.Text= totalM200.ToString("C0");
            txtTotalM100.Text= totalM100.ToString("C0");
            txtTotalM50.Text= totalM50.ToString("C0");

        }

        private void lbValorTotalEntregado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                oMenuda.IdUsuario = ppal.idUsuario;
                oMenuda.IdCierre = ppal.idCierre;
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

                string valorx = lbValorTotalEntregado.Text.Replace("$", "").Replace(".","");
                valorentregado = Convert.ToDecimal(valorx);

                // Guardar en la base de datos
                bool seInserto = new MenudaRepository().insertar(oMenuda);
                
                if(seInserto)
                {
                    FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                    if (frm != null)
                    {
                   
                        bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);
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

        private decimal TryConvertToInt(string input)
        {
            decimal result = 0;
            decimal.TryParse(input, out result);
            return result;
        }
        //Metodo para actualizar la cantidad sin necesidad de tocar los botones + -
        private void ActualizarTotalManual(TextBox tx, TextBox txtotal, decimal denominacion)
        {

            decimal cantidad = 0;

            if (!decimal.TryParse(tx.Text, out cantidad))
            {
                cantidad = 0;
            }


            decimal total = cantidad * denominacion;
            txtotal.Text = total.ToString();


            calcularTotalEntregado();
            calcularTotalCaja();
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
                txM50.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB100, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txB50.Focus();
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
                txB100.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB50, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txB20.Focus();
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
                txB50.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB20, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txB10.Focus();
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
                txB20.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB10, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txB5.Focus();
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
                txB10.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB5, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txB2.Focus();
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
                txB5.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB2, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txB1.Focus();
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
                txB2.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txB1, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txM1000.Focus();
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
                txB1.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM1000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txM500.Focus();
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
                txM1000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM500, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txM200.Focus();
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
                txM500.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM200, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txM100.Focus();
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
                txM200.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM100, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txM50.Focus();
            }
        }

        private void txM50_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM50, txtTotal50, 50);
                e.SuppressKeyPress = true;
                txB100.Focus(); 
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txM50, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txM100.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txM50, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txB100.Focus();
            }
        }


        #endregion

        #region ActualizartotalcajaEnter
        private void txt100000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt100000, txtTotalB100, 100000);

                e.SuppressKeyPress = true;
                txt50000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt100000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt50.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt100000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt50000.Focus();
            }
        }

        private void txt50000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt50000, txtTotalB50, 50000);

                e.SuppressKeyPress = true;
                txt20000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt50000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt100000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt50000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt20000.Focus();
            }
        }

        private void txt20000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt20000, txtTotalB20, 20000);

                e.SuppressKeyPress = true;
                txt10000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt20000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt50000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt20000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt10000.Focus();
            }

        }

        private void txt10000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt10000, txtTotalB10, 10000);

                e.SuppressKeyPress = true;
                txt5000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt10000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt20000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt10000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt5000.Focus();
            }

        }

        private void txt5000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt5000, txtTotalB5, 5000);

                e.SuppressKeyPress = true;
                txt2000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt5000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt10000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt5000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt2000.Focus();
            }
        }

        private void txt2000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt2000, txtTotalB2, 2000);

                e.SuppressKeyPress = true;
                txtB1000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt2000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt5000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt2000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txtB1000.Focus();
            }
        }
        private void txtB1000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txtB1000, txtTotalB1, 1000);

                e.SuppressKeyPress = true;
                txt1000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txtB1000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt2000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txtB1000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt1000.Focus();
            }

        }

        private void txt1000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt1000, txtTotalM1, 1000);

                e.SuppressKeyPress = true;
                txt500.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt1000, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txtB1000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt1000, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt500.Focus();
            }
        }

        private void txt500_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt500, txtTotalM500, 500);

                e.SuppressKeyPress = true;
                txt200.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt500, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt1000.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt500, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt200.Focus();
            }
        }

        private void txt200_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt200, txtTotalM200, 200);

                e.SuppressKeyPress = true;
                txt100.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt200, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt500.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt200, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt100.Focus();
            }
        }

        private void txt100_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt100, txtTotalM100, 100);

                e.SuppressKeyPress = true;
                txt50.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt100, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt200.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt100, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt50.Focus();
            }
        }

        private void txt50_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt50, txtTotalM50, 50);

                e.SuppressKeyPress = true;
                txt100000.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Mover el foco al control anterior
                this.SelectNextControl(txt50, false, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt100.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(txt50, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
                txt100000.Focus();
            }
        }

        #endregion

       

        private void FrmMenuda_FormClosing_1(object sender, FormClosingEventArgs e)
        {
         
        }
    }
}
