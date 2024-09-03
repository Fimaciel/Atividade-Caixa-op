using AppCaixa.database;
using AppCaixa.Models;
using MySql.Data.MySqlClient;

namespace AppCaixa.DAO;

public class CaixaDAO
{
    public void Insert(Caixa caixa)
    {
        try
        {
            string insertSql =
                "INSERT INTO caixas (saldo_inicial, total_entradas, total_saidas, status_caixa, func_fk, saldo_total)" +
                "VALUES(@saldo_inicial, @total_entradas, @total_saidas, @status_caixa, @func_fk, @saldo_total)";

            MySqlCommand command = new MySqlCommand(insertSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@saldo_inicial", caixa.saldo_inicial);
            command.Parameters.AddWithValue("@total_entradas", caixa.total_entradas);
            command.Parameters.AddWithValue("@total_saidas", caixa.total_saidas);
            command.Parameters.AddWithValue("@status_caixa", caixa.status_caixa);
            command.Parameters.AddWithValue("@func_fk", caixa.func_fk);
            command.Parameters.AddWithValue("@saldo_total", caixa.saldo_total);
            command.ExecuteNonQuery();
            Console.WriteLine("Caixa salvo com sucesso.");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Delete(Caixa caixa)
    {
        try
        {
            string deleteSql = "DELETE FROM caixas WHERE id = @id";
            MySqlCommand command = new MySqlCommand(deleteSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@id", caixa.id);
            command.ExecuteNonQuery();
            Console.WriteLine("Caixa excluído com sucesso!");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Caixa> Listar()
    {
        List<Caixa> listaCaixa = new List<Caixa>();

        try
        {
            string sqlList = @"
                    SELECT caixas.*, funcionarios.nome AS funcionario_nome
                    FROM caixas
                    LEFT JOIN funcionarios ON caixas.func_fk = funcionarios.id";

            MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Caixa caixa = new Caixa
                {
                    id = reader.GetInt32("id"),
                    saldo_inicial = reader.IsDBNull(reader.GetOrdinal("saldo_inicial"))
                        ? 0
                        : reader.GetDecimal("saldo_inicial"),
                    total_entradas = reader.IsDBNull(reader.GetOrdinal("total_entradas"))
                        ? 0
                        : reader.GetInt32("total_entradas"),
                    total_saidas = reader.IsDBNull(reader.GetOrdinal("total_saidas"))
                        ? 0
                        : reader.GetInt32("total_saidas"),
                    status_caixa = reader.IsDBNull(reader.GetOrdinal("status_caixa"))
                        ? string.Empty
                        : reader.GetString("status_caixa"),
                    func_fk = reader.IsDBNull(reader.GetOrdinal("func_fk")) ? 0 : reader.GetInt32("func_fk"),
                    saldo_total = reader.IsDBNull(reader.GetOrdinal("saldo_total"))
                        ? 0
                        : reader.GetDecimal("saldo_total"),
                    funcionario_nome = reader.IsDBNull(reader.GetOrdinal("funcionario_nome"))
                        ? "Desconhecido"
                        : reader.GetString("funcionario_nome")
                };
                listaCaixa.Add(caixa);
            }

            reader.Close();
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return listaCaixa;
    }
    

    public void Update(Caixa caixa)
    {
        try
        {
            List<string> updates = new List<string>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = Conexao.Conectar();

            if (caixa.saldo_inicial > 0)
            {
                updates.Add("saldo_inicial = @saldo_inicial");
                command.Parameters.AddWithValue("@saldo_inicial", caixa.saldo_inicial);
            }

            if (caixa.total_entradas >= 0)
            {
                updates.Add("total_entradas = @total_entradas");
                command.Parameters.AddWithValue("@total_entradas", caixa.total_entradas);
            }

            if (caixa.total_saidas >= 0)
            {
                updates.Add("total_saidas = @total_saidas");
                command.Parameters.AddWithValue("@total_saidas", caixa.total_saidas);
            }

            if (!string.IsNullOrEmpty(caixa.status_caixa))
            {
                updates.Add("status_caixa = @status_caixa");
                command.Parameters.AddWithValue("@status_caixa", caixa.status_caixa);
            }

            if (caixa.func_fk > 0)
            {
                updates.Add("func_fk = @func_fk");
                command.Parameters.AddWithValue("@func_fk", caixa.func_fk);
            }

            if (caixa.saldo_total > 0)
            {
                updates.Add("saldo_total = @saldo_total");
                command.Parameters.AddWithValue("@saldo_total", caixa.saldo_total);
            }

            if (updates.Count > 0)
            {
                string updateSql = $"UPDATE caixas SET {string.Join(", ", updates)} WHERE id = @id";
                command.CommandText = updateSql;
                command.Parameters.AddWithValue("@id", caixa.id);
                command.ExecuteNonQuery();
                Console.WriteLine("Caixa atualizado com sucesso!");
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