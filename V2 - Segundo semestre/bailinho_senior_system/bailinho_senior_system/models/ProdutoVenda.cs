using System;

namespace bailinho_senior_system.models
{
    public class ProdutoVenda
    {
        public ProdutoVenda() { }
        public ProdutoVenda(int id, int idProduto, int idVenda, int quantidade, 
                                decimal valor, string nomeProduto, decimal precoUnitario)
        {
            Id = id;
            IdProduto = idProduto;
            IdVenda = idVenda;
            Quantidade = quantidade;
            Valor = valor;
            NomeProduto = nomeProduto;
            PrecoUnitario = precoUnitario;
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Id não pode ser negativo.", nameof(value));
                id = value;
            }
        }

        private int id_produto;
        public int IdProduto
        {
            get { return id_produto; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID do produto deve ser válido.");

                id_produto = value;
            }
        }

        private int id_venda;
        public int IdVenda
        {
            get { return id_venda; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID do produto deve ser válido.");
                    
                id_venda = value;
            }
        }

        private int quantidade;
        public int Quantidade
        {
            get { return quantidade; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantidade deve ser positiva.");
                quantidade = value;
            }
        }

        private decimal valor;
        public decimal Valor
        {
            get { return valor; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Valor não pode ser negativo.");

                valor = value;
            }
        }

        public string NomeProduto { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}