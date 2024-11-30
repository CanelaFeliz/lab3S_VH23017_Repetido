using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//Laboratorio 3 - 03/11/2024
//Autor: Cristian Mauricio Vasquez Hernandez - VH23017

namespace lab3S_VH23017
{
    public partial class Form1 : Form
    {
        //Datos del cliente vehiculos
        string[] nombre;
        string[] sexo;
        string[] marcas;
        int[] anio;
        double[] precio;
        //cantidad de clientes
        int cantClientes;
        int actual = 0;
        //cantidad de vehiculos
        int nissan = 0, toyota = 0, kia = 0;
        double nissanMonto = 0, toyotamonto = 0, kiaMonto = 0;
        int autosViejos = 0;
        int autosNuevos = 0;

        public Form1()
        {
            InitializeComponent();
        }

        //Boton para salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //boton total
        private void btnTotal_Click(object sender, EventArgs e)
        {
            //agregar datos
            for (int i = 0; i < cantClientes; i++)
            {
                dgvClientes.Rows.Add(nombre[i], sexo[i], marcas[i], anio[i], precio[i]);

                //calculo de estadisticas de autos vendidos
                switch (marcas[i])
                {
                    case "Nissan":
                        nissan++;
                        nissanMonto += precio[i];
                        break;
                    case "Toyota":
                        toyota++;
                        toyotamonto += precio[i];
                        break;
                    case "Kia":
                        kia++;
                        kiaMonto += precio[i];
                        break;
                }
                //autos viejos
                if (anio[i] >= 2000 && anio[i] <= 2015)
                {
                    autosViejos++;
                }
                //autos nuevos
                else if (anio[i] >= 2016 && anio[i] <= 2024)
                {
                    autosNuevos++;
                }
            }
            //datos a las estadisticas
            txtNissan.Text = nissan.ToString();
            txtNissanMonto.Text = "$"+nissanMonto.ToString("F");
            txtToyota.Text = toyota.ToString();
            txtToyotaMonto.Text = "$" + toyotamonto.ToString("F");
            txtKia.Text = kia.ToString();
            txtKiaMonto.Text = "$" + kiaMonto.ToString("F");
            txtAutosViejos.Text = autosViejos.ToString();
            txtAutosNuevos.Text = autosNuevos.ToString();
        }

        //boton limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //activa boton iniciar para nuevos registros
            btnIniciar.Enabled = true;
            LimpiarCampos();
            reinicioEstaAutos();
        }

        //boton agreagar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (actual >= cantClientes)
            {
                //si se intenta agregar un cliente mas
                MessageBox.Show("Ya no se pueden registrar mas clientes");
                return;
            }

            if (ValidarDatos())
            {
                nombre[actual] = txtNombre.Text;
                sexo[actual] = cbxSexo.SelectedItem.ToString();
                marcas[actual] = cbxMarca.SelectedItem.ToString();
                anio[actual] = int.Parse(txtAnio.Text);
                precio[actual] = Convert.ToDouble(txtPrecio.Text);

                actual++;

                //limpiar para agregar cliente
                LimpiarCampos();
            }
        }

        //boton inicir
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCantidad.Text, out cantClientes) && cantClientes > 0)
            {
                nombre = new string[cantClientes];
                sexo = new string[cantClientes];
                marcas = new string[cantClientes];
                anio = new int[cantClientes];
                precio = new double[cantClientes];

                actual = 0;
                reinicioEstaAutos();

                MessageBox.Show("Ingrese los datos de los clientes");
                btnIniciar.Enabled = false;
            }
            else
            {
                MessageBox.Show("Ingrese una cantidad válida");
            }
        }

        //validacion de datos de clientes
        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre");
                return false;
            }
            if (cbxSexo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione sexo");
                return false;
            }
            if (cbxMarca.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione marca");
                return false;
            }
            if (!int.TryParse(txtAnio.Text, out int anio) || anio < 2000 || anio > 2024)
            {
                MessageBox.Show("Ingrese un año de vehiculo válido\n Debe ser entre 2000 y 2024");
                return false;
            }
            if (!double.TryParse(txtPrecio.Text, out double precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio válido para el vehiculo");
                return false;
            }
            return true;
        }

        //limpia los controles del form
        private void LimpiarCampos()
        {
            //datos del cliente
            txtNombre.Clear();
            cbxSexo.SelectedIndex = -1;
            cbxMarca.SelectedIndex = -1;
            txtAnio.Clear();
            txtPrecio.Clear();
            //datagridview
            dgvClientes.Rows.Clear();
            //estadisticas
            txtNissan.Clear();
            txtNissanMonto.Clear();
            txtToyota.Clear();
            txtToyotaMonto.Clear();
            txtKia.Clear();
            txtKiaMonto.Clear();

            txtAutosNuevos.Clear();
            txtAutosViejos.Clear();
        }

        //limpiar estadisticas de autos vendidos
        private void reinicioEstaAutos()
        {
            //autos
            nissan = 0;
            toyota = 0;
            kia = 0;
            //cantidad autos
            autosViejos = 0;
            autosNuevos = 0;
            //montos
            nissanMonto = 0;
            toyotamonto = 0;
            kiaMonto = 0;
        }
    }
}
