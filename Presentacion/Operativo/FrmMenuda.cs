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
        Menuda oMenuda = new Menuda();
        private decimal valorventas = 0;


        public FrmMenuda(Principal ppal)
        {
            this.ppal = ppal;
            InitializeComponent();
          
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
            calcularTotalCaja();

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
                Menuda oMenuda = new Menuda();
                oMenuda.IdCierre = ppal.idCierre;
                MenudaRepository menuda = new MenudaRepository();

                if (menuda.CargarDenominaciones(oMenuda))
                {
                    MostrarDenominaciones(oMenuda);
                    calcularTotalEntregado();
  
                }
                else
                {

                    InicializarTextBoxesConCero();
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
        


        }
        private void InicializarTextBoxesConCero()
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

            calcularTotalEntregado();
        }



        public void calcularTotalEntregado()
        {
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
            valorentregado = total;

            lbValorTotalEntregado.Text = total.ToString("C0");
            //int total = 0;


            //int valor;
            //if (int.TryParse(txtTotal100000.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal50000.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal20000.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal10000.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal5000.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal2000.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotalB1000.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal1000.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal500.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal200.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal100.Text, out valor)) total += valor;
            //if (int.TryParse(txtTotal50.Text, out valor)) total += valor;
            //valorentregado = total;


            //lbValorTotalEntregado.Text = $"${total}";


        }
        

        private void calcularTotalCaja()
        {
            int total = 0;
            int valor;

            if(int.TryParse(txtTotalB100.Text,out valor)) total += valor;
            if(int.TryParse(txtTotalB50.Text,out valor)) total += valor;
            if(int.TryParse(txtTotalB20.Text,out valor)) total+= valor;
            if (int.TryParse(txtTotalB10.Text, out valor)) total += valor;
            if (int.TryParse(txtTotalB5.Text, out valor)) total += valor;
            if (int.TryParse(txtTotalB2.Text, out valor)) total += valor;
            if (int.TryParse(txtTotalB1.Text, out valor)) total += valor;
            if (int.TryParse(txtTotalM1.Text, out valor)) total += valor;
            if (int.TryParse(txtTotalM500.Text, out valor)) total += valor;
            if (int.TryParse(txtTotalM200.Text, out valor)) total += valor;
            if (int.TryParse(txtTotalM100.Text, out valor)) total += valor;
            if (int.TryParse(txtTotalM50.Text, out valor)) total += valor;

            lbValorTotalCaja.Text = $"${total}";
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

                // Guardar en la base de datos
                bool seInserto = new MenudaRepository().insertar(oMenuda);
                


                // Verificar si la actualización del cierre de caja es exitosa
                bool actualizacionExitosa = new CierreCajaRepository().ActualizarCierre(ppal.idCierre,valorventas);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }

                // Cargar los datos en el formulario de cierre de ventas
                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                frm.CargarCierreVentas();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el formato de entrada: " + ex.Message);
            }

        }

        private int TryConvertToInt(string input)
        {
            int result = 0;
            int.TryParse(input, out result);
            return result;
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
            calcularTotalCaja();
        }

        #region ActualizartotalentregadoEnter

        private void txB100_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB100, txtTotal100000, 100000);

                e.SuppressKeyPress = true;
            }
        }

        private void txB50_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB50, txtTotal50000, 50000);

                e.SuppressKeyPress = true;
            }
           
        }

        private void txB20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB20, txtTotal20000, 20000);

                e.SuppressKeyPress = true;
            }

        }

        private void txB10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB10, txtTotal10000, 10000);

                e.SuppressKeyPress = true;
            }
        }

        private void txB5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB5, txtTotal5000, 5000);

                e.SuppressKeyPress = true;
            }
            
        }

        private void txB2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB2, txtTotal2000, 2000);

                e.SuppressKeyPress = true;
            }

        }

        private void txB1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txB1, txtTotalB1000, 1000);

                e.SuppressKeyPress = true;
            }
        }

        private void txM1000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM1000, txtTotal1000, 1000);

                e.SuppressKeyPress = true;
            }
        }

        private void txM500_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM500, txtTotal500, 500);

                e.SuppressKeyPress = true;
            }
        }

        private void txM200_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM200, txtTotal200, 200);

                e.SuppressKeyPress = true;
            }
        }

        private void txM100_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM100, txtTotal100, 100);

                e.SuppressKeyPress = true;
            }
        }

        private void txM50_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txM50, txtTotal50, 50);

                e.SuppressKeyPress = true;
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
            }
        }

        private void txt50000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt50000, txtTotalB50, 50000);

                e.SuppressKeyPress = true;
            }
        }

        private void txt20000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt20000, txtTotalB20, 20000);

                e.SuppressKeyPress = true;
            }

        }

        private void txt10000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt10000, txtTotalB10, 10000);

                e.SuppressKeyPress = true;
            }

        }

        private void txt5000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt5000, txtTotalB5, 5000);

                e.SuppressKeyPress = true;
            }
        }

        private void txt2000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt2000, txtTotalB2, 2000);

                e.SuppressKeyPress = true;
            }
        }
        private void txtB1000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txtB1000, txtTotalB1, 1000);

                e.SuppressKeyPress = true;
            }

        }

        private void txt1000_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt1000, txtTotalM1, 1000);

                e.SuppressKeyPress = true;
            }
        }

        private void txt500_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt500, txtTotalM500, 500);

                e.SuppressKeyPress = true;
            }
        }

        private void txt200_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt200, txtTotalM200, 200);

                e.SuppressKeyPress = true;
            }
        }

        private void txt100_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt100, txtTotalM100, 100);

                e.SuppressKeyPress = true;
            }
        }

        private void txt50_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActualizarTotalManual(txt50, txtTotalM50, 50);

                e.SuppressKeyPress = true;
            }
        }

        #endregion

       

        private void FrmMenuda_FormClosing_1(object sender, FormClosingEventArgs e)
        {
         
        }
    }
}
