using System;
using System.Reflection;

class Program
{

    static void Main(string[] args)
    {
        Menu();
    }


    static void Menu()
    {
        Console.WriteLine("Olá, bem vindo(a) ao programa de conta bancária. Por favor, escolha uma das seguintes opções: ");
        bool continuarUsando = true;
        ContaBancaria contaBancaria = null;

        while (continuarUsando)
        {
            Console.WriteLine($"1 - Colocar dados\n2 - Depositar\n3 - Sacar\n4 - Sair;");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Insira o número da sua conta, seu nome e seu saldo.");
                    int contNumero = int.Parse(Console.ReadLine());
                    string nome = Console.ReadLine();
                    double saldo = double.Parse(Console.ReadLine());

                    contaBancaria = new ContaBancaria(contNumero, nome, saldo);
                    contaBancaria.MostrarInfoConta();
                    break;

                case 2:
                    if (contaBancaria == null)
                    {
                        Console.WriteLine("Você precisa criar uma conta primeiro.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write($"Quanto deseja depositar?");
                        double depositarValor = double.Parse(Console.ReadLine());
                        contaBancaria.Depositar(depositarValor);
                        contaBancaria.SaldoFinal();
                    }
                    break;

                case 3:
                    if (contaBancaria == null)
                    {
                        Console.WriteLine("Você precisa criar uma conta primeiro.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write($"Quanto deseja sacar?");
                        int sacarValor = int.Parse(Console.ReadLine());
                        contaBancaria.Sacar(sacarValor);
                        contaBancaria.SaldoFinal();
                    }
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Nós agradecemos a preferência.");
                    continuarUsando = false;
                    Environment.Exit(0);
                    break;

            }
        }

    }

}



class ContaBancaria
{

    public int _numeroConta { get; private set; }
    public string _titular { get; private set; }
    public double _saldo { get; private set; }

    public ContaBancaria(int numero, string titular, double saldoInic = 0)
    {
        _numeroConta = numero;
        _titular = titular;

        if (saldoInic < 0)
        {
            Console.WriteLine("Seu saldo não pode ser negativo.");
        }
        else
        {
            _saldo = saldoInic;
        }
    }

    public void MostrarInfoConta()
    {
        Console.WriteLine($"O titular é: {_titular}, com o número da conta: {_numeroConta}, com o saldo de: R$ {_saldo:0.00}");
    }

    public void Depositar(double saldoAdicionado)
    {
        if (saldoAdicionado < 0)
        {
            throw new TargetException("Isso é impossível, um depósito não pode ser negativo");
        }
        else
        {
            _saldo += saldoAdicionado;
        }
    }

    public void Sacar(double saldoRetirado)
    {
        if (saldoRetirado > _saldo)
        {
            throw new TargetException("Saldo não pode ser sacado, pois é um valor muito acima de sua conta.");
        }

        else
        {
            _saldo -= saldoRetirado;
        }
    }
    public void SaldoFinal()
    {
        Console.WriteLine($"O saldo no final é de: R${_saldo:0.00}");
    }
}