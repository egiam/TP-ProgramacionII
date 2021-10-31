using FacturasBack.dominio;
using FacturasBack.negocio;
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //List<Parametro> filtros = new List<Parametro>();
            //Parametro fecha_desde = new Parametro();
            //fecha_desde.Nombre = "@fecha_desde";
            //fecha_desde.Valor = dtpDesde.Value.ToShortDateString();
            //filtros.Add(fecha_desde);
            //filtros.Add(new Parametro("@fecha_hasta", dtpHasta.Value.ToShortDateString()));



            //object val = DBNull.Value;
            //if (!String.IsNullOrEmpty(txtCliente.Text))
            //    val = txtCliente.Text;
            //filtros.Add(new Parametro("@cliente", val));

            //string dadoBaja = "N";
            //if (chkBaja.Checked)
            //    dadoBaja = "S";
            //filtros.Add(new Parametro("@datos_baja", dadoBaja));



            //List<Factura> lst = null;

            //string filtrosJSON = JsonConvert.SerializeObject(filtros);
            //string url = "https://localhost:44373/api/Presupuestos/consultar";

            //var resultado = await ClienteSingleton.GetInstancia().PostAsync(url, filtrosJSON);

            //lst = JsonConvert.DeserializeObject<List<Presupuesto>>(resultado);



            //dgvResultados.Rows.Clear();
            //foreach (Presupuesto oPresupuesto in lst)
            //{
            //    dgvResultados.Rows.Add(new object[]{
            //                            oPresupuesto.PresupuestoNro,
            //                            oPresupuesto.Fecha.ToString("dd/MM/yyyy"),
            //                            oPresupuesto.Cliente,
            //                            oPresupuesto.Descuento,
            //                            oPresupuesto.Total,
            //                            oPresupuesto.GetFechaBajaFormato()
            //     }); ;
            //}
        }
    }
}
