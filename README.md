# Tic-Tac-Toe (Jogo da Velha)
Jogo em um tabuleiro de 9 espaços onde a vitória é dada para o jogador que conseguir preencher em uma sequencia de três espaços o caracter que o representa.

## Mecânica do jogo
Para jogar o jogador deve inserir uma posição de 1 a 9 que representam os espaços do tabuleiro, não é permitido inserir em um espaço já inserido nem um valor fora
desse intervalo é aceito. Uma vitória representa +2 pontos, derrota é -1 ponto e empate contabiliza +1 ponto. Todas as partidas são registradas e seu resultado pode ser observado posteriormente bem como o histórico dos jogadores individualmente. 
```
¹ | ² | ³
⁴ | ⁵ | ⁶
⁷ | ⁸ | ⁹
```

## Configuração Adicional
O comando ``` Console.SetWindowSize(Constants.WindowWidthSize, Constants.WindowHeightSize); ``` localizado na linha 17 da classe Game.cs funciona somente no Windows, o que significa que para rodar em outro sistema operacional ela deve ser comentada. Lembrando também que não utilizado o sistema Windows alguns caracteres podem não ser reconhecidos e exibidos de forma incorreta.


## Recursos Implementados
    1 - Cadastrar jogadores;
    2 - Mostrar histórico de jogador(nome, pontos, vitórias, derrotas e empates);
    3 - Mostrar histórico de partidas (jogadores envolvidos, vitorias e empates);
    4 - Verificar cadastro de jogadores baseado no nome informado;
    5 - Jogo;
    


## Tecnologias Utilizadas
  * Lista serve como base de dados para salvar jogadores e partidas jogadas;
  * Classes com responsabilidades separadas para interface; mecânicas do jogo; representação dos modelos de jogador, partida e tabuleiro; persistência de dados em JSON;
  
