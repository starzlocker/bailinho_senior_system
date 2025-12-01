using System;

namespace bailinho_senior_system.models
{
    public class Endereco
    {
        public Endereco() { }
        public Endereco(int id, string cep, string logradouro,string bairro,
                        string cidade, string numero, string estado, string complemento)
        {
            Id = id;
            Cep = cep;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Numero = numero;
            Estado = estado;
            Complemento = complemento;
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

        private string cep;
        public string Cep
        {
            get { return cep; }
            set
            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "CEP");

                if (validacaoDeVazio != null)
                    throw new ArgumentException(validacaoDeVazio);

                if (value.Length != 8)
                    throw new ArgumentException("CEP deve ter 8 caracteres.");

                this.cep = value;
            }
        }

        private string logradouro;
        public string Logradouro
        {
            get { return logradouro; }
            set
            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "Logradouro");

                if (validacaoDeVazio != null)
                    throw new ArgumentException(validacaoDeVazio);

                if (value.Length > 150)
                    throw new ArgumentException("Logradouro deve ter no máximo 150 caracteres.");

                this.logradouro = value;
            }
        }

        private string bairro;
        public string Bairro
        {
            get { return bairro; }
            set
            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "Bairro");

                if (validacaoDeVazio != null)
                    throw new ArgumentException(validacaoDeVazio);
                if (value.Length > 100)
                    throw new ArgumentException("Bairro deve ter no máximo 100 caracteres.");

                this.bairro = value;
            }
        }

        private string cidade;
        public string Cidade
        {
            get { return cidade; }
            set
            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "Cidade");

                if (validacaoDeVazio != null)
                    throw new ArgumentException(validacaoDeVazio);
                if (value.Length > 100)
                    throw new ArgumentException("Cidade deve ter no máximo 100 caracteres.");

                this.cidade = value;
            }
        }

        private string numero;
        public string Numero
        {
            get { return numero; }
            set
            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "Número");

                if (validacaoDeVazio != null)
                    throw new ArgumentException(validacaoDeVazio);
                if (value.Length > 10)
                    throw new ArgumentException("Número deve ter no máximo 10 caracteres.");

                this.numero = value;
            }
        }

        private string estado;
        public string Estado
        {
            get { return estado; }
            set
            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "Estado (UF)");

                if (validacaoDeVazio != null)
                    throw new ArgumentException(validacaoDeVazio);
                if (value.Length != 2)
                    throw new ArgumentException("Estado deve ter 2 caracteres (UF).");

                this.estado = value;
            }
        }

        private string complemento;
        public string Complemento
        {
            get { return complemento; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentException("Complemento deve ter no máximo 100 caracteres.");

                this.complemento = value;
            }
        }

        public string EnderecoCompleto
        {
            get
            {
                if (string.IsNullOrEmpty(logradouro))
                {
                    return "Endereço Não Informado";
                }

                string complementoStr = string.IsNullOrWhiteSpace(complemento) ? "" : $" ({complemento})";

                return $"{logradouro}, {numero}{complementoStr} - {bairro}, {cidade}/{estado} - CEP: {cep}";
            }
        }
    }
}