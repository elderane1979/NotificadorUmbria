namespace NotificadorMensajesUmbria
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.tbResumen = new System.Windows.Forms.TextBox();
            this.BtnComprobar = new System.Windows.Forms.Button();
            this.LblLast = new System.Windows.Forms.Label();
            this.LblNext = new System.Windows.Forms.Label();
            this.BtnConfiguración = new System.Windows.Forms.Button();
            this.timerProgreso = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.comprogarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblMsgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.irANovedadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.maximizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNovedades = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(13, 92);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(351, 23);
            this.ProgressBar.TabIndex = 4;
            // 
            // tbResumen
            // 
            this.tbResumen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbResumen.Location = new System.Drawing.Point(13, 122);
            this.tbResumen.Multiline = true;
            this.tbResumen.Name = "tbResumen";
            this.tbResumen.ReadOnly = true;
            this.tbResumen.Size = new System.Drawing.Size(351, 111);
            this.tbResumen.TabIndex = 5;
            // 
            // BtnComprobar
            // 
            this.BtnComprobar.Location = new System.Drawing.Point(15, 44);
            this.BtnComprobar.Name = "BtnComprobar";
            this.BtnComprobar.Size = new System.Drawing.Size(75, 23);
            this.BtnComprobar.TabIndex = 2;
            this.BtnComprobar.Text = "Comprobar";
            this.BtnComprobar.UseVisualStyleBackColor = true;
            this.BtnComprobar.Click += new System.EventHandler(this.BtnComprobar_Click);
            // 
            // LblLast
            // 
            this.LblLast.AutoSize = true;
            this.LblLast.Location = new System.Drawing.Point(12, 9);
            this.LblLast.Name = "LblLast";
            this.LblLast.Size = new System.Drawing.Size(112, 13);
            this.LblLast.TabIndex = 0;
            this.LblLast.Text = "Última comprobación: ";
            // 
            // LblNext
            // 
            this.LblNext.AutoSize = true;
            this.LblNext.Location = new System.Drawing.Point(12, 28);
            this.LblNext.Name = "LblNext";
            this.LblNext.Size = new System.Drawing.Size(125, 13);
            this.LblNext.TabIndex = 1;
            this.LblNext.Text = "Siguiente Comprobación:";
            // 
            // BtnConfiguración
            // 
            this.BtnConfiguración.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnConfiguración.Location = new System.Drawing.Point(289, 44);
            this.BtnConfiguración.Name = "BtnConfiguración";
            this.BtnConfiguración.Size = new System.Drawing.Size(75, 23);
            this.BtnConfiguración.TabIndex = 3;
            this.BtnConfiguración.Text = "Configurar";
            this.BtnConfiguración.UseVisualStyleBackColor = true;
            this.BtnConfiguración.Click += new System.EventHandler(this.BtnConfiguración_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Notificador Novedades Umbria";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comprogarToolStripMenuItem,
            this.configurarToolStripMenuItem,
            this.toolStripMenuItem2,
            this.lblMsgToolStripMenuItem,
            this.irANovedadesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.maximizarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 148);
            // 
            // comprogarToolStripMenuItem
            // 
            this.comprogarToolStripMenuItem.Name = "comprogarToolStripMenuItem";
            this.comprogarToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.comprogarToolStripMenuItem.Text = "Comprobar";
            this.comprogarToolStripMenuItem.Click += new System.EventHandler(this.comprogarToolStripMenuItem_Click);
            // 
            // configurarToolStripMenuItem
            // 
            this.configurarToolStripMenuItem.Name = "configurarToolStripMenuItem";
            this.configurarToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.configurarToolStripMenuItem.Text = "Configurar";
            this.configurarToolStripMenuItem.Click += new System.EventHandler(this.configurarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(150, 6);
            // 
            // lblMsgToolStripMenuItem
            // 
            this.lblMsgToolStripMenuItem.Name = "lblMsgToolStripMenuItem";
            this.lblMsgToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.lblMsgToolStripMenuItem.Text = "sin Comprobar";
            this.lblMsgToolStripMenuItem.Click += new System.EventHandler(this.lblMsgToolStripMenuItem_Click);
            // 
            // irANovedadesToolStripMenuItem
            // 
            this.irANovedadesToolStripMenuItem.Name = "irANovedadesToolStripMenuItem";
            this.irANovedadesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.irANovedadesToolStripMenuItem.Text = "Ir a Novedades";
            this.irANovedadesToolStripMenuItem.Click += new System.EventHandler(this.irANovedadesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 6);
            // 
            // maximizarToolStripMenuItem
            // 
            this.maximizarToolStripMenuItem.Name = "maximizarToolStripMenuItem";
            this.maximizarToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.maximizarToolStripMenuItem.Text = "Maximizar";
            this.maximizarToolStripMenuItem.Click += new System.EventHandler(this.maximizarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // btnNovedades
            // 
            this.btnNovedades.Location = new System.Drawing.Point(97, 44);
            this.btnNovedades.Name = "btnNovedades";
            this.btnNovedades.Size = new System.Drawing.Size(186, 23);
            this.btnNovedades.TabIndex = 6;
            this.btnNovedades.Text = "Ir a Novedades";
            this.btnNovedades.UseVisualStyleBackColor = true;
            this.btnNovedades.Click += new System.EventHandler(this.btnNovedades_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 247);
            this.Controls.Add(this.btnNovedades);
            this.Controls.Add(this.LblNext);
            this.Controls.Add(this.LblLast);
            this.Controls.Add(this.BtnConfiguración);
            this.Controls.Add(this.BtnComprobar);
            this.Controls.Add(this.tbResumen);
            this.Controls.Add(this.ProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Notificador Mensajes Umbría";
            this.Shown += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.TextBox tbResumen;
        private System.Windows.Forms.Button BtnComprobar;
        private System.Windows.Forms.Label LblLast;
        private System.Windows.Forms.Label LblNext;
        private System.Windows.Forms.Button BtnConfiguración;
        private System.Windows.Forms.Timer timerProgreso;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem comprogarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem maximizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem irANovedadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lblMsgToolStripMenuItem;
        private System.Windows.Forms.Button btnNovedades;
    }
}

