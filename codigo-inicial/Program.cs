using System.Diagnostics;
using Spectre.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// ===================== BANCO DE QUESTÕES =====================
string[] faixasEtarias = {
    "Até 12 anos", "13 a 17 anos", "18 a 24 anos",
    "25 a 39 anos", "40 anos ou mais", "Prefiro não informar"
};

string[] enunciados = {
    "Qual é o formato do chocolate Tortuguita?",
    "A marca Triunfo, da Arcor, é conhecida por qual tipo de produto?",
    "Em qual país a Arcor foi fundada?"
};

string[] niveis = { "F", "M", "D" };

string[] dicas = {
    "O nome do chocolate já diz muito sobre ele.",
    "Combina com um café da tarde.",
    "A sede da empresa fica na província de Córdoba."
};

string[,] alternativas = {
    { "Coelho", "Estrela", "Tartaruga", "Peixe" },
    { "Biscoitos", "Balas", "Sorvetes", "Refrigerantes" },
    { "Brasil", "Chile", "Uruguai", "Argentina" }
};

int[] corretas = { 2, 0, 3 };

// ===================== INÍCIO DO JOGO =====================
MostrarAbertura();
string nickname = LerNickname();
string faixa = LerFaixaEtaria(faixasEtarias);

// PROVISÓRIO: apagar no início do Bloco 3
//for (int i = 0; i < enunciados.Length; i++)
//{
//    string[] opcoes = ExtrairAlternativas(alternativas, i);
//    string nivel = niveis[i];
//    string pergunta = Markup.Escape(enunciados[i]);
//    string respCorreta = Markup.Escape(opcoes[corretas[i]]);
//    AnsiConsole.MarkupLine($"[green]{nivel}[/] {pergunta} -> {respCorreta}");
//}

int totalQuestoes = enunciados.Length;
int[] questoesPorNivel = new int[3];
int[] acertosPorNivel = new int[3];
bool[] usouDica = new bool[totalQuestoes];
bool terminou = false;

Stopwatch cronometro = Stopwatch.StartNew();

for (int i = 0; i < totalQuestoes; i++)
{
    int segundos = (int)cronometro.Elapsed.TotalSeconds;
    int minutos = segundos / 60;
    int segs = segundos % 60;
    AnsiConsole.Write(new Rule($"Questão {i + 1} de {totalQuestoes} | Tempo: {minutos} min {segs} s"));

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
        acertosPorNivel[nivel]++;
        AnsiConsole.MarkupLine("[green]Você acertou![/]");
    }
    else
    {
        AnsiConsole.MarkupLine("[red]Você errou.[/] Resposta certa: " + Markup.Escape(opcoes[corretas[i]]));
    }
}

cronometro.Stop();

if (terminou)
{
    AnsiConsole.MarkupLine("[yellow]Quiz encerrado pelo jogador. Até a próxima![/]");
}
else
{
    int tempo = (int)cronometro.Elapsed.TotalSeconds;
    int dicasUsadas = ContarDicas(usouDica);
    MostrarRelatorio(nickname, faixa, questoesPorNivel, acertosPorNivel, dicasUsadas, tempo);
}

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

static string[] ExtrairAlternativas(string[,] alternativas, int linha)
{
    int quantidade = alternativas.GetLength(1);
    string[] opcoes = new string[quantidade];
    for (int j = 0; j < quantidade; j++)
    {
        opcoes[j] = alternativas[linha,j];
    }
    return opcoes;
}

static int MostrarQuestao(string enunciado, string[] opcoes, int correta, string dica, bool[] usouDica, int indice)
{
    SelectionPrompt<string> prompt = new SelectionPrompt<string>();
    prompt.Title(Markup.Escape(enunciado));
    prompt.AddChoices(opcoes);
    prompt.AddChoices("MOSTRAR DICA", "TERMINAR");

    string escolha = AnsiConsole.Prompt(prompt);

    while (escolha == "MOSTRAR DICA")
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
    if (escolha == opcoes[correta])
    {
        return 1;
    }
    return 0;
}

static int IndiceNivel(string nivel)
{
    if (nivel == "F")
    {
        return 0;
    }
    else if (nivel == "M")
    {
        return 1;
    }
    return 2;
}

static int ContarDicas(bool[] usouDica)
{
    int total = 0;
    for (int i = 0; i < usouDica.Length; i++)
    {
        if (usouDica[i])
        {
            total++;
        }
    }
    return total;
}

static double Percentual(int acertos, int questoes)
{
    return acertos * 100.0 / questoes;
}

