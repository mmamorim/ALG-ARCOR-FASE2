# Quiz ARCOR – Desafio das Marcas

**Entrega 2 de Algoritmos e Lógica de Programação – Projeto Interdisciplinar, 1º ADS, 2º semestre de 2026**

Nesta entrega, cada grupo constrói em C# o **Quiz ARCOR – Desafio das Marcas**, jogado no console com a biblioteca **Spectre.Console**. O jogador escolhe as respostas com as setas do teclado, pode pedir dicas e, no fim, recebe o mesmo relatório que vocês programaram na Entrega 1. A diferença é que agora os dados vêm do próprio jogo, e não são mais digitados.

A atividade é feita **durante a aula**, seguindo os blocos deste repositório, e entregue ao final dela.

## Objetivo

Aplicar as estruturas desta etapa num programa real do projeto:

- **Modularização:** o programa é dividido em funções, cada uma com uma tarefa.
- **Vetores:** enunciados, níveis, dicas, respostas certas e contadores de desempenho.
- **Matriz:** as alternativas de todas as questões.

Boa parte do relatório sai do código da Entrega 1, agora transformado em funções.

## Regras

### Código

- Todo o programa fica em **um único arquivo `Program.cs`**.
- **Sem orientação a objetos:** o grupo não cria classes próprias. Usar os recursos prontos do Spectre.Console (como `SelectionPrompt` e `Rule`) e o `Stopwatch` é permitido.
- Dados guardados apenas em **vetores e matrizes**. Não use `List`, `Dictionary`, LINQ (`Sum()`, `Max()`, `Count()` e similares) nem métodos de `Math`.
- Cada função recebe pelos parâmetros os dados de que precisa. As funções são declaradas com `static`, o que impede que usem variáveis de fora sem recebê-las.

### Banco de questões

- O tutorial já traz **3 questões de exemplo**, uma de cada nível. O grupo acrescenta **no mínimo 6 questões próprias**, chegando a pelo menos **9 questões, 3 de cada nível**.
- Toda questão tem enunciado, nível (`F`, `M` ou `D`), dica e **exatamente 4 alternativas**.
- A resposta certa **não pode ficar sempre na mesma posição**.
- Não use colchetes `[ ]` nos textos: o Spectre os interpreta como formatação.
- O conteúdo deve vir de fontes oficiais da Arcor, por exemplo, o site [arcor.com.br](https://www.arcor.com.br/), como pede o briefing do PI.

## Como funciona o tutorial

O tutorial tem 5 blocos. Cada bloco termina com um **checkpoint**: o programa precisa compilar e rodar antes de passar ao próximo. Se o tempo acabar no meio, o grupo entrega o que tiver funcionando.

**Lacunas.** O código vem quase todo pronto. Nos pontos marcados como `LACUNA`, o grupo completa uma linha ou uma condição. São **10 lacunas** no total, e cada uma traz uma dica no comentário. O programa só compila depois que a lacuna do bloco é preenchida.

**Desafios.** Dois trechos do Bloco 5 são opcionais e marcados como `DESAFIO`. Eles já compilam com um valor provisório, e o programa funciona mesmo sem resolvê-los.

**Organização do arquivo.** O `Program.cs` tem três seções, marcadas por comentários. Cada bloco diz em qual seção colar o código:

```csharp
// ===================== BANCO DE QUESTÕES =====================
// (vetores e matriz com os dados do jogo)

// ===================== INÍCIO DO JOGO =====================
// (o que o programa executa, na ordem)

// ===================== FUNÇÕES =====================
// (todas as funções, sempre no final do arquivo)
```

## Roteiro

1. [Bloco 1 – Preparação e tela de abertura](blocos/bloco1-abertura.md)
2. [Bloco 2 – Banco de questões em vetores e matriz](blocos/bloco2-banco-de-questoes.md)
3. [Bloco 3 – Função da questão e laço da partida](blocos/bloco3-questao-e-partida.md)
4. [Bloco 4 – Dicas e cronômetro](blocos/bloco4-dicas-e-cronometro.md)
5. [Bloco 5 – Relatório em funções](blocos/bloco5-relatorio.md)

## O que entregar

### Documento de entrega

Preencham o modelo [`entrega/Template_Entrega2_ALP.docx`](entrega/Template_Entrega2_ALP.docx) e exportem em PDF. Ele tem quatro partes curtas, que podem ser preenchidas durante a aula:

1. **Identificação** do grupo.
2. **Lacunas:** o que o grupo escreveu em cada uma, por quê e a dificuldade sentida (fácil, média ou difícil).
3. **Desafios:** se o grupo resolveu cada desafio e a dificuldade sentida.
4. **Reflexão** comparando o código da Entrega 1 com o desta entrega.

### Onde entregar

Tudo fica no repositório do grupo no GitHub, na pasta **Algoritmos e Lógica de Programação**, subpasta **Entrega 2**:

```
Algoritmos e Lógica de Programação/
└── Entrega 2/
    ├── Entrega2_<NomeDoGrupo>.pdf
    └── Program.cs
```

Enviem somente o `Program.cs`, sem as pastas `bin` e `obj` que o `dotnet` cria. 