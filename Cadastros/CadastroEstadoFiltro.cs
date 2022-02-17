using ProvaMeta.Model;
using ProvaMeta.Repositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ProvaMeta.Cadastros
{
    public partial class CadastroEstadoFiltro : Form
    {
        List<Estado> dataSource;
        public Estado SelectedEntity = null;
        private ProvaMetaDbContext dbContext;

        public CadastroEstadoFiltro()
        {
            InitializeComponent();
        }

        public CadastroEstadoFiltro(ProvaMetaDbContext dbContext) : this()
        {
            this.dbContext = dbContext;
        }

        private async void pesquisarBtn_Click(object sender, EventArgs e)
        {
            var textSearch = this.filtroConsultaTextBox.Text;
            this.SetupFormControls(true);
            try
            {
                this.dataSource =
                await dbContext.Estados
                .Where(estado => estado.Nome.Contains(textSearch))
                .ToListAsync();
                this.consultaDataGridView.DataSource = dataSource;
                SetupGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de consulta");
            }
            finally
            {
                this.SetupFormControls(false);
            }
            

        }

        #region Métodos de bind e setup de tela

        private void SetupGrid()
        {
            this.consultaDataGridView.Columns[0].HeaderText = "Código";
            this.consultaDataGridView.Columns[1].HeaderText = "Nome do Estado";
            this.consultaDataGridView.Columns[2].HeaderText = "Governador";
            this.consultaDataGridView.Columns[3].HeaderText = "Quantidade de Deputados";
            foreach (DataGridViewColumn column in this.consultaDataGridView.Columns)
            {
                column.Visible = false;
            }
            this.consultaDataGridView.Columns[0].Visible = true;
            this.consultaDataGridView.Columns[1].Visible = true;
            this.consultaDataGridView.Columns[2].Visible = true;
            this.consultaDataGridView.Columns[3].Visible = true;
        }

        private void consultaDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedEntity = this.dataSource[e.RowIndex];
            this.SelectedEntity = selectedEntity;
            this.Close();
        }

        public void SetupFormControls(bool pesquisando)
        {
            this.pesquisarBtn.Enabled = !pesquisando;
            if (pesquisando)
            {
                this.pictureBox1.Show();
                this.pictureBox1.Update();
            }
            else
            {
                pictureBox1.Hide();
            }
        }
        #endregion
    }
}
