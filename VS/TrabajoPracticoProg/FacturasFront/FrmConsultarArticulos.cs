using FacturasBack.dominio;
using FacturasBack.negocio;
using FacturasFront.clienteHttp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturasFront
{
    public partial class FrmConsultarArticulos : Form
    {
        public FrmConsultarArticulos()
        {
            InitializeComponent();
        }

        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            List<Parametro> filtros = new List<Parametro>();
            Parametro precio_desde = new Parametro();
            precio_desde.Nombre = "@precio_desde";
            precio_desde.Valor = Convert.ToDouble(nudPrecioDesde.Value);
            filtros.Add(precio_desde);
            filtros.Add(new Parametro("@precio_hasta", Convert.ToDouble(nudPrecioHasta.Value)));

            object val = DBNull.Value;
            if (!String.IsNullOrEmpty(txtNombre.Text))
                val = txtNombre.Text;
            filtros.Add(new Parametro("@nombre", val));

            string dadoBaja = "N";

            if (chkDadoBaja.Checked)
                dadoBaja = "S";
            filtros.Add(new Parametro("@dado_baja", dadoBaja));


            List<Articulo> lst = null;

            string filtrosJSON = JsonConvert.SerializeObject(filtros);
            string url = "https://localhost:44357/api/Articulos/consultar";

            var resultado = await ClienteSingleton.GetInstancia().PostAsync(url, filtrosJSON);

            lst = JsonConvert.DeserializeObject<List<Articulo>>(resultado);


            dgvResultados.Rows.Clear();
            foreach (Articulo oArticulo in lst)
            {
                dgvResultados.Rows.Add(new object[]{
                                        oArticulo.IdArticulo,
                                        oArticulo.Nombre,
                                        oArticulo.PrecioUnitario
                }); ;
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            nudPrecioDesde.Value = 0;
            nudPrecioHasta.Value = 15000;
            chkDadoBaja.Checked = false;
            dgvResultados.Rows.Clear();
        }

        private async void dgvResultados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvResultados.CurrentCell.ColumnIndex == 3)
            {
                Articulo articuloSeleccionado = new Articulo();
                articuloSeleccionado.Nombre =dgvResultados.CurrentRow.Cells["ColNombre"].Value.ToString();
                articuloSeleccionado.IdArticulo = Convert.ToInt32(dgvResultados.CurrentRow.Cells["Id"].Value.ToString());
                articuloSeleccionado.PrecioUnitario = Convert.ToDouble(dgvResultados.CurrentRow.Cells["ColPrecio"].Value.ToString());
                FrmNuevoArticulo frm = new FrmNuevoArticulo((FrmNuevoArticulo.Accion)Accion.UPDATE, articuloSeleccionado);
                frm.ShowDialog();
            }
            if (dgvResultados.CurrentCell.ColumnIndex == 4)
            {
                DataGridViewRow row = dgvResultados.CurrentRow; // fila actual o seleccionada
                if (row != null)
                {
                    int idFactura = Int32.Parse(row.Cells["Id"].Value.ToString());
                    if (MessageBox.Show("Seguro que desea dar de baja el artículo seleccionado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string url = "https://localhost:44357/api/articulos/" + idFactura.ToString();
                        string respuesta = await ClienteSingleton.GetInstancia().DeleteAsync(url);

                        if (respuesta == "true")
                        {
                            MessageBox.Show("Artículo dado de baja!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.btnConsultar_Click(null, null);
                        }
                        else
                            MessageBox.Show("Error al intentar dar de baja el artículo!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void nudPrecioDesde_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
