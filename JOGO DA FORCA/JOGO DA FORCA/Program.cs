using System;
using System.Collections.Generic;
using System.Linq;

class Program
{

    const int TENTATIVAS = 10;
    static readonly List<string> PALAVRAS = new List<string>()
    {
        "PROGRAMACAO",
        "COMPUTADOR",
        "SISTEMA OPERACIONAL",
        "GERENCIADOR DE DISPOSITIVOS",
        "ALGORITMO",
        "COMPILADOR",
        "INTERFACE",
        "BANCO DE DADOS",
        "REDES",
        "SEGURANCA"
    };

    static void Main()
    {
        try
        {
            bool jogar = true;
            while (jogar)
            {
                Jogar();
                jogar = PerguntarJogarNovamente();
            }
        }
        catch (Exception ex)
        {
            // Tratamento de erro inesperado simplificado
            Console.WriteLine("Ocorreu um erro: " + ex.Message);
            Console.WriteLine("Pressione Enter para sair.");
            Console.ReadLine();
        }
    }

    static void Jogar()
    {
        string palavra = SelecionarPalavra().ToUpperInvariant();
        HashSet<char> letrasTentadas = new HashSet<char>();
        int erros = 0;

        // Inicializa o estado parcial (revela espaços)
        char[] estado = palavra.Select(c => c == ' ' ? ' ' : '_').ToArray();

        while (erros < TENTATIVAS && estado.Contains('_'))
        {
            Console.Clear();
            DesenharForca(erros);
            Console.WriteLine();
            ExibirPalavraParcial(estado);
            Console.WriteLine();
            Console.WriteLine("Letras tentadas: " + (letrasTentadas.Count > 0 ? string.Join(", ", letrasTentadas) : "Nenhuma"));
            Console.WriteLine($"Tentativas restantes: {TENTATIVAS - erros}");
            Console.WriteLine();

            char letra = LerLetra();

            if (letrasTentadas.Contains(letra))
            {
                Console.WriteLine("Essa letra ja foi !");
                Console.ReadLine();
                continue;
            }

            letrasTentadas.Add(letra);


            if (palavra.Contains(letra))
            {
                for (int i = 0; i < palavra.Length; i++)
                {
                    if (palavra[i] == letra)
                    {
                        estado[i] = letra;
                    }
                }
            }
            else
            {
                erros++;
            }
        }

        bool venceu = !estado.Contains('_');

        Console.Clear();
        DesenharForca(erros);
        Console.WriteLine();
        ExibirPalavraParcial(palavra.ToCharArray());
        Console.WriteLine();

        if (venceu)
        {
            Console.WriteLine("voce ganhou");
            Console.WriteLine("A palavra era: " + palavra);
            Console.WriteLine("Erros: {erros}");
        }
        else
        {
            Console.WriteLine(" Voce perdeu.");
            Console.WriteLine("A palavra correta era: " + palavra);
        }

        Console.WriteLine("Pressione Enter para dar continuidade");
        Console.ReadLine();
    }

    static string SelecionarPalavra()
    {
        Random rnd = new Random();
        int idx = rnd.Next(PALAVRAS.Count);
        return PALAVRAS[idx];
    }

    static void ExibirPalavraParcial(char[] estado)
    {

        Console.WriteLine(string.Join(" ", estado));
    }

    static char LerLetra()
    {
        while (true)
        {
            Console.Write("Digite uma letra: ");
            string entrada = Console.ReadLine()?.Trim().ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine("Aceito apenas letras");
                continue;
            }
            if (entrada.Length != 1 || !char.IsLetter(entrada[0]))
            {
                Console.WriteLine("Digite apenas Letras !");
                continue;
            }

            return entrada[0];
        }
    }

    static bool PerguntarJogarNovamente()
    {
        while (true)
        {
            Console.Write("Deseja jogar novamente? (S/N): ");
            string resp = Console.ReadLine()?.Trim().ToUpperInvariant();

            if (resp == "S") return true;
            if (resp == "N") return false;

            Console.WriteLine("Resposta inválida. Digite 'S' ou 'N'.");
        }
    }

    static void DesenharForca(int erros)
    {

        string[] arteForca = new string[]
        {
@"  _____
 |/    
 |     
 |     
 |     
_|_",
@"  _____
 |/    |
 |     O
 |     
 |     
_|_",
@"  _____
 |/    |
 |     O
 |     |
 |     
_|_",
@"  _____
 |/    |
 |     O
 |    /|
 |     
_|_",
@"  _____
 |/    |
 |     O
 |    /|\
 |     
_|_",
@"  _____
 |/    |
 |     O
 |    /|\
 |    / 
_|_",
@"  _____
 |/    |
 |     O
 |    /|\
 |    / \
_|_"
        };

        int idx = Math.Clamp(erros, 0, TENTATIVAS);

        Console.WriteLine(arteForca[idx]);
    }
}