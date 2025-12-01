using bailinho_senior_system.config;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace bailinho_senior_system.repositories
{
    public class RelatoriosRepository
    {
        private string connectionString => DatabaseConfig.ConnectionString;

        private string GetDateFilter(DateTime? startDate, DateTime? endDate, string dateColumnName)
        {
            string filter = "";

            if (startDate.HasValue)
                filter += $" AND {dateColumnName} >= '{startDate.Value:yyyy-MM-dd}'";

            if (endDate.HasValue)
                filter += $" AND {dateColumnName} <= '{endDate.Value:yyyy-MM-dd}'";

            if (!string.IsNullOrEmpty(filter))
                return $" WHERE {filter.Substring(5)}";

            return "";
        }

        private int ExecuteScalarInt(string sqlQuery)
        {
            int total = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(sqlQuery, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int count))
                            total = count;
                    }
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erro ao executar escalar INT: {ex.Message}");
                    throw new Exception("Erro de banco de dados ao executar o relatório.", ex);
                }
            }
            return total;
        }

        private float ExecuteScalarFloat(string sqlQuery)
        {
            float total = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(sqlQuery, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && float.TryParse(result.ToString(), out float value))
                            total = value;
                    }
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erro ao executar escalar FLOAT: {ex.Message}");
                    throw new Exception("Erro de banco de dados ao executar o relatório.", ex);
                }
            }
            return total;
        }

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
                    System.Diagnostics.Debug.WriteLine($"Erro ao executar relatório: {ex.Message}");
                    throw new Exception("Erro de banco de dados ao executar o relatório.", ex);
                }
            }
            return dataTable;
        }

        public DataTable GetProdutosMaisVendidos(DateTime? startDate = null, DateTime? endDate = null)
        {
            string dateFilterJoin = "";
            if (startDate.HasValue || endDate.HasValue)
            {
                string internalFilter = GetDateFilter(startDate, endDate, "v.data_venda").Replace("WHERE", "AND");
                dateFilterJoin = $@"
                    INNER JOIN venda v ON v.id = pv.id_venda
                    {internalFilter}";
            }

            string sql = $@"
                SELECT 
                    p.nome AS Nome_Produto, 
                    SUM(pv.quantidade) AS Quantidade_Vendida
                FROM 
                    produto p
                LEFT JOIN 
                    produtoVenda pv ON p.id = pv.id_produto
                {dateFilterJoin}
                GROUP BY 
                    p.nome
                ORDER BY 
                    Quantidade_Vendida DESC;";
            return ExecuteQuery(sql);
        }

        public DataTable GetVendasPorCliente(DateTime? startDate = null, DateTime? endDate = null)
        {
            string dateFilterWhere = GetDateFilter(startDate, endDate, "v.data_venda");

            string sql = $@"
                SELECT 
                    c.nome AS Nome_Cliente, 
                    COUNT(v.id) AS Total_Vendas
                FROM 
                    cliente c
                LEFT JOIN 
                    venda v ON c.id = v.id_cliente
                {dateFilterWhere}
                GROUP BY 
                    c.nome
                ORDER BY
                    Total_Vendas DESC;";
            return ExecuteQuery(sql);
        }

        public DataTable GetProdutosVendidosPorEvento(DateTime? startDate = null, DateTime? endDate = null)
        {
            string joinFilter = GetDateFilter(startDate, endDate, "v.data_venda").Replace("WHERE", "AND");

            string sql = $@"
                SELECT 
                    e.nome AS Nome_Evento, 
                    SUM(pv.quantidade) AS Total_Itens_Vendidos
                FROM 
                    evento e
                INNER JOIN 
                    venda v ON e.id = v.id_evento {joinFilter}
                INNER JOIN 
                    produtovenda pv ON v.id = pv.id_venda
                GROUP BY 
                    e.nome
                ORDER BY 
                    Total_Itens_Vendidos DESC;";
            return ExecuteQuery(sql);
        }

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

        public DataTable GetItensVendidosPorCategoria(DateTime? startDate = null, DateTime? endDate = null)
        {
            string dateFilterJoin = "";
            if (startDate.HasValue || endDate.HasValue)
            {
                string internalFilter = GetDateFilter(startDate, endDate, "v.data_venda").Replace("WHERE", "AND");
                dateFilterJoin = $@"
                    INNER JOIN venda v ON v.id = pv.id_venda
                    {internalFilter}";
            }

            string sql = $@"
                SELECT 
                    c.nome AS Nome_Categoria, 
                    SUM(pv.quantidade) AS Quantidade_Vendida_Categoria
                FROM 
                    categoria c
                INNER JOIN 
                    produto p ON c.id = p.id_categoria
                INNER JOIN 
                    produtovenda pv ON p.id = pv.id_produto
                {dateFilterJoin}
                GROUP BY 
                    c.nome
                ORDER BY
                    Quantidade_Vendida_Categoria DESC;";
            return ExecuteQuery(sql);
        }

        public DataTable GetFaturamentoTotalPorEvento(DateTime? startDate = null, DateTime? endDate = null)
        {
            string joinFilter = GetDateFilter(startDate, endDate, "v.data_venda").Replace("WHERE", "AND");

            string sql = $@"
                SELECT
                    e.nome AS Nome_Evento,
                    ROUND((COUNT(DISTINCT v.id) * e.valor_entrada) + SUM(v.valor_total), 2) AS Faturamento_Total
                FROM
                    Evento e
                INNER JOIN
                    Venda v ON e.id = v.id_evento {joinFilter}
                GROUP BY
                    e.nome, e.valor_entrada
                ORDER BY
                    Faturamento_Total DESC;";
            return ExecuteQuery(sql);
        }

        public DataTable GetFormaPagamentoMaisUtilizada(DateTime? startDate = null, DateTime? endDate = null)
        {
            string dateFilterWhere = GetDateFilter(startDate, endDate, "data_venda");

            string sql = $@"
                SELECT
                    forma_pagamento AS Forma_de_Pagamento,
                    COUNT(*) AS Total_Vendas,
                    ROUND(SUM(valor_total), 2) AS Valor_Transacionado
                FROM
                    Venda
                {dateFilterWhere}
                GROUP BY
                    forma_pagamento
                ORDER BY
                    Total_Vendas DESC;";
            return ExecuteQuery(sql);
        }

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

        public DataTable GetTop5ClientesMaisGastaram(DateTime? startDate = null, DateTime? endDate = null)
        {
            string dateFilterWhere = GetDateFilter(startDate, endDate, "v.data_venda");

            string sql = $@"
                SELECT
                    c.nome AS Nome_Cliente,
                    COUNT(v.id) AS Total_de_Compras,
                    ROUND(SUM(v.valor_total), 2) AS Valor_Total_Gasto
                FROM
                    Cliente c
                INNER JOIN
                    Venda v ON c.id = v.id_cliente
                {dateFilterWhere}
                GROUP BY
                    c.nome
                ORDER BY
                    Valor_Total_Gasto DESC
                LIMIT 5;";
            return ExecuteQuery(sql);
        }

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

        public int GetTotalVendas(DateTime? startDate = null, DateTime? endDate = null)
        {
            string dateFilter = GetDateFilter(startDate, endDate, "data_venda");
            string sql = $"SELECT COUNT(*) FROM venda {dateFilter};";
            return ExecuteScalarInt(sql);
        }

        public float getTotalVendido(DateTime? startDate = null, DateTime? endDate = null)
        {
            string dateFilter = GetDateFilter(startDate, endDate, "data_venda");
            string sql = $"SELECT SUM(valor_total) FROM venda {dateFilter};";
            return ExecuteScalarFloat(sql);
        }

        public float getTicketMedio(DateTime? startDate = null, DateTime? endDate = null)
        {
            string dateFilter = GetDateFilter(startDate, endDate, "data_venda");
            string sql = $"SELECT AVG(valor_total) FROM venda {dateFilter};";
            return ExecuteScalarFloat(sql);
        }

        public int getEventosRealizados(DateTime? startDate = null, DateTime? endDate = null)
        {
            string filter = "";
            if (startDate.HasValue) filter += $" AND `data` >= '{startDate.Value:yyyy-MM-dd}'";
            if (endDate.HasValue) filter += $" AND `data` <= '{endDate.Value:yyyy-MM-dd}'";

            string sql = $"SELECT COUNT(*) FROM evento WHERE `data` < CURDATE() {filter};";

            return ExecuteScalarInt(sql);
        }

        public int getFornecedoresCadastrados()
        {
            return ExecuteScalarInt("SELECT COUNT(*) FROM fornecedor;");
        }

        public int getClientesCadastrados()
        {
            return ExecuteScalarInt("SELECT COUNT(*) FROM cliente;");
        }

        public int getEstoqueTotalProdutos()
        {
            return ExecuteScalarInt("SELECT SUM(qtd_estoque) FROM produto;");
        }

        public int getProdutosVendidos(DateTime? startDate = null, DateTime? endDate = null)
        {
            string internalFilter = GetDateFilter(startDate, endDate, "data_venda");
            string dateFilter = "";

            if (!string.IsNullOrEmpty(internalFilter))
            {
                dateFilter = $@"
                    WHERE pv.id_venda IN (
                        SELECT id FROM venda {internalFilter}
                    )";
            }
            string sql = $"SELECT SUM(pv.quantidade) FROM produtovenda pv {dateFilter};";
            return ExecuteScalarInt(sql);
        }

        public int getProdutosCadastrados()
        {
            return ExecuteScalarInt("SELECT COUNT(*) FROM produto;");
        }
    }
}