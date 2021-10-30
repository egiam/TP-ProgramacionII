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
    }
}
