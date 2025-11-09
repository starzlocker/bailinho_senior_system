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
        private void panel1_Click(object sender, EventArgs e)
        {
            if (frmFornecedores == null || frmFornecedores.IsDisposed)
            {
                frmFornecedores = new FornecedoresView();
                frmFornecedores.Show();
            }
        }
    }
}
