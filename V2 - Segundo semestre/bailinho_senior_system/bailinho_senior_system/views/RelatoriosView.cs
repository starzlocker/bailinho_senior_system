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
    public partial class RelatoriosView : Form
    {
        public RelatoriosView()
        {
            InitializeComponent();

            this.Load += RelatoriosView_Load;
        }

        private void RelatoriosView_Load(object sender, EventArgs e)
        {
            RelatorioRepository relatorioRepository = new RelatorioRepository();

            this.totalDeVendas.Text = relatorioRepository.GetTotalVendas().ToString();
            this.totalVendido.Text = "R$ " + relatorioRepository.getTotalVendido().ToString("F2");
            this.ticketMedio.Text = "R$ " + relatorioRepository.getTicketMedio().ToString("F2");

            this.eventosRealizados.Text = relatorioRepository.getEventosRealizados().ToString();
            this.qntdFornecedores.Text = relatorioRepository.getFornecedoresCadastrados().ToString();
            this.qntdClientes.Text = relatorioRepository.getClientesCadastrados().ToString();

            this.estoqueTotal.Text = relatorioRepository.getEstoqueTotalProdutos().ToString();
            this.qntdProduto.Text = relatorioRepository.getProdutosCadastrados().ToString();
            this.produtosVendidos.Text = relatorioRepository.getProdutosVendidos().ToString();
        }
    }
}
