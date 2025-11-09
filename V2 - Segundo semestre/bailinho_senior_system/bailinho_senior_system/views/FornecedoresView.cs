using bailinho_senior_system.models;
using bailinho_senior_system.repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bailinho_senior_system.views
{
    public partial class FornecedoresView : Form
    {
        List<Fornecedor> fornecedores = new List<Fornecedor>();
        int currentIndex = 0;

        private enum ViewState { Listing, Editing, Creating }
        private ViewState state;
        private Fornecedor editItem = null;

        public FornecedoresView()
        {
            InitializeComponent();
        }

        private void FornecedoresView_Load(object sender, EventArgs e)
        {
            tabControl.Selecting += tabControl_Selecting;

            currentIndex = 0;
            ReadProdutos();
            ReadFornecedores();
            if (fornecedores.Count > 0)
                PopulateFornecedor(fornecedores[currentIndex]);

            SetState(ViewState.Listing);
        }

        private List<string> ValidateForm()
        {
            List<string> errors = new List<string>();

            if (nomeBox.Text.Length == 0) errors.Add("Nome não pode estar vazio!");
            else if (nomeBox.Text.Length > 150) errors.Add("Nome deve ter até 150 caracteres.");

            if (cnpjBox.Text.Length == 0) errors.Add("CNPJ não pode estar vazio!");
            else if (cnpjBox.Text.Length > 14) errors.Add("CPNJ deve ter no máximo 14 dígitos!");

            if (emailBox.Text.Length == 0) errors.Add("E-mail não pode estar vazio!");
            else if (emailBox.Text.Length > 150) errors.Add("E-mail deve ter até 150 caracteres.");

            if (telefoneBox.Text.Length == 0) errors.Add("Telefone não pode estar vazio!");

            return errors;
        }

        private void SetState(ViewState newState)
        {
            state = newState;

            var creating = state == ViewState.Creating;
            var editing = state == ViewState.Editing;
            var listing = state == ViewState.Listing;


            string curTab = tabControl.SelectedTab?.Name ?? "";

            if (creating || editing)
            {
                SwitchToTabByName("tabPageCadastro");
            }


            deleteBtn.Enabled = fornecedores.Count > 0 && listing;
            editBtn.Enabled = fornecedores.Count > 0 && listing;
            newBtn.Enabled = listing;
            searchBtn.Enabled = fornecedores.Count > 0 && listing;

            saveBtn.Enabled = editing || creating;
            cancelBtn.Enabled = editing || creating;

            nextBtn.Enabled = fornecedores.Count > 0 && listing && currentIndex < fornecedores.Count - 1;
            lastBtn.Enabled = fornecedores.Count > 0 && listing && currentIndex < fornecedores.Count - 1;
            firstBtn.Enabled = fornecedores.Count > 0 && listing && currentIndex > 0;
            previousBtn.Enabled = fornecedores.Count > 0 && listing && currentIndex > 0;

            nomeBox.ReadOnly = listing;
            cnpjBox.ReadOnly = listing;
            emailBox.ReadOnly = listing;
            telefoneBox.ReadOnly = listing;
            produtoBox.Enabled = !listing;
            addProdutoBtn.Enabled = !listing;
            removeProdutoBtn.Enabled = !listing;


            if (fornecedores.Count == 0 || creating)
            {
                CleanupFields();
            }
            else if (listing)
            {
                PopulateFornecedor(fornecedores[currentIndex]);
            }

        }

        private void ReadFornecedores()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Nome");
                dataTable.Columns.Add("CNPJ");
                dataTable.Columns.Add("E-mail");
                dataTable.Columns.Add("Telefone");

                FornecedorRepository fornecedorRepository = new FornecedorRepository();
                this.fornecedores = fornecedorRepository.GetFornecedores();
                foreach (Fornecedor f in fornecedores)
                {
                    var row = dataTable.NewRow();

                    row["Id"] = f.Id;
                    row["Nome"] = f.Nome;
                    row["CNPJ"] = f.Cnpj;
                    row["E-mail"] = f.Email;
                    row["Telefone"] = f.Telefone;

                    dataTable.Rows.Add(row);
                }

                listTable.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar fornecedores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < fornecedores.Count - 1) currentIndex++;
            SetState(ViewState.Listing);
        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex = 0;
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < fornecedores.Count - 1) currentIndex = fornecedores.Count - 1;
            SetState(ViewState.Listing);
        }

        private void PopulateFornecedor(Fornecedor fornecedor)
        {
            idBox.Text = fornecedor.Id.ToString();
            nomeBox.Text = fornecedor.Nome ?? "";
            cnpjBox.Text = fornecedor.Cnpj ?? "";
            emailBox.Text = fornecedor.Email ?? "";
            telefoneBox.Text = fornecedor.Telefone ?? "";

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

        }

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
            editItem = fornecedores[currentIndex];
            editItem.ProdutosApagados.Clear();
            SetState(ViewState.Editing);
        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (editItem == null)
                {
                    MessageBox.Show("Nenhum fornecedor selecionado para salvar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<string> errors = ValidateForm();
                if (errors.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", errors), "Erros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FornecedorRepository fornecedorRepository = new FornecedorRepository();
                ProdutoFornecedorRepository produtoFornecedorRepository = new ProdutoFornecedorRepository();

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


                if (state == ViewState.Creating)
                {
                    try
                    {
                        fornecedorRepository.CreateFornecedor(editItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao criar fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        produtoFornecedorRepository.CreateFromListProdutoFornecedor(editItem.Produtos);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao vincular produtos ao fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ReadFornecedores();
                    currentIndex = fornecedores.Count - 1;
                }
                else if (state == ViewState.Editing)
                {
                    try
                    {
                        fornecedorRepository.UpdateFornecedor(editItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        produtoFornecedorRepository.CreateFromListProdutoFornecedor(editItem.Produtos, editItem.ProdutosApagados);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao vincular produtos ao fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ReadFornecedores();
                }


                SetState(ViewState.Listing);
                MessageBox.Show("Fornecedor salvo com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (fornecedores.Count == 0)
                {
                    return;
                }

                var result = MessageBox.Show(
                    $"Tem certeza que deseja excluir a o fornecedor '{fornecedores[currentIndex].Nome}'?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;

                FornecedorRepository fornecedorRepository = new FornecedorRepository();
                ProdutoFornecedorRepository produtoFornecedorRepository = new ProdutoFornecedorRepository();

                fornecedorRepository.DeleteFornecedor(fornecedores[currentIndex].Id);
                produtoFornecedorRepository.DeleteAllProdutoFornecedor(fornecedores[currentIndex].Id);
                fornecedorRepository.DeleteFornecedor(fornecedores[currentIndex].Id);

                ReadFornecedores();

                if (fornecedores.Count > 0)
                {
                    if (currentIndex > fornecedores.Count - 1) currentIndex--;
                }
                else currentIndex = 0;

                editItem = null;
                SetState(ViewState.Listing);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

                if (result == DialogResult.Cancel)
                    return; // usuário cancelou — volta para o formulário
            }
            this.Close();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SwitchToTabByName("tabPageLista");
            searchBox.Focus();
        }

        private void SwitchToTabByName(string tabName)
        {
            if (string.IsNullOrEmpty(tabName)) return;
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Fornecedor> fornecedoresEncontrados = fornecedores;
            if (searchBox.Text.Trim().Length > 0)
            {
                string searchStr = searchBox.Text.Trim().ToLower();

                if (int.TryParse(searchStr, out int id))
                {
                    fornecedoresEncontrados = fornecedores.FindAll(f => f.Id == id);
                }
                else
                {
                    fornecedoresEncontrados = fornecedores.FindAll(f => f.Nome.ToLower().Contains(searchStr));
                }
            }
            if (fornecedoresEncontrados.Count > 0)
                currentIndex = fornecedores.FindIndex(f => f.Id == fornecedoresEncontrados[0].Id);
            listTable.DataSource = fornecedoresEncontrados;
            SetState(ViewState.Listing);
        }

        private void fornecedoresTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var cur = this.listTable.CurrentRow;
            if (cur != null)
                currentIndex = cur.Index;

            SetState(ViewState.Listing);
        }

        private void ReadProdutos()
        {
            try
            {
                ProdutoRepository repo = new ProdutoRepository();


                var produtos = repo.GetProdutos();


                if (produtos.Count == 0) { return; }

                produtoBox.DisplayMember = "Nome";
                produtoBox.ValueMember = "Id";
                produtoBox.DataSource = produtos;
                produtoBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ReadProdutosFornecedor(Fornecedor fornecedor)
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("IdProduto");
                dataTable.Columns.Add("Nome");

                ProdutoFornecedorRepository repo = new ProdutoFornecedorRepository();

                var produtosPorFornecedor = repo.GetProdutosPorFornecedor(fornecedor.Id);

                foreach (ProdutoFornecedor p in produtosPorFornecedor)
                {
                    fornecedor.Produtos.Add(p);
                    var row = dataTable.NewRow();
                    row["Id"] = p.Id;
                    row["IdProduto"] = p.IdProduto;
                    row["Nome"] = p.NomeProduto;
                    dataTable.Rows.Add(row);
                }

                listProdutos.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produtos do fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {

            if (state != ViewState.Listing)
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

        private void addProdutoBtn_Click(object sender, EventArgs e)
        {
            try
            {
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

                ProdutoFornecedor p = new ProdutoFornecedor();
                p.IdProduto = idProdutoSelecionado;
                p.NomeProduto = produtoBox.Text;
                p.IdFornecedor = editItem.Id;

                editItem.Produtos.Add(p);
                
                DataTable dt = listProdutos.DataSource as DataTable;
                if (dt == null)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Id");
                    dt.Columns.Add("IdProduto");
                    dt.Columns.Add("Nome");
                }
                
                // Adiciona a nova linha ao DataTable
                var row = dt.NewRow();
                row["Id"] = 0;
                row["IdProduto"] = p.IdProduto;
                row["Nome"] = p.NomeProduto;
                dt.Rows.Add(row);
                    
                listProdutos.DataSource = dt;
            
                // Limpa a seleção do combobox
                produtoBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao vincular produto com fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeProdutoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var cur = listProdutos.CurrentRow;
                if (cur == null)
                {
                    MessageBox.Show("Selecione um produto para remover.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int produtoId = int.Parse(cur.Cells["IdProduto"].Value.ToString());

                // Confirma a remoção
                var result = MessageBox.Show(
                    $"Tem certeza que deseja desvincular o produto '{cur.Cells["Nome"].Value}'?",
                    "Confirmar Remoção",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                // Remove da lista interna
                var produtoToRemove = editItem.Produtos.FirstOrDefault(pf => pf.IdProduto == produtoId);
                if (produtoToRemove != null)
                {
                    editItem.Produtos.Remove(produtoToRemove);
                    editItem.ProdutosApagados.Add(produtoToRemove);
                }

                // Remove do DataTable
                DataTable dt = listProdutos.DataSource as DataTable;
                if (dt != null)
                {
                    dt.Rows.RemoveAt(cur.Index);

                    listProdutos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao remover vínculo do produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
