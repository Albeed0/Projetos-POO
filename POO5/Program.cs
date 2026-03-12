class Program
{
    static void Main(string[] args)
    {
        Funcionario f1 = new Desenvolvedor("João", 5000, 700);
        Funcionario f2 = new Gerente("Pedro", 8000, 80);

        List<Funcionario> funcionarios = [f1, f2];

        foreach (var Funcionario in funcionarios)
        {
            Funcionario.MostrarSalario();
        }
    }

}



abstract class Funcionario
{
    public string Nome { get; private set; }
    public double SalarioBase { get; protected set; }

    public Funcionario(string nome, double salariobase)
    {
        Nome = nome;
        SalarioBase = salariobase;
    }


    public abstract double CalcularSalario();

    public abstract void MostrarSalario();

}

class Desenvolvedor : Funcionario
{
    public double BonusPorProjeto { get; private set; }
    public Desenvolvedor(string nomeDev, double salariobaseDev, double bonus = 0) : base(nomeDev, salariobaseDev)
    {
        BonusPorProjeto = bonus;
    }

    public override double CalcularSalario()
    {
        return SalarioBase + BonusPorProjeto;
    }

    public override void MostrarSalario()
    {
        double mostrarDev = CalcularSalario();
        Console.WriteLine($"Seu salário final é de: R$ {mostrarDev:0.00}");
    }
}

class Gerente : Funcionario
{
    public double BonusEquipe { get; private set; }

    public Gerente(string nomeGerente, double salarioGerente, double bonusequipe) : base(nomeGerente, salarioGerente)
    {
        BonusEquipe = bonusequipe;
    }

    public override double CalcularSalario()
    {
        return SalarioBase + BonusEquipe;
    }

    public override void MostrarSalario()
    {
        double mostrar = CalcularSalario();
        Console.WriteLine($"Seu salário final é de: R$ {mostrar:0.00}");
    }
}