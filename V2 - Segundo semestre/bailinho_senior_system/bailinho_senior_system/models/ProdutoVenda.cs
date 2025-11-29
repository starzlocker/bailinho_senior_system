using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class ProdutoVenda // CLASSE TORNADA PÚBLICA
    {
        // Chave primária (PK) adicionada para refletir o esquema SQL CREATE TABLE
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
                // O valor pode ser 0 inicialmente, antes de a Venda ser salva.
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
                // Valor aqui representa o subtotal da linha (quantidade * preco_unitario)
                if (value < 0)
                    throw new ArgumentException("Valor da linha não pode ser negativo.");
                valor = value;
            }
        }

        // Propriedades de Lookup (Para uso na View e Relatórios)
        public string NomeProduto { get; set; }
        public decimal PrecoUnitario { get; set; } // O preço unitário no momento da venda

        public ProdutoVenda() { }
    }
}