using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new();
        menu.ExibirMenu();
    }
}


public class Menu
{
    Logica logica = new();
    Usuario? usuarioLogado;
    public void ExibirMenu()
    {
        Console.WriteLine("Bem-vindo à Biblioteca! Por favor, coloque seu usuário e email para acessar o sistema.");
        string nome = Console.ReadLine() ?? "";
        string email = Console.ReadLine() ?? "";
        var usuario = logica.Login(nome, email);


        if(usuario is Admin)
        {
            usuarioLogado = usuario;
            OpcoesAdmin();
        }
        else if(usuario is UsuarioComum)
        {
            usuarioLogado = usuario;
            OpcoesNormais();
        }
        else if(usuario == null)
        {
            Console.WriteLine("Usuário não encontrado. Deseja se cadastrar? (s/n)");
            string resposta = Console.ReadLine() ?? "n";
            if(resposta.ToLower() == "s")
            {
                Console.WriteLine("Digite seu nome:");
                string nomeCadastro = Console.ReadLine() ?? "";
                Console.WriteLine("Digite seu email:");
                string emailCadastro = Console.ReadLine() ?? "";
                var novoUsuario = new UsuarioComum(nomeCadastro, emailCadastro);
                logica.CadastrarUsuario(novoUsuario);
                Console.WriteLine("Cadastro realizado com sucesso! Agora você pode fazer login e usar o sistema da biblioteca.");
                usuarioLogado = novoUsuario;
                OpcoesNormais();
            }
            else
            {
                Console.WriteLine("Encerrando o sistema...");
            }
        }
         else
        {  
            Console.WriteLine("Tipo de usuário desconhecido. Encerrando o sistema...");
        }
    }


    public void OpcoesAdmin()