static int CalcularPontuacao(int[] acertosPorNivel, int dicas)
{
    int[] pesos = { 10, 20, 30 };
    int pontos = 0;
    for (int n = 0; n < 3; n++)
    {
        pontos = pontos + acertosPorNivel[n] * pesos[n];
    }
    pontos = pontos - dicas * 5;
    if (pontos < 0)
    {
        pontos = 0;
    }
    return pontos;
}

static int CalcularPontuacaoMaxima(int[] questoesPorNivel)
{
    int[] pesos = { 10, 20, 30 };
    int maximo = 0;
    for (int n = 0; n < 3; n++)
    {
        maximo = maximo + questoesPorNivel[n] * pesos[n];
    }
    return maximo;
}

static string Classificar(double percentual)
{
    if (percentual >= 90)
    {
        return "Mestre das Marcas";
    }
    else if (percentual >= 70)
    {
        return "Conhecedor de Marcas";
    }
    else if (percentual >= 50)
    {
        return "Aprendiz";
    }
    return "Iniciante";
}

static string GerarBarra(int acertos)
{
    string barra = "";
    for (int i = 0; i < acertos; i++)
    {
        barra = barra + "*";
    }
    return barra;
}

static string DefinirRitmo(double mediaPorQuestao)
{
    // DESAFIO A (opcional): devolva "Rápido" (até 10 s), "Normal" (até 20 s)
    // ou "Pausado" (acima de 20 s), com as mesmas regras da Entrega 1.
    return "(não calculado)";
}

static string MelhorNivel(int[] questoesPorNivel, int[] acertosPorNivel, string[] nomesNiveis)
{
    // DESAFIO B (opcional): percorra os 3 níveis com um for, ignore os níveis
    // não jogados e devolva o nome do nível de maior percentual.
    // Em caso de empate, vence o nível mais difícil.
    // Dica: use a função Percentual e o vetor nomesNiveis.
    return "(não calculado)";
}

static void MostrarRelatorio(string nickname, string faixa, int[] questoesPorNivel, int[] acertosPorNivel, int dicas, int tempo)
{
    string[] nomesNiveis = { "Fácil", "Médio", "Difícil" };

    int totalQuestoes = 0;
    int totalAcertos = 0;
    for (int n = 0; n < 3; n++)
    {
        totalQuestoes = totalQuestoes + questoesPorNivel[n];
        totalAcertos = totalAcertos + acertosPorNivel[n];
    }

    double percentualGeral = Percentual(totalAcertos, totalQuestoes);
    int pontos = CalcularPontuacao(acertosPorNivel, dicas);
    int maximo = CalcularPontuacaoMaxima(questoesPorNivel);
    double media = tempo / totalQuestoes;

    Console.WriteLine();
    Console.WriteLine("===== ARCOR – DESAFIO DAS MARCAS: RESUMO DA PARTIDA =====");
    Console.WriteLine("Jogador: " + nickname);
    Console.WriteLine("Faixa etária: " + faixa);
    Console.WriteLine();
    Console.WriteLine("Desempenho por nível:");
    for (int n = 0; n < 3; n++)
    {
        if (questoesPorNivel[n] == 0)
        {
            Console.WriteLine($"{nomesNiveis[n],-8}(0/0)  não jogado");
        }
        else
        {
            double p = Percentual(acertosPorNivel[n], questoesPorNivel[n]);
            Console.WriteLine($"{nomesNiveis[n],-8}({acertosPorNivel[n]}/{questoesPorNivel[n]})  {p,5:F1}%  {GerarBarra(acertosPorNivel[n])}");
        }
    }
    Console.WriteLine();
    Console.WriteLine($"Total: {totalAcertos} acertos e {totalQuestoes - totalAcertos} erros em {totalQuestoes} questões ({percentualGeral:F1}%)");
    Console.WriteLine($"Pontuação: {pontos} de {maximo} pontos possíveis ({pontos * 100.0 / maximo:F1}%)");
    Console.WriteLine($"Dicas usadas: {dicas} (penalidade de {dicas * 5} pontos)");
    Console.WriteLine($"Tempo total: {tempo / 60} min {tempo % 60} s | Média: {media:F1} s por questão");
    Console.WriteLine("Ritmo: " + DefinirRitmo(media));
    Console.WriteLine("Melhor nível: " + MelhorNivel(questoesPorNivel, acertosPorNivel, nomesNiveis));
    Console.WriteLine("Classificação: " + Classificar(percentualGeral));
    Console.WriteLine("=========================================================");
}