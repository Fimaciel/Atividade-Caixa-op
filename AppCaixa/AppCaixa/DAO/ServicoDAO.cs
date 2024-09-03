using AppCaixa.database;
using AppCaixa.Models;
using MySql.Data.MySqlClient;

namespace AppCaixa.DAO;

public class ServicoDAO
{
    public void Insert(Servico servico)
    {
        try
        {
            string tempoFormatado = servico.tempo.ToString("HH:mm:ss");

            string insertSql = "INSERT INTO servicos (valor, descricao, tempo) VALUES (@valor, @descricao, @tempo)";

            MySqlCommand command = new MySqlCommand(insertSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@valor", servico.valor);
            command.Parameters.AddWithValue("@descricao", servico.descricao);
            command.Parameters.AddWithValue("@tempo", tempoFormatado);
            command.ExecuteNonQuery();
            Console.WriteLine("Serviço salvo com sucesso!");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }



    public void Delete(Servico servico)
    {
        try
        {
            string deleteSql = "DELETE FROM servicos WHERE id = @id";
            MySqlCommand command = new MySqlCommand(deleteSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@id", servico.id);
            command.ExecuteNonQuery();
            Console.WriteLine("Serviço excluído com sucesso!");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Servico> Listar()
    {
        List<Servico> listaServico = new List<Servico>();

        try
        {
            string sqlList = "SELECT * FROM servicos";
            MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Servico servico = new Servico
                {
                    id = reader.GetInt32("id"),
                    valor = reader.GetDecimal("valor"),
                    descricao = reader.GetString("descricao"),
                    tempo = TimeOnly.FromTimeSpan(reader.GetTimeSpan("tempo"))
                };
                listaServico.Add(servico);
            }
            reader.Close();
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return listaServico;
    }


    public void Update(Servico servico)
    {
        try
        {
            List<string> updates = new List<string>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = Conexao.Conectar();

            if (servico.valor != 0)
            {
                updates.Add("valor = @valor");
                command.Parameters.AddWithValue("@valor", servico.valor);
            }

            if (!string.IsNullOrEmpty(servico.descricao))
            {
                updates.Add("descricao = @descricao");
                command.Parameters.AddWithValue("@descricao", servico.descricao);
            }

            if (servico.tempo != default(TimeOnly))
            {
                string tempoFormatado = servico.tempo.ToString("HH:mm:ss");
                updates.Add("tempo = @tempo");
                command.Parameters.AddWithValue("@tempo", tempoFormatado);
            }

            if (updates.Count > 0)
            {
                string updateSql = $"UPDATE servicos SET {string.Join(", ", updates)} WHERE id = @id";
                command.CommandText = updateSql;
                command.Parameters.AddWithValue("@id", servico.id);
                command.ExecuteNonQuery();
                Console.WriteLine("Serviço atualizado com sucesso!");
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
