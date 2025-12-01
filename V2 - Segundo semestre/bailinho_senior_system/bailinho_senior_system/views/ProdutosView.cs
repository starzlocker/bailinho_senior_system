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
    public partial class ProdutosView : Form
    {
        private enum ViewState { Listing, Editing, Creating }

        private List<Produto> produtos = new List<Produto>();
        private int currentIndex = 0;
        private ViewState state;
        private Produto editItem = null;

        private ProdutoRepository produtoRepository = new ProdutoRepository();
        private CategoriaRepository categoriaRepository = new CategoriaRepository();
        private FornecedorRepository fornecedorRepository = new FornecedorRepository();
        private ProdutoFornecedorRepository produtoFornecedorRepository = new ProdutoFornecedorRepository();

        public ProdutosView()
        {
            InitializeComponent();
        }

        private void ProdutosView_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView(listTable);

            ReadCategorias();
            ReadProdutos();

            if (produtos.Count > 0)
                PopulateProduto(produtos[currentIndex]);

            SetState(ViewState.Listing);
        }

        private void tabControl_Selecting_1(object sender, TabControlCancelEventArgs e)
        {
            if (state != ViewState.Listing && e.TabPage.Name != "tabPageCadastro")
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

        private void ProdutosView_FormClosing_1(object sender, FormClosingEventArgs e)
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

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            // Configurações visuais e de comportamento
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
            dgv.Columns.Clear();

            // Adição e Mapeamento das Colunas
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Name = "Id",
                Visible = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Nome",
                DataPropertyName = "Nome",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Qtd. Estoque",
                DataPropertyName = "QtdEstoque",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Preço",
                DataPropertyName = "Preco",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C", Alignment = DataGridViewContentAlignment.MiddleRight },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Categoria",
                DataPropertyName = "Categoria",
            });
        }

        private void ReadCategorias()
        {
            try
            {
                List<Categoria> categorias = categoriaRepository.GetCategorias();

                if (categorias.Count == 0) { return; }

                categoriaBox.DisplayMember = "Nome";
                categoriaBox.ValueMember = "Id";
                categoriaBox.DataSource = categorias;
                categoriaBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadProdutos()
        {
            try
            {
                // Carrega todos os produtos
                this.produtos = produtoRepository.GetProdutos();
                listTable.DataSource = null;
                listTable.DataSource = this.produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadFornecedoresProduto(Produto produto)
        {
            // Busca e vincula a lista de fornecedores para o produto atual
            try
            {
                var fornecedoresPorProduto = produtoFornecedorRepository.GetFornecedoresPorProduto(produto.Id);

                // Limpa e repopula a lista interna do objeto produto (ou editItem)
                produto.Fornecedores.Clear();
                foreach (var pf in fornecedoresPorProduto)
                {
                    // Apenas adiciona se o item não estiver marcado para ser apagado
                    if (!produto.FornecedoresApagados.Any(pa => pa.IdFornecedor == pf.IdFornecedor))
                    {
                        produto.Fornecedores.Add(pf);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar fornecedores vinculados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PopulateProduto(Produto produto)
        {
            idBox.Text = produto.Id.ToString();
            nomeBox.Text = produto.Nome ?? "";
            descricaoBox.Text = produto.Descricao ?? "";

            qtdEstoqueBox.Value = Math.Max(0, produto.QtdEstoque);
            precoBox.Value = produto.Preco;
            categoriaBox.SelectedValue = produto.IdCategoria > 0 ? (object)produto.IdCategoria : null;

            ReadFornecedoresProduto(produto);

            lbFornecedores.DataSource = null;
            lbFornecedores.DisplayMember = "NomeFornecedor";
            lbFornecedores.ValueMember = "IdFornecedor";     

            List<ProdutoFornecedor> fornecedoresDoProduto = editItem != null ? editItem.Fornecedores : produto.Fornecedores;

            lbFornecedores.DataSource = fornecedoresDoProduto;
            lbFornecedores.ClearSelected();

        }

        private void CleanupFields()
        {
            idBox.Text = "";
            categoriaBox.SelectedValue = -1;

            nomeBox.Text = "";
            descricaoBox.Text = "";
            qtdEstoqueBox.Value = 0;
            precoBox.Value = 0;
            lbFornecedores.DataSource = null;

            if (editItem != null)
            {
                editItem.Fornecedores.Clear();
                editItem.FornecedoresApagados.Clear();
            }
        }

        private List<string> ValidateForm()
        {
            List<string> errors = new List<string>();

            if (nomeBox.Text.Length == 0) errors.Add("Nome não pode estar vazio!");
            else if (nomeBox.Text.Length > 150) errors.Add("Nome deve ter até 150 caracteres.");

            if (descricaoBox.Text.Length == 0) errors.Add("Descrição não pode estar vazio!");
            else if (descricaoBox.Text.Length > 150) errors.Add("Descricao deve ter até 150 caracteres.");

            if (qtdEstoqueBox.Value < 0) errors.Add("Quantidade não pode ser negativa");
            if (precoBox.Value < 0) errors.Add("Preço não pode ser negativo.");

            if (categoriaBox.SelectedValue == null || (int)categoriaBox.SelectedValue == 0) errors.Add("Selecione uma categoria.");

            return errors;
        }

        private void SetState(ViewState newState)
        {
            state = newState;

            var creating = state == ViewState.Creating;
            var editing = state == ViewState.Editing;
            var listing = state == ViewState.Listing;
            var creatingOrEditing = creating || editing;

            int count = produtos.Count;

            if (creatingOrEditing)
            {
                SwitchToTabByName("tabPageCadastro");
            }

            // CRUD
            deleteBtn.Enabled = count > 0 && listing;
            editBtn.Enabled = count > 0 && listing;
            newBtn.Enabled = listing;
            searchBtn.Enabled = listing;
            saveBtn.Enabled = creatingOrEditing;
            cancelBtn.Enabled = creatingOrEditing;

            // Navegação
            nextBtn.Enabled = count > 0 && listing && currentIndex < count - 1;
            lastBtn.Enabled = count > 0 && listing && currentIndex < count - 1;
            firstBtn.Enabled = count > 0 && listing && currentIndex > 0;
            previousBtn.Enabled = count > 0 && listing && currentIndex > 0;

            // ReadOnly/Enabled dos Campos
            nomeBox.ReadOnly = listing;
            descricaoBox.ReadOnly = listing;
            qtdEstoqueBox.Enabled = !listing;
            precoBox.Enabled = !listing;
            categoriaBox.Enabled = !listing;

            if (count == 0 || creating)
            {
                CleanupFields();
            }
            else if (listing && currentIndex >= 0)
            {
                lbFornecedores.ClearSelected();
                PopulateProduto(produtos[currentIndex]);
                UpdateDataGridViewSelection();
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
            // Seleciona o produto atual no listTable e rola para visualização
            if (listTable == null || produtos.Count == 0 || currentIndex < 0 || currentIndex >= produtos.Count)
            {
                listTable.ClearSelection();
                return;
            }

            // Sincroniza a seleção visual
            if (listTable.DataSource is List<Produto> listaExibida)
            {
                Produto itemAtual = produtos[currentIndex];
                int indexNaListaExibida = listaExibida.FindIndex(p => p.Id == itemAtual.Id);

                if (indexNaListaExibida != -1)
                {
                    listTable.ClearSelection();
                    listTable.Rows[indexNaListaExibida].Selected = true;
                    listTable.FirstDisplayedScrollingRowIndex = indexNaListaExibida;
                }
            }
        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            ReadProdutos();
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            ReadProdutos();
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < produtos.Count - 1) currentIndex++;
            ReadProdutos();
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            currentIndex = produtos.Count - 1;
            ReadProdutos();
            SetState(ViewState.Listing);
        }

        private void listTable_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= listTable.Rows.Count) return;
            if (state != ViewState.Listing) return;

            Produto produtoSelecionado = listTable.Rows[e.RowIndex].DataBoundItem as Produto;

            if (produtoSelecionado != null)
            {
                int novoIndex = produtos.FindIndex(p => p.Id == produtoSelecionado.Id);

                if (novoIndex != -1)
                {
                    currentIndex = novoIndex;
                }
                SetState(ViewState.Listing);
            }
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            editItem = new Produto();
            SetState(ViewState.Creating);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            editItem = null;
            SetState(ViewState.Listing);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (produtos.Count == 0) return;
            editItem = produtos[currentIndex];
            editItem.FornecedoresApagados.Clear(); // Limpa itens apagados antes de iniciar a edição
            SetState(ViewState.Editing);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (editItem == null) return;

            List<string> errors = ValidateForm();
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Erros de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ProdutoRepository produtoRepository = new ProdutoRepository();

                editItem.Nome = nomeBox.Text.Trim();
                editItem.Descricao = descricaoBox.Text.Trim();
                editItem.Preco = precoBox.Value;
                editItem.QtdEstoque = (int)qtdEstoqueBox.Value;
                editItem.IdCategoria = categoriaBox.SelectedValue != null ? (int)categoriaBox.SelectedValue : 0;

                if (state == ViewState.Creating)
                {
                    produtoRepository.CreateProduto(editItem);

                    foreach (ProdutoFornecedor pf in editItem.Fornecedores)
                    {
                        pf.IdProduto = editItem.Id;
                    }
                    produtoFornecedorRepository.CreateFromListProdutoFornecedor(editItem.Fornecedores);

                    ReadProdutos();
                    currentIndex = produtos.Count - 1;
                }
                else if (state == ViewState.Editing)
                {
                    produtoRepository.UpdateProduto(editItem);

                    // Cria/Atualiza/Exclui vinculações de fornecedores
                    produtoFornecedorRepository.CreateFromListProdutoFornecedor(editItem.Fornecedores, editItem.FornecedoresApagados);

                    ReadProdutos();
                }

                SetState(ViewState.Listing);
                MessageBox.Show("Produto salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (produtos.Count == 0) return;

            var result = MessageBox.Show(
                $"Tem certeza que deseja excluir a o produto '{produtos[currentIndex].Nome}'?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No) return;

            try
            {
                Produto produtoParaExcluir = produtos[currentIndex];

                // 1. Exclui as vinculações de fornecedores primeiro
                produtoFornecedorRepository.DeleteAllProdutoFornecedor(produtoParaExcluir.Id);

                // 2. Exclui o Produto
                produtoRepository.DeleteProduto(produtoParaExcluir.Id);

                ReadProdutos();

                if (produtos.Count > 0)
                {
                    currentIndex = Math.Max(0, currentIndex - 1);
                }
                else
                {
                    currentIndex = 0;
                }

                editItem = null;
                SetState(ViewState.Listing);

                MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException mysex) when (mysex.Number == 1451)
            {
                MessageBox.Show("Não foi possível excluir o Produto, pois ele possui vendas associadas.", "Erro de Chave Estrangeira", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void searchBtn_Click(object sender, EventArgs e)
        {
            ReadProdutos();
            SwitchToTabByName("tabPageLista");
            searchBox.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = searchBox.Text.Trim().ToLower();
            List<Produto> filteredProdutos;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Busca por ID, Nome, Descrição, Categoria ou Fornecedor
                filteredProdutos = produtos.Where(p =>
                    (int.TryParse(searchTerm, out int id) && p.Id == id) ||
                    (p.Nome != null && p.Nome.ToLower().Contains(searchTerm)) ||
                    (p.Descricao != null && p.Descricao.ToLower().Contains(searchTerm)) ||
                    (p.Categoria != null && p.Categoria.ToLower().Contains(searchTerm))
                ).ToList();
            }
            else
            {
                // Se a busca estiver vazia, retorna a lista completa
                filteredProdutos = produtos;
            }

            listTable.DataSource = null;
            listTable.DataSource = filteredProdutos;

            if (filteredProdutos.Count > 0)
            {
                // Sincroniza o currentIndex com o primeiro item encontrado na lista original
                currentIndex = produtos.FindIndex(p => p.Id == filteredProdutos[0].Id);
                PopulateProduto(filteredProdutos[0]);
            }
            else
            {
                currentIndex = -1;
                CleanupFields();
                MessageBox.Show("Nenhum produto encontrado.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SetState(ViewState.Listing);
        }
    }
}