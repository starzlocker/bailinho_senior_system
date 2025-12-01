using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using bailinho_senior_system.models;
using bailinho_senior_system.config;

namespace bailinho_senior_system.repositories
{
    public class ProdutoVendaRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public List<ProdutoVenda> GetItensByVendaId(int vendaId)
        {
            var itens = new List<ProdutoVenda>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"
                        SELECT 
                            tpv.id, tpv.id_produto, tpv.id_venda, tpv.quantidade, tpv.valor,
                            tp.nome AS NomeProduto, tp.preco AS PrecoUnitario
                        FROM ProdutoVenda tpv
                        INNER JOIN Produto tp ON tpv.id_produto = tp.id
                        WHERE tpv.id_venda = @id_venda;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id_venda", vendaId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ProdutoVenda(
                                    reader.GetInt32("id"),
                                    reader.GetInt32("id_produto"),
                                    reader.GetInt32("id_venda"),
                                    reader.GetInt32("quantidade"),
                                    reader.GetDecimal("valor"),
                                    reader.GetString("NomeProduto"),
                                    reader.GetDecimal("PrecoUnitario")
                                );

                                itens.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar itens da venda {vendaId}.", ex);
            }

            return itens;
        }
    }
}