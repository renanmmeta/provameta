using ProvaMeta.Extensions;
using ProvaMeta.Model;
using ProvaMeta.Repositorio;
using System;
using System.Windows.Forms;

namespace ProvaMeta.Cadastros
{
    public partial class CadastroEstado : Form
    {
        Estado entityView;
        private ProvaMetaDbContext dbContext;

        #region construtores
        public CadastroEstado()
        {
            InitializeComponent();
            this.SetStateFormControls(false);
            this.SetStateToolstripButons(false);
            this.SetupTelaSemEntidade();
            this.SetupEventosFormulario();
        }

        public CadastroEstado(ProvaMetaDbContext dbContext) : this()
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Métodos Click
        private void btnConsultarClicked(object sender, EventArgs e)
        {
            var cadastroEstadoFiltroForm = new CadastroEstadoFiltro(dbContext);
            cadastroEstadoFiltroForm.ShowDialog();
            if (cadastroEstadoFiltroForm.SelectedEntity != null)
            {
                this.entityView = cadastroEstadoFiltroForm.SelectedEntity;
                this.BindEntityToView();
            }
        }

        private void btnExcluirClicked(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Deseja excluir o registro de estado " + this.entityView.Nome + "?",
                "Confirmação de exclusão",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    this.dbContext.Estados.Remove(this.entityView);
                    this.dbContext.SaveChanges();
                }
                catch (Exception ex)
                {

                    var messageError = MessageBox.Show(ex.Message, "Erro ao excluir estado");
                }
                finally
                {
                    this.dbContext.Entry(this.entityView).State = System.Data.Entity.EntityState.Detached;
                    this.SetupTelaSemEntidade();
                }
            }
        }

        private void btnSalvarClicked(object sender, EventArgs e)
        {
            this.BindViewToEntity();
            if (this.entityView.EstadoId == 0)
            {
                this.dbContext.Estados.Add(this.entityView);
            }
            else
            {
                this.dbContext.Entry(entityView).State = System.Data.Entity.EntityState.Modified;
            }
            try
            {
                this.dbContext.SaveChanges();
                var successMessage = MessageBox.Show("Cadastro salvo com sucesso");
            }
            catch (Exception ex)
            {
                var messageError = MessageBox.Show(ex.Message, "Erro ao salvar estado");
            }
            finally
            {
                this.SetupTelaSemEntidade();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.entityView = new Estado();
            this.SetStateToolstripButons(true);
            this.SetStateFormControls(true);
            this.BindEntityToView();
        }
       
        private void cancelarBtn_Click(object sender, EventArgs e)
        {
            this.SetupTelaSemEntidade();
        }
        #endregion

        #region Métodos de bind e setup de tela
        /// <summary>
        /// Faz o bind da entity para a view e seta o estado de botões toolstrip
        /// </summary>
        public void BindEntityToView()
        {
            this.codigoTextBox.Text = this.entityView?.EstadoId.ToString();
            this.nomeTextBox.Text = this.entityView?.Nome;
            this.nomeGovernadorTextBox.Text = this.entityView?.NomeGovernador;
            this.quantidadeDeputadosTextBox.Text = this.entityView?.QuantidadeDeputados.ToString();
            this.SetStateToolstripButons(this.entityView != null);
            this.SetStateFormControls(this.entityView != null);
        }
        public void BindViewToEntity()
        {
            this.entityView.Nome = this.nomeTextBox.Text;
            this.entityView.NomeGovernador = this.nomeGovernadorTextBox.Text;
            this.entityView.QuantidadeDeputados = this.quantidadeDeputadosTextBox.Text.GetValueOrDefault(0);
        }

        public void SetStateToolstripButons(bool cadastroAberto)
        {
            this.btnAdd.Enabled = !cadastroAberto;
            this.consultarBtn.Enabled = !cadastroAberto;
            this.btnExcluir.Enabled = cadastroAberto && this.entityView?.EstadoId > 0;
            this.btnSalvar.Enabled = cadastroAberto;
            this.cancelarBtn.Enabled = cadastroAberto;

        }

        public void SetStateFormControls(bool cadastroAberto)
        {
            this.codigoTextBox.Enabled = !cadastroAberto;
            this.nomeTextBox.Enabled = cadastroAberto;
            this.nomeGovernadorTextBox.Enabled = cadastroAberto;
            this.quantidadeDeputadosTextBox.Enabled = cadastroAberto;
        }

        public void SetupTelaSemEntidade()
        {
            this.entityView = null;
            this.BindEntityToView();
        }

        private void SetupEventosFormulario()
        {
            this.consultarBtn.Click += btnConsultarClicked;
            this.btnAdd.Click += btnAdd_Click;
            this.btnSalvar.Click += btnSalvarClicked;
            this.btnExcluir.Click += btnExcluirClicked;
            this.cancelarBtn.Click += cancelarBtn_Click;
        }
        #endregion
    }
}
