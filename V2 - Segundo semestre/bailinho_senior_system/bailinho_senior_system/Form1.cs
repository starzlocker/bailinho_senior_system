using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bailinho_senior_system.views;

namespace bailinho_senior_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void vendasBtn_Click(object sender, EventArgs e)
        {
            using (VendasView form = new VendasView())
            {
                form.ShowDialog();
            }
        }

        private void produtosBtn_Click(object sender, EventArgs e)
        {
            using (ProdutosView form = new ProdutosView())
            {
                form.ShowDialog();
            }
        }

        private void eventosBtn_Click(object sender, EventArgs e)
        {
            using (EventosView form = new EventosView())
            {
                form.ShowDialog();
            }
        }

        private void fornecedoresBtn_Click(object sender, EventArgs e)
        {
            using (FornecedoresView form = new FornecedoresView())
            {
                form.ShowDialog();
            }
        }

        private void clientesBtn_Click(object sender, EventArgs e)
        {
            using (ClientesView form = new ClientesView())
            {
                form.ShowDialog();
            }
        }

        private void categoriasBtn_Click(object sender, EventArgs e)
        {
            using (CategoriasView form = new CategoriasView())
            {
                form.ShowDialog();
            }
        }
    }
}
