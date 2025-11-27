using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using bailinho_senior_system.models;
using bailinho_senior_system.config;

namespace bailinho_senior_system.repositories
{
    public class EventoRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public List<Evento> GetEventos()
        {
            var eventos = new List<Evento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Dentro de EventoRepository.cs, método GetEventos()
                    string sql = @"
                        SELECT 
                            te.id, te.nome, te.descricao, te.data, te.hora, te.valor_entrada, 
                            te.endereco AS id_endereco, 
        
                            -- Garanta que o SELECT inclua estes aliases:
                            ten.cep AS EnderecoCep,
                            ten.logradouro AS EnderecoLogradouro, 
                            ten.bairro AS EnderecoBairro,
                            ten.cidade AS EnderecoCidade,
                            ten.numero AS EnderecoNumero,
                            ten.estado AS EnderecoEstado,
                            ten.complemento AS EnderecoComplemento
        
                        FROM Evento te 
                        LEFT JOIN Endereco ten ON te.endereco = ten.id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new Evento
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString("descricao"),
                                Data = reader.GetDateTime("data"),
                                Hora = reader.GetTimeSpan("hora"),
                                ValorEntrada = reader.GetDecimal("valor_entrada"),
                                IdEndereco = reader.GetInt32("id_endereco"),

                                EnderecoCep = reader.GetString("EnderecoCep"),
                                EnderecoLogradouro = reader.GetString("EnderecoLogradouro"),
                                EnderecoBairro = reader.GetString("EnderecoBairro"),
                                EnderecoCidade = reader.GetString("EnderecoCidade"),
                                EnderecoNumero = reader.GetString("EnderecoNumero"),
                                EnderecoEstado = reader.GetString("EnderecoEstado"),
                                EnderecoComplemento = reader.IsDBNull(reader.GetOrdinal("EnderecoComplemento")) ? null : reader.GetString("EnderecoComplemento")
                            };
                            eventos.Add(evento);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            return eventos;
        }

        public Evento GetEvento(int id)
        {
            Evento evento = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"
                        SELECT 
                            te.id, te.nome, te.descricao, te.data, te.hora, te.valor_entrada, 
                            te.endereco AS id_endereco, 
                            
                            ten.cep AS EnderecoCep,
                            ten.logradouro AS EnderecoLogradouro, 
                            ten.bairro AS EnderecoBairro,
                            ten.cidade AS EnderecoCidade,
                            ten.numero AS EnderecoNumero,
                            ten.estado AS EnderecoEstado,
                            ten.complemento AS EnderecoComplemento
                            
                        FROM Evento te 
                        LEFT JOIN Endereco ten ON te.endereco = ten.id 
                        WHERE te.id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                evento = new Evento();
                                
                                evento.Id = reader.GetInt32("id");
                                evento.Nome = reader.GetString("nome");
                                evento.Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString("descricao");
                                evento.Data = reader.GetDateTime("data");
                                evento.Hora = reader.GetTimeSpan("hora");
                                evento.ValorEntrada = reader.GetDecimal("valor_entrada");
                                evento.IdEndereco = reader.GetInt32("id_endereco");
                                
                                evento.EnderecoCep = reader.GetString("EnderecoCep");
                                evento.EnderecoLogradouro = reader.GetString("EnderecoLogradouro");
                                evento.EnderecoBairro = reader.GetString("EnderecoBairro");
                                evento.EnderecoCidade = reader.GetString("EnderecoCidade");
                                evento.EnderecoNumero = reader.GetString("EnderecoNumero");
                                evento.EnderecoEstado = reader.GetString("EnderecoEstado");
                                evento.EnderecoComplemento = reader.IsDBNull(reader.GetOrdinal("EnderecoComplemento")) ? null : reader.GetString("EnderecoComplemento");
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
            return evento;
        }

        public void CreateEvento(Evento evento)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"
                        INSERT INTO Evento 
                        (nome, descricao, data, hora, valor_entrada, endereco) 
                        VALUES (@nome, @descricao, @data, @hora, @valor_entrada, @endereco); 
                        SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", evento.Nome);
                        command.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(evento.Descricao) ? (object)DBNull.Value : evento.Descricao);
                        command.Parameters.AddWithValue("@data", evento.Data.Date);
                        command.Parameters.AddWithValue("@hora", evento.Hora);
                        command.Parameters.AddWithValue("@valor_entrada", evento.ValorEntrada);
                        command.Parameters.AddWithValue("@endereco", evento.IdEndereco);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            evento.Id = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
        }

        public void UpdateEvento(Evento evento)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"
                        UPDATE Evento 
                        SET 
                            nome = @nome, 
                            descricao = @descricao, 
                            data = @data, 
                            hora = @hora, 
                            valor_entrada = @valor_entrada, 
                            endereco = @endereco 
                        WHERE id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", evento.Id);
                        command.Parameters.AddWithValue("@nome", evento.Nome);
                        command.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(evento.Descricao) ? (object)DBNull.Value : evento.Descricao);
                        command.Parameters.AddWithValue("@data", evento.Data.Date);
                        command.Parameters.AddWithValue("@hora", evento.Hora);
                        command.Parameters.AddWithValue("@valor_entrada", evento.ValorEntrada);
                        command.Parameters.AddWithValue("@endereco", evento.IdEndereco);

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

        public void DeleteEvento(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Evento WHERE id = @id;";
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