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
        public FrmNuevaFactura()
        {
            InitializeComponent();
        }

        private async void FrmNuevaFactura_Load(object sender, EventArgs e)
        {
            await CargarCboArticulosAsync();
            await CargarCboFormasPagoAsync();

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

    }
}
