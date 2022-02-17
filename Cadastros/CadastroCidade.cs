using ProvaMeta.Extensions;
using ProvaMeta.Model;
using ProvaMeta.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProvaMeta.Cadastros
{
    public partial class CadastroCidade : Form
    {
        Cidade entityView;
        private ProvaMetaDbContext dbContext;
        private List<Estado> estados;

        public CadastroCidade()
        {
            InitializeComponent();
            this.SetStateFormControls(false);
            this.SetStateToolstripButons(false);
            this.SetupTelaSemEntidade();
            SetupEventosFormulario();
        }

       


        public CadastroCidade(ProvaMetaDbContext dbContext) : this()
        {
            this.dbContext = dbContext;
        }

        #region métodos click
        private void consultarBtn_Click(object sender, EventArgs e)
        {
            var cadastroEstadoFiltroForm = new CadastroCidadeFiltro(dbContext);
            cadastroEstadoFiltroForm.ShowDialog();
            if (cadastroEstadoFiltroForm.SelectedEntity != null)
            {
                this.entityView = cadastroEstadoFiltroForm.SelectedEntity;
                this.BindEntityToView();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            this.btnSalvar.Enabled = false;

            this.BindViewToEntity();
            if (this.entityView.CidadeId == 0)
            {
                this.dbContext.Cidades.Add(this.entityView);
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
                var messageError = MessageBox.Show(ex.Message, "Erro ao salvar cidade");
            }
            finally
            {
                this.SetupTelaSemEntidade();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.entityView = new Cidade();
            this.SetStateToolstripButons(true);
            this.SetStateFormControls(true);
            this.BindEntityToView();
        }

        private void cancelarBtn_Click(object sender, EventArgs e)
        {
            this.SetupTelaSemEntidade();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Deseja excluir o registro de cidade " + this.entityView.Nome + "?",
                "Confirmação de exclusão",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    this.dbContext.Cidades.Remove(this.entityView);
                    this.dbContext.SaveChanges();
                    
                }
                catch (Exception ex)
                {

                    var messageError = MessageBox.Show(ex.Message, "Erro ao excluir cidade");
                }
                finally
                {
                    this.dbContext.Entry(this.entityView).State = System.Data.Entity.EntityState.Detached;
                    this.SetupTelaSemEntidade();
                }
            }
        }
        #endregion

        #region Métodos de bind e setup de tela
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
            this.nomePrefeitoTextBox.Enabled = cadastroAberto;
            this.quantidadeVereadoresTextBox.Enabled = cadastroAberto;
            this.estadoComboBox.Enabled = cadastroAberto;
            this.quantidadeHabitantesTextBox.Enabled = cadastroAberto;
        }

        public void SetupTelaSemEntidade()
        {
            this.entityView = null;
            this.BindEntityToView();
        }

        private void SetupComboEstados()
        {
            try
            {
                estados = this.dbContext.Estados.ToList();
                BeginInvoke((Action)(() =>
                {
                    this.estadoComboBox.Items.Clear();
                    estados.ForEach(estado =>
                    {
                        this.estadoComboBox.Items.Add(estado.Nome);
                    });
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não foi possível recuperar a lista de estados.");
            }
            
            
        }
        public void BindEntityToView()
        {
            this.codigoTextBox.Text = this.entityView?.EstadoId.ToString();
            this.nomeTextBox.Text = this.entityView?.Nome;
            this.nomePrefeitoTextBox.Text = this.entityView?.NomePrefeito;
            this.quantidadeVereadoresTextBox.Text = this.entityView?.QuantidadeVereadores.ToString();
            this.quantidadeHabitantesTextBox.Text = this.entityView?.Habitantes.ToString();

            if(this.entityView != null)
            {
                if(this.estadoComboBox.Items.Count > 0)
                { 
                    var ufSelecionada = this.estados.FirstOrDefault(x => x.EstadoId == this.entityView?.EstadoId);
                    var indexUFSelecionada = this.estados.IndexOf(ufSelecionada);
                    this.estadoComboBox.SelectedIndex = indexUFSelecionada;
                }
            } else
            {
                this.estadoComboBox.SelectedIndex = -1;
            }

            this.SetStateToolstripButons(this.entityView != null);
            this.SetStateFormControls(this.entityView != null);
        }
        public void BindViewToEntity()
        {
            this.entityView.Nome = this.nomeTextBox.Text;
            this.entityView.NomePrefeito = this.nomePrefeitoTextBox.Text;
            this.entityView.QuantidadeVereadores = this.quantidadeVereadoresTextBox.Text.GetValueOrDefault(0);
            this.entityView.Habitantes = this.quantidadeHabitantesTextBox.Text.GetValueOrDefault(0);

            if(this.estadoComboBox.SelectedIndex > -1)
            {
                this.entityView.EstadoId = this.estados[this.estadoComboBox.SelectedIndex].EstadoId;
                this.entityView.Estado = this.estados[this.estadoComboBox.SelectedIndex];
            }
            
        }

        private void SetupEventosFormulario()
        {
            this.consultarBtn.Click += consultarBtn_Click;
            this.btnAdd.Click += btnAdd_Click;
            this.btnSalvar.Click += btnSalvar_Click;
            this.btnExcluir.Click += btnExcluir_Click;
            this.cancelarBtn.Click += cancelarBtn_Click;
        }

        private void CadastroCidade_Shown(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                this.SetupComboEstados();
            });
        }
        #endregion
    }
}
