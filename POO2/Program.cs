// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices.Marshalling;

Carro carro = new Carro("Buggatti", "4A");
carro.MostrarInfo();
carro.VelocidadeAtual = -999;
carro.Acelerar(10);
carro.Frear(5);
carro.VelocidadeFinal();


class Carro
{

    public string Marca { get; set; }
    public string Modelo { get; set; }
    private int _velocidadeatual = 0;

    public int VelocidadeAtual
    {
        get { return _velocidadeatual; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("A velocidade não pode ser menor que zero se não o carro não anda né cabeça.");
            }
            else
            {
                _velocidadeatual = value;
            }
        }
    }

    public Carro(string marca, string modelo)
    {
        Marca = marca;
        Modelo = modelo;

        if (marca == string.Empty || modelo == string.Empty)
        {
            Console.WriteLine("Impossível prosseguir, por favor, insira uma marca e um modelo de carro.");
        }
    }
    public void MostrarInfo()
    {
        Console.WriteLine($"O modelo do carro é: {Modelo} da marca: {Marca} e possui velocidade de: {VelocidadeAtual}Km/h");
    }

    public void Acelerar(int VelocidadeAumentada)
    {
        if (VelocidadeAumentada < 0)
        {
            Console.WriteLine("Não é possível fazer nada com esse valor. Por favor, insira um número maior que 0(zero)");
        }
        else
        {
            _velocidadeatual += VelocidadeAumentada;
            Console.WriteLine($"A velocidade do carro aumentou. Atual: {_velocidadeatual}");
        }

    }

    public void Frear(int VelocidadeDiminuida)
    {
        if (VelocidadeDiminuida < 0)
        {
            Console.WriteLine("Não é possível fazer nada com esse valor. Por favor, insira um número maior que 0(zero)");
        }
        else if (VelocidadeDiminuida > _velocidadeatual)
        {
            Console.WriteLine("Pode não. Não pode");
        }
        else
        {
            _velocidadeatual -= VelocidadeDiminuida;
            Console.WriteLine($"O carro freou. Agora a velocidade é de: {_velocidadeatual}");
        }

    }

    public void VelocidadeFinal()
    {
        if (VelocidadeAtual < 0)
        {
            Console.WriteLine("O carro parou. Por favor, acelere ele");
        }
        Console.WriteLine($"A velocidade final é: {VelocidadeAtual}Km/h");
    }

}