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
    public partial class FrmNuevoArticulo : Form
    {
        public enum Accion
        {
            CREATE,
            READ,
            UPDATE,
            DELETE
        }
        private Articulo articulo;
        private Accion modo;

        public FrmNuevoArticulo()
        {
            InitializeComponent();
            articulo = new Articulo();
            AsignarNumeroArticuloAsync();
            this.modo = Accion.CREATE;
        }
        public FrmNuevoArticulo(Accion modo, Articulo articuloSeleccionado)
        {
            InitializeComponent();
            // servicio = new ServiceFactoryImp().CrearService();
            this.modo = modo;

            if (modo.Equals(Accion.UPDATE))
            {
                articulo = articuloSeleccionado;
                txtNombre.Text = articuloSeleccionado.Nombre;
                nudPrecio.Value = Convert.ToDecimal(articuloSeleccionado.PrecioUnitario);
                lblNroArticulo.Text = "Articulo Nro: " + articulo.IdArticulo.ToString() ;
                this.Text = "Editar articulo";
            }

            if (modo.Equals(Accion.CREATE))
            {
                articulo = new Articulo();
                AsignarNumeroArticuloAsync();
            }

        }

        private async void FrmNuevoArticulo_Load(object sender, EventArgs e)
        {
        }

        private async Task AsignarNumeroArticuloAsync()
        {
            string url = "https://localhost:44357/api/Articulos/proximo_nro_articulo";
            using (HttpClient cliente = new HttpClient())
            {
                var result = await cliente.GetStringAsync(url);
                articulo.IdArticulo = Int32.Parse(result);
                lblNroArticulo.Text = "Articulo Nro: " + result;
            }

        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrEmpty(nudPrecio.Text))
            {
                MessageBox.Show("Debe ingresar un precio!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nudPrecio.Focus();
                return;
            }


            articulo.Nombre = txtNombre.Text;
            articulo.PrecioUnitario = Convert.ToDouble(nudPrecio.Text);
            string data = JsonConvert.SerializeObject(articulo);

            bool success = false;

            if (modo.Equals(Accion.CREATE))
            {
                success = await GrabarArticuloAsync(data);
                MostrarMensajeResultado(success);
                await limpiarCamposAsync();
            }
            else
            if (modo.Equals(Accion.UPDATE))
            {
                success = await EditarArticuloAsync(data);
                MostrarMensajeResultado(success);
                this.Dispose();
            }
       
           
        }

        private void MostrarMensajeResultado(bool resultado)
        {
            if (resultado)
            {
                MessageBox.Show("Artículo registrado con éxito!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("Ha ocurrido un inconveniente al registrar el artículo!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async Task<bool> GrabarArticuloAsync(string data)
        {
            string url = "https://localhost:44357/api/Articulos/articulos";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                return result.IsSuccessStatusCode;
            }
        }

        private async Task<bool> EditarArticuloAsync(string data)
        {
            string url = "https://localhost:44357/api/Articulos/articulo";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PutAsync(url, content);
                return result.IsSuccessStatusCode;
            }
        }

        private async Task limpiarCamposAsync()
        {
            txtNombre.Text = string.Empty;
            txtNombre.Focus();
            nudPrecio.Value = 0;
            await AsignarNumeroArticuloAsync();
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }

        }

        private void nudPrecio_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
