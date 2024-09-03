using MySql.Data.MySqlClient;
using AppCaixa.Models;
using AppCaixa.database;

namespace AppCaixa.DAO;

public class EncargoDAO
{
    public void Insert(Encargo encargo)
    {
        try
        {
            MySqlConnection conexao = Conexao.Conectar();
            string insertSql = "INSERT INTO encargos (descricao, valor, id_dispositivo_fk, recebimento_fk) " +
                               "VALUES (@descricao, @valor, @id_dispositivo_fk, @recebimento_fk)";
            
            MySqlCommand command = new MySqlCommand(insertSql, conexao);
            command.Parameters.AddWithValue("@descricao", encargo.descricao);
            command.Parameters.AddWithValue("@valor", encargo.valor);
            command.Parameters.AddWithValue("@id_dispositivo_fk", encargo.id_dispositivo_fk);
            command.Parameters.AddWithValue("@recebimento_fk", encargo.recebimento_fk);
            command.ExecuteNonQuery();
            
            Console.WriteLine("Encargo salvo com sucesso!");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Delete(Encargo encargo)
    {
        try
        {
            string deleteSql = "DELETE FROM encargos WHERE id = @id";
            MySqlCommand command = new MySqlCommand(deleteSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@id", encargo.id);
            command.ExecuteNonQuery();
            Console.WriteLine("Encargo excluído com sucesso!");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

public List<Encargo> Listar()
{
    List<Encargo> listaEncargo = new List<Encargo>();

    try
    {
        string sqlList = "SELECT * FROM encargos";
        MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Encargo encargo = new Encargo
            {
                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id"),
                descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? string.Empty : reader.GetString("descricao"),
                valor = reader.IsDBNull(reader.GetOrdinal("valor")) ? 0 : reader.GetDecimal("valor"),
                id_dispositivo_fk = reader.IsDBNull(reader.GetOrdinal("id_dispositivo_fk")) ? 0 : reader.GetInt32("id_dispositivo_fk"),
                recebimento_fk = reader.IsDBNull(reader.GetOrdinal("recebimento_fk")) ? 0 : reader.GetInt32("recebimento_fk")
            };
            listaEncargo.Add(encargo);
        }
        reader.Close();
        Conexao.FecharConexao();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

    return listaEncargo;
}

    public void Update(Encargo encargo)
    {
        try
        {
            List<string> updates = new List<string>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = Conexao.Conectar();

            if (!string.IsNullOrEmpty(encargo.descricao))
            {
                updates.Add("descricao = @descricao");
                command.Parameters.AddWithValue("@descricao", encargo.descricao);
            }

            if (encargo.valor != 0)
            {
                updates.Add("valor = @valor");
                command.Parameters.AddWithValue("@valor", encargo.valor);
            }

            if (encargo.id_dispositivo_fk != 0)
            {
                updates.Add("id_dispositivo_fk = @id_dispositivo_fk");
                command.Parameters.AddWithValue("@id_dispositivo_fk", encargo.id_dispositivo_fk);
            }

            if (encargo.recebimento_fk != 0)
            {
                updates.Add("recebimento_fk = @recebimento_fk");
                command.Parameters.AddWithValue("@recebimento_fk", encargo.recebimento_fk);
            }

            if (updates.Count > 0)
            {
                string updateSql = $"UPDATE encargos SET {string.Join(", ", updates)} WHERE id = @id";
                command.CommandText = updateSql;
                command.Parameters.AddWithValue("@id", encargo.id);
                command.ExecuteNonQuery();
                Console.WriteLine("Encargo atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhuma atualização realizada.");
            }

            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
