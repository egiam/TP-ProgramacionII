
namespace FacturasFront
{
    partial class FrmConsultarArticulos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.chkDadoBaja = new System.Windows.Forms.CheckBox();
            this.btnLimpiarBusqueda = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.nudPrecioHasta = new System.Windows.Forms.NumericUpDown();
            this.nudPrecioDesde = new System.Windows.Forms.NumericUpDown();
            this.lblPrecioHasta = new System.Windows.Forms.Label();
            this.lblPrecioDesde = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.gbResultados = new System.Windows.Forms.GroupBox();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAccion = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColAccionEditar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.gbBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioHasta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioDesde)).BeginInit();
            this.gbResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.Controls.Add(this.chkDadoBaja);
            this.gbBusqueda.Controls.Add(this.btnLimpiarBusqueda);
            this.gbBusqueda.Controls.Add(this.btnConsultar);
            this.gbBusqueda.Controls.Add(this.nudPrecioHasta);
            this.gbBusqueda.Controls.Add(this.nudPrecioDesde);
            this.gbBusqueda.Controls.Add(this.lblPrecioHasta);
            this.gbBusqueda.Controls.Add(this.lblPrecioDesde);
            this.gbBusqueda.Controls.Add(this.txtNombre);
            this.gbBusqueda.Controls.Add(this.lblNombre);
            this.gbBusqueda.Location = new System.Drawing.Point(52, 30);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(560, 192);
            this.gbBusqueda.TabIndex = 0;
            this.gbBusqueda.TabStop = false;
            this.gbBusqueda.Text = "Criterios de búsqueda";
            // 
            // chkDadoBaja
            // 
            this.chkDadoBaja.AutoSize = true;
            this.chkDadoBaja.Location = new System.Drawing.Point(287, 85);
            this.chkDadoBaja.Name = "chkDadoBaja";
            this.chkDadoBaja.Size = new System.Drawing.Size(79, 19);
            this.chkDadoBaja.TabIndex = 8;
            this.chkDadoBaja.Text = "Dado baja";
            this.chkDadoBaja.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarBusqueda
            // 
            this.btnLimpiarBusqueda.Location = new System.Drawing.Point(234, 147);
            this.btnLimpiarBusqueda.Name = "btnLimpiarBusqueda";
            this.btnLimpiarBusqueda.Size = new System.Drawing.Size(156, 23);
            this.btnLimpiarBusqueda.TabIndex = 7;
            this.btnLimpiarBusqueda.Text = "Limpiar Búsqueda";
            this.btnLimpiarBusqueda.UseVisualStyleBackColor = true;
            this.btnLimpiarBusqueda.Click += new System.EventHandler(this.btnLimpiarBusqueda_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(52, 147);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(156, 23);
            this.btnConsultar.TabIndex = 6;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // nudPrecioHasta
            // 
            this.nudPrecioHasta.DecimalPlaces = 2;
            this.nudPrecioHasta.Location = new System.Drawing.Point(127, 107);
            this.nudPrecioHasta.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPrecioHasta.Name = "nudPrecioHasta";
            this.nudPrecioHasta.Size = new System.Drawing.Size(120, 23);
            this.nudPrecioHasta.TabIndex = 5;
            this.nudPrecioHasta.Value = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            // 
            // nudPrecioDesde
            // 
            this.nudPrecioDesde.DecimalPlaces = 2;
            this.nudPrecioDesde.Location = new System.Drawing.Point(127, 73);
            this.nudPrecioDesde.Name = "nudPrecioDesde";
            this.nudPrecioDesde.Size = new System.Drawing.Size(120, 23);
            this.nudPrecioDesde.TabIndex = 4;
            // 
            // lblPrecioHasta
            // 
            this.lblPrecioHasta.AutoSize = true;
            this.lblPrecioHasta.Location = new System.Drawing.Point(27, 107);
            this.lblPrecioHasta.Name = "lblPrecioHasta";
            this.lblPrecioHasta.Size = new System.Drawing.Size(91, 15);
            this.lblPrecioHasta.TabIndex = 3;
            this.lblPrecioHasta.Text = "Precio hasta ($):";
            // 
            // lblPrecioDesde
            // 
            this.lblPrecioDesde.AutoSize = true;
            this.lblPrecioDesde.Location = new System.Drawing.Point(27, 75);
            this.lblPrecioDesde.Name = "lblPrecioDesde";
            this.lblPrecioDesde.Size = new System.Drawing.Size(94, 15);
            this.lblPrecioDesde.TabIndex = 2;
            this.lblPrecioDesde.Text = "Precio desde ($):";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(87, 38);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(191, 23);
            this.txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(27, 41);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            // 
            // gbResultados
            // 
            this.gbResultados.Controls.Add(this.dgvResultados);
            this.gbResultados.Location = new System.Drawing.Point(52, 248);
            this.gbResultados.Name = "gbResultados";
            this.gbResultados.Size = new System.Drawing.Size(710, 234);
            this.gbResultados.TabIndex = 1;
            this.gbResultados.TabStop = false;
            this.gbResultados.Text = "Resultados";
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ColNombre,
            this.ColPrecio,
            this.ColAccion,
            this.ColAccionEditar});
            this.dgvResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultados.Location = new System.Drawing.Point(3, 19);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.RowTemplate.Height = 25;
            this.dgvResultados.Size = new System.Drawing.Size(704, 212);
            this.dgvResultados.TabIndex = 0;
            this.dgvResultados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultados_CellContentClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // ColNombre
            // 
            this.ColNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColNombre.HeaderText = "Nombre";
            this.ColNombre.Name = "ColNombre";
            this.ColNombre.ReadOnly = true;
            // 
            // ColPrecio
            // 
            this.ColPrecio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColPrecio.HeaderText = "Precio";
            this.ColPrecio.Name = "ColPrecio";
            this.ColPrecio.ReadOnly = true;
            // 
            // ColAccion
            // 
            this.ColAccion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColAccion.HeaderText = "Acción";
            this.ColAccion.Name = "ColAccion";
            this.ColAccion.ReadOnly = true;
            this.ColAccion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColAccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColAccion.Text = "Dar de baja";
            this.ColAccion.UseColumnTextForButtonValue = true;
            // 
            // ColAccionEditar
            // 
            this.ColAccionEditar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColAccionEditar.HeaderText = "Acción";
            this.ColAccionEditar.Name = "ColAccionEditar";
            this.ColAccionEditar.ReadOnly = true;
            this.ColAccionEditar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColAccionEditar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColAccionEditar.Text = "Editar";
            this.ColAccionEditar.UseColumnTextForButtonValue = true;
            // 
            // FrmConsultarArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 494);
            this.Controls.Add(this.gbResultados);
            this.Controls.Add(this.gbBusqueda);
            this.Name = "FrmConsultarArticulos";
            this.Text = "Consultar Articulos";
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioHasta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioDesde)).EndInit();
            this.gbResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.Button btnLimpiarBusqueda;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.NumericUpDown nudPrecioHasta;
        private System.Windows.Forms.NumericUpDown nudPrecioDesde;
        private System.Windows.Forms.Label lblPrecioHasta;
        private System.Windows.Forms.Label lblPrecioDesde;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.GroupBox gbResultados;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.CheckBox chkDadoBaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrecio;
        private System.Windows.Forms.DataGridViewButtonColumn ColAccion;
        private System.Windows.Forms.DataGridViewButtonColumn ColAccionEditar;
    }
}