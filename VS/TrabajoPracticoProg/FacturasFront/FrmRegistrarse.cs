using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CommonLogin.Cache;
using FacturasBack.negocio;
using FacturasBack.dominio;
using FacturasBack.datos;
//using AccesoDatosLogin;

namespace TrabajoPracticoProg.Presentacion
{
    public partial class FrmRegistrarse : Form
    {
        public enum Accion
        {
            CREATE,
            READ,
            UPDATE,
            DELETE
        }

        //private RegistroDao registro;
        //private UserLoginCache cache;
        private Usuario oUsuario;
        private IAplicacion aplicacion;
        private GestorRegistro gestor;
        private Accion modo;

        public FrmRegistrarse()
        {
            InitializeComponent();
            oUsuario = new Usuario();
            gestor = new GestorRegistro(new DaoFactory());
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Usuario";
                txtUsuario.ForeColor = Color.Silver;
            }
        }

        private void txtContra_Enter(object sender, EventArgs e)
        {
            if (txtContra.Text == "Contraseña")
            {
                txtContra.Text = "";
                txtContra.ForeColor = Color.LightGray;
                txtContra.UseSystemPasswordChar = true;
            }
        }

        private void txtContra_Leave(object sender, EventArgs e)
        {
            if (txtContra.Text == "")
            {
                txtContra.Text = "Contraseña";
                txtContra.ForeColor = Color.Silver;
                txtContra.UseSystemPasswordChar = false;
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            if (txtNombre.Text == "Nombre")
            {
                txtNombre.Text = "";
                txtNombre.ForeColor = Color.LightGray;
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                txtNombre.Text = "Nombre";
                txtNombre.ForeColor = Color.Silver;
            }
        }

        private void txtApellido_Enter(object sender, EventArgs e)
        {
            if (txtApellido.Text == "Apellido")
            {
                txtApellido.Text = "";
                txtApellido.ForeColor = Color.LightGray;
            }
        }

        private void txtApellido_Leave(object sender, EventArgs e)
        {
            if (txtApellido.Text == "")
            {
                txtApellido.Text = "Apellido";
                txtApellido.ForeColor = Color.Silver;
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.LightGray;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Email";
                txtEmail.ForeColor = Color.Silver;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FrmRegistrarse_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmRegistrarse_Load(object sender, EventArgs e)
        {

        }

        private void btnVolver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtUsuario.Text) || txtUsuario.Text == "Usuario")
            {
                MessageBox.Show("Debe ingresar un Usuario valido!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtContra.Text) || txtContra.Text == "Contraseña")
            {
                MessageBox.Show("Debe ingresar una Contraseña!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContra.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNombre.Text) || txtNombre.Text == "Nombre")
            {
                MessageBox.Show("Debe ingresar un Nombre!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtApellido.Text) || txtApellido.Text == "Apellido")
            {
                MessageBox.Show("Debe ingresar un Apellido!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtApellido.Focus();
                return;
            }
            
            if (string.IsNullOrEmpty(txtEmail.Text) || txtEmail.Text == "Email")
            {
                MessageBox.Show("Debe ingresar un Email!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }  

            if (string.IsNullOrEmpty(txtPosicion.Text) || txtPosicion.Text == "Posicion")
            {
                MessageBox.Show("Debe ingresar una Posicion Laboral!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPosicion.Focus();
                return;
            }

            GuradarRegistro();
        }

        private void GuradarRegistro()
        {
            oUsuario.Password = txtContra.Text;
            oUsuario.Position = txtPosicion.Text;
            oUsuario.LoginName = txtUsuario.Text;
            oUsuario.LastName = txtApellido.Text;
            oUsuario.FirstName = txtNombre.Text;
            oUsuario.Email = txtEmail.Text;

            if (gestor.NuevoRegistro(oUsuario))
            {
                MessageBox.Show("Registro realizado con exito.", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Close();
                Terminar();
            }
            else
            {
                MessageBox.Show("ERROR. No se pudo registrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Terminar()
        {
            this.Close();
        }

        private void txtPosicion_Enter(object sender, EventArgs e)
        {
            if (txtPosicion.Text == "Posicion")
            {
                txtPosicion.Text = "";
                txtPosicion.ForeColor = Color.LightGray;
            }
        }

        private void txtPosicion_Leave(object sender, EventArgs e)
        {
            if (txtPosicion.Text == "")
            {
                txtPosicion.Text = "Posicion";
                txtPosicion.ForeColor = Color.Silver;
            }
        }
    }
}
