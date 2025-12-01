using bailinho_senior_system.repositories;
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace bailinho_senior_system.views
{
    public partial class RelatoriosView : Form
    {
        private RelatoriosRepository relatoriosRepository;

        private Dictionary<int, DataTable> relatoriosCache = new Dictionary<int, DataTable>();

        private DateTime? dataInicioFiltro = null;
        private DateTime? dataFimFiltro = null;

        public RelatoriosView()
        {
            InitializeComponent();
            relatoriosRepository = new RelatoriosRepository();
            ConfigurarBotoesRelatorio();
            ConfigurarFiltrosPeriodo();
        }

        private void ConfigurarFiltrosPeriodo()
        {
            Control containerMeses = this.Controls.Find("pnlPeriodoMeses", true).FirstOrDefault();
            if (containerMeses == null) containerMeses = this;

            DateTime agora = DateTime.Now;

            for (int i = 1; i <= 12; i++)
            {
                // i = 1: Mês passado, i = 12: 12 meses atrás.
                DateTime dataMes = agora.AddMonths(-i);
                string textoMes = dataMes.ToString("MMM/yyyy").ToUpper();

                RadioButton rb = containerMeses.Controls.Find($"rbMes{i}", true).FirstOrDefault() as RadioButton;

                if (rb != null)
                {
                    rb.Text = textoMes;
                    rb.Tag = i;
                    rb.CheckedChanged += FiltroPeriodo_CheckedChanged;
                }
            }

            List<string> rbsAgrupamento = new List<string> { "rbUltimos3Meses", "rbUltimos6Meses", "rbUltimos9Meses", "rbUltimos12Meses", "rbTotal" };
            foreach (var rbName in rbsAgrupamento)
            {
                RadioButton rb = this.Controls.Find(rbName, true).FirstOrDefault() as RadioButton;
                if (rb != null)
                {
                    rb.CheckedChanged += FiltroPeriodo_CheckedChanged;
                }
            }
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = true;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;

            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 30;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarBotoesRelatorio()
        {
            List<(RadioButton rb, string descricao)> botoes = new List<(RadioButton, string)>
            {
                (rbRelatorio1, ObterTitulo(1)), (rbRelatorio2, ObterTitulo(2)), (rbRelatorio3, ObterTitulo(3)),
                (rbRelatorio4, ObterTitulo(4)), (rbRelatorio5, ObterTitulo(5)), (rbRelatorio6, ObterTitulo(6)),
                (rbRelatorio7, ObterTitulo(7)), (rbRelatorio8, ObterTitulo(8)), (rbRelatorio9, ObterTitulo(9)),
                (rbRelatorio10, ObterTitulo(10))
            };

            foreach (var item in botoes)
            {
                if (item.rb != null)
                {
                    item.rb.Text = $"{botoes.IndexOf(item) + 1}: {item.descricao}";
                    item.rb.CheckedChanged += RadioButton_CheckedChanged;
                }
            }
        }

        private void RelatoriosView_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView(dgvRelatorios);

            // 1. Inicializa o filtro de data
            RadioButton rbTotal = this.Controls.Find("rbTotal", true).FirstOrDefault() as RadioButton;
            if (rbTotal != null) rbTotal.Checked = true;

            // 2. Atualiza os dados numericos
            AtualizarDadosEscalares();

            // 3. Força o carregamento do primeiro relatório na inicialização
            if (rbRelatorio1 != null)
            {
                rbRelatorio1.Checked = true;
            }
        }

        // Calcula o mês exato e a data de fim
        private void FiltroPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                dataInicioFiltro = null;
                dataFimFiltro = null;

                // Determina a data de início e fim
                if (rb.Name.StartsWith("rbMes") && rb.Tag is int mesesAtras)
                {
                    // Lógica para MÊS EXATO (ex: só Maio/2025)
                    DateTime dataMes = DateTime.Today.AddMonths(-mesesAtras);

                    dataInicioFiltro = new DateTime(dataMes.Year, dataMes.Month, 1);
                    // dataFimFiltro é o primeiro dia do mês seguinte (exclusivo no WHERE)
                    dataFimFiltro = dataInicioFiltro.Value.AddMonths(1).AddDays(-1);
                }
                else if (rb.Name.Contains("Ultimos"))
                {
                    // Lógica para AGRUPAMENTO (ex: últimos 6 meses)
                    int meses = 0;
                    if (rb.Name.Contains("3")) meses = 3;
                    else if (rb.Name.Contains("6")) meses = 6;
                    else if (rb.Name.Contains("9")) meses = 9;
                    else if (rb.Name.Contains("12")) meses = 12;

                    if (meses > 0)
                    {
                        DateTime dataPeriodo = DateTime.Today.AddMonths(-meses);
                        dataInicioFiltro = new DateTime(dataPeriodo.Year, dataPeriodo.Month, 1);
                        // dataFimFiltro permanece null, para filtrar até o dia de hoje.
                    }
                }

                // Limpa o cache, pois o período mudou
                relatoriosCache.Clear();

                // Atualiza todos os valores numericos
                AtualizarDadosEscalares();

                // Recarrega o relatório atualmente selecionado
                var rbAtual = this.Controls.OfType<RadioButton>()
                                  .Where(r => r.Checked && r.Name.StartsWith("rbRelatorio"))
                                  .FirstOrDefault();

                if (rbAtual != null && int.TryParse(rbAtual.Name.Replace("rbRelatorio", ""), out int numeroRelatorio))
                {
                    SelecionarRelatorio(numeroRelatorio);
                }
            }
        }

        private void AtualizarDadosEscalares()
        {
            try
            {
                this.totalDeVendas.Text = relatoriosRepository.GetTotalVendas(dataInicioFiltro, dataFimFiltro).ToString();
                this.totalVendido.Text = "R$ " + relatoriosRepository.getTotalVendido(dataInicioFiltro, dataFimFiltro).ToString("F2");
                this.ticketMedio.Text = "R$ " + relatoriosRepository.getTicketMedio(dataInicioFiltro, dataFimFiltro).ToString("F2");

                this.eventosRealizados.Text = relatoriosRepository.getEventosRealizados(dataInicioFiltro, dataFimFiltro).ToString();

                this.qntdFornecedores.Text = relatoriosRepository.getFornecedoresCadastrados().ToString();
                this.qntdClientes.Text = relatoriosRepository.getClientesCadastrados().ToString();

                this.estoqueTotal.Text = relatoriosRepository.getEstoqueTotalProdutos().ToString();
                this.qntdProduto.Text = relatoriosRepository.getProdutosCadastrados().ToString();
                this.produtosVendidos.Text = relatoriosRepository.getProdutosVendidos(dataInicioFiltro, dataFimFiltro).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar os totais: {ex.Message}", "Erro de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                if (int.TryParse(rb.Name.Replace("rbRelatorio", ""), out int numeroRelatorio))
                {
                    SelecionarRelatorio(numeroRelatorio);
                }
            }
        }

        private void SelecionarRelatorio(int numeroRelatorio)
        {
            try
            {
                DataTable data;
                string tituloCompleto = ObterTitulo(numeroRelatorio);

                bool isRelatorioEstatico = (numeroRelatorio == 4 || numeroRelatorio == 8 || numeroRelatorio == 10);

                if (dataInicioFiltro == null && isRelatorioEstatico && relatoriosCache.ContainsKey(numeroRelatorio))
                {
                    data = relatoriosCache[numeroRelatorio];
                }
                else
                {
                    data = CarregarDadosRelatorio(numeroRelatorio, dataInicioFiltro, dataFimFiltro);

                    if (dataInicioFiltro == null && isRelatorioEstatico)
                    {
                        if (relatoriosCache.ContainsKey(numeroRelatorio)) relatoriosCache[numeroRelatorio] = data;
                        else relatoriosCache.Add(numeroRelatorio, data);
                    }
                }

                if (lblTituloRelatorio != null)
                {
                    string periodo;
                    if (dataInicioFiltro.HasValue)
                    {
                        if (dataFimFiltro.HasValue)
                        {
                            // Mês exato
                            periodo = $" (Mês de {dataInicioFiltro.Value:MMMM/yyyy})";
                        }
                        else
                        {
                            // Agrupamento (Ultimos X meses)
                            periodo = $" (A partir de {dataInicioFiltro.Value:dd/MM/yyyy})";
                        }
                    }
                    else
                    {
                        periodo = " (Total)";
                    }

                    lblTituloRelatorio.Text = $"Relatório {numeroRelatorio}: {tituloCompleto} {periodo}";
                }

                if (dgvRelatorios != null)
                {
                    dgvRelatorios.DataSource = data;
                    dgvRelatorios.ReadOnly = true;

                    if (data == null || data.Rows.Count == 0)
                    {
                        MessageBox.Show($"Nenhum dado encontrado para o relatório {numeroRelatorio}: {tituloCompleto}", "Resultado Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                if (lblTituloRelatorio != null)
                {
                    lblTituloRelatorio.Text = $"Erro ao carregar o relatório {numeroRelatorio}";
                }
                MessageBox.Show($"Erro ao carregar o relatório: {ex.Message}", "Erro de Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dgvRelatorios != null) dgvRelatorios.DataSource = null;
            }
        }

        private DataTable CarregarDadosRelatorio(int numeroRelatorio, DateTime? startDate, DateTime? endDate)
        {
            switch (numeroRelatorio)
            {
                case 1:
                    return relatoriosRepository.GetProdutosMaisVendidos(startDate, endDate);
                case 2:
                    return relatoriosRepository.GetVendasPorCliente(startDate, endDate);
                case 3:
                    return relatoriosRepository.GetProdutosVendidosPorEvento(startDate, endDate);
                case 4:
                    return relatoriosRepository.GetProdutosPorFornecedor();
                case 5:
                    return relatoriosRepository.GetItensVendidosPorCategoria(startDate, endDate);
                case 6:
                    return relatoriosRepository.GetFaturamentoTotalPorEvento(startDate, endDate);
                case 7:
                    return relatoriosRepository.GetFormaPagamentoMaisUtilizada(startDate, endDate);
                case 8:
                    return relatoriosRepository.GetProdutosBaixoEstoque(20);
                case 9:
                    return relatoriosRepository.GetTop5ClientesMaisGastaram(startDate, endDate);
                case 10:
                    return relatoriosRepository.GetPrecoMedioPorCategoria();
                default:
                    throw new ArgumentException($"Relatório {numeroRelatorio} não encontrado.");
            }
        }

        private string ObterTitulo(int numeroRelatorio)
        {
            switch (numeroRelatorio)
            {
                case 1: return "Produtos Mais Vendidos";
                case 2: return "Número de Vendas por Cliente";
                case 3: return "Quantidade de Produtos Vendidos por Evento";
                case 4: return "Número de Produtos Diferentes Fornecidos por Fornecedor";
                case 5: return "Quantidade de Produtos Vendidos por Categoria";
                case 6: return "Faturamento Total por Evento";
                case 7: return "Forma de Pagamento Mais Utilizada";
                case 8: return "Produtos com Estoque Baixo (Limite <= 20)";
                case 9: return "Top 5 Clientes que Mais Gastaram";
                case 10: return "Média de Preço dos Produtos por Categoria";
                default: return "Relatório Desconhecido";
            }
        }
    }
}