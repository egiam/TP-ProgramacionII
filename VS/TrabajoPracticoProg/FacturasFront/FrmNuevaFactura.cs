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

    }
}
