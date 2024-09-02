namespace AppCaixa.Helpers;
using System.Linq;

public static class Helpers
{
    public static bool ValidarCpf(string cpf)
    {
        List<int> arrayCPF = new List<int>();
        
        string cpfLimpo = LimparCPF(cpf);

        int quantCpf = cpfLimpo.Length;
        for (int i = 0; i < quantCpf ; i++)
        {
            arrayCPF.Add(Convert.ToInt32(cpfLimpo[i].ToString()));
        }

        if (quantCpf == 11)
        {
            if (PrimeiroVerificador(arrayCPF, cpfLimpo) && SegundoVerificador(arrayCPF, cpfLimpo))
            {
                return true;
            }
        }
            
        return false;
    }

    private static bool PrimeiroVerificador(List<int> arrayCPF, string cpfLimpo)
    {
        int multiplicador = 10;
        int mult;
        int n = 0;
        List<int> r = new List<int>();

        for (int i = 2; i < cpfLimpo.Length; i++)
        {
            mult = arrayCPF[n] * multiplicador;
            multiplicador--;
            n++;
            r.Add(mult);
        }

        int resultado = r.Sum();
        int mod = resultado % 11;

        if (mod < 2 && arrayCPF[10] == 0)
        {
            return true;
        }
        else
        {
            int primeiroVerificador = 11 - mod;
            if (mod >= 2 && primeiroVerificador == arrayCPF[9])
            {
                return true;
            }
        }

        return false;
    }

    private static bool SegundoVerificador(List<int> arrayCPF, string cpfLimpo)
    {
        int multiplicador = 11;
        int mult;
        int n = 0;
        List<int> r = new List<int>();

        for (int i = 9; i >= 0; i--)
        {
            mult = arrayCPF[n] * multiplicador;
            multiplicador--;
            n++;
            r.Add(mult);
        }

        int resultado = r.Sum();
        int mod = resultado % 11;

        if (mod < 2 && arrayCPF[10] == 0)
        {
            return true;
        }
        else
        {
            int primeiroVerificador = 11 - mod;
            if (mod >= 2 && primeiroVerificador == arrayCPF[10])
            {
                return true;
            }
        }

        return false;
    }

    static string LimparCPF(string cpf)
    {
        cpf = cpf.Replace(".", "").Replace("-", "");
        
        return cpf;
    }
}