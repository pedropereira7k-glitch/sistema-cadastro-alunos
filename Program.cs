using System;
using System.Collections.Generic;
using System.Globalization;

namespace SistemaCadastroAlunos;

class Aluno
{
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public double Nota { get; set; }

    public Aluno(string nome, int idade, double nota, string matricula)
    {
        Matricula = matricula;
        Nome = nome;
        Idade = idade;
        Nota = nota;
    }

    public void ExibirInfo()
    {
        Console.WriteLine(
            $"Matricula: {Matricula} | Nome: {Nome} | Idade: {Idade} | Nota: {Nota:F2}"
        );
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Aluno> listaAlunos = new List<Aluno>();
        bool rodarMenu = true;

        while (rodarMenu)
        {
            Console.WriteLine("\n=== SISTEMA DE CADASTRO DE ALUNOS ===");
            Console.WriteLine("1. Cadastrar aluno");
            Console.WriteLine("2. Listar alunos");
            Console.WriteLine("3. Buscar aluno");
            Console.WriteLine("4. Remover aluno");
            Console.WriteLine("5. Média geral da turma");
            Console.WriteLine("6. Maior nota");
            Console.WriteLine("7. Sair");
            Console.Write("Escolha uma opção (1-7): ");

            int opcao;
            while (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 1 || opcao > 7)
            {
                Console.Write("Opção inválida! Digite uma opção de 1 a 7: ");
            }

            switch (opcao)
            {
                case 1:
                    CadastrarAluno(listaAlunos);
                    break;
                case 2:
                    ListarAlunos(listaAlunos);
                    break;
                case 3:
                    BuscarPorNome(listaAlunos);
                    break;
                case 4:
                    RemoverAluno(listaAlunos);
                    break;
                case 5:
                    ExibirMediaGeral(listaAlunos);
                    break;
                case 6:
                    ExibirMaiorNota(listaAlunos);
                    break;
                case 7:
                    Console.WriteLine("Encerrando o sistema...");
                    rodarMenu = false;
                    break;
            }
        }
    }

    // --- MÉTODOS DO SISTEMA  ---

    // Cadastro de aluno
    static void CadastrarAluno(List<Aluno> alunos)
    {
        Console.Write("Matrícula: ");
        string matricula = Console.ReadLine() ?? "";

        while (string.IsNullOrWhiteSpace(matricula))
        {
            Console.Write("A matrícula não pode ser vazia. Tente novamente: ");
            matricula = Console.ReadLine() ?? "";
        }
        foreach (var aluno in alunos)
        {
            if (aluno.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Já existe um aluno cadastrado com essa matrícula.");
                return;
            }
        }
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? "";

        while (string.IsNullOrWhiteSpace(nome))
        {
            Console.Write("O nome não pode ser vazio. Tente novamente: ");
            nome = Console.ReadLine() ?? "";
        }
        int idade;
        Console.Write("Idade: ");
        while (!int.TryParse(Console.ReadLine(), out idade) || idade < 0 || idade > 120)
        {
            Console.Write("Idade inválida. Digite um valor numérico entre 0 e 120: ");
        }

        Console.Write("Nota (0 a 10): ");
        string entradaNota = Console.ReadLine() ?? "";

        // Aceita vírgula ou ponto usando CultureInfo
        double nota;
        while (
            !double.TryParse(entradaNota.Replace(',', '.'), CultureInfo.InvariantCulture, out nota)
            || nota < 0
            || nota > 10
        )
        {
            Console.Write("Nota inválida. Digite um valor entre 0 e 10: ");
            entradaNota = Console.ReadLine() ?? "";
        }

        alunos.Add(new Aluno(nome, idade, nota, matricula));
        Console.WriteLine("Aluno cadastrado com sucesso!");
    }

    // Listar alunos
    static void ListarAlunos(List<Aluno> alunos)
    {
        Console.WriteLine("\n--- Lista de Alunos ---");

        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        foreach (var aluno in alunos)
        {
            aluno.ExibirInfo();
        }
    }

    // Buscar aluno por nome
    static void BuscarPorNome(List<Aluno> alunos)
    {
        Console.WriteLine("\n--- Buscar Aluno por nome ---");

        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        Console.Write("Digite o nome do aluno: ");
        string nomeBusca = Console.ReadLine() ?? "";

        while (string.IsNullOrWhiteSpace(nomeBusca))
        {
            Console.Write("O nome não pode ser vazio. Tente novamente: ");
            nomeBusca = Console.ReadLine() ?? "";
        }

        bool encontrado = false;
        foreach (var aluno in alunos)
        {
            if (aluno.Nome.Equals(nomeBusca, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Aluno encontrado.");
                aluno.ExibirInfo();
                encontrado = true;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("Nenhum aluno encontrado com esse nome.");
        }
    }

    // Remover aluno
    static void RemoverAluno(List<Aluno> alunos)
    {
        Console.WriteLine("\n--- Remover Aluno ---");

        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        Console.Write("Digite o nome do aluno a remover: ");
        string alunoRemover = Console.ReadLine() ?? "";

        while (string.IsNullOrWhiteSpace(alunoRemover))
        {
            Console.Write("O nome não pode ser vazio. Tente novamente: ");
            alunoRemover = Console.ReadLine() ?? "";
        }

        Aluno? alunoEncontrado = alunos.Find(a =>
            a.Nome.Equals(alunoRemover, StringComparison.OrdinalIgnoreCase)
        );

        if (alunoEncontrado != null)
        {
            alunos.Remove(alunoEncontrado);
            Console.WriteLine($"Aluno {alunoEncontrado.Nome} removido com sucesso.");
        }
        else
        {
            Console.WriteLine("Aluno não encontrado.");
        }
    }

    // Média geral da turma
    static void ExibirMediaGeral(List<Aluno> alunos)
    {
        Console.WriteLine("\n--- Média geral da turma ---");

        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado para calcular a média.");
            return;
        }

        double somaNotas = 0;
        foreach (var aluno in alunos)
        {
            somaNotas += aluno.Nota;
        }

        double media = somaNotas / alunos.Count;
        Console.WriteLine($"Média geral de {alunos.Count} aluno(s): {media:F2}");
    }

    // Maior nota da turma
    static void ExibirMaiorNota(List<Aluno> alunos)
    {
        Console.WriteLine("\n--- Maior Nota ---");

        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }
        List<Aluno> alunosComMaiorNota = new List<Aluno>();

        double maiorNota = alunos[0].Nota;
        foreach (var aluno in alunos)
        {
            if (aluno.Nota > maiorNota)
            {
                alunosComMaiorNota.Clear();
                maiorNota = aluno.Nota;
                alunosComMaiorNota.Add(aluno);
            }
            if (aluno.Nota == maiorNota)
            {
                alunosComMaiorNota.Add(aluno);
            }
        }

        Console.WriteLine($"A maior nota cadastrada foi: {maiorNota:F2}");
        Console.WriteLine("Aluno(s) com essa nota:");

        foreach (var aluno in alunosComMaiorNota)
        {
            aluno.ExibirInfo();
        }
    }
}
