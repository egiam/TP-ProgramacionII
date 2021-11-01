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
using TrabajoPracticoProg.Presentacion;
using DominoLogin;
using System.Data.SqlClient;
using Presentacion;
using CommonLogin.Cache;
using FacturasFront;

namespace TrabajoPracticoProg
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            
        }

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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "Username")// && txtUsuario.TextLength > 2)
            {
                if (txtContra.Text != "Password")
                {
                    UserModel user = new UserModel();
                    var validLogin = user.LoginUser(txtUsuario.Text, txtContra.Text);
                    if (validLogin == true)
                    {
                        FrmPrincipal mainMenu = new FrmPrincipal();
                        MessageBox.Show("Bienvenido " + UserLoginCache.FirstName + ", " + UserLoginCache.LastName);
                        mainMenu.Show();
                        mainMenu.FormClosed += Logout;
                        this.Hide();
                    }
                    else
                    {
                        msgError("Incorrect username or password entered. \n   Please try again.");
                        txtContra.Text = "Password";
                        txtContra.UseSystemPasswordChar = false;
                        txtUsuario.Focus();
                    }
                }
                else msgError("Please enter password.");
            }
            else msgError("Please enter username.");
        }

        private void msgError(string msg)
        {
            lblErrorMessage.Text = "      " + msg;
            lblErrorMessage.Visible = true;
        }

        private void Logout(object sender, FormClosedEventArgs e)
        {
            txtContra.Text = "Password";
            txtContra.UseSystemPasswordChar = false;
            txtUsuario.Text = "Username";
            lblErrorMessage.Visible = false;
            this.Show();
        }


        private void btnRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegistrarse registro = new FrmRegistrarse();
            registro.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
