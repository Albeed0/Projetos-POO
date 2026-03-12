using System.IO;
class Program
{

    static void Main(string[] args)
    {

        /*
        Guerreiro guerreiro = new Guerreiro("Golias", 32, 43);
        Mago mago = new Mago("Alfredo", 39, 75);

        List<IAtacante> atacantes = new List<IAtacante> {guerreiro, mago};
        List<IDefesa> defensor = new List<IDefesa>{guerreiro};
        List<ICuravel> curandeiros = new List<ICuravel>{mago};

        Arma arma1 = new Arma(TipoArma.Espada, 50);
        BananaWeapon arma2 = new();
        Arma arma3 = new(TipoArma.Arco, 90);
        List<Arma> armas = new(){arma1, arma2, arma3};
        Arma teste = Arma.CriarAPartirDoTexto("Espada;50");
        teste.InfoArma();
        teste.MostrarDescricao();
        */

        try
        {
            Arma teste = Arma.CriarAPartirDoTexto("pote;90");
            teste.InfoArma();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar arma: {ex.Message}");
        }


    }

}


//Ações
public interface IAtacante
{
    void Atacar();
}

public interface IDefesa
{
    void Defender();
}

public interface ICuravel
{
    void Curar();
}

//Classes de personagens
class Guerreiro : IAtacante, IDefesa
{
    public string Nome { get; private set; }
    public int Forca { get; private set; }
    public int Defesa { get; private set; }

    public Guerreiro(string nomeG, int forcaG, int defesaG)
    {
        Nome = nomeG;
        Forca = forcaG;
        Defesa = defesaG;
    }

    public void Atacar()
    {
        Console.WriteLine($"Guerreiro {Nome} atacou com força {Forca}");
    }

    public void Defender()
    {
        Console.WriteLine($"O ataque foi bloqueado pois a defesa é de: {Defesa}");
    }

}

class Mago : IAtacante, ICuravel
{
    public string Nome { get; private set; }
    public int PoderMagico { get; private set; }
    public int ValorCura { get; private set; }

    public Mago(string nomeM, int poderMagico, int poderdecura)
    {
        Nome = nomeM;
        PoderMagico = poderMagico;
        ValorCura = poderdecura;
    }

    public void Atacar()
    {
        Console.WriteLine($"Mago {Nome} lançou magia com poder {PoderMagico}");
    }

    public void Curar()
    {
        Console.WriteLine($"O mago curou {ValorCura} pontos de vida");
    }
}

//Armas

class ArmaInvalidaException : Exception
{
    public ArmaInvalidaException(string mensagem) : base(mensagem)
    {

    }
}


enum TipoArma
{
    Espada,
    Machado,
    Cajado,
    Arco,
    SpecialWeapon
}

class Arma
{

    public TipoArma Tipo { get; private set; }
    public int Dano { get; private set; }

    public Arma(TipoArma tipo, int dano)
    {
        Tipo = tipo;
        Dano = dano;
    }

    public void InfoArma()
    {
        Console.WriteLine($"O tipo dessa arma é: {Tipo}, e dá {Dano} pontos de dano");
    }
    public virtual void MostrarDescricao()
    {
        switch (Tipo)
        {
            case TipoArma.Arco:
                Console.WriteLine("Um tipo de arma feita para ataques a distância");
                break;

            case TipoArma.Espada:
                Console.WriteLine("Uma arma forjada de metal, contendo dois lados com fios cortantes");
                break;

            case TipoArma.Machado:
                Console.WriteLine("Não exatamente uma arma, mas uma ferramenta pensada para cortar troncos de árvore, porém, à depender da situação, pode servir");
                break;

            case TipoArma.Cajado:
                Console.WriteLine("Um catalisador de magias, onde o mago usa para conjurar feitiços e os canalizar");
                break;

            case TipoArma.SpecialWeapon:
                Console.WriteLine("Arma especial obtida em eventos");
                break;
        }
    }


    public string ConverterParaTexto()
    {
        return $"{Tipo};{Dano}\n";
    }


    public static Arma CriarAPartirDoTexto(string linha)
    {

        string[] dividir = linha.Split(';');
        string tipoTexto = dividir[0].Trim();
        string danoTexto = dividir[1].Trim();

        if (dividir.Length != 2)
            throw new ArmaInvalidaException("Formato Inválido. use TipoArma;Dano");


        if (!Enum.TryParse(dividir[0], true, out TipoArma tipo))
            throw new FormatException("Tipo de arma inválido");

        if (!int.TryParse(dividir[1], out int dano))
            throw new ArmaInvalidaException("Dano inválido. Dever ser do tipo inteiro");

        return new Arma(tipo, dano);
    }

}

class BananaWeapon : Arma
{
    public BananaWeapon() : base(TipoArma.SpecialWeapon, 9999)
    {

    }

    public override void MostrarDescricao()
    {
        Console.WriteLine("Arma especial obtida na parceria com donkey kong. Para obtê-la, é necessário falar com o macaco misterioso DD.");
    }
}

