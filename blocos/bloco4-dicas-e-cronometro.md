# Bloco 4 – Dicas e cronômetro

[← Bloco 3](bloco3-questao-e-partida.md) · [Início](../README.md)

## 1. Função `ContarDicas` (lacuna 6)

O vetor `usouDica` tem uma posição por questão, com `true` onde o jogador pediu dica. Esta função conta quantas posições são `true`. Colem na seção **FUNÇÕES**:

~~~csharp
static int ContarDicas(bool[] usouDica)
{
    int total = 0;
    for (int i = 0; i < usouDica.Length; i++)
    {
        if (/* LACUNA 6: a posição i do vetor usouDica é verdadeira */)
        {
            total++;
        }
    }
    return total;
}
~~~

Como cada questão tem uma só posição no vetor, pedir a dica várias vezes na mesma questão conta como **uma** dica.

## 2. Cronômetro da partida

O `Stopwatch` mede o tempo passado. Ele começa quando o quiz inicia, depois dos dados do jogador, e **não para durante a dica**: pedir dica também é tempo de jogo. Façam três alterações na seção **INÍCIO DO JOGO**.

**a) Ligar o cronômetro.** Logo **antes** da linha `for (int i = 0; i < totalQuestoes; i++)`, acrescentem:

~~~csharp
Stopwatch cronometro = Stopwatch.StartNew();
~~~

**b) Mostrar o tempo no cabeçalho (lacuna 7).** Dentro do `for`, **troquem** a linha do `Rule` por estas duas:

~~~csharp
int segundos = (int)cronometro.Elapsed.TotalSeconds;
int minutos = /* LACUNA 7: minutos */
int segs = /* LACUNA 7: segundos restantes */
AnsiConsole.Write(new Rule($"Questão {i + 1} de {totalQuestoes} | Tempo: {minutos} min {segs} s"));
~~~

A lacuna 7 é a mesma conversão de segundos em minutos e segundos da Entrega 1.

**c) Parar o cronômetro.** Logo **depois** da chave `}` que fecha o `for` (antes do `if (terminou)`), acrescentem:

~~~csharp
cronometro.Stop();
~~~

## 3. Checkpoint do Bloco 4

Dentro do `else` do final, abaixo da linha provisória do Bloco 3, acrescentem mais uma linha **provisória**:

~~~csharp
Console.WriteLine($"Dicas usadas: {ContarDicas(usouDica)} | Tempo: {(int)cronometro.Elapsed.TotalSeconds} s");
~~~

Executem com `dotnet run`. O cabeçalho de cada questão deve mostrar o tempo aumentando. No fim, confiram se a quantidade de dicas e o tempo total fazem sentido. O tempo não corre na tela: ele é atualizado a cada nova questão.

---

[Próximo: Bloco 5 – Relatório em funções →](bloco5-relatorio.md)
