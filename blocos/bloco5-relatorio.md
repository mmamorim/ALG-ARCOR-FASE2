# Bloco 5 – Relatório em funções

[← Bloco 4](bloco4-dicas-e-cronometro.md) · [Início](../README.md)

Neste bloco, o relatório da Entrega 1 vira um conjunto de funções. As regras são **as mesmas**: pontuação 10/20/30 por acerto, −5 por dica, mínimo zero, classificação e ritmo com os mesmos limites. Todas as funções abaixo vão na seção **FUNÇÕES**.

## 1. Percentual e pontuação (lacuna 8)

~~~csharp
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
        pontos = /* LACUNA 8: pontos atuais + acertos do nível n vezes o peso do nível n */;
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
~~~

Na Entrega 1, a pontuação era uma conta com três termos. Aqui, o vetor `pesos` e o laço fazem a mesma conta, nível por nível.

## 2. Classificação e barra de asteriscos (lacunas 9 e 10)

~~~csharp
static string Classificar(double percentual)
{
    if (percentual >= /* LACUNA 9 */)
    {
        return "Mestre das Marcas";
    }
    else if (percentual >= /* LACUNA 9 */)
    {
        return "Conhecedor de Marcas";
    }
    else if (percentual >= /* LACUNA 9 */)
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
        barra = /* LACUNA 10: a barra atual com mais um asterisco */;
    }
    return barra;
}
~~~

A lacuna 9 usa os três limites da classificação da Entrega 1. A lacuna 10 é o mesmo gráfico de asteriscos, agora escrito **uma vez só** e usado para os três níveis.

## 3. Desafios opcionais: ritmo e melhor nível

Estas duas funções já compilam e devolvem "(não calculado)". O programa funciona sem elas, mas o grupo que tiver tempo pode completá-las:

~~~csharp
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
~~~

## 4. Função `MostrarRelatorio`

Esta função junta tudo no mesmo modelo de relatório da Entrega 1:

~~~csharp
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
    double media = tempo * 1.0 / totalQuestoes;

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
~~~

Reparem no `for` do desempenho por nível: na Entrega 1 eram três blocos quase iguais, um por nível. Com os vetores, é um bloco só, repetido três vezes. Na expressão `{p,5:F1}`, o `5` alinha o número em 5 caracteres e o `F1` mostra uma casa decimal.

## 5. Ligar o relatório ao jogo

Na seção **INÍCIO DO JOGO**, **troquem todo o conteúdo do `else`** do final (as duas linhas provisórias) por:

~~~csharp
int tempo = (int)cronometro.Elapsed.TotalSeconds;
int dicasUsadas = ContarDicas(usouDica);
MostrarRelatorio(nickname, faixa, questoesPorNivel, acertosPorNivel, dicasUsadas, tempo);
~~~

## 6. Checkpoint do Bloco 5

Executem com `dotnet run`, joguem uma partida completa e anotem o que fizeram em cada questão (acertou ou errou, usou dica ou não). Depois, confiram o relatório à mão, com as regras da Entrega 1: os acertos por nível, a pontuação e a classificação devem bater com as contas de vocês.

Quando o programa estiver funcionando, preencham o [documento de entrega](../entrega/Template_Entrega2_ALP.docx) e enviem tudo, como explicado no [início](../README.md#o-que-entregar).

---

[← Voltar ao início](../README.md)
