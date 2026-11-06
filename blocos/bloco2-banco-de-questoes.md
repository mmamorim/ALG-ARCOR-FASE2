# Bloco 2 – Banco de questões em vetores e matriz

[← Bloco 1](bloco1-abertura.md) · [Início](../README.md)

## 1. Os dados das questões

Colem na seção **BANCO DE QUESTÕES**, logo abaixo do vetor `faixasEtarias`:

~~~csharp
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
~~~

Esses dados formam **vetores paralelos**: a posição `i` de cada vetor fala da mesma questão. A questão 0, por exemplo, tem o enunciado `enunciados[0]`, o nível `niveis[0]` e a dica `dicas[0]`.

As alternativas ficam numa **matriz** `string[,]`: cada **linha** é uma questão, e cada **coluna** é uma alternativa. A alternativa 2 da questão 0 é `alternativas[0, 2]`, que vale "Tartaruga".

O vetor `corretas` guarda **a coluna** da resposta certa de cada questão. Na questão 0, a certa é a coluna 2 ("Tartaruga"); na questão 1, a coluna 0 ("Biscoitos").

## 2. Função `ExtrairAlternativas` (lacuna 1)

O `SelectionPrompt` precisa de um **vetor** com as opções, mas as alternativas estão numa **matriz**. Esta função copia uma linha da matriz para um vetor novo. Colem na seção **FUNÇÕES**:

~~~csharp
static string[] ExtrairAlternativas(string[,] alternativas, int linha)
{
    int quantidade = alternativas.GetLength(1);
    string[] opcoes = new string[quantidade];
    for (int j = 0; j < quantidade; j++)
    {
        opcoes[j] = /* LACUNA 1: o elemento da matriz na linha "linha" e na coluna "j" */;
    }
    return opcoes;
}
~~~

`GetLength(1)` devolve o número de **colunas** da matriz (aqui, 4). `GetLength(0)` devolveria o número de linhas.

## 3. Checkpoint do Bloco 2

Colem este trecho **provisório** na seção **INÍCIO DO JOGO**, logo abaixo da linha `string faixa = ...`. Ele lista as questões e a resposta certa de cada uma:

~~~csharp
// PROVISÓRIO: apagar no início do Bloco 3
for (int i = 0; i < enunciados.Length; i++)
{
    string[] opcoes = ExtrairAlternativas(alternativas, i);
    string nivel = niveis[i];
    string pergunta = Markup.Escape(enunciados[i]);
    string respCorreta = Markup.Escape(opcoes[corretas[i]]);
    AnsiConsole.MarkupLine($"[green]{nivel}[/] {pergunta} -> {respCorreta}");
}
~~~
Pontos importantes:

- `Markup.Escape` protege o texto caso ele tenha algum colchete.

Executem com `dotnet run`. Depois da faixa etária, devem aparecer as 3 questões, cada uma com a resposta certa ao lado.

## 4. Tarefa do grupo: acrescentar 6 questões

Para acrescentar uma questão, incluam **um item em cada vetor** (`enunciados`, `niveis`, `dicas`, `corretas`) e **uma linha na matriz** `alternativas`, sempre na mesma ordem. Ao final, o banco deve ter pelo menos 9 questões, 3 de cada nível.

Rodem o checkpoint de novo e confiram se cada questão mostra a resposta certa ao lado. Se aparecer a resposta errada, o índice em `corretas` está trocado. Se o programa der erro ao compilar, alguma linha da matriz ficou com mais ou menos de 4 alternativas.

---

[Próximo: Bloco 3 – Função da questão e laço da partida →](bloco3-questao-e-partida.md)
