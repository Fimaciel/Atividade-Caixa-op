namespace AppCaixa.Models;

public class Despesa
{
    public int id { get; set; }
    public decimal valor { get; set; }
    public string status_despesa { get; set; }
    public DateTime data_vencimento { get; set; }
    public DateTime data_pagamento { get; set; }
    public int caixa_fk { get; set; }
    public int fornecedor_fk { get; set; }
}