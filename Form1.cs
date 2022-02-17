using ProvaMeta.Cadastros;
using ProvaMeta.Model;
using ProvaMeta.Relatorios;
using ProvaMeta.Repositorio;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ProvaMeta
{
    public partial class Form1 : Form
    {
        ProvaMetaDbContext dbContext = new ProvaMetaDbContext();
        private DbSet<Estado> estadosRef;

        public Form1()
        {
            InitializeComponent();
            this.estadosRef = dbContext.Estados;
        }

        #region eventos click

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formCidades = new CadastroCidade(dbContext))
            {
                formCidades.ShowDialog();
            }
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formEstados = new CadastroEstado(dbContext))
            {
                formEstados.ShowDialog();
            }
        }
       

        private void relatórioDePolíticosEPopulaçãoPorCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formRel = new RelatorioPopulacaoEPoliticosPorCidadeEstado(dbContext, estadosRef))
            {
                formRel.ShowDialog();
            }
        }

        #endregion

        #region eventos de setup de tela
        public void SetupFormControls(bool loading)
        {
            this.menuStrip1.Enabled = !loading;

            if (loading)
            {
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.SetupFormControls(true);

            //Carregamento inicial de dados sensíveis ao funcionamento do sistema e conexão inicial com banco de dados
            var estados = estadosRef.ToList();
            this.SetupFormControls(false);
        }
        #endregion
    }
}
