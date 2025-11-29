using bailinho_senior_system.config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace bailinho_senior_system.repositories
{
    // Classe de serviço/repositório para executar consultas de relatórios complexos.
    public class RelatoriosRepository
    {
        private string connectionString => DatabaseConfig.ConnectionString;

        // Método genérico para executar qualquer consulta e retornar um DataTable
        private DataTable ExecuteQuery(string sqlQuery)
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                catch (MySqlException ex)
                {
                    // Lidar com exceções do MySQL (conexão, sintaxe, etc.)
                    // Em um app real, você faria um log aqui.
                    System.Diagnostics.Debug.WriteLine($"Erro ao executar relatório: {ex.Message}");
                    throw new Exception("Erro de banco de dados ao executar o relatório.", ex);
                }
            }
            return dataTable;
        }

        // 1º Relatório: Produtos mais vendidos
        public DataTable GetProdutosMaisVendidos()
        {
            string sql = @"
                SELECT 
                    p.nome AS Nome_Produto, 
                    SUM(pv.quantidade) AS Quantidade_Vendida
                FROM 
                    produto p
                LEFT JOIN 
                    produtoVenda pv ON p.id = pv.id_produto
                GROUP BY 
                    p.nome
                ORDER BY 
                    Quantidade_Vendida DESC;";
            return ExecuteQuery(sql);
        }

        // 2º Relatório: Número de vendas por cliente
        public DataTable GetVendasPorCliente()
        {
            string sql = @"
                SELECT 
                    c.nome AS Nome_Cliente, 
                    COUNT(v.id) AS Total_Vendas
                FROM 
                    cliente c
                LEFT JOIN 
                    venda v ON c.id = v.id_cliente
                GROUP BY 
                    c.nome
                ORDER BY
                    Total_Vendas DESC;";
            return ExecuteQuery(sql);
        }

        // 3º Relatório: Itens vendidos por evento
        public DataTable GetProdutosVendidosPorEvento()
        {
            string sql = @"
                SELECT 
                    e.nome AS Nome_Evento, 
                    SUM(pv.quantidade) AS Total_Itens_Vendidos
                FROM 
                    evento e
                INNER JOIN 
                    venda v ON e.id = v.id_evento
                INNER JOIN 
                    produtovenda pv ON v.id = pv.id_venda
                GROUP BY 
                    e.nome
                ORDER BY 
                    Total_Itens_Vendidos DESC;";
            return ExecuteQuery(sql);
        }

        // 4º Relatório: Produtos diferentes por fornecedor
        public DataTable GetProdutosPorFornecedor()
        {
            string sql = @"
                SELECT 
                    f.nome AS Nome_Fornecedor, 
                    COUNT(pf.id_produto) AS Qtd_Produtos_Diferentes
                FROM 
                    fornecedor f
                INNER JOIN 
                    produtofornecedor pf ON f.id = pf.id_fornecedor
                GROUP BY 
                    f.nome
                ORDER BY 
                    Qtd_Produtos_Diferentes DESC;";
            return ExecuteQuery(sql);
        }

        // 5º Relatório: Itens vendidos por categoria
        public DataTable GetItensVendidosPorCategoria()
        {
            string sql = @"
                SELECT 
                    c.nome AS Nome_Categoria, 
                    SUM(pv.quantidade) AS Quantidade_Vendida_Categoria
                FROM 
                    categoria c
                INNER JOIN 
                    produto p ON c.id = p.id_categoria
                INNER JOIN 
                    produtovenda pv ON p.id = pv.id_produto
                GROUP BY 
                    c.nome
                ORDER BY
                    Quantidade_Vendida_Categoria DESC;";
            return ExecuteQuery(sql);
        }

        // 6º Relatório: Faturamento Total por Evento
        public DataTable GetFaturamentoTotalPorEvento()
        {
            string sql = @"
                SELECT
                    e.nome AS Nome_Evento,
                    ROUND((COUNT(DISTINCT v.id) * e.valor_entrada) + SUM(v.valor_total), 2) AS Faturamento_Total
                FROM
                    Evento e
                INNER JOIN
                    Venda v ON e.id = v.id_evento
                GROUP BY
                    e.nome, e.valor_entrada
                ORDER BY
                    Faturamento_Total DESC;";
            return ExecuteQuery(sql);
        }

        // 7º Relatório: Forma de Pagamento Mais Utilizada
        public DataTable GetFormaPagamentoMaisUtilizada()
        {
            string sql = @"
                SELECT
                    forma_pagamento AS Forma_de_Pagamento,
                    COUNT(*) AS Total_Vendas,
                    ROUND(SUM(valor_total), 2) AS Valor_Transacionado
                FROM
                    Venda
                GROUP BY
                    forma_pagamento
                ORDER BY
                    Total_Vendas DESC;";
            return ExecuteQuery(sql);
        }

        // 8º Relatório: Produtos com Baixo Estoque (Limite < 20)
        public DataTable GetProdutosBaixoEstoque(int limite = 20)
        {
            string sql = $@"
                SELECT
                    p.nome AS Produto,
                    p.qtd_estoque AS Estoque_Atual,
                    c.nome AS Categoria
                FROM
                    Produto p
                INNER JOIN
                    Categoria c ON p.id_categoria = c.id
                WHERE
                    p.qtd_estoque <= {limite}
                ORDER BY
                    p.qtd_estoque ASC;";
            return ExecuteQuery(sql);
        }

        // 9º Relatório: Cliente que Mais Gastou (Top 5)
        public DataTable GetTop5ClientesMaisGastaram()
        {
            string sql = @"
                SELECT
                    c.nome AS Nome_Cliente,
                    COUNT(v.id) AS Total_de_Compras,
                    ROUND(SUM(v.valor_total), 2) AS Valor_Total_Gasto
                FROM
                    Cliente c
                INNER JOIN
                    Venda v ON c.id = v.id_cliente
                GROUP BY
                    c.nome
                ORDER BY
                    Valor_Total_Gasto DESC
                LIMIT 5;";
            return ExecuteQuery(sql);
        }

        // 10º Relatório: Média de Preço dos Produtos por Categoria
        public DataTable GetPrecoMedioPorCategoria()
        {
            string sql = @"
                SELECT
                    c.nome AS Categoria,
                    COUNT(p.id) AS Qtd_Produtos_Cadastrados,
                    ROUND(AVG(p.preco), 2) AS Preco_Medio_Categoria
                FROM
                    Categoria c
                INNER JOIN
                    Produto p ON c.id = p.id_categoria
                GROUP BY
                    c.nome
                ORDER BY
                    Preco_Medio_Categoria DESC;";
            return ExecuteQuery(sql);
        }
    }
}