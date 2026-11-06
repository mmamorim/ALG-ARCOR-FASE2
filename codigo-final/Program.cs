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
        acertosPorNivel[nivel]++;
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