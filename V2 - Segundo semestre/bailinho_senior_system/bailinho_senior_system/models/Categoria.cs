using System;

namespace bailinho_senior_system.models
{
    public class Categoria
    {
        public Categoria() { }
        public Categoria(int id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
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

        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                string validacao = ValidadorHelper.VerificarVazio(value, "Nome");
                if (validacao != null) throw new ArgumentException(validacao);

                if (value.Length > 150)
                    throw new ArgumentException("O nome da categoria não pode conter mais que 150 caracteres");

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
                    throw new ArgumentException("A descrição da categoria não pode conter mais que 150 caracteres");

                this.descricao = value;
            }
        }
    }
}
