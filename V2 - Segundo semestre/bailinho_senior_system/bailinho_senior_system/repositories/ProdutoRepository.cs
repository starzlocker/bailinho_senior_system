using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bailinho_senior_system.models;
using MySql.Data.MySqlClient;

namespace bailinho_senior_system.repositories
{
    public class ProdutoRepository
    {
        string ConnectionString = @"server=127.0.0.1;uid=root;pwd=ifsp;database=bailinhoseniorsystem;ConnectionTimeout=1";
        //private string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\adolfo\\Documents\\bailinho_senior_system2\\bailinho_senior_system\\V2 - Segundo semestre\\bailinho_senior_system\\bailinho_senior_system\\data\\Database1.mdf\";Integrated Security=True";


        public List<Produto> GetProdutos()
        {
            var products = new List<Produto>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT tp.id, tp.nome, tp.descricao, tp.qtd_estoque, tp.preco, tp.id_categoria, tc.nome as categoria FROM Produto tp LEFT JOIN Categoria tc on tp.id_categoria = tc.id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNome = reader.GetOrdinal("nome");
                            int idxDescricao = reader.GetOrdinal("descricao");
                            int idxQtd = reader.GetOrdinal("qtd_estoque");
                            int idxPreco = reader.GetOrdinal("preco");
                            int idxIdCategoria = reader.GetOrdinal("id_categoria");
                            int idxCategoria = reader.GetOrdinal("categoria");

                            while (reader.Read())
                            {
                                var produto = new Produto();

                                // Só atribui quando o valor não for nulo no banco
                                if (!reader.IsDBNull(idxId))
                                    produto.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNome))
                                    produto.Nome = reader.GetString(idxNome);

                                if (!reader.IsDBNull(idxDescricao))
                                    produto.Descricao = reader.GetString(idxDescricao);

                                if (!reader.IsDBNull(idxQtd))
                                    produto.QtdEstoque = reader.GetInt32(idxQtd);

                                if (!reader.IsDBNull(idxPreco))
                                    produto.Preco = reader.GetDecimal(idxPreco);

                                if (!reader.IsDBNull(idxCategoria))
                                    produto.Categoria = reader.GetString(idxCategoria);

                                if (!reader.IsDBNull(idxIdCategoria))
                                    produto.IdCategoria = reader.GetInt32(idxIdCategoria);

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
                MessageBox.Show(ex.Message, "Erro ao conectar com banco!");
                Console.Write(ex.ToString());
            }

            return products;
        }


        public Produto GetProduto(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = $"SELECT tp.id, tp.nome, tp.descricao, tp.qtd_estoque, tp.preco, tp.id_categoria, tc.nome as categoria FROM Produto tp LEFT JOIN Categoria tc on tp.id_categoria = tc.id WHERE tp.id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNome = reader.GetOrdinal("nome");
                            int idxDescricao = reader.GetOrdinal("descricao");
                            int idxQtd = reader.GetOrdinal("qtd_estoque");
                            int idxPreco = reader.GetOrdinal("preco");
                            int idxCategoria = reader.GetOrdinal("categoria");
                            int idxIdCategoria = reader.GetOrdinal("id_categoria");
                            if (reader.Read())
                            {
                                var produto = new Produto();

                                // Só atribui quando o valor não for nulo no banco
                                if (!reader.IsDBNull(idxId))
                                    produto.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNome))
                                    produto.Nome = reader.GetString(idxNome);

                                if (!reader.IsDBNull(idxDescricao))
                                    produto.Descricao = reader.GetString(idxDescricao);

                                if (!reader.IsDBNull(idxQtd))
                                    produto.QtdEstoque = reader.GetInt32(idxQtd);

                                if (!reader.IsDBNull(idxPreco))
                                    produto.Preco = reader.GetDecimal(idxPreco);

                                if (!reader.IsDBNull(idxCategoria))
                                    produto.Categoria = reader.GetString(idxCategoria);

                                if (!reader.IsDBNull(idxIdCategoria))
                                    produto.IdCategoria = reader.GetInt32(idxIdCategoria);

                                // Se precisar do Id e não houver propriedade pública, você pode adicionar uma propriedade Id no model.
                                // Ex.: produto.Id = reader.GetInt32(idxId);

                                return produto;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao conectar com banco!");
                Console.Write(ex.ToString());

            }

            return null;
        }

        public void CreateProduto(Produto produto)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "INSERT INTO Produto " +
                                 "(nome, descricao, qtd_estoque, preco, id_categoria) " +
                                 "VALUES (@nome, @descricao, @qtd_estoque, @preco, @id_categoria);";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", produto.Nome);
                        command.Parameters.AddWithValue("@descricao", produto.Descricao);
                        command.Parameters.AddWithValue("@qtd_estoque", produto.QtdEstoque);
                        command.Parameters.AddWithValue("@preco", produto.Preco);
                        command.Parameters.AddWithValue("@id_categoria", produto.IdCategoria);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de conexão com o banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Write(ex.ToString());

            }
        }

        public void UpdateProduto(Produto produto)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "UPDATE Produto " +
                                 "set nome = @nome, descricao = @descricao, qtd_estoque = @qtd_estoque, preco = @preco, id_categoria = @id_categoria " +
                                 "WHERE id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", produto.Id);
                        command.Parameters.AddWithValue("@nome", produto.Nome);
                        command.Parameters.AddWithValue("@descricao", produto.Descricao);
                        command.Parameters.AddWithValue("@qtd_estoque", produto.QtdEstoque);
                        command.Parameters.AddWithValue("@preco", produto.Preco);
                        command.Parameters.AddWithValue("@id_categoria", produto.IdCategoria);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de conexão com o banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Write(ex.ToString());

            }

        }

        public void DeleteProduto(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "DELETE FROM Produto WHERE id = @id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de conexão com o banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Write(ex.ToString());

            }
        }
    }
}
