using ProvaMeta.DTO;
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
    public partial class CadastroCidadeFiltro : Form
    {
        List<Cidade> dataSourceRef;
        List<CidadeEstadoDTO> dataSourceGrid = new List<CidadeEstadoDTO>();
        public Cidade SelectedEntity = null;
        private ProvaMetaDbContext dbContext;

        public CadastroCidadeFiltro()
        {
            InitializeComponent();
        }

        public CadastroCidadeFiltro(ProvaMetaDbContext dbContext) : this()
        {
            this.dbContext = dbContext;
        }

        private async void pesquisarBtn_Click(object sender, EventArgs e)
        {
            
            var textSearch = this.filtroConsultaTextBox.Text;

            this.SetupFormControls(true);

            try
            {
                this.dataSourceRef = await dbContext.Cidades.Include("Estado")
                .Where(estado => estado.Nome.Contains(textSearch))
                .ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dataSourceRef != null)
                {
                    this.dataSourceGrid = dataSourceRef.Select(cidade => new CidadeEstadoDTO
                    {
                        CidadeId = cidade.CidadeId,
                        EstadoId = cidade.EstadoId,
                        EstadoNome = cidade.Estado?.Nome,
                        Habitantes = cidade.Habitantes,
                        NomeCidade = cidade.Nome,
                        NomePrefeito = cidade.NomePrefeito,
                        QuantidadeVereadores = cidade.QuantidadeVereadores
                    }).ToList();
                    this.consultaDataGridView.DataSource = this.dataSourceGrid;
                }
                SetupGrid();
                this.SetupFormControls(false);
            }
        }

        #region Métodos de bind e setup de tela

        private void SetupGrid()
        {
            this.consultaDataGridView.Columns[0].HeaderText = "Código";
            this.consultaDataGridView.Columns[1].HeaderText = "Nome da Cidade";
            this.consultaDataGridView.Columns[2].HeaderText = "Prefeito";
            this.consultaDataGridView.Columns[3].HeaderText = "Quantidade de Vereadores";
            this.consultaDataGridView.Columns[4].HeaderText = "Quantidade de Habitantes";
            this.consultaDataGridView.Columns[5].HeaderText = "Estado";
            foreach (DataGridViewColumn column in this.consultaDataGridView.Columns)
            {
                column.Visible = false;
            }
            this.consultaDataGridView.Columns[0].Visible = true;
            this.consultaDataGridView.Columns[1].Visible = true;
            this.consultaDataGridView.Columns[2].Visible = true;
            this.consultaDataGridView.Columns[3].Visible = true;
            this.consultaDataGridView.Columns[4].Visible = true;
            this.consultaDataGridView.Columns[5].Visible = true;
        }

        private void consultaDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedEntity = this.dataSourceRef[e.RowIndex];
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
