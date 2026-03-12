// See https://aka.ms/new-console-template for more information
using System.Dynamic;

Pessoa pessoa = new Pessoa();
pessoa.Nome = "Jorge";
pessoa.Idade = -1;

Console.WriteLine(pessoa.Nome + " " + pessoa.Idade);



class Pessoa
{
    public string Nome { get; set; }
    int _idade;
    public int Idade
    {
        get
        {
            return _idade;
        }
        set
        {
            if ((value < 0) || (value >= 120))
            {
                Console.WriteLine("É impossível uma idade ser negativa ou maior que 120 nesse programa. Sua idade foi redefinida para zero.");
                value = 0;
                _idade = value;
            }
            else
            {
                _idade = value;
            }
        }
    }

}
