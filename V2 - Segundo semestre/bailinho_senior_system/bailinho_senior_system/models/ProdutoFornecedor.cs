using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class ProdutoFornecedor
    {
        private int id;
        private int id_produto;
        private int id_fornecedor;
        private string nome_produto;
        private string nome_fornecedor;

        public ProdutoFornecedor() { }

        public ProdutoFornecedor(int id, int id_produto, int id_fornecedor) 
        {
            this.id = id;
            this.id_produto = id_produto;
            this.id_fornecedor = id_fornecedor;
        }

        public int Id
        {
            get { return id; } 
            set { id = value; }
        }
        public int IdProduto { get { return id_produto; } set { id_produto = value; } }
        public int IdFornecedor { get { return id_fornecedor; } set { id_fornecedor = value; } }

        public string NomeProduto { get { return nome_produto; } set { nome_produto = value; } }
        public string NomeFornecedor { get { return nome_fornecedor; } set { nome_fornecedor = value; } }
    }
}
