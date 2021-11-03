using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLogin.Cache;
using FacturasFront;

namespace TrabajoPracticoProg.Presentacion
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            LoadUserData();
            //Manage Positions
            if(UserLoginCache.Position == Positions.Cliente)
            {
                btnNuevaFactura.Enabled = false;
                btnNuevoArticulo.Enabled = false;
                nuevoArticuloToolStripMenuItem.Enabled = false;
                nuevoFacturaToolStripMenuItem.Enabled = false;
            }
            if(UserLoginCache.Position == Positions.Empleado)
            {
                btnMenu.Enabled = false;
            }
        }

        private void LoadUserData()
        {
            lblName.Text = UserLoginCache.FirstName + ", " + UserLoginCache.LastName;
            lblEmail.Text = UserLoginCache.Email;
            lblPosition.Text = UserLoginCache.Position;
        }

        #region Funcionalidades del formulario
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelContenedor.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to close the application?", "Warning",
           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Application.Exit();
        }
        //Capturar posicion y tamaño antes de maximizar para restaurar
        int lx, ly;
        int sw, sh;
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            btnRestaurar.Enabled = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        //private void btnRestaurar_Click(object sender, EventArgs e)
        //{
        //    btnMaximizar.Visible = true ;
        //    btnRestaurar.Visible = false;
        //    this.Size = new Size(sw,sh);
        //    this.Location = new Point(lx,ly);
        //}

        private void panelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //METODO PARA ARRASTRAR EL FORMULARIO---------------------------------------------------------------------
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    AbrirFormulario<Form1>();
        //    button1.BackColor = Color.FromArgb(12, 61, 92);
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    AbrirFormulario<Form2>();
        //    button2.BackColor = Color.FromArgb(12, 61, 92);
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    AbrirFormulario<Form3>();
        //    button3.BackColor = Color.FromArgb(12, 61, 92);
        //}

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro quiere cerrar la sesión?", "Warning",
           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmNuevaFactura>();
            //btnNuevaFactura.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnConsultarFactura_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmConsultarFactura>();
            //btnConsultarFactura.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnConsultarArticulos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmConsultarArticulos>();
            //btnConsultarArticulos.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmNuevoArticulo>();
            //btnNuevoArticulo.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnRestaurar_Click_1(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (menuPrincipal.Visible == true)
                menuPrincipal.Visible = false;
            else
                menuPrincipal.Visible = true;
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro quiere cerrar la aplicación?", "Warning",
           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Application.Exit();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click_1(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            btnRestaurar.Enabled = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro quiere cerrar la aplicación?", "Warning",
           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Application.Exit();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro quiere cerrar la sesión?", "Warning",
           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void nuevoArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmNuevoArticulo>();
        }

        private void consultarArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmConsultarArticulos>();
        }

        private void nuevoFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmNuevaFactura>();
        }

        private void cosultarFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmConsultarFactura>();
        }

        private void desarrolladoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmDesarrolladores>();
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion




        //METODO PARA ABRIR FORMULARIOS DENTRO DEL PANEL
        private void AbrirFormulario<MiForm>() where MiForm : Form, new() {
            Form formulario;
            formulario = panelformularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
            //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelformularios.Controls.Add(formulario);
                panelformularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms );
            }
            //si el formulario/instancia existe
            else {
                formulario.BringToFront();
            }
        }
        private void CloseForms(object sender,FormClosedEventArgs e) {
            if (Application.OpenForms["FrmNuevaFactura"] == null)
                btnNuevaFactura.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["FrmConsultarFactura"] == null)
                btnConsultarFactura.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["FrmConsultarArticulos"] == null)
                btnConsultarArticulos.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["FrmNuevoArticulo"] == null)
                btnNuevoArticulo.BackColor = Color.FromArgb(4, 41, 68);
        }
    }
}
