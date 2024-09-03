namespace AppCaixa.Models;

public class Caixa
{
    public int id { get; set; }
    public decimal saldo_inicial { get; set; }
    public int total_entradas { get; set; }
    public int total_saidas { get; set; }
    public string status_caixa { get; set; }
    public int func_fk { get; set; }
    public decimal saldo_total { get; set; }

    public string funcionario_nome { get; set; }
    
}