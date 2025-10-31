using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system
{
    internal class Venda
    {
        private decimal valor_total;
        private string forma_pagamento;
        private int id_cliente;
        private int id_evento;

        public Venda() { }

        public Venda(
            decimal valor_total,
            string forma_pagamento,
            int id_cliente,
            int id_evento
        )
        {
            setValorTotal(valor_total);
            setFormaPagamento(forma_pagamento);
            setIdCliente(id_cliente);
            setIdEvento(id_evento);
        }

        public decimal getValorTotal()
        {
            return valor_total;
        }

        public string getFormaPagamento()
        {
            return forma_pagamento;
        }

        public int getIdCliente()
        {
            return id_cliente;
        }

        public int getIdEvento()
        {
            return id_evento;
        }

        public string setValorTotal(decimal valor)
        {
            if (valor < 0)
                return "O valor total da venda não pode ser negativo";

            this.valor_total = valor;
            return "sucesso";
        }

        public string setFormaPagamento(string forma)
        {
            string validacao = ValidadorHelper.VerificarVazio(forma, "Forma de Pagamento");
            if (validacao != null) return validacao;

            if (forma.Length > 15)
                return "A forma de pagamento não pode conter mais que 15 caracteres";

            this.forma_pagamento = forma;
            return "sucesso";
        }

        public string setIdCliente(int id_cliente)
        {
            if (id_cliente <= 0)
                return "O ID do cliente deve ser maior que zero";

            this.id_cliente = id_cliente;
            return "sucesso";
        }

        public string setIdEvento(int id_evento)
        {
            if (id_evento <= 0)
                return "O ID do evento deve ser maior que zero";

            this.id_evento = id_evento;
            return "sucesso";

        }
    }
}
