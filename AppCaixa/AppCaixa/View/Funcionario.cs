namespace AppCaixa.view;

using AppCaixa.DAO;
using AppCaixa.Models;
using AppCaixa.Helpers;

public static class FuncionarioView
{
    public static void menu()
    {
        FuncionarioDAO funcionarioDao = new FuncionarioDAO();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir Funcionário");
            Console.WriteLine("2. Listar Funcionário");
            Console.WriteLine("3. Atualizar Funcionário");
            Console.WriteLine("4. Deletar Funcionário");
            Console.WriteLine("5. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Inserir(funcionarioDao);
                    break;
                case "2":
                    Listar(funcionarioDao);
                    break;
                case "3":
                    Atualizar(funcionarioDao);
                    break;
                case "4":
                    Deletar(funcionarioDao);
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

    public static void Inserir(FuncionarioDAO funcionarioDao)
    {
        Funcionario funcionario = new Funcionario();

        Console.Write("Nome: ");
        funcionario.nome = Console.ReadLine();
        
        Console.Write("CPF: ");
        bool cpfValido = true;
        while (cpfValido)
        {
            Console.Write("Novo CPF: ");
            funcionario.cpf = Helpers.LimparCPF(Console.ReadLine());

            if (!Helpers.ValidarCpf(funcionario.cpf))
            {
                Console.WriteLine("Informe um cpf Válido!");
            }
            else
            {
                cpfValido = false;
            }
        }

        funcionarioDao.Insert(funcionario);
    }

    public static void Listar(FuncionarioDAO funcionarioDao)
    {
        List<Funcionario> funcionarios = funcionarioDao.Listar();

        foreach (var f in funcionarios)
        {
            Console.WriteLine(
                $"ID: {f.id}, Nome: {f.nome}, CPF: {f.cpf}");
        }
    }

    public static void Atualizar(FuncionarioDAO funcionarioDao)
    {
        Funcionario funcionario = new Funcionario();

        Console.Write("ID do Cliente a ser atualizado: ");
        funcionario.id = int.Parse(Console.ReadLine());

        Console.WriteLine("Deseja atualizar o Nome? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Nome: ");
            funcionario.nome = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o CPF? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            bool cpfValido = true;
            while (cpfValido)
            {
                Console.Write("Novo CPF: ");
                funcionario.cpf = Helpers.LimparCPF(Console.ReadLine());

                if (!Helpers.ValidarCpf(funcionario.cpf))
                {
                    Console.WriteLine("Informe um cpf Válido!");
                }
                else
                {
                    cpfValido = false;
                }
            }
        }
        
        funcionarioDao.Update(funcionario);
    }


    public static void Deletar(FuncionarioDAO funcionarioDao)
    {
        Console.Write("ID do Funcionário a ser deletado: ");
        int id = int.Parse(Console.ReadLine());

        Funcionario funcionario = new Funcionario { id = id };

        funcionarioDao.Delete(funcionario);
    }
}