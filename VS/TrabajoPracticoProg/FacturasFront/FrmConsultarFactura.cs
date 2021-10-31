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
    }
}
