using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class Fornecedor
    {
        private int id;
        private string cnpj;
        private string nome;
        private string email;
        private string telefone;
        private List<ProdutoFornecedor> produtos;
        private List<ProdutoFornecedor> produtos_apagados;

        public Fornecedor() 
        { 
            produtos = new List<ProdutoFornecedor>();
            produtos_apagados = new List<ProdutoFornecedor>();
        }

        public Fornecedor(
            string cnpj,
            string nome,
            string email,
            string telefone,
            int id
        )
        {
            produtos = new List<ProdutoFornecedor>();
            produtos_apagados = new List<ProdutoFornecedor>();
            Cnpj = cnpj;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Id = id;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Cnpj
        {
            get { return cnpj; }
            set
            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "CNPJ");
                if (validacaoDeVazio != null) throw new ArgumentException(validacaoDeVazio);

                string cnpjNumeros = new string(value.Where(char.IsDigit).ToArray());

                if (cnpjNumeros.Length != 14)
                    throw new ArgumentException("O CNPJ deve conter exatamente 14 dígitos.");

                if (!CnpjValido(cnpjNumeros))
                    throw new ArgumentException("O CNPJ informado é inválido.");

                cnpj = value;
            }
        }

        public string Nome
        {
            get { return nome; }
            set
            {
                string validacao = ValidadorHelper.VerificarVazio(value, "Nome");
                if (validacao != null) throw new ArgumentException(validacao);

                if (value.Length > 150)
                    throw new ArgumentException("O nome do fornecedor não pode conter mais que 150 caracteres");

                nome = value;
            }
        }

        public string Email
        {
            get { return email; }
            set

            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "Email");
                if (validacaoDeVazio != null) throw new ArgumentException(validacaoDeVazio);

                if (value.Length > 150)
                    throw new ArgumentException("O email não pode conter mais que 150 caracteres");

                if (!value.Contains("@") || !value.Contains("."))
                    throw new ArgumentException("O email informado é inválido.");

                email = value;
            }
        }

        public string Telefone
        {
            get { return telefone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O campo 'Telefone' não pode estar vazio.");

                string numeros = new string(value.Where(char.IsDigit).ToArray());
                if (numeros.Length != 10 && numeros.Length != 11)
                    throw new ArgumentException("O telefone deve conter 10 ou 11 dígitos.");

                telefone = numeros;
            }
        }

        public List<ProdutoFornecedor> Produtos
        {
            get { return produtos; }
            set { produtos = value; }
        }

        public List<ProdutoFornecedor> ProdutosApagados
        {
            get { return produtos_apagados; }
            set { produtos_apagados = value; }
        }


        private static bool CnpjValido(string cnpj)
        {
            if (cnpj.Distinct().Count() == 1) return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string cnpjCalculado = tempCnpj + digito2;
            return cnpj.EndsWith(cnpjCalculado.Substring(12));
        }
    }
}
