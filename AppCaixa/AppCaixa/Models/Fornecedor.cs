namespace AppCaixa.Models;

public class Fornecedor
{
    public int id { get; set; }
    public string razao_social { get; set; }
    public string nome_fantasia { get; set; }
    public string email { get; set; }
    public string telefone { get; set; }
    public string atividade_economica { get; set; }
    public bool ativo { get; set; }
}

