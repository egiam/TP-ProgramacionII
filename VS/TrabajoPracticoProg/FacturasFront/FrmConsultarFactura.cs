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
using TrabajoPracticoProg.Presentacion;

namespace FacturasFront
{

    public partial class FrmConsultarFactura : Form
    {
        public FrmConsultarFactura()
        {
            InitializeComponent();
        }


        private void FrmConsultarFactura_Load(object sender, EventArgs e)
        {

        }

        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            List<Parametro> filtros = new List<Parametro>();
            Parametro fecha_desde = new Parametro();
            fecha_desde.Nombre = "@fecha_desde";
            fecha_desde.Valor = dtpDesde.Value.ToShortDateString();
            filtros.Add(fecha_desde);
            filtros.Add(new Parametro("@fecha_hasta", dtpHasta.Value.ToString()));



            object val = DBNull.Value;
            if (!String.IsNullOrEmpty(txtCliente.Text))
                val = txtCliente.Text;
            filtros.Add(new Parametro("@cliente", val));

            string dadoBaja = "N";
            if (chkBaja.Checked)
                dadoBaja = "S";
            filtros.Add(new Parametro("@datos_baja", dadoBaja));



            List<Factura> lst = null;

            string filtrosJSON = JsonConvert.SerializeObject(filtros);
            string url = "https://localhost:44357/api/Facturas/consultar";

            var resultado = await ClienteSingleton.GetInstancia().PostAsync(url, filtrosJSON);

            lst = JsonConvert.DeserializeObject<List<Factura>>(resultado);


            dgvResultados.Rows.Clear();
            foreach (Factura oFactura in lst)
            {
                dgvResultados.Rows.Add(new object[]{
                                        oFactura.NroFactura,
                                        oFactura.Fecha.ToString("dd/MM/yyyy"),
                                        oFactura.Cliente,
                                        oFactura.Total,
                                        oFactura.GetFechaBajaFormato()
                }); ;
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            dtpDesde.Value = new DateTime(2021, 01, 01);
            dtpHasta.Value = DateTime.Now;
            chkBaja.Checked = false;
            dgvResultados.Rows.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
                this.Dispose();
        }

        private async void dgvResultados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvResultados.CurrentCell.ColumnIndex == 5)
            {
                int nroFactura = Convert.ToInt32(dgvResultados.CurrentRow.Cells["Id"].Value.ToString());
                FrmNuevaFactura frm = new FrmNuevaFactura(Accion.READ, nroFactura);
                frm.ShowDialog();
            }

            if (dgvResultados.CurrentCell.ColumnIndex == 6)
            {
                DataGridViewRow row = dgvResultados.CurrentRow; // fila actual o seleccionada
                if (row != null)
                {
                    int idFactura = Int32.Parse(row.Cells["Id"].Value.ToString());
                    if (MessageBox.Show("Seguro que desea dar de baja la factura seleccionada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string url = "https://localhost:44357/api/Facturas/" + idFactura.ToString();
                        string respuesta = await ClienteSingleton.GetInstancia().DeleteAsync(url);

                        if (respuesta=="true")
                        {
                            MessageBox.Show("Factura dada de baja!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.btnConsultar_Click(null, null);
                        }
                        else
                            MessageBox.Show("Error al intentar dar de baja la factura!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmNuevaFactura frm = new FrmNuevaFactura(Accion.CREATE, 0);
            frm.ShowDialog();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
