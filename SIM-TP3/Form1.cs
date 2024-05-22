using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIM_TP3.Simulacion;

namespace SIM_TP3
{
    public partial class Principal : Form
    {
        /// Constructor de la clase Principal
        public Principal()
        {
            InitializeComponent();
            OcultarDatos();
        }

        /// Método que nos permite ocultar todos los datos visuales.
        private void OcultarDatos()
        {
            gbPolitica1.Hide();
            gbPolitica2.Hide();
            txtResultado.Hide();
        }

        /// Método que nos permite mostrar todos los datos visuales.
        private void MostrarDatos()
        {
            gbPolitica1.Show();
            gbPolitica2.Show();
            txtResultado.Show();
        }

        /// Método que nos permite realizar la ejecución completa de cada una de las Politicas
        /// utilizando el evento del botón: btnGenerar_Click
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            ActivarGeneracion();
        }

        /// Método que nos permite activar la generación de cada una de las Politicas.
        public void ActivarGeneracion()
        {
            Montecarlo montecarlo = new Montecarlo();

            //Controlamos que no haya ningun campo vacio
            if (txtDias.Text.Trim() == string.Empty || txtStockInicial.Text.Trim() == string.Empty || txtPrecioVenta.Text.Trim() == string.Empty 
                || txtPrecioDev.Text.Trim() == string.Empty || txtCostoPeriodico.Text.Trim() == string.Empty || txtCostoSO.Text.Trim() == string.Empty 
                || txtCostoFaltantes.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Error: Complete los datos requeridos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                montecarlo.Dias = Convert.ToDouble(txtDias.Text.Trim());
                montecarlo.StockIncial = Convert.ToDouble(txtStockInicial.Text.Trim());
                montecarlo.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text.Trim());
                montecarlo.PrecioDev = Convert.ToDouble(txtPrecioDev.Text.Trim());
                montecarlo.CostoPeriodico = Convert.ToDouble(txtCostoPeriodico.Text.Trim());
                montecarlo.CostoSO = Convert.ToDouble(txtCostoSO.Text.Trim());
                montecarlo.CostoFaltantes = Convert.ToDouble(txtCostoFaltantes.Text.Trim());

                montecarlo.Politica1 = dgvPolitica1;
                montecarlo.Politica2 = dgvPolitica2;
                montecarlo.Resultados = txtResultado;

                montecarlo.GenerarDistribucion();
                MostrarDatos();
            }
        }


        //
        //Controladores que evitan ingresar letras en los campos
        //

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica que solo se ingresan numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Permite el ingreso de decimales
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica que solo se ingresan numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtStockInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica que solo se ingresan numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrecioDev_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica que solo se ingresan numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Permite el ingreso de decimales
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCostoSO_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica que solo se ingresan numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Permite el ingreso de decimales
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCostoPeriodico_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica que solo se ingresan numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Permite el ingreso de decimales
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCostoFaltantes_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica que solo se ingresan numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Permite el ingreso de decimales
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
