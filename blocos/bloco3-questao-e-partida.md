# Bloco 3 – Função da questão e laço da partida

[← Bloco 2](bloco2-banco-de-questoes.md) · [Início](../README.md)

Antes de começar, **apaguem o trecho provisório** do checkpoint do Bloco 2.

## 1. Função `MostrarQuestao` (lacunas 2 e 3)

Esta função mostra uma questão e devolve um número com o resultado: **1** se o jogador acertou, **0** se errou e **-1** se escolheu TERMINAR. Colem na seção **FUNÇÕES**:

~~~csharp
static int MostrarQuestao(string enunciado, string[] opcoes, int correta, string dica, bool[] usouDica, int indice)
{
    SelectionPrompt<string> prompt = new SelectionPrompt<string>();
    prompt.Title(Markup.Escape(enunciado));
    prompt.AddChoices(opcoes);
    prompt.AddChoices("MOSTRAR DICA", "TERMINAR");

    string escolha = AnsiConsole.Prompt(prompt);

    while (/* LACUNA 2: repetir enquanto a escolha for "MOSTRAR DICA" */)
    {
        usouDica[indice] = true;
        AnsiConsole.Status().Start("Buscando a dica...", ctx =>
        {
            Thread.Sleep(1500);
        });
        AnsiConsole.MarkupLine("[yellow]Dica:[/] " + Markup.Escape(dica));
        escolha = AnsiConsole.Prompt(prompt);
    }

    if (escolha == "TERMINAR")
    {
        return -1;
    }
    if (/* LACUNA 3: a escolha é igual à opção da posição "correta" do vetor "opcoes" */)
    {
        return 1;
    }
    return 0;
}
~~~

Pontos importantes:

- **O `while` da dica:** quando o jogador pede a dica, a função mostra uma animação curta, escreve a dica e pergunta de novo. Isso se repete até ele escolher outra opção.
- **O vetor `usouDica`:** a função precisa avisar duas coisas, o resultado e se a dica foi usada, mas só pode devolver um valor. A solução é receber o vetor `usouDica` e marcar a posição da questão. **Um vetor passado para uma função é o mesmo vetor de fora**, então a marcação feita aqui dentro continua valendo depois que a função termina.
- `Markup.Escape` protege o texto caso ele tenha algum colchete.

## 2. Função `IndiceNivel` (lacuna 4)

Os contadores de desempenho serão vetores de 3 posições: 0 para fácil, 1 para médio e 2 para difícil. Esta função converte a letra do nível na posição. Colem na seção **FUNÇÕES**:

~~~csharp
static int IndiceNivel(string nivel)
{
    if (nivel == "F")
    {
        return 0;
    }
    else if (/* LACUNA 4: o nível é "M" */)
    {
        return 1;
    }
    return 2;
}
~~~

## 3. Laço da partida (lacuna 5)

Colem na seção **INÍCIO DO JOGO**, logo abaixo da linha `string faixa = ...`:

~~~csharp
int totalQuestoes = enunciados.Length;
int[] questoesPorNivel = new int[3];
int[] acertosPorNivel = new int[3];
bool[] usouDica = new bool[totalQuestoes];
bool terminou = false;

for (int i = 0; i < totalQuestoes; i++)
{
    AnsiConsole.Write(new Rule($"Questão {i + 1} de {totalQuestoes}"));

    string[] opcoes = ExtrairAlternativas(alternativas, i);
    int resultado = MostrarQuestao(enunciados[i], opcoes, corretas[i], dicas[i], usouDica, i);

    if (resultado == -1)
    {
        terminou = true;
        break;
    }

    int nivel = IndiceNivel(niveis[i]);
    questoesPorNivel[nivel]++;

    if (resultado == 1)
    {
        acertosPorNivel[nivel] = /* LACUNA 5: o valor atual do contador mais 1 */;
        AnsiConsole.MarkupLine("[green]Você acertou![/]");
    }
    else
    {
        AnsiConsole.MarkupLine("[red]Você errou.[/] Resposta certa: " + Markup.Escape(opcoes[corretas[i]]));
    }
}

if (terminou)
{
    AnsiConsole.MarkupLine("[yellow]Quiz encerrado pelo jogador. Até a próxima![/]");
}
else
{
    // PROVISÓRIO: será substituído no Bloco 5
    Console.WriteLine($"Fácil {acertosPorNivel[0]}/{questoesPorNivel[0]} | Médio {acertosPorNivel[1]}/{questoesPorNivel[1]} | Difícil {acertosPorNivel[2]}/{questoesPorNivel[2]}");
}
~~~

Pontos importantes:

- `enunciados.Length` faz o laço se ajustar sozinho à quantidade de questões. Para incluir questões novas, basta mexer nos vetores, sem alterar o laço.
- `Rule` desenha uma linha horizontal com o progresso no meio.
- `break` interrompe o `for` imediatamente quando o jogador escolhe TERMINAR. Nesse caso, não há relatório.

## 4. Checkpoint do Bloco 3

Executem com `dotnet run` e testem três partidas:

1. Joguem até o fim e confiram se a contagem provisória bate com o que vocês acertaram.
2. Peçam uma dica em alguma questão e vejam se ela aparece antes de a pergunta se repetir.
3. Escolham TERMINAR no meio e confiram se aparece só a mensagem de encerramento.

Se o programa parar com o erro `IndexOutOfRangeException`, algum vetor do banco de questões tem menos itens que os outros.

---

[Próximo: Bloco 4 – Dicas e cronômetro →](bloco4-dicas-e-cronometro.md)
