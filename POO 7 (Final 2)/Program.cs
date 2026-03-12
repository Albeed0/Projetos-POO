using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        var iniciarjogo = new Jogo();
        iniciarjogo.Iniciar();
    }

}

//Inicio do jogo
public class Jogo
{
    public void Iniciar()
    {
        Console.WriteLine("=== BEM-VINDO AO RPG POO ===");
        Console.WriteLine("Escolha sua classe:");
        Console.WriteLine("1 - Bárbaro");
        Console.WriteLine("2 - Mago");
        Console.WriteLine("3 - Paladino");

        int escolha = int.Parse(Console.ReadLine());
        Console.WriteLine("Por favor, também digite o nome do seu personagem");
        string nome = Console.ReadLine();
        Personagem jogador = null;

        switch (escolha)
        {
            case 1:
                jogador = new Barbaro(nome);
                break;

            case 2:
                jogador = new Mago(nome);
                break;

            case 3:
                jogador = new Paladino(nome);
                break;

            default:
                Console.WriteLine("Classe inválida!");
                return;
        }

        Personagem inimigo = new Barbaro("Inimigo Selvagem");

        Combate combate = new Combate();
        combate.Iniciar(jogador, inimigo);
    }
}

//Combate

public class Combate
{
    public void Iniciar(Personagem jogador, Personagem inimigo)
    {
        Console.WriteLine("⚔️ O combate começou!");

        while (jogador.EstaVivo() && inimigo.EstaVivo())
        {
            Console.WriteLine("\nSua vez! Escolha uma ação:");
            Console.WriteLine("1 - Atacar");
            Console.WriteLine("2 - Ataque mágico");
            Console.WriteLine("3 - Curar");

            int escolha = int.Parse(Console.ReadLine());

            switch (escolha)
            {
                case 1:
                    jogador.Acao(inimigo);
                    break;

                case 2:
                    if (jogador is Mago magico)
                        magico.AtaqueMagico();
                    else
                        Console.WriteLine("Você não pode usar magia!");
                    break;

                case 3:
                    if (jogador is Mago cura)
                        cura.Curar();
                    else
                        Console.WriteLine("Você não pode se curar!");
                    break;

                default:
                    Console.WriteLine("Ação inválida!");
                    break;
            }

            if (inimigo.EstaVivo())
            {
                Console.WriteLine("\nTurno do inimigo!");
                inimigo.Acao(jogador);
            }
        }

        if (jogador.EstaVivo())
        {
            Console.WriteLine("🏆 Você venceu!");
        }

        else
        {
            Console.WriteLine("💀 Você foi derrotado...");
        }
            
    }
    
} 
public interface IAcao
{
    void Acao(Personagem alvo);
}

//Personagens e Classes
public abstract class Personagem : IAcao
{
    public string Nome { get; private set; }
    public int DanoBase { get; private set; }
    public int DanoBaseMagico { get; private set; }
    public int DefesaBase { get; private set; }
    public int DefesaMagicaBase { get; private set; }
    public int PontosdeCura { get; private set; }
    public int Vida { get; private set; }
    public bool EstaVivo()
    {
        return Vida > 0;
    }

    public Personagem(int vida, string nome, int danoBase, int defesaBase, int defesaMagicaBase, int pontoCura = 0, int danoMagicoBase = 0)
    {
        Vida = vida;
        Nome = nome;
        DanoBase = danoBase;
        DefesaMagicaBase = defesaMagicaBase;
        DefesaBase = defesaBase;
        PontosdeCura = pontoCura;
        DanoBaseMagico = danoMagicoBase;
    }

    public void ReceberDanoFisico(int dano)
    {
       int danoFinal = dano - DefesaBase;
       if(danoFinal < 1)
        {
            danoFinal = 1;
        }
        Vida -= danoFinal;

        if(Vida < 0)
        {
            Vida = 0;
        }
        Console.WriteLine($"{Nome} recebeu {danoFinal} de dano! Vida atual: {Vida}");
    }


    public void ReceberDanoMagico(int dano)
    {
        int danoFinal = dano - DefesaMagicaBase;
        if (danoFinal < 1) danoFinal = 1;

        Vida -= danoFinal;

        if (Vida < 0)
            Vida = 0;

        Console.WriteLine($"{Nome} recebeu {danoFinal} de dano mágico! Vida atual: {Vida}");
    }

    public abstract void Acao(Personagem alvo);
    
}
public enum TipoPersonagem
{
    Barbaro,
    Mago,
    Paladino,
}
public interface IAtacante
{
    void Atacar(Personagem alvo);
}
public interface IMagico
{
    void AtaqueMagico();
}
public interface IDefender
{
    void Defender();
}
public interface ICurandeiro
{
    void Curar();
}
public interface IDefMagia
{
    void DefenderMagia();
}

class Barbaro : Personagem, IAtacante, IDefender
{
    public Barbaro(string nomeBar) : base(190, nomeBar, 67, 40, 0)
    {

    }
    public override void Acao(Personagem alvo)
    {
    Atacar(alvo);
    }

    public void Atacar(Personagem alvo)
    {
    Console.WriteLine($"O bárbaro {Nome} atacou!");
    alvo.ReceberDanoFisico(DanoBase);
    }
    public void Defender()
    {
        Console.WriteLine($"O bárbaro {Nome} defendeu!");
        int defesaBB = DefesaBase + 5;
    }

    public void FuriaBarbara()
    {
        Console.WriteLine($"O bárbaro {Nome} usou a fúria bárbara, e seu dano triplicou! ");
        int danoF = DanoBase * 3;

    }
}

class Mago : Personagem, IMagico, IDefMagia, ICurandeiro
{
    public Mago(string nomeM) : base(90, nomeM, 20, 50, 100, 15, 70)
    {

    }

    public override void Acao(Personagem alvo)
    {
        Console.WriteLine($"{Nome} lançou magia!");
        alvo.ReceberDanoMagico(DanoBaseMagico);
    }
    public void AtaqueMagico()
    {

        Console.WriteLine($"O mago {Nome} atacou com magia de {DanoBaseMagico} pontos de dano!");
    }
    public void DefenderMagia()
    {
        Console.WriteLine($"O mago defendeu a magia!");
    }
    public void Curar()
    {

        Console.WriteLine($"O mago curou {PontosdeCura} pontos de vida!");
        if(Vida > PontosdeCura)
        {

        }
    }
}

class Paladino : Personagem, IAtacante, IDefender, IMagico, ICurandeiro
{
    public Paladino(string nomeP) : base(120, nomeP, 55, 40, 30, 7, 30)
    {

    }
      public override void Acao(Personagem alvo)
    {

    }

    public void Atacar(Personagem alvo)
    {

    }
    public void Defender()
    {

    }
    public void Curar()
    {

    }
    public void AtaqueMagico()
    {

    }
}

//Armas

enum Categorias
{
    Espada,
    Arco,
    Alabarda,
    Lança,
    Cajado,
    Catalisador,
    Especial,
}

class Armas
{
    public Categorias ClasseArma { get; private set; }
    public int DanoArma { get; private set; }
    public Armas(Categorias classe, int danoArma)
    {
        ClasseArma = classe;
        DanoArma = danoArma;
    }

    public void InfoArma()
    {
        Console.WriteLine($"É uma arma da categoria {ClasseArma} com o dano de {DanoArma}");
    }

}


class KeyBlade : Armas
{
    public KeyBlade() : base(Categorias.Especial, 9999)
    {

    }
}


