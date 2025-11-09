using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class Produto
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Id cannot be negative", nameof(value));

                id = value;
            }
        }
        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                string validacao = ValidadorHelper.VerificarVazio(value, "Nome");
                if (validacao != null) throw new ArgumentException(validacao);

                if (value.Length > 150)
                    throw new ArgumentException("O nome do produto não pode conter mais que 150 caracteres");

                this.nome = value;
            }
        }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                string validacao = ValidadorHelper.VerificarVazio(value, "Descrição");
                if (validacao != null) throw new ArgumentException(validacao);

                if (value.Length > 150)
                    throw new ArgumentException("A descrição do produto não pode conter mais que 150 caracteres");

                this.descricao = value;
            }
        }

        private int qtd_estoque;
        public int QtdEstoque
        {
            get { return qtd_estoque; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("A quantidade em estoque não pode ser negativa");
                }

                this.qtd_estoque = value;
            }
        }

        private decimal preco;
        public decimal Preco
        {
            get { return preco; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("O preço do produto não pode ser negativo");

                this.preco = value;
            }
        }

        private int id_categoria;
        public int IdCategoria
        {
            get { return id_categoria; }
            set { id_categoria = value; }
        }

        private string categoria;
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        private int id_fornecedor;
        public int IdFornecedor
        {
            get { return id_fornecedor; }
            set
            {
                id_fornecedor = value;
            }
        }

        private string fornecedor;
        public string Fornecedor
        {
            get { return fornecedor; }
            set 
            {
                fornecedor = value;
            }
        }

        public Produto() { }
        public Produto(
            string Nome,
            string Descricao,
            int QtdEstoque,
            decimal Preco
        )
        {
            this.Nome = Nome;
            this.Descricao = Descricao;
            this.QtdEstoque = QtdEstoque;
            this.Preco = Preco;
        }
    }
}
