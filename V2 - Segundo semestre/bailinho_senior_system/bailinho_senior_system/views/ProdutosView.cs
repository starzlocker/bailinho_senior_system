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
        
        public ProdutosView()
        {
            InitializeComponent();
        }

        private void ProdutosView_Load(object sender, EventArgs e)
        {

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

            Produto produto = new Produto();
            string nome = nomeBox.Text;
            string descricao = descricaoBox.Text;
            int qtd_estoque = (int)qtdEstoqueBox.Value;
            decimal preco = precoBox.Value;
            List<string> erros = new List<string>();

            try { produto.Nome = nome; }
            catch (Exception ex) { erros.Add(ex.Message); }

            try { produto.Descricao = descricao; }
            catch (Exception ex) { erros.Add(ex.Message); }

            try { produto.QtdEstoque = qtd_estoque; }
            catch (Exception ex) { erros.Add(ex.Message); }

            try { produto.Preco = preco; }
            catch (Exception ex) { erros.Add(ex.Message); }

            if (erros.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, erros), "Erros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Produto salvo com sucesso!");
            CleanupFields();

        }
    }
}
