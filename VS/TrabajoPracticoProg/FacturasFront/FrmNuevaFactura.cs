using FacturasBack.dominio;
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
            await AsignarNumeroFacturaAsync();

        }

        private async Task CargarCboArticulosAsync()
        {
            string urlArticulo = "https://localhost:44357/api/Facturas/articulos";
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(urlArticulo);
            var contenido = await result.Content.ReadAsStringAsync();
            List<Articulo> lst = JsonConvert.DeserializeObject<List<Articulo>>(contenido);

            cboArticulos.DataSource = lst;
            cboArticulos.DisplayMember = "Nombre";
            cboArticulos.ValueMember = "IdArticulo";
        }

        private async Task CargarCboFormasPagoAsync()
        {
            string urlFormaPago = "https://localhost:44357/api/Facturas/formas_de_pago";
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(urlFormaPago);
            var contenido = await result.Content.ReadAsStringAsync();
            List<FormaPago> lst = JsonConvert.DeserializeObject<List<FormaPago>>(contenido);

            cboFormasPago.DataSource = lst;
            cboFormasPago.DisplayMember = "Nombre";
            cboFormasPago.ValueMember = "IdFormaPago";
        }

        private async Task AsignarNumeroFacturaAsync()
        {
            string url = "https://localhost:44357/api/Facturas/proximo_nro_factura";
            using (HttpClient cliente = new HttpClient())
            {
                var result = await cliente.GetStringAsync(url);
                factura.NroFactura = Int32.Parse(result);
                lblFacturaNro.Text = "Factura Nro: " + result;
            }

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
            ActualizarTotales();

        }
        private void ActualizarTotales()
        {
            lblTotal.Text = "Total: $" + factura.CalcularTotal();
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
                ActualizarTotales();
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

            bool success = await GrabarFacturaAsync(data);
   
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

        private async Task<bool> GrabarFacturaAsync(string data)
        {
            string url = "https://localhost:44357/api/Facturas/facturas";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                return (int)result.StatusCode == 200;
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
            await AsignarNumeroFacturaAsync();
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
