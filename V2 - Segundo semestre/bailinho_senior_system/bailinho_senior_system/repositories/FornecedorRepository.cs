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
    internal class FornecedorRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        // private readonly string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\adolfo\\Documents\\bailinho_senior_system2\\bailinho_senior_system\\V2 - Segundo semestre\\bailinho_senior_system\\bailinho_senior_system\\data\\Database1.mdf\";Integrated Security=True";


        public List<Fornecedor> GetFornecedores()
        {
            var fornecedores = new List<Fornecedor>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT id, nome, cnpj, email, telefone FROM Fornecedor;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNome = reader.GetOrdinal("nome");
                            int idxCnpj = reader.GetOrdinal("cnpj");
                            int idxEmail = reader.GetOrdinal("email");
                            int idxTelefone = reader.GetOrdinal("telefone");


                            while (reader.Read())
                            {
                                var fornecedor = new Fornecedor();

                                // Só atribui quando o valor não for nulo no banco
                                if (!reader.IsDBNull(idxId))
                                    fornecedor.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNome))
                                    fornecedor.Nome = reader.GetString(idxNome);

                                if (!reader.IsDBNull(idxCnpj))
                                    fornecedor.Cnpj = reader.GetString(idxCnpj);

                                if (!reader.IsDBNull(idxEmail))
                                    fornecedor.Email = reader.GetString(idxEmail);

                                if (!reader.IsDBNull(idxTelefone))
                                    fornecedor.Telefone = reader.GetString(idxTelefone);

                                fornecedores.Add(fornecedor);
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


        public Fornecedor GetFornecedor(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT id, nome, cnpj, email, telefone FROM Fornecedor WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int idxId = reader.GetOrdinal("id");
                            int idxNome = reader.GetOrdinal("nome");
                            int idxCnpj = reader.GetOrdinal("cnpj");
                            int idxEmail = reader.GetOrdinal("email");
                            int idxTelefone = reader.GetOrdinal("telefone");

                            if (reader.Read())
                            {
                                var fornecedor = new Fornecedor();

                                // Só atribui quando o valor não for nulo no banco
                                if (!reader.IsDBNull(idxId))
                                    fornecedor.Id = reader.GetInt32(idxId);

                                if (!reader.IsDBNull(idxNome))
                                    fornecedor.Nome = reader.GetString(idxNome);

                                if (!reader.IsDBNull(idxCnpj))
                                    fornecedor.Cnpj = reader.GetString(idxCnpj);

                                if (!reader.IsDBNull(idxEmail))
                                    fornecedor.Email = reader.GetString(idxEmail);

                                if (!reader.IsDBNull(idxTelefone))
                                    fornecedor.Telefone = reader.GetString(idxTelefone);

                                return fornecedor;
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

            return null;
        }

        public void CreateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "INSERT INTO Fornecedor " +
                                 "(nome, cnpj, email, telefone) " +
                                 "VALUES (@nome, @cnpj, @email, @telefone);";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", fornecedor.Nome);
                        command.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                        command.Parameters.AddWithValue("@email", fornecedor.Email);
                        command.Parameters.AddWithValue("@telefone", fornecedor.Telefone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;

            }
        }

        public void UpdateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "UPDATE Fornecedor " +
                                 "SET nome = @nome, cnpj = @cnpj, email = @email, telefone = @telefone " +
                                 "WHERE id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", fornecedor.Id);
                        command.Parameters.AddWithValue("@nome", fornecedor.Nome);
                        command.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                        command.Parameters.AddWithValue("@email", fornecedor.Email);
                        command.Parameters.AddWithValue("@telefone", fornecedor.Telefone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                Console.Write(ex.ToString());

            }

        }

        public void DeleteFornecedor(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "DELETE FROM Fornecedor WHERE id = @id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.Write(ex.ToString());
                throw;
            }
        }
    }
}
