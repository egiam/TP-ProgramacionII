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
    public partial class FrmDesarrolladores : Form
    {
        public FrmDesarrolladores()
        {
            InitializeComponent();
        }

        private void FrmDesarrolladores_Load(object sender, EventArgs e)
        {
        }

        private void lblVolver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
