using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bailinho_senior_system;

namespace bailinho_senior_system.views
{
    public partial class ProdutosView : Form
    {
        List<Produto> produtos = new List<Produto>();
        int currentIndex = 0;

        private enum ViewState { Listing, Creating, Editing }
        private ViewState state;
        private Produto editItem = null;

        private List<string> ValidateForm()
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(nomeBox.Text)) erros.Add("Nome é obrigatório.");
            else if (nomeBox.Text.Length > 150) erros.Add("Nome não pode ter mais de 150 caracteres.");

            if (string.IsNullOrWhiteSpace(descricaoBox.Text)) erros.Add("Descrição é obrigatória.");
            else if (descricaoBox.Text.Length > 150) erros.Add("Descrição não pode ter mais de 150 caracteres.");

            if (qtdEstoqueBox.Value < 0) erros.Add("Quantidade em estoque não pode ser negativa.");
            if (precoBox.Value < 0) erros.Add("Preço não pode ser negativo.");

            return erros;
        }

        private void SetState(ViewState newState)
        {
            state = newState;
            bool listing = state == ViewState.Listing;
            bool editing = state == ViewState.Editing || state == ViewState.Creating;

            nomeBox.ReadOnly = !editing;
            descricaoBox.ReadOnly = !editing;
            qtdEstoqueBox.Enabled = editing;
            precoBox.Enabled = editing;
            newBtn.Enabled = listing;
            editBtn.Enabled = listing && produtos.Count > 0;
            deleteBtn.Enabled = listing && produtos.Count > 0;
            searchBtn.Enabled = listing;
            firstBtn.Enabled = listing && produtos.Count > 1 && currentIndex != 0;
            previousBtn.Enabled = listing && currentIndex > 0;
            nextBtn.Enabled = listing && currentIndex < produtos.Count - 1;
            lastBtn.Enabled = listing && produtos.Count > 1 && currentIndex != produtos.Count -1;
            saveBtn.Enabled = editing;
            cancelBtn.Enabled = editing;
            exitBtn.Enabled = true;

            if (listing)
            {
                // mostrar registro atual
                if (produtos.Count > 0)
                    PopulateProduto(produtos[currentIndex]);
                else
                    CleanupFields();
            }
            else
            {
                if (state == ViewState.Creating)
                {
                    editItem = new Produto();
                    CleanupFields();
                }
                else if (state == ViewState.Editing)
                {
                    editItem = produtos[currentIndex];
                    PopulateProduto(editItem);
                }
            }
        }

        public ProdutosView()
        {
            InitializeComponent();
        }

        private void ProdutosView_Load(object sender, EventArgs e)
        {
            produtos.Add(new Produto() { Nome = "Produto 1", Descricao = "Descrição do Produto 1", QtdEstoque = 10, Preco = 19.99m });
            produtos.Add(new Produto() { Nome = "Produto 2", Descricao = "Descrição do Produto 2", QtdEstoque = 10, Preco = 19.99m });
            produtos.Add(new Produto() { Nome = "Produto 3", Descricao = "Descrição do Produto 3", QtdEstoque = 10, Preco = 19.99m });
            produtos.Add(new Produto() { Nome = "Produto 4", Descricao = "Descrição do Produto 4", QtdEstoque = 10, Preco = 19.99m });

            currentIndex = 0;
            PopulateProduto(produtos[currentIndex]);

            SetState(ViewState.Listing);
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

        private void CleanupFields()
        {
            nomeBox.Text = "";
            descricaoBox.Text = "";
            qtdEstoqueBox.Value = 0;
            precoBox.Value = 0;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            var erros = ValidateForm();
            if (erros.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, erros), "Erros de validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // aplica valores ao editItem (objeto temporário ou existente)
            editItem.Nome = nomeBox.Text.Trim();
            editItem.Descricao = descricaoBox.Text.Trim();
            editItem.QtdEstoque = (int)qtdEstoqueBox.Value;
            editItem.Preco = precoBox.Value;

            if (state == ViewState.Creating)
            {
                produtos.Add(editItem);
                currentIndex = produtos.Count - 1;
            }

            SetState(ViewState.Listing);

            MessageBox.Show("Produto salvo com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            currentIndex = 0;
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            currentIndex = Math.Max(0, produtos.Count - 1);
            SetState(ViewState.Listing);
        }

        private void PopulateProduto(Produto produto)
        {
            nomeBox.Text = produto.Nome ?? "";
            descricaoBox.Text = produto.Descricao ?? "";
            qtdEstoqueBox.Value = Math.Max(0, produto.QtdEstoque);
            precoBox.Value = produto.Preco;
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            SetState(ViewState.Creating);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            SetState(ViewState.Listing);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (produtos.Count == 0) return;
            SetState(ViewState.Editing);
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (produtos.Count == 0) return;
            if (MessageBox.Show("Excluir este produto?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                produtos.RemoveAt(currentIndex);
                if (currentIndex >= produtos.Count) currentIndex = Math.Max(0, produtos.Count - 1);
                SetState(ViewState.Listing);
                MessageBox.Show("Produto deletado com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            if (state != ViewState.Listing)
            {
                if (MessageBox.Show("Existem alterações não salvas. Deseja sair mesmo assim?", "Confirmar saída", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }
    }
}
