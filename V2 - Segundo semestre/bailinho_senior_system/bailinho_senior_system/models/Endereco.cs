using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class Endereco
    {
        public Endereco() { }

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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("CEP não pode ser vazio.");
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Logradouro não pode ser vazio.");
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Bairro não pode ser vazio.");
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Cidade não pode ser vazia.");
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Número não pode ser vazio.");
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Estado não pode ser vazio.");
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
    }
}