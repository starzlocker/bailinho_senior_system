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
    public partial class FornecedoresView : Form
    {
        private enum ViewState { Listing, Editing, Creating }

        // --- Variáveis de Estado da View ---
        private List<Fornecedor> fornecedores = new List<Fornecedor>();
        private int currentIndex = 0;
        private ViewState state;
        private Fornecedor editItem = null;

        // --- Repositórios ---
        private FornecedorRepository fornecedorRepository = new FornecedorRepository();
        private ProdutoRepository produtoRepository = new ProdutoRepository();
        private ProdutoFornecedorRepository produtoFornecedorRepository = new ProdutoFornecedorRepository();


        // --- Inicialização e Setup ---

        public FornecedoresView()
        {
            InitializeComponent();
        }

        private void FornecedoresView_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView(listTable);
            ConfigurarListProdutosView(listProdutos);

            ReadFornecedores();
            ReadProdutos();

            if (fornecedores.Count > 0)
                PopulateFornecedor(fornecedores[currentIndex]);

            SetState(ViewState.Listing);
        }

        private void tabControl_Selecting_1(object sender, TabControlCancelEventArgs e)
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

                    editItem = null;
                    SetState(ViewState.Listing);
                }
            }
        }

        private void FornecedoresView_FormClosing(object sender, FormClosingEventArgs e)
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

        // --- Configuração e Leitura de Dados ---

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            // Configurações visuais e de comportamento (listTable - Fornecedores)
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;

            // Estilos visuais
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 30;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            // Limpa colunas para redefinição programática
            dgv.Columns.Clear();

            // Adição e Mapeamento das Colunas
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Name = "Id",
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Nome",
                DataPropertyName = "Nome",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "CNPJ",
                DataPropertyName = "Cnpj",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "E-mail",
                DataPropertyName = "Email",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Telefone",
                DataPropertyName = "Telefone",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void ConfigurarListProdutosView(DataGridView dgv)
        {
            // Configurações visuais e de comportamento (listProdutos - Produtos do Fornecedor)
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;

            // Estilos visuais
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            dgv.ColumnHeadersHeight = 30;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            // Limpa colunas para redefinição programática
            dgv.Columns.Clear();

            // 1. Coluna de Ação (Remover)
            dgv.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "Remover",
                Text = "X",
                UseColumnTextForButtonValue = true,
                Name = "colRemoverItem",
                Width = 70,
                Resizable = DataGridViewTriState.False,
                ReadOnly = false
            });

            // 2. Coluna ID do Produto (VISÍVEL)
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ID Prod.",
                DataPropertyName = "IdProduto",
                Name = "IdProduto",
                Visible = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // 3. Coluna ID de Vínculo (Oculta)
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Name = "Id",
                Visible = false
            });

            // 4. Nome do Produto (Preenchimento)
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Produto",
                DataPropertyName = "NomeProduto",
                Name = "NomeProduto",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void ReadFornecedores()
        {
            try
            {
                // Carrega todos os fornecedores do repositório
                this.fornecedores = fornecedorRepository.GetFornecedores();

                // Vincula a lista de objetos diretamente ao DataGridView
                listTable.DataSource = null;
                listTable.DataSource = this.fornecedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar fornecedores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadProdutos()
        {
            try
            {
                var produtos = produtoRepository.GetProdutos();

                if (produtos.Count == 0) { return; }

                produtoBox.DisplayMember = "Nome";
                produtoBox.ValueMember = "Id";
                produtoBox.DataSource = produtos;
                produtoBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadProdutosFornecedor(Fornecedor fornecedor)
        {
            try
            {
                // Busca produtos vinculados ao fornecedor
                var produtosPorFornecedor = produtoFornecedorRepository.GetProdutosPorFornecedor(fornecedor.Id);

                // Limpa e repopula a lista interna do objeto editItem
                fornecedor.Produtos.Clear();
                foreach (var p in produtosPorFornecedor)
                {
                    // Apenas adiciona se o item não estiver marcado para ser apagado (durante edição)
                    if (fornecedor.ProdutosApagados == null || !fornecedor.ProdutosApagados.Any(pa => pa.IdProduto == p.IdProduto))
                    {
                        fornecedor.Produtos.Add(p);
                    }
                }

                // Vincula a lista de produtos (ProdutoFornecedor) ao listProdutos
                listProdutos.DataSource = null;
                listProdutos.DataSource = fornecedor.Produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produtos do fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFornecedor(Fornecedor fornecedor)
        {
            idBox.Text = fornecedor.Id.ToString();
            nomeBox.Text = fornecedor.Nome ?? "";
            cnpjBox.Text = fornecedor.Cnpj ?? "";
            emailBox.Text = fornecedor.Email ?? "";
            telefoneBox.Text = fornecedor.Telefone ?? "";

            // Recarrega a grade de produtos vinculados
            ReadProdutosFornecedor(fornecedor);
        }

        private void CleanupFields()
        {
            idBox.Text = "";
            nomeBox.Text = "";
            cnpjBox.Text = "";
            emailBox.Text = "";
            telefoneBox.Text = "";
            produtoBox.SelectedValue = -1;
            listProdutos.DataSource = null;

            // Limpa produtos apagados em memória, caso tenha sido cancelado um delete
            if (editItem != null)
            {
                editItem.ProdutosApagados.Clear();
            }
        }

        private List<string> ValidateForm()
        {
            List<string> errors = new List<string>();

            if (nomeBox.Text.Length == 0) errors.Add("Nome não pode estar vazio!");
            else if (nomeBox.Text.Length > 150) errors.Add("Nome deve ter até 150 caracteres.");

            if (cnpjBox.Text.Length == 0) errors.Add("CNPJ não pode estar vazio!");
            else if (cnpjBox.Text.Length > 14) errors.Add("CPNJ deve ter no máximo 14 dígitos.");

            if (emailBox.Text.Length == 0) errors.Add("E-mail não pode estar vazio!");
            else if (emailBox.Text.Length > 150) errors.Add("E-mail deve ter até 150 caracteres.");

            if (telefoneBox.Text.Length == 0) errors.Add("Telefone não pode estar vazio!");

            return errors;
        }

        // --- Navegação e Estado da View ---

        private void SetState(ViewState newState)
        {
            state = newState;

            bool creating = state == ViewState.Creating;
            bool editing = state == ViewState.Editing;
            bool listing = state == ViewState.Listing;
            bool creatingOrEditing = creating || editing;

            int count = fornecedores.Count;

            if (creatingOrEditing)
            {
                SwitchToTabByName("tabPageCadastro");
            }

            // Habilita/Desabilita Botões CRUD e Navegação
            deleteBtn.Enabled = count > 0 && listing;
            editBtn.Enabled = count > 0 && listing;
            newBtn.Enabled = listing;
            searchBtn.Enabled = listing;

            saveBtn.Enabled = creatingOrEditing;
            cancelBtn.Enabled = creatingOrEditing;

            nextBtn.Enabled = listing && currentIndex < count - 1;
            lastBtn.Enabled = listing && currentIndex < count - 1;
            firstBtn.Enabled = listing && currentIndex > 0;
            previousBtn.Enabled = listing && currentIndex > 0;

            // ReadOnly dos Campos
            nomeBox.ReadOnly = listing;
            cnpjBox.ReadOnly = listing;
            emailBox.ReadOnly = listing;
            telefoneBox.ReadOnly = listing;

            // Controles de Produto
            produtoBox.Enabled = !listing;
            addProdutoBtn.Enabled = !listing;


            if (count == 0 || creating)
            {
                CleanupFields();
            }
            else if (listing && currentIndex >= 0)
            {
                PopulateFornecedor(fornecedores[currentIndex]);
                UpdateDataGridViewSelection();
            }

            // Controle de visibilidade do botão 'X' (Remover Item) na grade listProdutos
            if (listProdutos.Columns.Contains("colRemoverItem"))
            {
                listProdutos.Columns["colRemoverItem"].Visible = creatingOrEditing;
            }
        }

        private void SwitchToTabByName(string tabName)
        {
            if (string.IsNullOrEmpty(tabName)) return;
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void UpdateDataGridViewSelection()
        {
            // Seleciona o fornecedor atual no listTable e rola para visualização
            if (listTable == null || fornecedores.Count == 0 || currentIndex < 0 || currentIndex >= fornecedores.Count)
            {
                listTable.ClearSelection();
                return;
            }

            // Sincroniza a seleção visual
            if (listTable.DataSource is List<Fornecedor> listaExibida)
            {
                Fornecedor itemAtual = fornecedores[currentIndex];
                int indexNaListaExibida = listaExibida.FindIndex(f => f.Id == itemAtual.Id);

                if (indexNaListaExibida != -1)
                {
                    listTable.ClearSelection(); // Limpa seleções anteriores
                    listTable.Rows[indexNaListaExibida].Selected = true;
                    listTable.FirstDisplayedScrollingRowIndex = indexNaListaExibida;
                }
            }
        }

        // --- Manipuladores de Eventos de Navegação ---

        private void firstBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex = 0;
            ReadFornecedores(); // Recarrega lista completa
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            ReadFornecedores(); // Recarrega lista completa
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < fornecedores.Count - 1) currentIndex++;
            ReadFornecedores(); // Recarrega lista completa
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < fornecedores.Count - 1) currentIndex = fornecedores.Count - 1;
            ReadFornecedores(); // Recarrega lista completa
            SetState(ViewState.Listing);
        }

        private void listTable_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= listTable.Rows.Count) return;
            if (state != ViewState.Listing) return;

            Fornecedor fornecedorSelecionado = listTable.Rows[e.RowIndex].DataBoundItem as Fornecedor;

            if (fornecedorSelecionado != null)
            {
                int novoIndex = fornecedores.FindIndex(f => f.Id == fornecedorSelecionado.Id);

                if (novoIndex != -1)
                {
                    currentIndex = novoIndex;
                }
                SetState(ViewState.Listing);
            }
        }

        // --- Eventos CRUD ---

        private void newBtn_Click(object sender, EventArgs e)
        {
            editItem = new Fornecedor();
            SetState(ViewState.Creating);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            editItem = null;
            SetState(ViewState.Listing);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (fornecedores.Count == 0) return;
            editItem = fornecedores[currentIndex];

            editItem.ProdutosApagados.Clear();
            SetState(ViewState.Editing);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (editItem == null) return;

            List<string> errors = ValidateForm();
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Erros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. Coleta dados e valida as propriedades
                try
                {
                    editItem.Nome = nomeBox.Text.Trim();
                    editItem.Cnpj = cnpjBox.Text.Trim();
                    editItem.Email = emailBox.Text.Trim();
                    editItem.Telefone = telefoneBox.Text.Trim();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Persistência
                if (state == ViewState.Creating)
                {
                    fornecedorRepository.CreateFornecedor(editItem);

                    // Vincula produtos após a criação do fornecedor (agora editItem.Id está preenchido)
                    foreach (ProdutoFornecedor p in editItem.Produtos)
                    {
                        p.IdFornecedor = editItem.Id;
                    }
                    produtoFornecedorRepository.CreateFromListProdutoFornecedor(editItem.Produtos);

                    ReadFornecedores();
                    currentIndex = fornecedores.Count - 1;
                }
                else if (state == ViewState.Editing)
                {
                    fornecedorRepository.UpdateFornecedor(editItem);

                    // Cria/Atualiza/Exclui produtos vinculados
                    produtoFornecedorRepository.CreateFromListProdutoFornecedor(editItem.Produtos, editItem.ProdutosApagados);

                    ReadFornecedores();
                }

                SetState(ViewState.Listing);
                MessageBox.Show("Fornecedor salvo com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (fornecedores.Count == 0) return;

                var result = MessageBox.Show(
                    $"Tem certeza que deseja excluir a o fornecedor '{fornecedores[currentIndex].Nome}'?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;

                Fornecedor fornecedorParaExcluir = fornecedores[currentIndex];

                // 1. Exclui os vínculos de produtos primeiro
                produtoFornecedorRepository.DeleteAllProdutoFornecedor(fornecedorParaExcluir.Id);

                // 2. Exclui o Fornecedor
                fornecedorRepository.DeleteFornecedor(fornecedorParaExcluir.Id);

                ReadFornecedores();

                if (fornecedores.Count > 0)
                {
                    currentIndex = Math.Max(0, currentIndex - 1);
                }
                else
                {
                    currentIndex = 0;
                }

                editItem = null;
                SetState(ViewState.Listing);

                MessageBox.Show("Fornecedor excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException mysex) when (mysex.Number == 1451)
            {
                MessageBox.Show("Não foi possível excluir o Fornecedor, pois ele possui referências em outras tabelas.", "Erro de Chave Estrangeira", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Eventos de Busca e Saída ---

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

        private void searchBtn_Click(object sender, EventArgs e)
        {
            ReadFornecedores(); // Garante que a lista completa esteja vinculada
            SwitchToTabByName("tabPageLista");
            searchBox.Focus();
        }

        private void makeSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = searchBox.Text.Trim().ToLower();
            List<Fornecedor> filteredFornecedores;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredFornecedores = fornecedores.Where(f =>
                    (int.TryParse(searchTerm, out int id) && f.Id == id) ||
                    (f.Nome.ToLower().Contains(searchTerm)) ||
                    (f.Cnpj != null && f.Cnpj.Contains(searchTerm)) ||
                    (f.Email != null && f.Email.ToLower().Contains(searchTerm))
                ).ToList();
            }
            else
            {
                // Se a busca estiver vazia, retorna a lista completa
                filteredFornecedores = fornecedores;
            }

            listTable.DataSource = null;
            listTable.DataSource = filteredFornecedores;

            if (filteredFornecedores.Count > 0)
            {
                // Sincroniza o currentIndex com o primeiro item encontrado na lista original
                currentIndex = fornecedores.FindIndex(f => f.Id == filteredFornecedores[0].Id);
                PopulateFornecedor(filteredFornecedores[0]);
            }
            else
            {
                currentIndex = -1;
                CleanupFields();
                MessageBox.Show("Nenhum fornecedor encontrado.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SetState(ViewState.Listing);
        }

        // --- Manipuladores de Produtos Vinculados ---

        private void addProdutoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (editItem == null) return;

                if (produtoBox.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um produto para adicionar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProdutoSelecionado = (int)produtoBox.SelectedValue;

                if (editItem.Produtos.Any(pf => pf.IdProduto == idProdutoSelecionado))
                {
                    MessageBox.Show("Este produto já está vinculado a este fornecedor.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cria novo ProdutoFornecedor em memória
                ProdutoFornecedor p = new ProdutoFornecedor();
                p.IdProduto = idProdutoSelecionado;
                p.NomeProduto = produtoBox.Text;
                p.IdFornecedor = editItem.Id;

                editItem.Produtos.Add(p);

                // Força a atualização do BindingSource
                listProdutos.DataSource = null;
                listProdutos.DataSource = editItem.Produtos;

                // MANTÉM a seleção do ComboBox para evitar aviso de "nenhum produto selecionado"
                // Apenas limpa o valor (que é o que o SelectedIndex = -1 fazia)
                produtoBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao vincular produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeProdutoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (listProdutos.CurrentRow == null || editItem == null || editItem.Produtos.Count == 0)
                {
                    MessageBox.Show("Selecione um produto para remover.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itemSelecionado = listProdutos.CurrentRow.DataBoundItem as ProdutoFornecedor;
                if (itemSelecionado == null) return;

                var result = MessageBox.Show(
                    $"Tem certeza que deseja desvincular o produto '{itemSelecionado.NomeProduto}'?",
                    "Confirmar Remoção",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No) return;

                // Se o item tem ID > 0, ele já está no banco e deve ser marcado para exclusão
                if (itemSelecionado.Id > 0)
                {
                    editItem.ProdutosApagados.Add(itemSelecionado);
                }

                // Remove da lista interna (que é o DataSource)
                editItem.Produtos.Remove(itemSelecionado);

                // Força a atualização do BindingSource
                listProdutos.DataSource = null;
                listProdutos.DataSource = editItem.Produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao remover vínculo do produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi na coluna de botão "Remover"
            if (e.RowIndex >= 0 && listProdutos.Columns[e.ColumnIndex].Name == "colRemoverItem")
            {
                // Verifica se o modo não é Listagem (permitindo ação apenas em edição/criação)
                if (state != ViewState.Listing && e.RowIndex < listProdutos.Rows.Count)
                {
                    // A lógica do removeProdutoBtn_Click usa listProdutos.CurrentRow.
                    // Para evitar a dupla confirmação, chamamos a lógica de remoção
                    // SÓ DEPOIS que o clique já está processado pelo DGV (impedindo recursão)

                    // Garante que a linha clicada seja a linha atual
                    listProdutos.Rows[e.RowIndex].Selected = true;

                    // Executa a lógica de confirmação e remoção
                    removeProdutoBtn_Click(sender, EventArgs.Empty);
                }
            }
        }
    }
}