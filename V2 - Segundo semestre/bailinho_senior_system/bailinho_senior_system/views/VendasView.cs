using bailinho_senior_system.models;
using bailinho_senior_system.repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace bailinho_senior_system.views
{
    public enum ViewState { Listing, Editing, Creating }

    public partial class VendasView : Form
    {
        // --- Variáveis de Estado da Venda ---
        private List<Venda> vendas = new List<Venda>();
        private BindingList<ProdutoVenda> itensVenda = new BindingList<ProdutoVenda>();
        private List<Produto> produtosDisponiveis = new List<Produto>();

        private Venda editVenda = null;

        private int currentIndex = 0;
        private ViewState state;

        // --- Repositórios ---
        private VendaRepository vendaRepository = new VendaRepository();
        private ClienteRepository clienteRepository = new ClienteRepository();
        private EventoRepository eventoRepository = new EventoRepository();
        private ProdutoRepository produtoRepository = new ProdutoRepository();
        private ProdutoVendaRepository produtoVendaRepository = new ProdutoVendaRepository();


        // --- Inicialização e Setup ---

        public VendasView()
        {
            InitializeComponent();
        }

        private void VendasView_Load(object sender, EventArgs e)
        {
            LoadLookups();
            ConfigurarDgvItensVendidos();
            ConfigurarListTable(listTable);

            ReadVendas();

            dgvItensVendidos.DataSource = itensVenda;

            if (vendas.Count > 0)
                PopulateVenda(vendas[currentIndex]);

            SetState(ViewState.Listing);
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (state == ViewState.Editing || state == ViewState.Creating)
            {
                if (e.TabPage.Name != "tabPageCadastro")
                {
                    var result = MessageBox.Show(
                        "Se você sair, suas alterações serão perdidas. Deseja continuar?",
                        "Confirmar",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }

                    editVenda = null;
                    SetState(ViewState.Listing);
                }
            }
        }

        // --- Configuração e Dados ---

        private void ConfigurarListTable(DataGridView dgv)
        {
            // Configuração da grade principal (listTable)
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 30;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            // Adição e Mapeamento das Colunas
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Name = "Id",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Cliente",
                DataPropertyName = "NomeCliente",
                Name = "colNomeCliente",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Evento",
                DataPropertyName = "NomeEvento",
                Name = "colNomeEvento",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Pagamento",
                DataPropertyName = "FormaPagamento",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Total",
                DataPropertyName = "ValorTotal",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgv.Columns["colNomeCliente"].FillWeight = 40;
            dgv.Columns["colNomeEvento"].FillWeight = 40;
        }

        private void ConfigurarDgvItensVendidos()
        {
            // Configurações visuais e de comportamento para a grade de itens
            dgvItensVendidos.AutoGenerateColumns = false;
            dgvItensVendidos.ReadOnly = false;
            dgvItensVendidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItensVendidos.AllowUserToAddRows = false;
            dgvItensVendidos.MultiSelect = false;
            dgvItensVendidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Estilos visuais
            dgvItensVendidos.EnableHeadersVisualStyles = false;
            dgvItensVendidos.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvItensVendidos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            dgvItensVendidos.ColumnHeadersHeight = 30;

            dgvItensVendidos.Columns.Clear();

            // 1. Coluna de Ação (Remover)
            dgvItensVendidos.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "Remover",
                Text = "X",
                UseColumnTextForButtonValue = true,
                Name = "colRemover",
                Width = 60,
                Resizable = DataGridViewTriState.False,
                ReadOnly = false
            });

            // 2. Coluna ID do Produto (Oculta)
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "IdProduto",
                HeaderText = "Prod. ID",
                Visible = false,
                Name = "colIdProduto",
                ReadOnly = true
            });

            // 3. Coluna Nome do Produto
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "NomeProduto",
                HeaderText = "Produto",
                FillWeight = 150,
                ReadOnly = true
            });

            // 4. Coluna Preço Unitário
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PrecoUnitario",
                HeaderText = "Preço Unit.",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = 90,
                ReadOnly = true
            });

            // 5. Coluna Quantidade (EDITÁVEL)
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Quantidade",
                HeaderText = "Qtd",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter },
                ReadOnly = false
            });

            // 6. Coluna Valor Total da Linha (Calculado)
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Valor",
                HeaderText = "Subtotal",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C", Alignment = DataGridViewContentAlignment.MiddleRight },
                FillWeight = 100,
                ReadOnly = true
            });
        }

        private void LoadLookups()
        {
            try
            {
                // Carregar Clientes (cbClientes)
                cbClientes.DisplayMember = "Nome";
                cbClientes.ValueMember = "Id";
                cbClientes.DataSource = clienteRepository.GetClientes();
                cbClientes.SelectedIndex = -1;

                // Carregar Eventos (cbEventos)
                cbEventos.DisplayMember = "Nome";
                cbEventos.ValueMember = "Id";
                cbEventos.DataSource = eventoRepository.GetEventos();
                cbEventos.SelectedIndex = -1;

                // Carregar Produtos (comboBox1)
                this.produtosDisponiveis = produtoRepository.GetProdutos();
                comboBox1.DisplayMember = "Nome";
                comboBox1.ValueMember = "Id";
                comboBox1.DataSource = this.produtosDisponiveis;
                comboBox1.SelectedIndex = -1;

                // Opções de Pagamento (cbPagamentos)
                cbPagamentos.Items.Clear();
                cbPagamentos.Items.AddRange(new object[] { "Pix", "Crédito", "Débito", "Dinheiro" });
                cbPagamentos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados iniciais: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadVendas()
        {
            try
            {
                this.vendas = vendaRepository.GetVendas();

                listTable.DataSource = null;
                listTable.DataSource = this.vendas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar vendas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ProdutoVenda> GetItensVenda(int vendaId)
        {
            return produtoVendaRepository.GetItensByVendaId(vendaId);
        }

        private void PopulateVenda(Venda venda)
        {
            // Popula os ComboBoxes de cabeçalho
            cbClientes.SelectedValue = venda.IdCliente;
            cbEventos.SelectedValue = venda.IdEvento;
            cbPagamentos.SelectedItem = venda.FormaPagamento;

            // Carrega os itens da venda (apenas para Edição/Visualização)
            itensVenda.Clear();
            GetItensVenda(venda.Id).ForEach(item => itensVenda.Add(item));

            CalcularValorTotal();
        }

        private void CleanupFields()
        {
            cbClientes.SelectedIndex = -1;
            cbEventos.SelectedIndex = -1;
            cbPagamentos.SelectedIndex = -1;
            itensVenda.Clear();
            txtTotal.Text = 0.ToString("C");
        }

        private void CalcularValorTotal()
        {
            decimal total = itensVenda.Sum(i => i.Valor);

            // Atualiza o Label/TextBox que exibe o total
            txtTotal.Text = total.ToString("C");

            if (editVenda != null)
            {
                editVenda.ValorTotal = total;
            }
        }

        private Produto GetProdutoSelecionado()
        {
            if (comboBox1.SelectedValue == null) return null;
            int produtoId = (int)comboBox1.SelectedValue;

            return produtosDisponiveis.FirstOrDefault(p => p.Id == produtoId);
        }

        private Produto GetProdutoById(int produtoId)
        {
            return produtosDisponiveis.FirstOrDefault(p => p.Id == produtoId);
        }

        private List<string> ValidateVenda()
        {
            List<string> errors = new List<string>();

            if (cbClientes.SelectedValue == null) errors.Add("Selecione um cliente.");
            if (cbEventos.SelectedValue == null) errors.Add("Selecione um evento.");
            if (cbPagamentos.SelectedItem == null) errors.Add("Selecione a forma de pagamento.");
            if (itensVenda.Count == 0) errors.Add("A venda deve conter itens.");

            return errors;
        }

        // --- Navegação e Estado da View ---

        private void SwitchToTabByName(string tabName)
        {
            if (string.IsNullOrEmpty(tabName)) return;
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void SetState(ViewState newState)
        {
            state = newState;

            bool creatingOrEditing = state != ViewState.Listing;
            bool listing = state == ViewState.Listing;
            int count = vendas.Count;

            // --- 1. CONTROLE DE NAVEGAÇÃO E CRUD (TOOLBAR) ---
            firstBtn.Enabled = listing && currentIndex > 0;
            previousBtn.Enabled = listing && currentIndex > 0;
            nextBtn.Enabled = listing && currentIndex < count - 1;
            lastBtn.Enabled = listing && currentIndex < count - 1;

            newBtn.Enabled = listing;
            editBtn.Enabled = listing && count > 0;
            deleteBtn.Enabled = listing && count > 0;
            searchBtn.Enabled = listing;

            saveBtn.Enabled = creatingOrEditing;
            cancelBtn.Enabled = creatingOrEditing;

            // --- 2. CONTROLE DOS CAMPOS DE ENTRADA (ABA CADASTRO) ---

            // ComboBoxes e Controles de Adição
            cbClientes.Enabled = creatingOrEditing;
            cbEventos.Enabled = creatingOrEditing;
            cbPagamentos.Enabled = creatingOrEditing;
            comboBox1.Enabled = creatingOrEditing;
            numQuantidade.Enabled = creatingOrEditing;
            btnAdicionar.Enabled = creatingOrEditing;
            btnRemover.Enabled = creatingOrEditing;

            // txtTotal é ReadOnly
            txtTotal.ReadOnly = true;

            // --- 3. CONTROLE DA GRADE DE ITENS (dgvItensVendidos) ---

            if (creatingOrEditing)
            {
                SwitchToTabByName("tabPageCadastro");
                dgvItensVendidos.ReadOnly = false;
            }
            else
            {
                dgvItensVendidos.ReadOnly = true;
                dgvItensVendidos.ClearSelection();
            }

            // Habilita/Desabilita a Coluna de Botão de Remoção 'colRemover'
            if (dgvItensVendidos.Columns.Contains("colRemover"))
            {
                dgvItensVendidos.Columns["colRemover"].Visible = creatingOrEditing;
            }

            // --- 4. CARREGAMENTO/SELEÇÃO DE DADOS ---
            if (state == ViewState.Creating || count == 0)
            {
                CleanupFields();
                itensVenda.Clear();
            }
            else if (listing && count > 0 && currentIndex >= 0)
            {
                PopulateVenda(vendas[currentIndex]);
                UpdateDataGridViewSelection();
            }
        }

        private void UpdateDataGridViewSelection()
        {
            // Seleciona a venda atual no listTable e rola para visualização
            if (listTable == null || vendas.Count == 0 || currentIndex < 0 || currentIndex >= vendas.Count)
            {
                return;
            }

            Venda vendaAtual = vendas[currentIndex];
            List<Venda> listaExibida = listTable.DataSource as List<Venda>;

            // Encontra o índice da venda atual na lista exibida (que pode ser filtrada)
            int indexNaListaExibida = listaExibida?.FindIndex(v => v.Id == vendaAtual.Id) ?? -1;

            if (indexNaListaExibida != -1)
            {
                // Limpa seleções anteriores e força a seleção usando o BindingContext
                listTable.ClearSelection();

                // Seleciona a linha pelo índice na lista exibida
                listTable.Rows[indexNaListaExibida].Selected = true;

                // Rola para a linha selecionada
                listTable.FirstDisplayedScrollingRowIndex = indexNaListaExibida;
            }
            else
            {
                listTable.ClearSelection();
            }
        }

        // --- Eventos de Navegação e Lista ---

        private void firstBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex = 0;
            ReadVendas();
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            ReadVendas();
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < vendas.Count - 1) currentIndex++;
            ReadVendas();
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < vendas.Count - 1) currentIndex = vendas.Count - 1;
            ReadVendas();
            SetState(ViewState.Listing);
        }

        private void listTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= listTable.Rows.Count) return;
            if (state != ViewState.Listing) return;

            Venda vendaSelecionada = listTable.Rows[e.RowIndex].DataBoundItem as Venda;

            if (vendaSelecionada != null)
            {
                int novoIndex = vendas.FindIndex(v => v.Id == vendaSelecionada.Id);

                if (novoIndex != -1)
                {
                    currentIndex = novoIndex;
                }
                SetState(ViewState.Listing);
            }
        }

        private void makeSearch_Click(object sender, EventArgs e)
        {
            // Implementa a lógica de busca/filtro
            string searchTerm = searchBox.Text.Trim().ToLower();
            List<Venda> filteredVendas;
            decimal totalBusca = -1;

            // Tenta converter o termo de busca para valor monetário (total)
            if (decimal.TryParse(searchTerm.Replace("r$", "").Replace(",", "").Replace(".", ""),
                                 System.Globalization.NumberStyles.Currency,
                                 System.Globalization.CultureInfo.InvariantCulture,
                                 out totalBusca))
            {
                // Tenta ajustar o valor total se a entrada parecer um inteiro formatado como moeda (ex: 1200 -> 12.00)
                if (totalBusca > 100 && !searchTerm.Contains(","))
                {
                    totalBusca /= 100;
                }
            }


            if (!string.IsNullOrEmpty(searchTerm))
            {
                // NOVO: Limpa o termo de busca de símbolos e pontuações para a comparação do Valor Total
                string cleanSearchTerm = searchTerm.Replace("r$", "").Replace(" ", "").Replace(",", "").Replace(".", "");

                filteredVendas = vendas.Where(venda =>
                    // 1. Busca por ID (Igualdade Exata)
                    (int.TryParse(searchTerm, out int id) && venda.Id == id) ||
                    // 2. Busca por Nome de Cliente (Substring)
                    (venda.NomeCliente != null && venda.NomeCliente.ToLower().Contains(searchTerm)) ||
                    // 3. Busca por Nome de Evento (Substring)
                    (venda.NomeEvento != null && venda.NomeEvento.ToLower().Contains(searchTerm)) ||
                    // 4. Busca por Forma de Pagamento (Substring)
                    (venda.FormaPagamento != null && venda.FormaPagamento.ToLower().Contains(searchTerm)) ||

                    // 5. Busca por Valor Total (Igualdade Exata) - Se a conversão foi bem-sucedida
                    (totalBusca >= 0 && venda.ValorTotal == totalBusca) ||

                    // 6. Busca por Valor Total (Substring) - Robusta
                    // Converte o valor para string neutra (ex: "307.00" -> "307"), remove o ponto, e verifica a substring.
                    venda.ValorTotal.ToString(
                        "0.##", // Omite zeros finais (R$ 307,00 -> "307"; R$ 12,50 -> "12.5")
                        System.Globalization.CultureInfo.InvariantCulture
                    ).Replace(".", "").Contains(cleanSearchTerm)

                ).ToList();
            }
            else
            {
                // Se a busca estiver vazia, retorna a lista completa
                filteredVendas = vendas;
            }

            // Atualiza o DataGridView com a lista filtrada
            listTable.DataSource = null;
            listTable.DataSource = filteredVendas;

            // Atualiza o currentIndex e o estado com o primeiro item encontrado
            if (filteredVendas.Count > 0)
            {
                int idPrimeiroItem = filteredVendas[0].Id;
                // O currentIndex deve refletir a posição do item na lista COMPLETA (vendas)
                currentIndex = vendas.FindIndex(venda => venda.Id == idPrimeiroItem);

                // Popula com a venda encontrada na lista completa
                PopulateVenda(vendas[currentIndex]);
            }
            else
            {
                currentIndex = -1;
                CleanupFields();
                MessageBox.Show("Nenhuma venda encontrada.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SetState(ViewState.Listing);
        }

        // --- Eventos CRUD ---

        private void newBtn_Click(object sender, EventArgs e)
        {
            editVenda = new Venda();
            SetState(ViewState.Creating);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (vendas.Count > 0)
            {
                editVenda = vendas[currentIndex];
                SetState(ViewState.Editing);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            editVenda = null;
            SetState(ViewState.Listing);

            numQuantidade.Value = 0;
            comboBox1.SelectedIndex = -1;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (editVenda == null) return;

            try
            {
                // 1. Validação final
                List<string> errors = ValidateVenda();
                if (errors.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", errors), "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Coleta final de dados
                editVenda.IdCliente = (int)cbClientes.SelectedValue;
                editVenda.IdEvento = (int)cbEventos.SelectedValue;
                editVenda.FormaPagamento = cbPagamentos.SelectedItem.ToString();
                CalcularValorTotal(); // Último cálculo antes de salvar

                // 3. Persistência Transacional (criação/atualização e ajuste de estoque)
                if (state == ViewState.Creating)
                {
                    vendaRepository.CreateVenda(editVenda, itensVenda.ToList());
                }
                else if (state == ViewState.Editing)
                {
                    vendaRepository.UpdateVenda(editVenda, itensVenda.ToList());
                }

                MessageBox.Show("Venda salva e estoque atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Resetar a View
                ReadVendas();
                currentIndex = vendas.FindIndex(v => v.Id == editVenda.Id); // Tenta focar no item salvo
                if (currentIndex == -1) currentIndex = 0;

                SetState(ViewState.Listing);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao finalizar venda: " + ex.Message, "Erro Transacional", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Limpar inputs de item após falha
                comboBox1.SelectedIndex = -1;
                numQuantidade.Value = 0;
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (vendas.Count == 0 || currentIndex < 0) return;

            try
            {
                int vendaIdParaExcluir = vendas[currentIndex].Id;
                var result = MessageBox.Show($"Tem certeza que deseja excluir a venda ID {vendaIdParaExcluir}? O estoque será revertido.", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;

                vendaRepository.DeleteVenda(vendaIdParaExcluir);

                MessageBox.Show("Venda excluída com sucesso! Estoque revertido.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recarrega e ajusta o índice
                ReadVendas();
                if (vendas.Count > 0)
                {
                    currentIndex = Math.Max(0, currentIndex - 1);
                }
                else
                {
                    currentIndex = 0;
                }

                SetState(ViewState.Listing);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar venda: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Eventos de Adição/Remoção de Itens da Venda (dgvItensVendidos) ---

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Produto produto = GetProdutoSelecionado();
            int quantidade = (int)numQuantidade.Value;

            if (produto == null || quantidade <= 0)
            {
                MessageBox.Show("Selecione um produto e defina a quantidade.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Verificar Estoque (Validação Pessimista)
            if (produto.QtdEstoque < quantidade)
            {
                MessageBox.Show($"Estoque insuficiente. Disponível: {produto.QtdEstoque}", "Erro de Estoque", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Criar Item de Venda Temporário
            ProdutoVenda novoItem = new ProdutoVenda
            {
                IdProduto = produto.Id,
                NomeProduto = produto.Nome,
                PrecoUnitario = produto.Preco,
                Quantidade = quantidade,
                Valor = quantidade * produto.Preco
            };

            // 3. Adicionar/Atualizar na Lista
            var itemExistente = itensVenda.FirstOrDefault(i => i.IdProduto == produto.Id);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
                itemExistente.Valor = itemExistente.Quantidade * produto.Preco;
                itensVenda.ResetBindings(); // Força a atualização da BindingList/grade
            }
            else
            {
                itensVenda.Add(novoItem);
            }

            // 4. Atualizar Estoque (Em Memória - para não vender em excesso na mesma sessão)
            produto.QtdEstoque -= quantidade;

            // 5. Atualizar UI
            CalcularValorTotal();
            numQuantidade.Value = 0;
            comboBox1.SelectedIndex = -1; // Limpa a seleção do produto
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (state == ViewState.Listing) return;

            // Lógica para remover a quantidade do item selecionado nos ComboBoxes

            Produto produto = GetProdutoSelecionado();
            int quantidadeParaRemover = (int)numQuantidade.Value;

            if (produto == null || quantidadeParaRemover <= 0)
            {
                MessageBox.Show("Selecione o produto no campo Produtos e a quantidade para remover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemExistente = itensVenda.FirstOrDefault(i => i.IdProduto == produto.Id);

            if (itemExistente != null)
            {
                if (itemExistente.Quantidade < quantidadeParaRemover)
                {
                    MessageBox.Show($"Não é possível remover {quantidadeParaRemover} itens. Apenas {itemExistente.Quantidade} estão no carrinho.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 1. Reverter Estoque (Memória - ADICIONAR DE VOLTA)
                Produto produtoOriginal = GetProdutoById(itemExistente.IdProduto);
                if (produtoOriginal != null)
                {
                    produtoOriginal.QtdEstoque += quantidadeParaRemover;
                }

                // 2. Reduz a quantidade na venda
                itemExistente.Quantidade -= quantidadeParaRemover;

                if (itemExistente.Quantidade > 0)
                {
                    // Atualiza o valor total do item e a grade
                    itemExistente.Valor = itemExistente.Quantidade * itemExistente.PrecoUnitario;
                    itensVenda.ResetBindings();
                }
                else
                {
                    // Se a quantidade chegou a zero, remove o item da lista
                    itensVenda.Remove(itemExistente);
                }

                // 3. Forçar Recálculo total da venda
                CalcularValorTotal();

                MessageBox.Show($"{quantidadeParaRemover} unidades de {produto.Nome} removidas.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("O produto selecionado não está no carrinho.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvItensVendidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lógica de remoção ao clicar no botão "X" da grade (remove item completo)
            if (e.RowIndex >= 0 && dgvItensVendidos.Columns[e.ColumnIndex].Name == "colRemover" && state != ViewState.Listing)
            {
                if (dgvItensVendidos.CurrentRow == null) return;

                var itemParaRemover = dgvItensVendidos.CurrentRow.DataBoundItem as ProdutoVenda;
                if (itemParaRemover == null) return;

                var result = MessageBox.Show($"Deseja remover '{itemParaRemover.NomeProduto}' (item completo) da venda?", "Confirmar Remoção Completa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 1. Reverter Estoque (Memória)
                    Produto produtoOriginal = GetProdutoById(itemParaRemover.IdProduto);
                    if (produtoOriginal != null)
                    {
                        // Devolve a quantidade TOTAL ao estoque em memória
                        produtoOriginal.QtdEstoque += itemParaRemover.Quantidade;
                    }

                    // 2. Remover APENAS da BindingList (MEMÓRIA)
                    itensVenda.Remove(itemParaRemover);

                    // 3. Forçar Recálculo
                    CalcularValorTotal();
                }
            }
        }

        private void dgvItensVendidos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Permite edição APENAS se estiver em modo de edição/criação
            if (state == ViewState.Listing)
            {
                e.Cancel = true;
                return;
            }

            // Permite edição APENAS na coluna Quantidade
            string columnName = dgvItensVendidos.Columns[e.ColumnIndex].DataPropertyName;
            if (columnName != "Quantidade")
            {
                e.Cancel = true;
            }
        }

        private void dgvItensVendidos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItensVendidos.Columns[e.ColumnIndex].DataPropertyName == "Quantidade")
            {
                DataGridViewRow row = dgvItensVendidos.Rows[e.RowIndex];
                var item = itensVenda[e.RowIndex];

                // Salva a quantidade antiga antes de tentar a nova edição
                int quantidadeAntiga = item.Quantidade;

                if (int.TryParse(row.Cells[e.ColumnIndex].Value?.ToString(), out int novaQuantidade) && novaQuantidade > 0)
                {
                    Produto produtoOriginal = GetProdutoById(item.IdProduto);
                    if (produtoOriginal == null) return;

                    int diferenca = novaQuantidade - quantidadeAntiga; // Quanto foi aumentado (+) ou diminuído (-)

                    // Validação de Estoque (Se a diferença for positiva)
                    if (diferenca > 0 && produtoOriginal.QtdEstoque < diferenca)
                    {
                        MessageBox.Show($"Estoque insuficiente para aumentar. Disponível: {produtoOriginal.QtdEstoque}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Reverte o valor da célula visualmente
                        row.Cells[e.ColumnIndex].Value = quantidadeAntiga;
                        dgvItensVendidos.CancelEdit();
                        return;
                    }

                    // 1. Atualiza Estoque em Memória
                    produtoOriginal.QtdEstoque -= diferenca;

                    // 2. Atualiza o objeto ProdutoVenda
                    item.Quantidade = novaQuantidade;
                    item.Valor = novaQuantidade * item.PrecoUnitario;

                    // 3. Atualiza a UI
                    itensVenda.ResetBindings();
                    CalcularValorTotal();
                }
                else
                {
                    // Se o valor digitado for inválido (<= 0 ou não numérico)
                    MessageBox.Show("Quantidade inválida. Deve ser um número inteiro positivo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Reverte a edição (o valor antigo persiste na lista item)
                    row.Cells[e.ColumnIndex].Value = quantidadeAntiga;
                    dgvItensVendidos.CancelEdit();
                }
            }
        }

        // --- Eventos de Busca e Saída ---

        private void searchBtn_Click(object sender, EventArgs e)
        {
            ReadVendas();
            SwitchToTabByName("tabPageLista");
            searchBox.Focus();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            if (state == ViewState.Editing || state == ViewState.Creating)
            {
                var result = MessageBox.Show(
                    "Se você sair, suas alterações serão perdidas. Deseja continuar?",
                    "Confirmar",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Cancel) return;
            }
            this.Close();
        }

        private void VendasView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (state == ViewState.Editing || state == ViewState.Creating)
            {
                var result = MessageBox.Show(
                    "Se você sair, suas alterações serão perdidas. Deseja continuar?",
                    "Confirmar",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}