    {
        bool rodar = true;


        while (rodar)
        {
            Console.Clear();
            Console.WriteLine($"Bem-vindo, Admin! O que você gostaria de fazer?");
            Console.WriteLine("1. Adicionar Livros");
            Console.WriteLine("2. Mostrar Livros");
            Console.WriteLine("3. Atualizar Livros");
            Console.WriteLine("4. Deletar Livros");
            Console.WriteLine("5. Sair");
            int options = int.Parse(Console.ReadLine() ?? "0");

            switch (options)
            {
                case 1:
                    Console.Clear();
                        Console.WriteLine("Digite o título do livro físico:");
                        string titulo = Console.ReadLine();
                        Console.WriteLine("Digite o autor do livro físico:");
                        string autor = Console.ReadLine();
                        Console.WriteLine("Digite o ano de publicação do livro físico:");
                        int anoPublicacao = int.Parse(Console.ReadLine());
                    Console.WriteLine("Escolha o tipo de livro que deseja adicionar:");

                    Console.WriteLine("1. Livro Físico");
                    Console.WriteLine("2. Livro Digital");
                    int tipoLivro = int.Parse(Console.ReadLine());

                    if (tipoLivro == 1){
                        Console.WriteLine("Digite o número de páginas do livro físico:");
                        int numeroPaginas = int.Parse(Console.ReadLine());
                        logica.AdicionarLivro(new LivroFisico(titulo ?? "", autor ?? "", anoPublicacao, numeroPaginas));
            
                    }
                    else if (tipoLivro == 2){
                        Console.WriteLine("Digite o tamanho do arquivo em MB do livro digital:");
                        double tamanhoArquivoMB = double.Parse(Console.ReadLine());
                    logica.AdicionarLivro(new LivroDigital(titulo ?? "", autor ?? "", anoPublicacao, tamanhoArquivoMB));
                    }
                    else{
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Listando Livros...");
                    var livros = logica.MostrarLivros();

                    if (livros.Count == 0)
                    {
                        Console.WriteLine("Nenhum livro encontrado.");
                    }
                    else
                    {
                        foreach (var livro in livros)
                        {
                            livro.ExibirInformacoes();
                            Console.WriteLine("-----------------------------");
                        }
                            Console.WriteLine("Pressione Enter para continuar...");
                            Console.ReadLine();
                    }
                    break;
                case 3:
                string novoTitulo, novoAutor;
                int novoAnoPublicacao;
                int novoNumeroPaginas;
                double novoTamanhoArquivoMB;
                    Console.WriteLine("Atualizando Livro...");
                    Console.WriteLine("Digite o título do livro que deseja atualizar:");
                    string tituloAntigo = Console.ReadLine();
                    var livroAntigo = logica.MostrarLivros().Find(l => l.Titulo == tituloAntigo);
                    if(livroAntigo is LivroFisico)
                    {
                        Console.WriteLine("O livro encontrado é um Livro Físico.");
                        Console.WriteLine("Digite o novo título do livro:");
                        novoTitulo = Console.ReadLine();
                        Console.WriteLine("Digite o novo autor do livro:");
                        novoAutor = Console.ReadLine();
                        Console.WriteLine("Digite o novo ano de publicação do livro:");
                        novoAnoPublicacao = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o novo número de páginas do livro:");
                        novoNumeroPaginas = int.Parse(Console.ReadLine());
                        var livroNovo = new LivroFisico(novoTitulo ?? "", novoAutor ?? "", novoAnoPublicacao, novoNumeroPaginas);
                        logica.AtualizarLivro(livroAntigo, livroNovo);
                    }
                    else if(livroAntigo is LivroDigital)
                    {
                        Console.WriteLine("O livro encontrado é um Livro Digital.");
                        Console.WriteLine("Digite o novo título do livro:");
                        novoTitulo = Console.ReadLine();
                        Console.WriteLine("Digite o novo autor do livro:");
                        novoAutor = Console.ReadLine();
                        Console.WriteLine("Digite o novo ano de publicação do livro:");
                        novoAnoPublicacao = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o novo tamanho do arquivo em MB do livro digital:");
                        novoTamanhoArquivoMB = double.Parse(Console.ReadLine());

                        var livroNovo = new LivroDigital(novoTitulo ?? "", novoAutor ?? "", novoAnoPublicacao, novoTamanhoArquivoMB);
                        logica.AtualizarLivro(livroAntigo, livroNovo);
                    }
                    else
                    {
                        Console.WriteLine("Livro não encontrado.");
                    }

                    Console.Clear();
                    break;
                case 4:
                    Console.WriteLine("Deletando Livro...");
                    Console.WriteLine("Digite o título do livro que deseja deletar:");
                    string tituloDeletar = Console.ReadLine();
                    var livroParaDeletar = logica.MostrarLivros().Find(l => l.Titulo == tituloDeletar);
                    if (livroParaDeletar == null)
                    {
                        Console.WriteLine("Livro não encontrado.");
                    }
                    else
                    {
                        logica.RemoverLivro(livroParaDeletar);
                        Console.WriteLine("Livro deletado com sucesso.");
                    }
                    Console.Clear();
                    break;
                    case 5:
                    Console.WriteLine("Saindo do sistema...");
                    rodar = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                    break;
            }

        }
    }

public void OpcoesNormais()
    {
        bool rodar = true;

        while (rodar)
        {
            Console.Clear();
            Console.WriteLine($"Bem-vindo, Usuário! O que você gostaria de fazer?");
            Console.WriteLine("1. Mostrar Livros");
            Console.WriteLine("2. Emprestar Livro");
            Console.WriteLine("3. Devolver Livro");
            Console.WriteLine("4. Sair");
            int options = int.Parse(Console.ReadLine() ?? "0");

            switch (options)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Listando Livros...");
                    var livros = logica.MostrarLivros();

                    if (livros.Count == 0)
                    {
                        Console.WriteLine("Nenhum livro encontrado.");
                    }
                    else
                    {
                        foreach (var livro in livros)
                        {
                            livro.ExibirInformacoes();
                            Console.WriteLine("-----------------------------");
                        }
                            Console.WriteLine("Pressione Enter para continuar...");
                            Console.ReadLine();
                    }
                    break;
                case 2:
                foreach (var livro in logica.MostrarLivros())
                    {
                        livro.ExibirInformacoes();
                        Console.WriteLine("-----------------------------");
                    }
                    Console.WriteLine("Digite o título do livro que deseja pegar emprestado:");
                    string tituloEmprestar = Console.ReadLine() ?? "";
                    logica.EmprestarLivrio(tituloEmprestar, usuarioLogado??new UsuarioComum("Usuário Desconecido", "desconhecido@exemplo.com"));
                    break;
                case 3:
                Console.WriteLine("Digite o título do livro que deseja devolver:");
                    string tituloDevolver = Console.ReadLine() ?? "";
                    logica.DevolverLivro(tituloDevolver);
                    break;
                case 4:
                    Console.WriteLine("Saindo do sistema...");
                    rodar = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                    break;
            }

        }
    }

}


public class Logica
{
    private List<Usuario> _usuarios = new();
    private List<Livro> _livros = new();
    private List<Emprestimo> _emprestimos = new();

    public Logica()
    {
        var livrosCarregados = SalvarLivros.Carregar();
        _livros = livrosCarregados;
        Console.WriteLine(_livros.Count + " livros carregados com sucesso.");
        var usuariosCarregados = GuardarUsuarios.Desserializar();
        _usuarios = usuariosCarregados;
        Console.WriteLine(_usuarios.Count + " usuários carregados com sucesso.");
    }


