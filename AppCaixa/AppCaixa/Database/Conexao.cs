namespace AppCaixa.database;

using MySql.Data.MySqlClient;

public static class Conexao
{
    private static MySqlConnection conexao;

    public static MySqlConnection Conectar()
    {
        try
        {
            conexao = new MySqlConnection("server=localhost;" +
                                          "port=3306;" +
                                          "uid=root;" +
                                          "pwd=root;" +
                                          "database=financeiroDB;");
            conexao.Open();
        }
        catch (Exception e)
        {
           throw new Exception("Erro ao conectar no banco de dados: " + e);
        }
        
        return conexao;
    }

    public static void FecharConexao()
    {
        conexao.Close();
    }
}