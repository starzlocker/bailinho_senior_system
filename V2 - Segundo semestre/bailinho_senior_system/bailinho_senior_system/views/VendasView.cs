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
using ZstdSharp.Unsafe;

namespace bailinho_senior_system.views
{
    public enum ViewState { Listing, Editing, Creating }

    public partial class VendasView : Form
    {
        // --- Variáveis de Estado da Venda ---
        private List<Venda> vendas = new List<Venda>();
        private BindingList<ProdutoVenda> itensVenda = new BindingList<ProdutoVenda>(); // Lista temporária para a grade
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


        public VendasView()
        {
            InitializeComponent();
            this.Load += VendasView_Load;
            this.tabControl.Selecting += tabControl_Selecting;
            this.listTable.SelectionChanged += listTable_SelectionChanged;

            // Adiciona o evento para o botão de Adicionar Item
            this.btnAdicionar.Click += btnAdicionar_Click;

            this.dgvItensVendidos.CellContentClick += dgvItensVendidos_CellContentClick;
            this.dgvItensVendidos.CellEndEdit += dgvItensVendidos_CellEndEdit;

            // NOVO: Intercepta o início da edição para controlar permissões
            this.dgvItensVendidos.CellBeginEdit += dgvItensVendidos_CellBeginEdit;
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


        private void VendasView_Load(object sender, EventArgs e)
        {
            LoadLookups();
            ReadVendas();

            ConfigurarDgvItensVendidos();

            // Associa a lista temporária à grade de itens
            dgvItensVendidos.DataSource = itensVenda;

            if (vendas.Count > 0)
                PopulateVenda(vendas[currentIndex]);

            SetState(ViewState.Listing);
        }

        // --- MÉTODOS DE CONFIGURAÇÃO E DADOS ---

        private void ConfigurarDgvItensVendidos()
        {
            dgvItensVendidos.Columns.Clear();
            dgvItensVendidos.AutoGenerateColumns = false;
            dgvItensVendidos.ReadOnly = true;
            dgvItensVendidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItensVendidos.AllowUserToAddRows = false;
            dgvItensVendidos.MultiSelect = false;
            dgvItensVendidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // --- Configurações de Aparência (Cores e Estilo) ---
            dgvItensVendidos.EnableHeadersVisualStyles = false;
            dgvItensVendidos.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgvItensVendidos.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvItensVendidos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230); // Azul claro
            dgvItensVendidos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvItensVendidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvItensVendidos.ColumnHeadersHeight = 30;

            // Ocultamos a autogeração para controle total das colunas
            dgvItensVendidos.AutoGenerateColumns = false;
            dgvItensVendidos.Columns.Clear();

            // ATENÇÃO: Mantemos ReadOnly = false para que a coluna QUANTIDADE possa ser editada.
            dgvItensVendidos.ReadOnly = false;

            dgvItensVendidos.AllowUserToAddRows = false;
            dgvItensVendidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // --- 1. Coluna de Ação (Remover) ---
            dgvItensVendidos.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "Remover",
                Text = "X",
                UseColumnTextForButtonValue = true,
                Name = "colRemover",
                Width = 60,
                Resizable = DataGridViewTriState.False,
                ReadOnly = false // Botão deve ser clicável
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
            // 3. Coluna Nome do Produto (FORÇADA A ReadOnly = TRUE)
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "NomeProduto",
                HeaderText = "Produto",
                FillWeight = 150,
                ReadOnly = true
            });
            // 4. Coluna Preço Unitário (FORÇADA A ReadOnly = TRUE)
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PrecoUnitario",
                HeaderText = "Preço Unit.",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = 90,
                ReadOnly = true
            });
            // 5. Coluna Quantidade (EDITÁVEL, ReadOnly = FALSE)
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Quantidade",
                HeaderText = "Qtd",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter },
                ReadOnly = false
            });
            // 6. Coluna Valor Total da Linha (Calculado) (ReadOnly = TRUE)
            dgvItensVendidos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Valor",
                HeaderText = "Subtotal",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C", Alignment = DataGridViewContentAlignment.MiddleRight },
                FillWeight = 100,
                ReadOnly = true
            });

            dgvItensVendidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                // Assumindo que a grade principal de vendas (listTable) está configurada
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
            // Utiliza o ProdutoVendaRepository para buscar os itens da venda
            return produtoVendaRepository.GetItensByVendaId(vendaId);
        }

        private void PopulateVenda(Venda venda)
        {
            // Popula os ComboBoxes de cabeçalho
            cbClientes.SelectedValue = venda.IdCliente;
            cbEventos.SelectedValue = venda.IdEvento;
            cbPagamentos.SelectedItem = venda.FormaPagamento;
            // NOTA: totalBox.Text = venda.ValorTotal.ToString("C");

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
            txtTotal.Text = "R$ 0,00";
        }

        private void CalcularValorTotal()
        {
            decimal total = itensVenda.Sum(i => i.Valor);

            // Atualiza o Label/TextBox que exibe o total (assumindo totalBox)
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
            // Busca o produto completo na lista de disponíveis
            return produtosDisponiveis.FirstOrDefault(p => p.Id == produtoId);
        }

        private void SwitchToTabByName(string tabName)
        {
            if (string.IsNullOrEmpty(tabName)) return;
            // Assumindo um TabControl nomeado 'tabControl'
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        // --- CONTROLE DE ESTADO E VALIDAÇÃO ---

        private void SetState(ViewState newState)
        {
            state = newState;

            bool creatingOrEditing = state != ViewState.Listing;
            bool listing = state == ViewState.Listing;
            int count = vendas.Count;

            // --- 1. CONTROLE DE NAVEGAÇÃO E CRUD (TOOLBAR) ---
            // (Lógica de botões mantida)
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

            // Comboboxes e Controles de Adição
            cbClientes.Enabled = creatingOrEditing;
            cbEventos.Enabled = creatingOrEditing;
            cbPagamentos.Enabled = creatingOrEditing;
            comboBox1.Enabled = creatingOrEditing;
            numQuantidade.Enabled = creatingOrEditing;
            btnAdicionar.Enabled = creatingOrEditing;

            // --- CONTROLE DA GRADE DE ITENS (dgvItensVendidos) ---

            // Habilita/Desabilita a Edição de Células (Quantidade)
            // Se não estiver listando, a edição da grade é permitida.
            if(creatingOrEditing)
            {
                SwitchToTabByName("tabPageCadastro");    // Se estiver criando ou editando, forçamos a ir para a aba de cadastro
                dgvItensVendidos.ReadOnly = false;
                dgvItensVendidos.Enabled = true;
            }
            else
            {
                dgvItensVendidos.Enabled = false;
                dgvItensVendidos.ReadOnly = true;
                dgvItensVendidos.ClearSelection();
            }

            // Habilita/Desabilita a Coluna de Botão de Remoção 'colRemover'
            if (dgvItensVendidos.Columns.Contains("colRemover"))
            {
                dgvItensVendidos.Columns["colRemover"].Visible = creatingOrEditing;
            }       

            // --- 3. CARREGAMENTO/LIMPEZA DE DADOS ---

            if (state == ViewState.Creating || count == 0)
            {
                CleanupFields();
                itensVenda.Clear();
            }
            else if (listing && count > 0)
            {
                PopulateVenda(vendas[currentIndex]);
            }
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

        // --- MANIPULADORES DE EVENTOS DE AÇÃO ---

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

        private void newBtn_Click(object sender, EventArgs e)
        {
            editVenda = new Venda();
            CleanupFields();
            SetState(ViewState.Creating);
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

                // 3. Persistência Transacional (inclui a atualização de estoque no banco)
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
                SetState(ViewState.Listing);
            }
            catch (Exception ex)
            {
                // Se a transação falhar no Repository, o estoque em memória e no banco foram revertidos.
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
                var result = MessageBox.Show($"Tem certeza que deseja excluir a venda ID {vendaIdParaExcluir}?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
            comboBox1.SelectedIndex = -1; // Limpa a seleção do produto
        }

        // --- MANIPULADORES DE NAVEGAÇÃO E LISTA ---

        private void listTable_SelectionChanged(object sender, EventArgs e)
        {
            if (listTable.CurrentRow == null) return;
            if (state != ViewState.Listing) return;

            try
            {
                // NOTA: A coluna ID deve ser mapeada no listTable.
                int selectedId = (int)listTable.CurrentRow.Cells["Id"].Value;
                int newIndex = vendas.FindIndex(v => v.Id == selectedId);

                if (newIndex != -1)
                {
                    currentIndex = newIndex;
                    PopulateVenda(vendas[currentIndex]);
                    SetState(ViewState.Listing);
                }
            }
            catch (Exception)
            {
                // Ignorar erro de re-vinculação
            }
        }

        private void firstBtn_Click_1(object sender, EventArgs e)
        {
            if (vendas.Count > 0 && currentIndex > 0) currentIndex = 0;
            PopulateVenda(vendas[currentIndex]);
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (vendas.Count > 0 && currentIndex > 0) currentIndex--;
            PopulateVenda(vendas[currentIndex]);
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (vendas.Count > 0 && currentIndex < vendas.Count - 1) currentIndex++;
            PopulateVenda(vendas[currentIndex]);
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (vendas.Count > 0 && currentIndex < vendas.Count - 1) currentIndex = vendas.Count - 1;
            PopulateVenda(vendas[currentIndex]);
            SetState(ViewState.Listing);
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            ReadVendas();
            tabControl.SelectedTab = tabPageLista;
            SetState(ViewState.Listing);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Produto GetProdutoById(int produtoId)
        {
            return produtosDisponiveis.FirstOrDefault(p => p.Id == produtoId);
        }

        private void dgvItensVendidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi na coluna de botão "Remover"
            if (e.RowIndex >= 0 && dgvItensVendidos.Columns[e.ColumnIndex].Name == "colRemover")
            {
                // Se a grade estiver ReadOnly (modo Listing), não deve permitir remover.
                if (state == ViewState.Listing) return;
                if(itensVenda.Count == 0) return;
                if(e.RowIndex >= itensVenda.Count) return;

                var itemParaRemover = itensVenda[e.RowIndex];

                var result = MessageBox.Show($"Deseja remover '{itemParaRemover.NomeProduto}' da venda?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 1. Reverter Estoque (Memória - ADICIONAR DE VOLTA)
                    Produto produtoOriginal = GetProdutoById(itemParaRemover.IdProduto);
                    if (produtoOriginal != null)
                    {
                        // Devolve a quantidade ao estoque em memória
                        produtoOriginal.QtdEstoque += itemParaRemover.Quantidade;
                    }

                    // 2. Remover APENAS da BindingList (MEMÓRIA)
                    itensVenda.RemoveAt(e.RowIndex);

                    // 3. Forçar Recálculo
                    CalcularValorTotal();

                    MessageBox.Show("Item removido da lista temporária. Clique em Salvar para confirmar no banco.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgvItensVendidos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Esta verificação já é feita no SetState (ReadOnly da grade), 
            // mas é bom ter uma camada de segurança.
            if (state == ViewState.Listing)
            {
                e.Cancel = true;
                return;
            }

            // Obtém o nome da propriedade associada à coluna que está prestes a ser editada
            string columnName = dgvItensVendidos.Columns[e.ColumnIndex].DataPropertyName;

            // Colunas permitidas para edição manual (além do botão de remoção)
            if (columnName != "Quantidade")
            {
                // Se a coluna NÃO for a de Quantidade, CANCELA a edição.
                e.Cancel = true;

                // Opcional: Se quiser permitir editar a Quantidade APENAS se houver estoque
                // (Isso já é checado no CellEndEdit, mas é um extra)
            }
        }

        private void dgvItensVendidos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Assumindo que a coluna Quantidade é a de índice 4, mas é mais seguro usar o DataPropertyName
            if (dgvItensVendidos.Columns[e.ColumnIndex].DataPropertyName == "Quantidade")
            {
                DataGridViewRow row = dgvItensVendidos.Rows[e.RowIndex];
                var item = itensVenda[e.RowIndex];

                // Tenta obter o novo valor digitado (e garante que é um número positivo)
                if (int.TryParse(row.Cells[e.ColumnIndex].Value?.ToString(), out int novaQuantidade) && novaQuantidade > 0)
                {
                    Produto produtoOriginal = GetProdutoById(item.IdProduto);
                    if (produtoOriginal == null) return;

                    int diferenca = novaQuantidade - item.Quantidade; // Quanto foi aumentado (positivo) ou diminuído (negativo)

                    // Validação de Estoque (Se a diferença for positiva)
                    if (diferenca > 0 && produtoOriginal.QtdEstoque < diferenca)
                    {
                        MessageBox.Show($"Estoque insuficiente para aumentar. Disponível: {produtoOriginal.QtdEstoque}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Reverte a célula para o valor original
                        row.Cells[e.ColumnIndex].Value = item.Quantidade;
                        dgvItensVendidos.CancelEdit();
                        return;
                    }

                    // 1. Atualiza Estoque em Memória (se diferenca > 0 subtrai, se < 0 soma)
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
                    // Reverte a edição (mas o valor original é mantido no item antes da edição)
                    dgvItensVendidos.CancelEdit();
                }
            }
        }
    }
}