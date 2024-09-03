namespace AppCaixa.view;

using System;
using AppCaixa.DAO;
using AppCaixa.Models;
using System.Collections.Generic;

public static class FornecedorView
{
    public static void menu()
    {
        FornecedorDAO fornecedorDao = new FornecedorDAO();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir Fornecedor");
            Console.WriteLine("2. Listar Fornecedor");
            Console.WriteLine("3. Atualizar Fornecedor");
            Console.WriteLine("4. Deletar Fornecedor");
            Console.WriteLine("5. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Inserir(fornecedorDao);
                    break;
                case "2":
                    Listar(fornecedorDao);
                    break;
                case "3":
                    Atualizar(fornecedorDao);
                    break;
                case "4":
                    Deletar(fornecedorDao);
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

    public static void Inserir(FornecedorDAO fornecedorDao)
    {
        Fornecedor fornecedor = new Fornecedor();

        Console.Write("Razão Social: ");
        fornecedor.razao_social = Console.ReadLine();
        
        Console.Write("Nome Fantasia: ");
        fornecedor.nome_fantasia = Console.ReadLine();

        Console.Write("Email: ");
        fornecedor.email = Console.ReadLine();

        Console.Write("Telefone: ");
        fornecedor.telefone = Console.ReadLine();

        Console.Write("Atividade Econômica: ");
        fornecedor.atividade_economica = Console.ReadLine();

        Console.Write("Ativo (S/N): ");
        fornecedor.ativo = Console.ReadLine().Trim().ToUpper() == "S";

        fornecedorDao.Insert(fornecedor);
    }

    public static void Listar(FornecedorDAO fornecedorDao)
    {
        List<Fornecedor> fornecedores = fornecedorDao.Listar();

        foreach (var f in fornecedores)
        {
            Console.WriteLine(
                $"ID: {f.id}, Razão Social: {f.razao_social}, Nome Fantasia: {f.nome_fantasia}, " +
                $"Email: {f.email}, Telefone: {f.telefone}, Atividade Econômica: {f.atividade_economica}, " +
                $"Ativo: {(f.ativo ? "Sim" : "Não")}");
        }
    }

    public static void Atualizar(FornecedorDAO fornecedorDao)
    {
        Fornecedor fornecedor = new Fornecedor();

        Console.Write("ID do Fornecedor a ser atualizado: ");
        fornecedor.id = int.Parse(Console.ReadLine());

        Console.WriteLine("Deseja atualizar a Razão Social? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Nova Razão Social: ");
            fornecedor.razao_social = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o Nome Fantasia? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Nome Fantasia: ");
            fornecedor.nome_fantasia = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o Email? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Email: ");
            fornecedor.email = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o Telefone? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Telefone: ");
            fornecedor.telefone = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar a Atividade Econômica? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Nova Atividade Econômica: ");
            fornecedor.atividade_economica = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o Status de Ativo? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Ativo (S/N): ");
            fornecedor.ativo = Console.ReadLine().Trim().ToUpper() == "S";
        }

        fornecedorDao.Update(fornecedor);
    }


    public static void Deletar(FornecedorDAO fornecedorDao)
    {
        Console.Write("ID do Fornecedor a ser deletado: ");
        int id = int.Parse(Console.ReadLine());

        Fornecedor fornecedor = new Fornecedor { id = id };

        fornecedorDao.Delete(fornecedor);
    }
}

