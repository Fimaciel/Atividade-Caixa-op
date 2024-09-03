using AppCaixa.view;

bool running = true;

while (running)
{
    Console.WriteLine("Menu Principal:");
    Console.WriteLine("1. Gerenciar Caixa");
    Console.WriteLine("2. Gerenciar Funcionário");
    Console.WriteLine("3. Sair");

    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CaixaView.menu();
            break;
        case "2":
            FuncionarioView.menu();
            break;
        case "3":
            running = false;
            break;
        default:
            Console.WriteLine("Opção inválida, tente novamente.");
            break;
    }
}