    public void AdicionarLivro(Livro livro)
    {
        _livros.Add(livro);
        SalvarLivros.Salvar(_livros);
    }

    public List<Livro> MostrarLivros()
    {
       return _livros;
    }

    public void RemoverLivro(Livro livro)
    {
        _livros.Remove(livro);
        SalvarLivros.Salvar(_livros);
    }

    public void AtualizarLivro(Livro livroAntigo, Livro livroNovo)
    {
        int index = _livros.IndexOf(livroAntigo);
        if (index != -1)
        {
            _livros[index] = livroNovo;
            SalvarLivros.Salvar(_livros);
        }
    }

    public void EmprestarLivrio(string tituloEmprestar, Usuario usuario)
    {
        var livroParaEmprestar = _livros.Find(l => l.Titulo == tituloEmprestar);
        if (livroParaEmprestar == null)
        {
            Console.WriteLine("Livro não encontrado.");
        }
        else if (livroParaEmprestar is IEmprestavel emprestavel)
        {
            emprestavel.Emprestar();
            _emprestimos.Add(new Emprestimo(usuario, livroParaEmprestar));
        }
        else
        {
            Console.WriteLine("Este livro não é emprestável.");
        }
    }

    public void DevolverLivro(string tituloDevolver)
    {
        var livroParaDevolver = _livros.Find(l => l.Titulo == tituloDevolver);
        if (livroParaDevolver == null)
        {
            Console.WriteLine("Livro não encontrado.");
        }
        else if (livroParaDevolver is IEmprestavel emprestavel)
        {
            emprestavel.Devolver();
        }
        else
        {
            Console.WriteLine("Este livro não é emprestável.");
        }
    }

    public void CadastrarUsuario(Usuario usuario)
    {
        _usuarios.Add(usuario);
        GuardarUsuarios.Salvar(_usuarios);
    }
    public Usuario? Login(string nome, string email)
    {
        var usuarioEncontrado = _usuarios.Find(u => u.Nome == nome && u.Email == email);
        if (usuarioEncontrado != null)
        {
            Console.WriteLine($"Bem-vindo, {usuarioEncontrado.Nome}!");
            usuarioEncontrado.ExibirInformacoes();
        }
        else
        {
            Console.WriteLine("Usuário não encontrado. Por favor, verifique suas credenciais.");
        }
        return usuarioEncontrado;
    }

     public void AdicionarEmprestimo(Emprestimo emprestimo)
    {
        _emprestimos.Add(emprestimo);
    }
}



public interface IEmprestavel
{
    void Emprestar();
    void Devolver();
}

public class Emprestimo
{
    public Usuario Usuario { get; private set; }
    public Livro Livro { get; private set; }
    public DateTime DataEmprestimo { get; private set; }
    public DateTime? DataDevolucao { get; private set; }
    public Emprestimo(Usuario usuario, Livro livro)
    {
        Usuario = usuario;
        Livro = livro;
        DataEmprestimo = DateTime.Now;
    }

    public void ExibirInformacoes()
    {
        Console.WriteLine($"Empréstimo: {Usuario.Nome} pegou emprestado '{Livro.Titulo}' em {DataEmprestimo} e deverá devolver em um mês ({DataEmprestimo.AddDays(30)}).");
    }

}


public class GuardarUsuarios
{

    public List<Usuario> Usuarios { get; set; } = new();
    private static readonly string filepath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/usuariosComuns.json";
    public static void Salvar(List<Usuario> dados)
    {
        var json = JsonSerializer.Serialize(dados);
        Console.WriteLine($"{json} foi salvo com sucesso.");
        File.WriteAllText(filepath, json);
    }

    public static List<Usuario> Desserializar()
    {
        if(!File.Exists(filepath))
        {
            Console.WriteLine($"O arquivo {filepath} não existe. Criando um novo arquivo.");
            File.WriteAllText(filepath, "[]");
        }

        var usuariosDesserializados = SerializarUsuarios.Desserializar(filepath);
        Console.WriteLine($"Os dados foram carregados de {filepath}");
        return usuariosDesserializados;
    }
}

public class SerializarUsuarios
{
    public List<Usuario> Usuarios { get; set; } = new();
    public static string Serializar(List<Usuario> dados)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        var json = JsonSerializer.Serialize(dados, options);
        Console.WriteLine($"{json} foi salvo com sucesso.");
        return json;
    }

    public static List<Usuario> Desserializar(string filepath)
        {
        string json = File.ReadAllText(filepath);
        var usuariosDesserializados = JsonSerializer.Deserialize<List<Usuario>>(json);
        return usuariosDesserializados ?? new List<Usuario>();
        }
    
}

