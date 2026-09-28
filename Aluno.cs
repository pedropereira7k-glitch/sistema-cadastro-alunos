using System;

namespace SistemaCadastroAlunos;

public class Aluno
{
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public double Nota { get; set; }
    public int Idade { get; set; }

    public Aluno(string matricula, string nome, double nota, int idade)
    {
        Matricula = matricula;
        Nome = nome;
        Nota = nota;
        Idade = idade;
    }

    public void ExibirInfo()
    {
        Console.WriteLine(
            $"Matrícula: {Matricula} | Nome: {Nome} | Idade: {Idade} | Nota: {Nota:F2}"
        );
    }
}
