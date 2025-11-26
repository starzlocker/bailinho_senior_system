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
    public partial class FrmHome : Form
    {
        VendasView frmVendas;
        ProdutosView frmProdutos;
        EventosView frmEventos;
        ClientesView frmClientes;

        CategoriasView frmCategorias;
        FornecedoresView frmFornecedores;

        public FrmHome()
        {
            InitializeComponent();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            if(frmProdutos == null || frmProdutos.IsDisposed)
            {
                frmProdutos = new ProdutosView();
                frmProdutos.Show();
            }

        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            if (frmEventos == null || frmEventos.IsDisposed)
            {
                frmEventos = new EventosView();
                frmEventos.Show();
            }
        }

        private void btnParticipantes_Click(object sender, EventArgs e)
        {
            if (frmClientes == null || frmClientes.IsDisposed)
            {
                frmClientes = new ClientesView();
                frmClientes.Show();
            }
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            if (frmVendas == null || frmVendas.IsDisposed)
            {
                frmVendas = new VendasView();
                frmVendas.Show();
            }
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            if (frmCategorias == null || frmCategorias.IsDisposed)
            {
                frmCategorias = new CategoriasView();
                frmCategorias.Show();
            }
        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            if (frmFornecedores == null || frmFornecedores.IsDisposed)
            {
                frmFornecedores = new FornecedoresView();
                frmFornecedores.Show();
            }
        }

        private void imgProduto_Click(object sender, EventArgs e)
        {
            this.btnProdutos_Click(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.btnProdutos_Click(sender, e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.btnProdutos_Click(sender, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.btnEventos_Click(sender, e);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.btnEventos_Click(sender, e);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.btnEventos_Click(sender, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            btnParticipantes_Click(sender, e);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            btnParticipantes_Click(sender, e);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            btnParticipantes_Click(sender, e);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            btnCategorias_Click(sender, e);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            btnCategorias_Click(sender, e);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            btnCategorias_Click(sender, e);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            btnFornecedores_Click(sender, e);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            btnFornecedores_Click(sender, e);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            btnFornecedores_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.btnVendas_Click(sender, e);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.btnVendas_Click(sender, e);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.btnVendas_Click(sender, e);
        }
    }
}
