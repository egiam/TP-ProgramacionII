using FacturasBack.dominio;
using FacturasBack.negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FacturasBack.datos
{
    class HelperDao
    {
        private static HelperDao instance;
        private string connectionString;

        private HelperDao()
        {
            connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=facturacion;Integrated Security=True";
        }

        public static HelperDao GetInstance()
        {

            if (instance == null)
            {
                instance = new HelperDao();
            }
            return instance;
        }

        public DataTable ConsultaSQL(string storeName)
        {
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            DataTable tabla = new DataTable();

            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storeName;
                tabla.Load(cmd.ExecuteReader());

            }
            catch (SqlException)
            {
                tabla = null;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();

            }
            return tabla;
        }


        public bool EjecutarInsertFactura(Factura factura, string spMaestro, string spDetalle)
        {
            bool ok = true;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                //Se inserta Factura
                SqlCommand cmdMaestro = new SqlCommand(spMaestro, connection, transaction);
                cmdMaestro.CommandType = CommandType.StoredProcedure;

                cmdMaestro.Parameters.AddWithValue("@nro_factura", factura.NroFactura);
                cmdMaestro.Parameters.AddWithValue("@cliente", factura.Cliente);
                cmdMaestro.Parameters.AddWithValue("@forma", factura.FormaPago.IdFormaPago);
                cmdMaestro.Parameters.AddWithValue("@total", factura.Total);


                cmdMaestro.ExecuteNonQuery();

                //Se inserta Detalle Factura 
                foreach (DetalleFactura detalle in factura.Detalles)
                {
                    SqlCommand cmdDetalle = new SqlCommand(spDetalle, connection, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", factura.NroFactura);
                    cmdDetalle.Parameters.AddWithValue("@id_articulo", detalle.Articulo.IdArticulo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);

                    cmdDetalle.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                ok = false;

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return ok;
        }


        public bool EjecutarInsertArticulo(Articulo articulo, string spArticulo)
        {
            bool ok = true;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                //Se inserta Articulo
                SqlCommand cmdMaestro = new SqlCommand(spArticulo, connection, transaction);
                cmdMaestro.CommandType = CommandType.StoredProcedure;

                cmdMaestro.Parameters.AddWithValue("@nombre", articulo.Nombre);
                cmdMaestro.Parameters.AddWithValue("@precio", articulo.PrecioUnitario);


                cmdMaestro.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                ok = false;

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return ok;
        }


        public int EjecutarSQLConValorOUT(string nombreSP, string nombreParametro)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            int val;
            try
            {
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = nombreSP;
                param.ParameterName = nombreParametro;
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                cmd.ExecuteScalar();
                val = (int)param.Value;

            }
            catch (SqlException)
            {
                val = 0;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return val;
        }

        public List<Factura> ConsultarFacturas(string nombreSP, List<Parametro> criterios)
        {
            
            List<Factura> lst = new List<Factura>();
            DataTable table = new DataTable();
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand(nombreSP, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (Parametro p in criterios)
                {
                    if (p.Valor == null)
                        cmd.Parameters.AddWithValue(p.Nombre, DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue(p.Nombre, p.Valor.ToString());
                }

                table.Load(cmd.ExecuteReader());
                //mappear los registros como objetos del dominio:

                foreach (DataRow row in table.Rows)
                {
                    Factura oFactura = new Factura();
                    oFactura.Cliente = row["cliente"].ToString();
                    oFactura.Fecha = Convert.ToDateTime(row["fecha"].ToString());
                    FormaPago formaPago = new FormaPago();
                    formaPago.IdFormaPago = Convert.ToInt32(row["id_forma_pago"].ToString());
                    formaPago.Nombre = row["nombre"].ToString();
                    oFactura.FormaPago = formaPago;
                    oFactura.NroFactura = Convert.ToInt32(row["nro_factura"].ToString());
                    oFactura.Total = Convert.ToDouble(row["total"].ToString());
                    //validar que fecha_baja no es null:
                    if (!row["fecha_baja"].Equals(DBNull.Value)) 
                        oFactura.FechaBaja = Convert.ToDateTime(row["fecha_baja"].ToString());

                    lst.Add(oFactura);
                }

                cnn.Close();
            }
            catch (SqlException)
            {
                lst = null;
            }
            return lst;
        }


        public Factura GetFacturaPorId(string nombreSP, int id)
        {
            Factura oFactura = new Factura();
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            bool esPrimerRegistro = true;

            while (reader.Read())
            {
                if (esPrimerRegistro)
                {
                    //solo para el primer resultado recuperamos los datos del MAESTRO:
                    oFactura.NroFactura = Convert.ToInt32(reader["nro_factura"].ToString());
                    oFactura.Cliente = reader["cliente"].ToString();
                    oFactura.Fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    FormaPago formaPago = new FormaPago();
                    formaPago.IdFormaPago= Convert.ToInt32(reader["id_forma_pago"].ToString());
                    formaPago.Nombre = reader["nombre_forma_pago"].ToString();
                    oFactura.FormaPago = formaPago;
                    oFactura.Total = Convert.ToDouble(reader["total"].ToString());
                    esPrimerRegistro = false;
                }

                DetalleFactura oDetalle = new DetalleFactura();
                Articulo oArticulo = new Articulo();
                oArticulo.IdArticulo = Convert.ToInt32(reader["id_articulo"].ToString());
                oArticulo.Nombre = reader["nombre_articulo"].ToString();
                oArticulo.PrecioUnitario = Convert.ToDouble(reader["pre_unitario"].ToString());
                oDetalle.Articulo = oArticulo;
                oDetalle.Cantidad = Convert.ToInt32(reader["cantidad"].ToString());
                esPrimerRegistro = false;
                oFactura.AgregarDetalle(oDetalle);
            }
            return oFactura;
        }






    }

}
