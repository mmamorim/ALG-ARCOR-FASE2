# Bloco 1 – Preparação e tela de abertura

[← Voltar ao início](../README.md)

## 1. Criar o projeto e instalar o Spectre.Console

No terminal, dentro da pasta onde o projeto vai ficar:

~~~bash
dotnet new console -n QuizArcor
~~~
~~~bash
cd QuizArcor
~~~
~~~bash
dotnet add package Spectre.Console
~~~

O terceiro comando baixa a biblioteca da internet. Se ele falhar, avisem o professor antes de continuar.

## 2. Código inicial

Apaguem todo o conteúdo do `Program.cs` criado pelo `dotnet new` e coloquem no lugar o conteúdo do arquivo [codigo-inicial/Program.cs](../codigo-inicial/Program.cs), que é este:

~~~csharp
using Spectre.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// ===================== BANCO DE QUESTÕES =====================
string[] faixasEtarias = {
    "Até 12 anos", "13 a 17 anos", "18 a 24 anos",
    "25 a 39 anos", "40 anos ou mais", "Prefiro não informar"
};

// ===================== INÍCIO DO JOGO =====================
MostrarAbertura();
string nickname = LerNickname();
string faixa = LerFaixaEtaria(faixasEtarias);

// ===================== FUNÇÕES =====================

static void MostrarAbertura()
{
    Console.Clear();
    AnsiConsole.Write(new FigletText("Desafio das Marcas").Color(Color.Green));
    AnsiConsole.MarkupLine("[yellow]Bem-vindo ao quiz da Arcor![/]");
    AnsiConsole.MarkupLine("Use as setas para escolher a resposta e tecle Enter.");
    AnsiConsole.MarkupLine("Cada dica usada custa 5 pontos. Escolha TERMINAR para sair a qualquer momento.");
    Console.WriteLine();
}

static string LerNickname()
{
    return AnsiConsole.Ask<string>("Digite seu [green]nickname[/]:");
}

static string LerFaixaEtaria(string[] faixas)
{
    SelectionPrompt<string> prompt = new SelectionPrompt<string>();
    prompt.Title("Escolha sua [green]faixa etária[/]:");
    prompt.AddChoices(faixas);
    return AnsiConsole.Prompt(prompt);
}
~~~

## 3. O que este código faz

- `Console.OutputEncoding = ...UTF8` faz os acentos aparecerem corretamente no terminal do Windows.
- `FigletText` desenha o título em letras grandes.
- `AnsiConsole.MarkupLine` escreve texto com formatação. O trecho entre `[yellow]` e `[/]` sai em amarelo. Por isso os colchetes não podem aparecer no texto das questões.
- `AnsiConsole.Ask<string>` pede um texto e **não aceita resposta vazia**: ele repete a pergunta sozinho. Na Entrega 1, essa validação foi feita à mão.
- `SelectionPrompt` mostra uma lista que o jogador percorre com as setas. A função devolve o texto da opção escolhida. Como o jogador só consegue escolher uma opção da lista, não há como digitar uma faixa inválida.
- Os dados do jogo (aqui, o vetor `faixasEtarias`) ficam na seção de dados e são **passados como parâmetro** para a função.

## 4. Checkpoint do Bloco 1

Executem com `dotnet run`. O programa deve mostrar o título, pedir o nickname (tentem deixar vazio para ver o que acontece) e mostrar a lista de faixas etárias. Depois da escolha, o programa termina: é o esperado por enquanto.

---

[Próximo: Bloco 2 – Banco de questões →](bloco2-banco-de-questoes.md)
