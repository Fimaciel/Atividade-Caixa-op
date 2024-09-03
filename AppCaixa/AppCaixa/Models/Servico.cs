namespace AppCaixa.Models;

public class Servico
{
    public int id { get; set; }
    public decimal valor { get; set; }
    public string descricao { get; set; }
    public TimeOnly tempo { get; set; }
}

