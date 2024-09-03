using AppCaixa.DAO;
using AppCaixa.Models;

namespace AppCaixa.view;

public static class EncargoView
{
    public static void Menu()
    {
        EncargoDAO encargoDao = new EncargoDAO();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir Encargo");
            Console.WriteLine("2. Listar Encargos");
            Console.WriteLine("3. Atualizar Encargo");
            Console.WriteLine("4. Deletar Encargo");
            Console.WriteLine("5. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Inserir(encargoDao);
                    break;
                case "2":
                    Listar(encargoDao);
                    break;
                case "3":
                    Atualizar(encargoDao);
                    break;
                case "4":
                    Deletar(encargoDao);
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }
        }
    }

    public static void Inserir(EncargoDAO encargoDao)
    {
        DispositivoDAO dispositivo = new DispositivoDAO();
        // RecebimentoDAO recebimento = new RecebimentoDAO();
        Encargo encargo = new Encargo();

        Console.Write("Descrição: ");
        encargo.descricao = Console.ReadLine();

        Console.Write("Valor: ");
        encargo.valor = decimal.Parse(Console.ReadLine());
        
        DispositivoView.Listar(dispositivo);
        Console.Write("ID do Dispositivo: ");
        encargo.id_dispositivo_fk = int.Parse(Console.ReadLine());

        Console.Write("ID do Recebimento: ");
        encargo.recebimento_fk = int.Parse(Console.ReadLine());

        encargoDao.Insert(encargo);
    }

    public static void Listar(EncargoDAO encargoDao)
    {
        List<Encargo> encargos = encargoDao.Listar();

        foreach (var e in encargos)
        {
            Console.WriteLine($"ID: {e.id}, Descrição: {e.descricao}, Valor: {e.valor}, " +
                              $"ID Dispositivo: {e.id_dispositivo_fk}, Recebimento: {e.recebimento_fk}");
        }
    }

    public static void Atualizar(EncargoDAO encargoDao)
    {
        Encargo encargo = new Encargo();

        Console.Write("ID do Encargo a ser atualizado: ");
        encargo.id = int.Parse(Console.ReadLine());

        Console.WriteLine("Deseja atualizar a Descrição? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Nova Descrição: ");
            encargo.descricao = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o Valor? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Valor: ");
            encargo.valor = decimal.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o ID do Dispositivo? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo ID do Dispositivo: ");
            encargo.id_dispositivo_fk = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o ID do Recebimento? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo ID do Recebimento: ");
            encargo.recebimento_fk = int.Parse(Console.ReadLine());
        }

        encargoDao.Update(encargo);
    }

    public static void Deletar(EncargoDAO encargoDao)
    {
        Console.Write("ID do Encargo a ser deletado: ");
        int id = int.Parse(Console.ReadLine());

        Encargo encargo = new Encargo { id = id };

        encargoDao.Delete(encargo);
    }
}
