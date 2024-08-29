namespace AppCaixa.Models;

public class Cliente
{
    public int id { get; set; }
    public string nome { get; set; }
    public string cpf { get; set; }
    public string email { get; set; }
    public string telefone { get; set; }
    public DateOnly data_nasc { get; set; }
}