using bailinho_senior_system.models;
using bailinho_senior_system.config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace bailinho_senior_system.repositories
{
    internal class CategoriaRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        // private readonly string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\adolfo\\Documents\\bailinho_senior_system2\\bailinho_senior_system\\V2 - Segundo semestre\\bailinho_senior_system\\bailinho_senior_system\\data\\Database1.mdf\";Integrated Security=True";


        public List<Categoria> GetCategorias()
        {
            var categorias = new List<Categoria>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT id, nome, descricao FROM Categoria;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNome = reader.GetOrdinal("nome");
                            int idxDescricao = reader.GetOrdinal("descricao");

                            while (reader.Read())
                            {
                                var categoria = new Categoria();

                                // Só atribui quando o valor não for nulo no banco
                                if (!reader.IsDBNull(idxId))
                                    categoria.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNome))
                                    categoria.Nome = reader.GetString(idxNome);

                                if (!reader.IsDBNull(idxDescricao))
                                    categoria.Descricao = reader.GetString(idxDescricao);

                                categorias.Add(categoria);
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

            return categorias;
        }


        public Categoria GetCategoria(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT id, nome, descricao FROM Categoria WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNome = reader.GetOrdinal("nome");
                            int idxDescricao = reader.GetOrdinal("descricao");

                            if (reader.Read())
                            {
                                var categoria = new Categoria();

                                // Só atribui quando o valor não for nulo no banco
                                if (!reader.IsDBNull(idxId))
                                    categoria.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNome))
                                    categoria.Nome = reader.GetString(idxNome);

                                if (!reader.IsDBNull(idxDescricao))
                                    categoria.Descricao = reader.GetString(idxDescricao);

                                return categoria;
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

        public void CreateCategoria(Categoria categoria)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "INSERT INTO Categoria " +
                                 "(nome, descricao) " +
                                 "VALUES (@nome, @descricao);";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", categoria.Nome);
                        command.Parameters.AddWithValue("@descricao", categoria.Descricao);

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

        public void UpdateCategoria(Categoria categoria)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "UPDATE Categoria " +
                                 "set nome = @nome, descricao = @descricao " +
                                 "WHERE id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", categoria.Id);
                        command.Parameters.AddWithValue("@nome", categoria.Nome);
                        command.Parameters.AddWithValue("@descricao", categoria.Descricao);

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

        public void DeleteCategoria(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "DELETE FROM Categoria WHERE id = @id;";

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
