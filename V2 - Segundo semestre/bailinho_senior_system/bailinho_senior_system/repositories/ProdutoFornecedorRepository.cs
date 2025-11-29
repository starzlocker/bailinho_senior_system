using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bailinho_senior_system.models;
using MySql.Data.MySqlClient;
using bailinho_senior_system.config;
namespace bailinho_senior_system.repositories
{
    internal class ProdutoFornecedorRepository
    {
        public List<ProdutoFornecedor> GetProdutosPorFornecedor(int id_fornecedor)
        {
            var products = new List<ProdutoFornecedor>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();

                    string sql =    $"SELECT " +
                                    $"tpf.id, tpf.id_produto, tpf.id_fornecedor, tp.nome as nome_produto, tf.nome as nome_fornecedor " +
                                    $"FROM produtofornecedor tpf " +
                                    $"INNER JOIN Produto tp on tp.id=tpf.id_produto " +
                                    $"INNER JOIN Fornecedor tf on tf.id=tpf.id_fornecedor " +
                                    $"WHERE tpf.id_fornecedor=@id_fornecedor;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id_fornecedor", id_fornecedor);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNomeProduto = reader.GetOrdinal("nome_produto");
                            int idxNomeFornecedor = reader.GetOrdinal("nome_fornecedor");
                            int idxIdProduto = reader.GetOrdinal("id_produto");
                            int idxIdFornecedor = reader.GetOrdinal("id_fornecedor");

                            while (reader.Read())
                            {
                                var produto = new ProdutoFornecedor();

                                // Só atribui quando o valor não for nulo no banco
                                if (!reader.IsDBNull(idxId))
                                    produto.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNomeProduto))
                                    produto.NomeProduto = reader.GetString(idxNomeProduto);

                                if (!reader.IsDBNull(idxNomeFornecedor))
                                    produto.NomeFornecedor = reader.GetString(idxNomeFornecedor);

                                if (!reader.IsDBNull(idxIdFornecedor))
                                    produto.IdFornecedor = reader.GetInt32(idxIdFornecedor);

                                if (!reader.IsDBNull(idxIdProduto))
                                    produto.IdProduto = reader.GetInt32(idxIdProduto);


                                // Se precisar do Id e não houver propriedade pública, você pode adicionar uma propriedade Id no model.
                                // Ex.: produto.Id = reader.GetInt32(idxId);

