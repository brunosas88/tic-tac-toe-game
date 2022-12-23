using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
	class Game
	{

		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetWindowSize(Constants.WindowWidthSize, Constants.WindowHeightSize);
			List<Player> players = new List<Player>();
			List<Match> matches = new List<Match>();
			int option;
			string warningMessage = "";
			bool warning = false, validEntry;

			do
			{
				Display.GameInterface("Menu Inicial");
				Display.ShowMenu();						

				if (warning)
				{
					Display.ShowWarning(warningMessage);
					warning = false;
				}

				Console.WriteLine(Display.AlignMessage("Escolha operação indicando seu número: "));
					
				validEntry = int.TryParse(Display.FormatConsoleReadLine(), out option);

				switch (option)
				{
					case 1:
						RegisterPlayer(players);
						break;
					case 2:
						GetLogs(players, matches);					
						break;
					case 3:
						SelectGameOptions(players, matches);
						break;
					case 0:
					default:
						if (validEntry && option == 0)
						{
							Display.GameInterface("Game Over!");
							break;
						}
						else
							option = -1;
						warningMessage = "Aviso: Opção inválida, favor inserir número de 0 a 3.";
						warning = true;
						break;
				}
			} while (option != 0);

		}

		private static void GetLogs(List<Player> players, List<Match> matches)
		{
			int option;
			string warningMessage = "";
			bool warning = false, validEntry;

			do
			{
				Display.GameInterface("Mostrar Histórico");
				Display.ShowLogMenu();

				if (warning)
				{
					Display.ShowWarning(warningMessage);
					warning = false;
				}

				Console.WriteLine(Display.AlignMessage("Escolha operação indicando seu número: "));

				validEntry = int.TryParse(Display.FormatConsoleReadLine(), out option);

				switch (option)
				{
					case 1:
						GetPlayersLogs(players);
						break;
					case 2:
						GetMatchesLogs(matches);						
						break;
					case 0:
					default:
						if (validEntry && option == 0)													
							break;						
						else
							option = -1;
						warningMessage = "Aviso: Opção inválida, favor inserir número de 0 a 3.";
						warning = true;
						break;
				}
			} while (option != 0);
		}

		private static void GetMatchesLogs(List<Match> matches)
		{
			Display.GameInterface("Histórico das Partidas");

			foreach (Match match in matches)
				Display.ShowMatchesDetails(match);				

			Display.BackToMenu();
		}

		private static void GetPlayersLogs(List<Player> players)
		{
			Display.GameInterface("Histórico dos Jogadores");

			foreach (Player player in players)
				Display.ShowPlayerDetails(player);		
			
			Display.BackToMenu();
		}

		static Player SelectPlayer(List<Player> players, int playerOrder)
		{
			string findPlayerAgain = "n";
			Player? player;

			do
			{
				Display.GameInterface("Configurações Iniciais do Jogo");

				Console.WriteLine(Display.AlignMessage($"Nome do Jogador {playerOrder}: "));
				string dataEntry = Display.FormatConsoleReadLine();

				player = players.Find(player => player.Nome == dataEntry);

				if (player == null)
				{
					Display.ShowWarning("Jogador Não Encontrado!");
					Console.WriteLine(Display.AlignMessage("Selecionar Outro Jogador? S - sim / Qualquer outra tecla - não: "));
					findPlayerAgain = Display.FormatConsoleReadLine();
				}
				else
					return player;

			} while (findPlayerAgain == "s" || findPlayerAgain == "S");

			return player;
			
		}

		static bool SelectGameOptions(List<Player> players, List<Match> matches)
		{
			Display.GameInterface("Configurações Iniciais do Jogo");

			Player?[] gamePlayers = new Player[2];

			gamePlayers[0] = SelectPlayer(players, 1);

			gamePlayers[1] = SelectPlayer(players, 2);

			if (gamePlayers.Contains(null))			
				Display.ShowWarning("Jogador(es) Inválidos!");
			else
			{
				string playAgain;
				int playerOnePreMatchWins = gamePlayers[0].Victories;
				int playerTwoPreMatchWins = gamePlayers[1].Victories;
				int playerOnepreMatchDraws = gamePlayers[0].Draws;
				Match currentMatch = new Match(gamePlayers[0].Nome, gamePlayers[1].Nome);
				do
				{
					Display.GameInterface("Jogar!");
					gamePlayers[0].PlayOrder = 1;
					gamePlayers[1].PlayOrder = 2;

					PlayGame(gamePlayers[0], gamePlayers[1]);

					Console.WriteLine(Display.AlignMessage("Continuar Jogando? S - sim / Qualquer outra tecla - não: "));
					playAgain = Display.FormatConsoleReadLine();

					if (playAgain == "s" || playAgain == "S")					
						Array.Reverse(gamePlayers);				

				} while (playAgain == "s" || playAgain == "S");

				CalculateMatchResults(gamePlayers, matches,	playerOnePreMatchWins, playerTwoPreMatchWins, playerOnepreMatchDraws);
			}

			Display.BackToMenu();

			return true;
		}

		private static void CalculateMatchResults(Player[] gamePlayers, List<Match> matches, int playerOnePreMatchWins, int playerTwoPreMatchWins, int playerOnepreMatchDraws)
		{
			int playerOnePoints;
			int playerTwoPoints;

			Match currentMatch = new Match(gamePlayers[0].Nome, gamePlayers[1].Nome);
			currentMatch.PlayerOneVictories = gamePlayers[0].Victories - playerOnePreMatchWins;
			currentMatch.PlayerTwoVictories = gamePlayers[1].Victories - playerTwoPreMatchWins;
			currentMatch.Draws = gamePlayers[0].Draws - playerOnepreMatchDraws;
			currentMatch.SetMatchesPlayed();
			matches.Add(currentMatch);

			gamePlayers[0].Points = currentMatch.PlayerOneVictories * Constants.VictoryPoints +
									currentMatch.PlayerTwoVictories * Constants.DefeatPoints +
									currentMatch.Draws * Constants.DrawPoints;

			gamePlayers[1].Points = currentMatch.PlayerTwoVictories * Constants.VictoryPoints +
									currentMatch.PlayerOneVictories * Constants.DefeatPoints +
									currentMatch.Draws * Constants.DrawPoints;
		}

		static void RegisterPlayer(List<Player> players)
		{
			string name, cpf, password, warning = "Jogador já Cadastrado!";
			bool isRegistered;
			Display.GameInterface("Cadastro de Novo Jogador");	

			Console.WriteLine(Display.AlignMessage("Insira nome do novo jogador: "));
			name = Display.FormatConsoleReadLine();

			isRegistered = players.Exists(player => player.Nome == name);

			if (!string.IsNullOrEmpty(name) && !isRegistered)
			{
				players.Add(new Player(name));				
				warning = $"Jogador {name} Cadastrado com Sucesso!";
			}
			else if (string.IsNullOrEmpty(name))
				warning = "Entrada Inválida! Operação Não Realizada!";
			else
				warning += " Operação Não Realizada!";

			Display.ShowWarning(warning);

			Display.BackToMenu();
		}

		static void PlayGame(Player playerOne, Player playerTwo)
		{
			Board gameBoard = new Board();
			Player currentPlayer = playerOne; 

			int position, winner;
			bool playerOneTurn = true;
			bool[] moveCount = new bool[9];
			Display.ShowWarning("Para sair da partida aperte a tecla 0 (zero)");
			gameBoard.PrintBoard();
			
			do
			{				
				Console.WriteLine(Display.AlignMessage($"Jogador {currentPlayer.Nome}, insira posição: "));
				position = CheckMove(moveCount);
				if (position != 0)
				{
					gameBoard.UpdateBoard(position, currentPlayer.PlayOrder);
					playerOneTurn = !playerOneTurn;
					currentPlayer = playerOneTurn ? playerOne : playerTwo;				
				}
				Display.GameInterface("Jogar!");
				Display.ShowWarning("Para sair da partida aperte a tecla 0 (zero)");
				gameBoard.PrintBoard();
				winner = CheckWinner(gameBoard.GetBoard());
				if (!moveCount.Contains(false))
					position = 0;
			} while (position != 0 && winner == 0);
			
			if (winner == 1)
			{
				playerOne.Victories++;
				playerTwo.Defeats++;
				Display.ShowWarning($"Jogador {playerOne.Nome} venceu!");
			}
			else if (winner == 2)
			{
				playerOne.Defeats++;
				playerTwo.Victories++;
				Display.ShowWarning($"Jogador {playerTwo.Nome} venceu!");
			}
			else
			{
				playerOne.Draws++;
				playerTwo.Draws++;
				Display.ShowWarning("Jogadores finalizaram a partida sem vencedores");
			}
		}

		static int CheckWinner(char[,] board)
		{
			char result = ' ';

			for (int row = 0; row < board.GetLength(0); row++) // verifica linhas
			{ 
				if ((board[row,0] == board[row,2] && board[row, 0] == board[row, 4]))				
					result = board[row, 0];
				
				for (int column = 0; column < board.GetLength(1); column += 2) // verifica colunas
				{ 
					if ((board[0, column] == board[1, column] && board[0, column] == board[2, column]))					
						result = board[0, column];					
				}
			}

			if ( (board[0, 0] == board[1, 2] && board[0, 0] == board[2, 4]) || // verifica diagonais
				 (board[0, 4] == board[1, 2] && board[0, 4] == board[2, 0])	)
			{ result = board[1, 2]; }
			
			return result == 'X' ? 1 : result == 'O' ? 2 : 0;
		}

		static int CheckMove(bool[] moveCount)
		{
			int position;
			bool validEndtry;
			do
			{
				validEndtry = int.TryParse(Display.FormatConsoleReadLine(), out position);

				if (position > 0 && position < 10)
				{
					if (!moveCount[position - 1])
					{
						moveCount[position - 1] = true;
						break;
					}
					else
						Console.WriteLine(Display.AlignMessage("Posição já ocupada, escolha outra: "));
				}
				else if (validEndtry && position == 0) // sair do jogo
					break;
				else
					Console.WriteLine(Display.AlignMessage("Insira uma posição válida (1 - 9): "));					
				
			} while (true);

			return position;
		}
	}
}
