﻿using FacturasBack.dominio;
using FacturasFront.clienteHttp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturasFront
{
    public partial class FrmNuevaFactura : Form
    {
        private Factura factura;
        public FrmNuevaFactura()
        {
            InitializeComponent();
            factura = new Factura();
        }

        private async void FrmNuevaFactura_Load(object sender, EventArgs e)
        {
            
            await CargarCboArticulosAsync();
            await CargarCboFormasPagoAsync();
            await AsignarNroFactura();


        }

        private async Task AsignarNroFactura()
        {
            string url = "https://localhost:44357/api/Facturas/proximo_nro_factura";
            int nroFactura = await ClienteSingleton.GetInstancia().AsignarNumeroFacturaAsync(url);
            factura.NroFactura = nroFactura;
            lblFacturaNro.Text = "Factura Nro: " + nroFactura;
        }

        private async Task CargarCboArticulosAsync()
        {
            string url = "https://localhost:44357/api/Facturas/articulos";
            List<Articulo> lst = await ClienteSingleton.GetInstancia().ConsultarArticulos(url);

            cboArticulos.DataSource = lst;
            cboArticulos.DisplayMember = "Nombre";
            cboArticulos.ValueMember = "IdArticulo";
        }

        private async Task CargarCboFormasPagoAsync()
        {
            string url = "https://localhost:44357/api/Facturas/formas_de_pago";
            List<FormaPago> lst = await ClienteSingleton.GetInstancia().ConsultarFormasPago(url);

            cboFormasPago.DataSource = lst;
            cboFormasPago.DisplayMember = "Nombre";
            cboFormasPago.ValueMember = "IdFormaPago";
        }

        private void cboArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ExisteArticuloEnGrilla(cboArticulos.Text))
            {
                MessageBox.Show("El Artículo ya fue ingresado!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DetalleFactura detalle = new DetalleFactura();
            detalle.Articulo = (Articulo)cboArticulos.SelectedItem;
            detalle.Cantidad = (int)nudCantidad.Value;
            factura.AgregarDetalle(detalle);
            dgvDetalles.Rows.Add(new string[] { "", detalle.Articulo.Nombre, detalle.Cantidad.ToString(), detalle.Articulo.PrecioUnitario.ToString() });

            CalcularTotal();

        }
        private void CalcularTotal()
        {
            double total = factura.CalcularTotal();
            lblTotal.Text = "Total: $" + total.ToString();
            factura.Total = total;

        }

        private bool ExisteArticuloEnGrilla(string text)
        {
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["ColArt"].Value.Equals(text))
                    return true;
            }
            return false;
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                factura.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                CalcularTotal();
            }
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Debe ingresar un cliente!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCliente.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cboFormasPago.Text))
            {
                MessageBox.Show("Debe ingresar una forma de pago!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboFormasPago.Focus();
                return;
            }

            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar un detalle al menos!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboArticulos.Focus();
                return;
            }

            factura.Cliente = txtCliente.Text;
            factura.FormaPago = new FormaPago(cboFormasPago.SelectedIndex + 1, "");
            string data = JsonConvert.SerializeObject(factura);
            string url = "https://localhost:44357/api/Facturas/facturas";
            bool success = await ClienteSingleton.GetInstancia().GrabarFacturaAsync(url, data);
   
            if (success)
            {
                MessageBox.Show("Factura registrada con éxito!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await limpiarCamposAsync();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un inconveniente al registrar la factura!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      
        private async Task limpiarCamposAsync()
        {
            txtCliente.Text = string.Empty;
            txtCliente.Focus();
            cboFormasPago.Text = string.Empty;
            cboArticulos.SelectedIndex = 0;
            dgvDetalles.Rows.Clear();
            lblTotal.Text = "Total: ";
            nudCantidad.Value = 1;
            await AsignarNroFactura();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
