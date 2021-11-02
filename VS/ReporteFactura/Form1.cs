using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Listados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.rpvVentas.RefreshReport();
          //  this.reportViewer2.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            txtFechaDesde.Text = "01/01/2021";
            txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=facturacion;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();

            string strSql = "select f.cliente 'Cliente', sum(df.cantidad*a.pre_unitario) 'Total' " +
                "from detalles_factura df join facturas f on df.nro_factura = f.nro_factura " +
                "join articulos a on df.id_articulo = a.id_articulo " +
                "where f.fecha >= @fechaDesde and f.fecha <= @fechaHasta group by f.cliente " +
                "order by 2 desc,1";

            SqlCommand cmd = new SqlCommand(strSql, cnn);

            cmd.Parameters.AddWithValue("@fechaDesde", txtFechaDesde.Text);
            cmd.Parameters.AddWithValue("@fechaHasta", txtFechaHasta.Text);

            DataTable tabla = new DataTable();
            tabla.Load(cmd.ExecuteReader());

            rpvVentas.LocalReport.DataSources.Clear();
            rpvVentas.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", tabla));

            rpvVentas.RefreshReport();

            cnn.Close();



        }

        private void txtFechaDesde_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
