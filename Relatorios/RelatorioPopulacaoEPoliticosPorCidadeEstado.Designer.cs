namespace ProvaMeta.Relatorios
{
    partial class RelatorioPopulacaoEPoliticosPorCidadeEstado
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.estadosCheckList = new System.Windows.Forms.CheckedListBox();
            this.filtrarBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ProvaMeta.Relatorios.PopulacaoPoliticoCidadeEstadoReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 136);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.ShowBackButton = false;
            this.reportViewer1.ShowContextMenu = false;
            this.reportViewer1.ShowCredentialPrompts = false;
            this.reportViewer1.ShowDocumentMapButton = false;
            this.reportViewer1.ShowPrintButton = false;
            this.reportViewer1.Size = new System.Drawing.Size(776, 302);
            this.reportViewer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.filtrarBtn);
            this.groupBox1.Controls.Add(this.estadosCheckList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 118);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro do Relatório";
            // 
            // estadosCheckList
            // 
            this.estadosCheckList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.estadosCheckList.FormattingEnabled = true;
            this.estadosCheckList.Items.AddRange(new object[] {
            "Todos"});
            this.estadosCheckList.Location = new System.Drawing.Point(15, 24);
            this.estadosCheckList.Name = "estadosCheckList";
            this.estadosCheckList.Size = new System.Drawing.Size(541, 79);
            this.estadosCheckList.TabIndex = 0;
            // 
            // filtrarBtn
            // 
            this.filtrarBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtrarBtn.Location = new System.Drawing.Point(562, 41);
            this.filtrarBtn.Name = "filtrarBtn";
            this.filtrarBtn.Size = new System.Drawing.Size(208, 50);
            this.filtrarBtn.TabIndex = 1;
            this.filtrarBtn.Text = "Filtrar";
            this.filtrarBtn.UseVisualStyleBackColor = true;
            // 
            // RelatorioPopulacaoEPoliticosPorCidadeEstado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "RelatorioPopulacaoEPoliticosPorCidadeEstado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório de População e Políticos por Cidade e Estado";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RelatorioPopulacaoEPoliticosPorCidadeEstado_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button filtrarBtn;
        private System.Windows.Forms.CheckedListBox estadosCheckList;
    }
}