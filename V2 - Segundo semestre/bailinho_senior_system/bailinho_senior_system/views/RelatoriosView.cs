using bailinho_senior_system.repositories;
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq; // Adicionei using System.Linq, pois pode ser necessário

namespace bailinho_senior_system.views
{
    public partial class RelatoriosView : Form
    {
        private RelatoriosRepository relatoriosRepository;

        // Dicionário de cache: Armazenará os dados (DataTable) para não consultar o DB repetidamente
        private Dictionary<int, DataTable> relatoriosCache = new Dictionary<int, DataTable>();


        public RelatoriosView()
        {
            InitializeComponent();
            relatoriosRepository = new RelatoriosRepository();
            ConfigurarBotoesRelatorio();
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            // Configurações visuais e de comportamento (replicadas de ClientesView)
            dgv.AutoGenerateColumns = true; // Mantido true para DataTables de relatórios
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;

            // Configurações de Aparência (replicadas de ClientesView)
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 30;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Não limpamos as colunas, pois DataTables serão atribuídos
        }

        private void ConfigurarBotoesRelatorio()
        {
            // Mapeia os RadioButtons e define seus textos descritivos
            List<(RadioButton rb, string descricao)> botoes = new List<(RadioButton, string)>
            {
                (rbRelatorio1, ObterTitulo(1)),
                (rbRelatorio2, ObterTitulo(2)),
                (rbRelatorio3, ObterTitulo(3)),
                (rbRelatorio4, ObterTitulo(4)),
                (rbRelatorio5, ObterTitulo(5)),
                (rbRelatorio6, ObterTitulo(6)),
                (rbRelatorio7, ObterTitulo(7)),
                (rbRelatorio8, ObterTitulo(8)),
                (rbRelatorio9, ObterTitulo(9)),
                (rbRelatorio10, ObterTitulo(10))
            };

            foreach (var item in botoes)
            {
                if (item.rb != null)
                {
                    // Define o texto usando a descrição completa
                    item.rb.Text = $"{botoes.IndexOf(item) + 1}: {item.descricao}";
                    // Adiciona o manipulador de eventos (caso não tenha sido feito no designer)
                    item.rb.CheckedChanged += RadioButton_CheckedChanged;
                }
            }
        }


        private void RelatoriosView_Load(object sender, EventArgs e)
        {
            // Adicionado: Configura o DataGridView com o estilo padronizado
            ConfigurarDataGridView(dgvRelatorios);

            // Força o carregamento do primeiro relatório na inicialização
            if (rbRelatorio1 != null)
            {
                rbRelatorio1.Checked = true;
                SelecionarRelatorio(1);
            }
        }

        // Método que é chamado por todos os eventos CheckedChanged dos RadioButtons
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                // Extrai o número do relatório do nome do RadioButton (ex: rbRelatorio1 -> 1)
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

                // 1. Tenta pegar do cache
                if (relatoriosCache.ContainsKey(numeroRelatorio))
                {
                    data = relatoriosCache[numeroRelatorio];
                }
                else
                {
                    // 2. Se não estiver no cache, carrega do repositório
                    data = CarregarDadosRelatorio(numeroRelatorio);

                    // 3. Adiciona ao cache
                    relatoriosCache.Add(numeroRelatorio, data);
                }

                // 4. Atualiza a UI
                if (lblTituloRelatorio != null)
                {
                    lblTituloRelatorio.Text = $"Relatório {numeroRelatorio}: {tituloCompleto}";
                }

                // Garante que o DataGridView existe antes de ligar a fonte de dados
                if (dgvRelatorios != null)
                {
                    dgvRelatorios.DataSource = data;

                    // Removida a linha dgvRelatorios.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); 
                    // pois o AutoSizeColumnsMode.Fill já foi definido em ConfigurarDataGridView.

                    dgvRelatorios.ReadOnly = true;

                    // Exibe uma mensagem se o resultado for vazio
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

        private DataTable CarregarDadosRelatorio(int numeroRelatorio)
        {
            // Chama o repositório para buscar os dados
            switch (numeroRelatorio)
            {
                case 1:
                    return relatoriosRepository.GetProdutosMaisVendidos();
                case 2:
                    return relatoriosRepository.GetVendasPorCliente();
                case 3:
                    return relatoriosRepository.GetProdutosVendidosPorEvento();
                case 4:
                    return relatoriosRepository.GetProdutosPorFornecedor();
                case 5:
                    return relatoriosRepository.GetItensVendidosPorCategoria();
                case 6:
                    return relatoriosRepository.GetFaturamentoTotalPorEvento();
                case 7:
                    return relatoriosRepository.GetFormaPagamentoMaisUtilizada();
                case 8:
                    // Limite crítico de 20 para o relatório de Baixo Estoque
                    return relatoriosRepository.GetProdutosBaixoEstoque(20);
                case 9:
                    return relatoriosRepository.GetTop5ClientesMaisGastaram();
                case 10:
                    return relatoriosRepository.GetPrecoMedioPorCategoria();
                default:
                    throw new ArgumentException($"Relatório {numeroRelatorio} não encontrado.");
            }
        }

        private string ObterTitulo(int numeroRelatorio)
        {
            // Retorna a descrição do relatório para ser usada no Label e no RadioButton
            switch (numeroRelatorio)
            {
                case 1: return "Itens Mais Vendidos (Nome e Quantidade)";
                case 2: return "Número de Vendas por Cliente";
                case 3: return "Quantidade de Produtos Vendidos por Evento";
                case 4: return "Número de Produtos Diferentes Fornecidos por Fornecedor";
                case 5: return "Quantidade de Itens Vendidos por Categoria";
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