[JsonDerivedType(typeof(Admin), typeDiscriminator: "Admin")]
[JsonDerivedType(typeof(UsuarioComum), typeDiscriminator: "UsuarioComum")]
public abstract class Usuario
{
    public string Nome { get; private set; }
    public string Email { get; private set; }

    public Usuario(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public abstract void ExibirInformacoes();
}
class Admin : Usuario

{
    public Admin(string nome, string email) : base(nome, email) { }

    public override void ExibirInformacoes()
    {
        Console.WriteLine($"Admin: {Nome}, Email: {Email}");
    }
}

class UsuarioComum : Usuario
{
    private static int _idCounter = 1;
    public int ID { get; private set; }
    public UsuarioComum(string nome, string email) : base(nome, email)
    {
        ID = _idCounter++;
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine($"Usuário Comum: {Nome}, Email: {Email}, ID: {ID}");
    }
}


public class SalvarLivros
{

    private static readonly string filepath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/livros.json";
    public static void Salvar(List<Livro> dados)
    {
        string json = SerializarLivros.Serializar(dados);
        File.WriteAllText(filepath, json);
        Console.WriteLine($"Os dados foram salvos em {filepath}");
    }

    public static List<Livro> Carregar()
    {
        if(!File.Exists(filepath))
        {
            Console.WriteLine($"O arquivo {filepath} não existe. Criando um novo arquivo.");
            File.WriteAllText(filepath, "[]");
        }

        var livrosDesserializados = SerializarLivros.Desserializar(filepath);
        Console.WriteLine($"Os dados foram carregados de {filepath}");
        return livrosDesserializados;
    }
}


public class SerializarLivros
{
    public List<Livro> Livros { get; set; } = new();
    public static string Serializar(List<Livro> dados)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        var json = JsonSerializer.Serialize(dados, options);
        Console.WriteLine($"{json} foi salvo com sucesso.");
        return json;
    }

    public static List<Livro> Desserializar(string filepath)
        {
        string json = File.ReadAllText(filepath);
        var livrosDesserializados = JsonSerializer.Deserialize<List<Livro>>(json);
        return livrosDesserializados ?? new List<Livro>();
        }
    
}


[JsonDerivedType(typeof(LivroFisico), typeDiscriminator: "LivroFisico")]
[JsonDerivedType(typeof(LivroDigital), typeDiscriminator: "LivroDigital")]
public abstract class Livro
{
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public int AnoPublicacao { get; private set; }



    public Livro(string titulo, string autor, int anoPublicacao)
    {
        Titulo = titulo;
        Autor = autor;
        AnoPublicacao = anoPublicacao;
    }
    public abstract void ExibirInformacoes();
}

public enum Estado
{
    Disponivel,
    Emprestado,
    Reservado,
    
}

class LivroFisico : Livro, IEmprestavel
{
    public int NumeroPaginas { get; private set; }
    public Estado Estado { get; set; } = Estado.Disponivel;
    public LivroFisico(string titulo, string autor, int anoPublicacao, int numeroPaginas)
        : base(titulo, autor, anoPublicacao)
    {
        NumeroPaginas = numeroPaginas;
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine($"Livro Físico: {Titulo} por {Autor}, publicado em {AnoPublicacao}, com {NumeroPaginas} páginas.");
    }

    public void Emprestar()
    {
      if(Estado == Estado.Disponivel)
        {
            Estado = Estado.Emprestado;
            Console.WriteLine($"O livro '{Titulo}' foi emprestado.");
        }
        else if(Estado == Estado.Reservado)
        {
            Console.WriteLine($"O livro '{Titulo}' está reservado e não pode ser emprestado.");
        }
        else
        {
            Console.WriteLine($"O livro '{Titulo}' não está disponível para empréstimo.");
        }
    }

    public void Devolver()
    {
        if(Estado == Estado.Emprestado)
        {
             Estado = Estado.Disponivel;
            Console.WriteLine($"O livro '{Titulo}' foi devolvido.");
        }
        else
        {
            Console.WriteLine($"O livro '{Titulo}' não está emprestado.");
        }
    }
    
}

class LivroDigital : Livro

{
    public double TamanhoArquivoMB { get; private set; }

    public LivroDigital(string titulo, string autor, int anoPublicacao, double tamanhoArquivoMB)
        : base(titulo, autor, anoPublicacao)
    {
        TamanhoArquivoMB = tamanhoArquivoMB;
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine($"Livro Digital: {Titulo} por {Autor}, publicado em {AnoPublicacao}, com tamanho de {TamanhoArquivoMB} MB.");
    }
}

