using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bailinho_senior_system.models;
using bailinho_senior_system.repositories;

namespace bailinho_senior_system.views
{
    public partial class ProdutosView : Form
    {
        List<Produto> produtos = new List<Produto>();
        int currentIndex = 0;

        private enum ViewState { Listing, Editing, Creating }
        private ViewState state;
        private Produto editItem = null;

        public ProdutosView()
        {
            InitializeComponent();
        }

        private void ProdutosView_Load(object sender, EventArgs e)
        {
            currentIndex = 0;
            ReadCategorias();
            ReadProdutos();
            if (produtos.Count > 0)
                PopulateProduto(produtos[currentIndex]);


            SetState(ViewState.Listing);
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

            if (categoriaBox.Text.Trim().Length == 0) errors.Add("Escolha um fornecedor.");

            return errors;
        }

        private void SetState(ViewState newState)
        {
            state = newState;

            var creating = state == ViewState.Creating;
            var editing = state == ViewState.Editing;
            var listing = state == ViewState.Listing;

            deleteBtn.Enabled = produtos.Count > 0 && listing;
            editBtn.Enabled = produtos.Count > 0 && listing;
            newBtn.Enabled = listing;
            searchBtn.Enabled = produtos.Count > 0 && listing;

            saveBtn.Enabled = editing || creating;
            cancelBtn.Enabled = editing || creating;

            nextBtn.Enabled = produtos.Count > 0 && listing && currentIndex < produtos.Count - 1;
            lastBtn.Enabled = produtos.Count > 0 && listing && currentIndex < produtos.Count - 1;
            firstBtn.Enabled = produtos.Count > 0 && listing && currentIndex > 0;
            previousBtn.Enabled = produtos.Count > 0 && listing && currentIndex > 0;

            nomeBox.ReadOnly = listing;
            descricaoBox.ReadOnly = listing;
            qtdEstoqueBox.Enabled = !(listing);
            precoBox.Enabled = !(listing);
            fornecedoresBox.Enabled = !(listing);
            categoriaBox.Enabled = !(listing);

            if (produtos.Count == 0 || creating)
            {
                CleanupFields();
            }
            else if (listing)
            {
                PopulateProduto(produtos[currentIndex]);
            }

        }

        private void ReadCategorias()
        {
            CategoriaRepository repo = new CategoriaRepository();
            List<Categoria> categorias = repo.GetCategorias();

            if (categorias.Count == 0) { return; }

            categoriaBox.DisplayMember = "Nome";
            categoriaBox.ValueMember = "Id";
            categoriaBox.DataSource = categorias;
            categoriaBox.SelectedIndex = -1;
        }

        private void ReadProdutos()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nome");
            dataTable.Columns.Add("Descrição");
            dataTable.Columns.Add("Qtd Estoque");
            dataTable.Columns.Add("Preço");
            dataTable.Columns.Add("Categoria");

            ProdutoRepository produtoRepository = new ProdutoRepository();
            this.produtos = produtoRepository.GetProdutos();
            foreach (Produto p in produtos)
            {
                var row = dataTable.NewRow();

                row["Id"] = p.Id;
                row["Nome"] = p.Nome;
                row["Descrição"] = p.Descricao;
                row["Qtd Estoque"] = p.QtdEstoque;
                row["Preço"] = p.Preco;
                row["Categoria"] = p.Categoria;

                dataTable.Rows.Add(row);
            }

            produtosTable.DataSource = dataTable;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void descricao_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void preco_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < produtos.Count - 1) currentIndex++;
            SetState(ViewState.Listing);
        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex = 0;
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < produtos.Count - 1) currentIndex = produtos.Count - 1;
            SetState(ViewState.Listing);
        }

        private void PopulateProduto(Produto produto)
        {
            idBox.Text = produto.Id.ToString();
            nomeBox.Text = produto.Nome ?? "";
            descricaoBox.Text = produto.Descricao ?? "";
            qtdEstoqueBox.Value = Math.Max(0, produto.QtdEstoque);
            precoBox.Value = produto.Preco;
            if (produto.IdCategoria > 0)
                categoriaBox.SelectedValue = produto.IdCategoria;
            else
                categoriaBox.SelectedValue = -1;
        }

        private void CleanupFields()
        {
            idBox.Text = "";
            categoriaBox.Text = "";
            nomeBox.Text = "";
            descricaoBox.Text = "";
            qtdEstoqueBox.Value = 0;
            precoBox.Value = 0;
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
            editItem = produtos[currentIndex];
            SetState(ViewState.Editing);
        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            List<string> errors = ValidateForm();
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Erros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProdutoRepository produtoRepository = new ProdutoRepository();

            editItem.Nome = nomeBox.Text.Trim();
            editItem.Descricao = descricaoBox.Text.Trim();
            editItem.Preco = precoBox.Value;
            editItem.QtdEstoque = (int)qtdEstoqueBox.Value;

            if (categoriaBox.SelectedValue != null && int.TryParse(categoriaBox.SelectedValue.ToString(), out int catId))
            {
                editItem.IdCategoria = catId;
            }
            else
            {
                editItem.IdCategoria = 0;
            }


            if (state == ViewState.Creating)
            {

                produtoRepository.CreateProduto(editItem);
                ReadProdutos();
                currentIndex = produtos.Count - 1;
            }
            else if (state == ViewState.Editing)
            {
                produtoRepository.UpdateProduto(editItem);
                ReadProdutos();
            }

            SetState(ViewState.Listing);
            MessageBox.Show("Produto salvo com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (produtos.Count == 0) 
            {
                return;
            }
            
            ProdutoRepository produtoRepository = new ProdutoRepository();
            produtoRepository.DeleteProduto(produtos[currentIndex].Id);

            if (produtos.Count > 0)
            {
                if (currentIndex > produtos.Count - 1) currentIndex--;
            }
            else currentIndex = 0;

            editItem = null;
            SetState(ViewState.Listing);
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
    }
}
