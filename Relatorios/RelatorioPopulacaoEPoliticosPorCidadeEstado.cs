using ProvaMeta.DTO;
using ProvaMeta.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ProvaMeta.Relatorios
{
    public partial class RelatorioPopulacaoEPoliticosPorCidadeEstado : Form
    {
        Repositorio.ProvaMetaDbContext context;
        private DbSet<Estado> estadosRef;
        System.Collections.Generic.List<Estado> estadosList;

        public RelatorioPopulacaoEPoliticosPorCidadeEstado(Repositorio.ProvaMetaDbContext dbContext, System.Data.Entity.DbSet<Model.Estado> estadosRef)
        {
            InitializeComponent();
            this.context = dbContext;

            this.filtrarBtn.Click += FiltrarBtn_Click;
            this.estadosRef = estadosRef;

            this.estadosCheckList.ItemCheck += EstadosCheckList_ItemCheck;
        }

        #region eventos click
        private void EstadosCheckList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(e.Index == 0)
            {
                for (int i = 1; i < estadosCheckList.Items.Count; i++)
                {
                    estadosCheckList.SetItemChecked(i, e.NewValue== CheckState.Checked);
                }
            }
        }

        private void FiltrarBtn_Click(object sender, EventArgs e)
        {
            this.SetupFormControls(true);
            List<int> estadosFiltrar = new List<int>();
            foreach (var item in this.estadosCheckList.CheckedItems)
            {
                var indexItem = this.estadosCheckList.Items.IndexOf(item) - 1;
                if (indexItem == -1) continue;
                
                var selectedItem = this.estadosList[indexItem];
                estadosFiltrar.Add(selectedItem.EstadoId);
            }
            
            var estados = context.Estados
                .Where(estado => estadosFiltrar.Any(estadoSelecionado => estadoSelecionado == estado.EstadoId))
                .Include("Cidades").ToList();
            var dataSource = estados.SelectMany(estado => estado.Cidades).Select(cidade => new CidadeEstadoDTO
            {
                CidadeId = cidade.CidadeId,
                NomeCidade = cidade.Nome,
                NomePrefeito = cidade.NomePrefeito,
                Habitantes = cidade.Habitantes,
                QuantidadeVereadores = cidade.QuantidadeVereadores,
                EstadoId = cidade.Estado.EstadoId,
                EstadoNome = cidade.Estado.Nome,
                QuantidadeDeputados = cidade.Estado.QuantidadeDeputados,
                NomeGovernador = cidade.Estado.NomeGovernador,
                TotalPoliticos = cidade.Estado.QuantidadeDeputados + cidade.Estado.Cidades.Sum(c => c.QuantidadeVereadores),
                TotalHabitantes = cidade.Estado.Cidades.Sum(c => c.Habitantes)
            });
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetRel", dataSource));
            this.reportViewer1.RefreshReport();

            this.SetupFormControls(false);
        }
        #endregion

        #region eventos setup tela
        private void RelatorioPopulacaoEPoliticosPorCidadeEstado_Load(object sender, EventArgs e)
        {
            this.estadosList = this.estadosRef.ToList();
            estadosList.ForEach(estado =>
            {
                this.estadosCheckList.Items.Add(estado.Nome);
            });
        }

        public void SetupFormControls(bool loading)
        {
            this.filtrarBtn.Enabled = !loading;
            this.estadosCheckList.Enabled = !loading;

            if (loading)
            {
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion
    }
}