                                products.Add(produto);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                throw;
            }

            return products;
        }

        public List<ProdutoFornecedor> GetProdutosPorIdProduto(int id_produto)
        {
            var products = new List<ProdutoFornecedor>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();



                    string sql = $"SELECT " +
                                    $"tpf.id, tpf.id_produto, tpf.id_fornecedor, tp.nome as nome_produto, tf.nome as nome_fornecedor " +
                                    $"FROM produtofornecedor tpf " +
                                    $"INNER JOIN Produto tp on tp.id=tpf.id_produto " +
                                    $"INNER JOIN Fornecedor tf on tf.id=tpf.id_fornecedor " +
                                    $"WHERE tpf.id_produto=@id_produto;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id_produto", id_produto);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNomeProduto = reader.GetOrdinal("nome_produto");
                            int idxNomeFornecedor = reader.GetOrdinal("nome_fornecedor");
                            int idxIdProduto = reader.GetOrdinal("id_produto");
                            int idxIdFornecedor = reader.GetOrdinal("id_fornecedor");

                            while (reader.Read())
                            {
                                var produto = new ProdutoFornecedor();

                                // Só atribui quando o valor não for nulo no banco
                                if (!reader.IsDBNull(idxId))
                                    produto.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNomeProduto))
                                    produto.NomeProduto = reader.GetString(idxNomeProduto);

                                if (!reader.IsDBNull(idxNomeFornecedor))
                                    produto.NomeFornecedor = reader.GetString(idxNomeFornecedor);

                                if (!reader.IsDBNull(idxIdFornecedor))
                                    produto.IdFornecedor = reader.GetInt32(idxIdFornecedor);

                                if (!reader.IsDBNull(idxIdProduto))
                                    produto.IdProduto = reader.GetInt32(idxIdProduto);


                                // Se precisar do Id e não houver propriedade pública, você pode adicionar uma propriedade Id no model.
                                // Ex.: produto.Id = reader.GetInt32(idxId);

                                products.Add(produto);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                throw;
            }

            return products;
        }

        public List<ProdutoFornecedor> GetFornecedoresPorProduto(int id_produto)
        {
            var fornecedores = new List<ProdutoFornecedor>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();

                    // Consulta que busca TODOS os fornecedores vinculados a um produto específico (id_produto)
                    string sql = $"SELECT " +
                                 $"tpf.id, tpf.id_produto, tpf.id_fornecedor, tf.nome as nome_fornecedor " +
                                 $"FROM produtofornecedor tpf " +
                                 $"INNER JOIN Fornecedor tf on tf.id=tpf.id_fornecedor " +
                                 $"WHERE tpf.id_produto=@id_produto;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id_produto", id_produto);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNomeFornecedor = reader.GetOrdinal("nome_fornecedor");
                            int idxIdProduto = reader.GetOrdinal("id_produto");
                            int idxIdFornecedor = reader.GetOrdinal("id_fornecedor");

                            while (reader.Read())
                            {
                                var pf = new ProdutoFornecedor();

                                if (!reader.IsDBNull(idxId))
                                    pf.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNomeFornecedor))
                                    pf.NomeFornecedor = reader.GetString(idxNomeFornecedor);

                                if (!reader.IsDBNull(idxIdFornecedor))
                                    pf.IdFornecedor = reader.GetInt32(idxIdFornecedor);

                                if (!reader.IsDBNull(idxIdProduto))
                                    pf.IdProduto = reader.GetInt32(idxIdProduto);

                                fornecedores.Add(pf);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            return fornecedores;
        }


        public void CreateProdutoFornecedor(ProdutoFornecedor p)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    long count = CheckExisting(p.IdProduto, p.IdFornecedor);

                    if (count > 0) return;

                    string sql =    $"INSERT INTO produtofornecedor (id_produto, id_fornecedor) " +
                                    $"VALUES (@id_produto, @id_fornecedor);";

                    using (MySqlCommand command = new MySqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@id_produto", p.IdProduto);
                        command.Parameters.AddWithValue("@id_fornecedor", p.IdFornecedor);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public long CheckExisting(int id_produto, int id_fornecedor)
        {
            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string selectSql = $"SELECT COUNT(*) FROM produtofornecedor " +
                   $"WHERE id_produto = @id_produto AND id_fornecedor = @id_fornecedor;";
                using (MySqlCommand command = new MySqlCommand(selectSql, conn))
                {
                    command.Parameters.AddWithValue("@id_produto", id_produto);
                    command.Parameters.AddWithValue("@id_fornecedor", id_fornecedor);

                    long count = (long)command.ExecuteScalar();

                    return count;
                }
            }
        }

        public void CreateFromListProdutoFornecedor(List<ProdutoFornecedor> p, List<ProdutoFornecedor> apagados = null)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string sql = $"SAVEPOINT sp_before_insert";

                    using (MySqlCommand command = new MySqlCommand(sql, conn))
                    {
                        command.ExecuteNonQuery();
                    }

                    try
                    {
                        foreach (var item in p)
                        {
                            long count = CheckExisting(item.IdProduto, item.IdFornecedor);

                            if (count > 0) continue;

                            string insertSql = $"INSERT INTO produtofornecedor (id_produto, id_fornecedor) " +
                                                $"VALUES (@id_produto, @id_fornecedor);";
                            using (MySqlCommand command = new MySqlCommand(insertSql, conn))
                            {
                                command.Parameters.AddWithValue("@id_produto", item.IdProduto);
                                command.Parameters.AddWithValue("@id_fornecedor", item.IdFornecedor);
                                command.ExecuteNonQuery();
                            }
                        }

                        // Processa itens apagados se fornecido
                        if (apagados != null)
                        {
                            foreach (var item in apagados)
                            {
                                if (item.Id > 0)
                                {
                                    DeleteProdutoFornecedor(item.Id);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string rollbackSql = $"ROLLBACK TO SAVEPOINT sp_before_insert";
                        using (MySqlCommand command = new MySqlCommand(rollbackSql, conn))
                        {
                            command.ExecuteNonQuery();
                        }
                        throw;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public void DeleteProdutoFornecedor(int id_produtofornecedor)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string sql =    $"DELETE FROM produtofornecedor " +
                                    $"WHERE id = @id;";

                    using (MySqlCommand command = new MySqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@id", id_produtofornecedor);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public void DeleteAllProdutoFornecedor(int id_fornecedor)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string sql =    $"DELETE FROM produtofornecedor " +
                                    $"WHERE id_fornecedor=@id_fornecedor;";

                    using (MySqlCommand command = new MySqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@id_fornecedor", id_fornecedor);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
