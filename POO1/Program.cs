// See https://aka.ms/new-console-template for more information

Livro livro1 = new Livro();
Livro livro2 = new Livro();

livro1.Titulo = "A Metamotfose";
livro1.Autor = "Franz Kafka";
livro1.PagNumber = 70;

livro2.Autor = "Fiodor Dostoyevski";
livro2.Titulo = "Noites Brancas";
livro2.PagNumber = 60;

livro1.ExibirInfo();
livro2.ExibirInfo();


class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int PagNumber { get; set; }

    public void ExibirInfo()
    {
        Console.WriteLine($"O livro: {Titulo} é do autor: {Autor} e contém: {PagNumber} páginas");
    }
}
