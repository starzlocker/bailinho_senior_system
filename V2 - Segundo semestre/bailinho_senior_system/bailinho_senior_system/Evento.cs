using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system
{
    internal class Evento
    {
        private string nome;
        private string descricao;
        private DateTime data;
        private TimeSpan hora;
        private decimal valor_entrada;
        private int endereco;

        public Evento() { }

        public Evento(
            string nome,
            string descricao,
            DateTime data,
            TimeSpan hora,
            decimal valor_entrada,
            int endereco
        ) {
            setNome(nome);
            setDescricao(descricao);
            setData(data);
            setHora(hora);
            setValorEntrada(valor_entrada);
            setEndereco(endereco);
        }

        public string getNome()
        {
            return nome;
        }

        public string getDescricao()
        {
            return descricao;
        }

        public DateTime getData()
        {
            return data;
        }

        public TimeSpan getHora()
        {
            return hora;
        }

        public decimal getValorEntrada()
        {
            return valor_entrada;
        }

        public int getEndereco()
        {
            return endereco;
        }

        public string setNome(string nome)
        {
            string validacao = ValidadorHelper.VerificarVazio(nome, "Nome");
            if (validacao != null) return validacao;

            if (nome.Length > 150)
                return "O nome do evento não pode conter mais que 150 caracteres";

            this.nome = nome;
            return "sucesso";
        }

        public string setDescricao(string descricao)
        {
            if (!string.IsNullOrWhiteSpace(descricao) && descricao.Length > 150)
                return "A descrição do evento não pode conter mais que 150 caracteres";

            this.descricao = descricao;
            return "sucesso";
        }

        public string setData(DateTime data)
        {
            if (data == DateTime.MinValue)
                return "O campo 'Data' não pode estar vazio";

            if (data < DateTime.Today)
                return "A data do evento não pode ser no passado";

            this.data = data;
            return "sucesso";
        }

        public string setHora(TimeSpan hora)
        {
            // TimeSpan não tem valor "MinValue" para indicar vazio, então assumimos >= 0
            if (hora < TimeSpan.Zero)
                return "O campo 'Hora' é inválido";

            this.hora = hora;
            return "sucesso";
        }

        public string setValorEntrada(decimal valor)
        {
            if (valor < 0)
                return "O valor da entrada não pode ser negativo";

            this.valor_entrada = valor;
            return "sucesso";
        }

        public string setEndereco(int endereco)
        {
            if (endereco <= 0)
                return "O campo 'Endereço' deve ser maior que zero";

            this.endereco = endereco;
            return "sucesso";
        }
    }
}
