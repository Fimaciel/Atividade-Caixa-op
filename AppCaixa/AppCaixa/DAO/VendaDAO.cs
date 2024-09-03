using MySql.Data.MySqlClient;
using AppCaixa.Models;
using AppCaixa.database;

namespace AppCaixa.DAO;

public class VendaDAO
{
    public void Insert(Venda venda, List<VendaServico> vendaServicos)
    {
        MySqlTransaction transaction = null;

        try
        {
            MySqlConnection conexao = Conexao.Conectar();
            transaction = conexao.BeginTransaction();

            // Inserção da venda
            string insertSql = "INSERT INTO vendas (data_venda, hora, valor_total, desconto, valor_final, tipo, id_cliente_fk) " +
                               "VALUES (@data_venda, @hora, @valor_total, @desconto, @valor_final, @tipo, @id_cliente_fk)";
            
            MySqlCommand command = new MySqlCommand(insertSql, conexao, transaction);
            command.Parameters.AddWithValue("@data_venda", venda.data_venda);
            command.Parameters.AddWithValue("@hora", venda.hora.ToString("HH:mm:ss"));
            command.Parameters.AddWithValue("@valor_total", venda.valor_total);
            command.Parameters.AddWithValue("@desconto", venda.desconto);
            command.Parameters.AddWithValue("@valor_final", venda.valor_final);
            command.Parameters.AddWithValue("@tipo", venda.tipo);
            command.Parameters.AddWithValue("@id_cliente_fk", venda.id_cliente_fk == 0 ? DBNull.Value : (object)venda.id_cliente_fk);
            command.ExecuteNonQuery();
            
            int vendaId = (int)command.LastInsertedId;

            // Inserção dos serviços associados à venda
            foreach (var vendaServico in vendaServicos)
            {
                vendaServico.venda_fk = vendaId;
                string insertVendaServicoSql = "INSERT INTO vendasServicos (venda_fk, servico_fk, valor_unitario, quantidade) " +
                                               "VALUES (@venda_fk, @servico_fk, @valor_unitario, @quantidade)";
                
                MySqlCommand vendaServicoCommand = new MySqlCommand(insertVendaServicoSql, conexao, transaction);
                vendaServicoCommand.Parameters.AddWithValue("@venda_fk", vendaServico.venda_fk);
                vendaServicoCommand.Parameters.AddWithValue("@servico_fk", vendaServico.servico_fk);
                vendaServicoCommand.Parameters.AddWithValue("@valor_unitario", vendaServico.valor_unitario);
                vendaServicoCommand.Parameters.AddWithValue("@quantidade", vendaServico.quantidade);
                vendaServicoCommand.ExecuteNonQuery();
            }

            transaction.Commit();
            Console.WriteLine("Venda e serviços associados salvos com sucesso!");
        }
        catch (Exception e)
        {
            transaction?.Rollback();
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Conexao.FecharConexao();
        }
    }

    public void Delete(Venda venda)
    {
        MySqlTransaction transaction = null;
        try
        {
            MySqlConnection conexao = Conexao.Conectar();
            transaction = conexao.BeginTransaction();

            // Deletar serviços associados à venda
            string deleteVendaServicosSql = "DELETE FROM vendasServicos WHERE venda_fk = @venda_fk";
            MySqlCommand commandVendaServicos = new MySqlCommand(deleteVendaServicosSql, conexao, transaction);
            commandVendaServicos.Parameters.AddWithValue("@venda_fk", venda.id);
            commandVendaServicos.ExecuteNonQuery();

            // Deletar venda
            string deleteSql = "DELETE FROM vendas WHERE id = @id";
            MySqlCommand command = new MySqlCommand(deleteSql, conexao, transaction);
            command.Parameters.AddWithValue("@id", venda.id);
            command.ExecuteNonQuery();

            transaction.Commit();
            Console.WriteLine("Venda e serviços associados excluídos com sucesso!");
        }
        catch (Exception e)
        {
            transaction?.Rollback();
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Conexao.FecharConexao();
        }
    }

    public List<Venda> Listar()
    {
        List<Venda> listaVenda = new List<Venda>();

        try
        {
            string sqlList = "SELECT * FROM vendas";
            MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Venda venda = new Venda
                {
                    id = reader.GetInt32("id"),
                    data_venda = reader.GetDateTime("data_venda"),
                    hora = TimeOnly.FromTimeSpan(reader.GetTimeSpan("hora")),
                    valor_total = reader.GetDecimal("valor_total"),
                    desconto = reader.GetDecimal("desconto"),
                    valor_final = reader.GetDecimal("valor_final"),
                    tipo = reader.GetString("tipo"),
                };
                
                listaVenda.Add(venda);
            }
            reader.Close();
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return listaVenda;
    }

    private List<VendaServico> ListarServicosPorVenda(int vendaId)
    {
        List<VendaServico> vendaServicos = new List<VendaServico>();

        try
        {
            string sqlList = "SELECT * FROM vendasServicos WHERE venda_fk = @venda_fk";
            MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
            command.Parameters.AddWithValue("@venda_fk", vendaId);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                VendaServico vendaServico = new VendaServico
                {
                    id = reader.GetInt32("id"),
                    venda_fk = reader.GetInt32("venda_fk"),
                    servico_fk = reader.GetInt32("servico_fk"),
                    valor_unitario = reader.GetDecimal("valor_unitario"),
                    quantidade = reader.GetInt32("quantidade")
                };

                vendaServicos.Add(vendaServico);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return vendaServicos;
    }

    public void Update(Venda venda)
    {
        MySqlTransaction transaction = null;

        try
        {
            MySqlConnection conexao = Conexao.Conectar();
            transaction = conexao.BeginTransaction();

            List<string> updates = new List<string>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = conexao;
            command.Transaction = transaction;

            if (venda.data_venda != default(DateTime))
            {
                updates.Add("data_venda = @data_venda");
                command.Parameters.AddWithValue("@data_venda", venda.data_venda);
            }

            if (venda.hora != default(TimeOnly))
            {
                updates.Add("hora = @hora");
                command.Parameters.AddWithValue("@hora", venda.hora.ToString("HH:mm:ss"));
            }

            if (venda.valor_total != 0)
            {
                updates.Add("valor_total = @valor_total");
                command.Parameters.AddWithValue("@valor_total", venda.valor_total);
            }

            if (venda.desconto != 0)
            {
                updates.Add("desconto = @desconto");
                command.Parameters.AddWithValue("@desconto", venda.desconto);
            }

            if (venda.valor_final != 0)
            {
                updates.Add("valor_final = @valor_final");
                command.Parameters.AddWithValue("@valor_final", venda.valor_final);
            }

            if (!string.IsNullOrEmpty(venda.tipo))
            {
                updates.Add("tipo = @tipo");
                command.Parameters.AddWithValue("@tipo", venda.tipo);
            }

            if (venda.id_cliente_fk != 0)
            {
                updates.Add("id_cliente_fk = @id_cliente_fk");
                command.Parameters.AddWithValue("@id_cliente_fk", venda.id_cliente_fk);
            }

            if (updates.Count > 0)
            {
                string updateSql = $"UPDATE vendas SET {string.Join(", ", updates)} WHERE id = @id";
                command.CommandText = updateSql;
                command.Parameters.AddWithValue("@id", venda.id);
                command.ExecuteNonQuery();
                Console.WriteLine("Venda atualizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhuma atualização realizada.");
            }

            transaction.Commit();
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            transaction?.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }
}
