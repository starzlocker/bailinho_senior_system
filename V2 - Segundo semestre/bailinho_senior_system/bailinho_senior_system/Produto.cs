using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system
{
    internal class Produto
    {
        private string nome;
        private string descricao;
        private int qtd_estoque;
        private decimal preco;

        public Produto() { }

        public Produto(
            string nome,
            string descricao,
            int qtd_estoque,
            decimal preco
        ) {
            setNome(nome);
            setDescricao(descricao);
            setQtdEstoque(qtd_estoque);
            setPreco(preco);
        }

        public string getNome()
        {
            return nome;
        }

        public string getDescricao()
        {
            return descricao;
        }

        public int getQtdEstoque()
        {
            return qtd_estoque;
        }

        public decimal getPreco()
        {
            return preco;
        }

        public string setNome(string nome)
        {
            string validacao = ValidadorHelper.VerificarVazio(nome, "Nome");
            if (validacao != null) return validacao;

            if (nome.Length > 150)
                return "O nome do produto não pode conter mais que 150 caracteres";

            this.nome = nome;
            return "sucesso";
        }

        public string setDescricao(string descricao)
        {
            string validacao = ValidadorHelper.VerificarVazio(descricao, "Descrição");
            if (validacao != null) return validacao;

            if (descricao.Length > 150)
                return "A descrição do produto não pode conter mais que 150 caracteres";

            this.descricao = descricao;
            return "sucesso";
        }

        public string setQtdEstoque(int qtd_estoque)
        {
            if (qtd_estoque < 0)
                return "A quantidade em estoque não pode ser negativa";

            this.qtd_estoque = qtd_estoque;
            return "sucesso";
        }

        public string setPreco(decimal preco)
        {
            if (preco < 0)
                return "O preço do produto não pode ser negativo";

            this.preco = preco;
            return "sucesso";
        }
    }
}
