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
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetWindowSize(Constants.WindowWidthSize, Constants.WindowHeightSize);
			List<Player> players = new List<Player>();
			List<Match> matches = new List<Match>();
			int option;
			string warningMessage = "";
			bool warning = false, validEntry;

			Board board = new Board();

			do
			{
				Display.GameInterface("Menu Inicial");
				Display.ShowMenu();
				board.PrintBoard();

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
							Display.GameInterface("Muito Obrigado por utilizar nosso aplicativo!");
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
				int beforeMAtchPlayerOneWins = gamePlayers[0].Victories;
				int beforeMAtchPlayerTwoWins = gamePlayers[1].Victories;
				int beforeMAtchDraws = gamePlayers[1].Draws;
				Match currentMatch = new Match(gamePlayers[0].Nome, gamePlayers[1].Nome);
				do
				{
					Display.GameInterface("Jogar!");
					gamePlayers[0].PlayOrder = 1;
					gamePlayers[1].PlayOrder = 2;

					PlayGame(gamePlayers[0], gamePlayers[1]);

					Console.WriteLine(Display.AlignMessage("Selecionar Outro Jogador? S - sim / Qualquer outra tecla - não: "));
					playAgain = Display.FormatConsoleReadLine();

					if (playAgain == "s" || playAgain == "S")					
						Array.Reverse(gamePlayers);				

				} while (playAgain == "s" || playAgain == "S");
				currentMatch.PlayerOneVictories = gamePlayers[0].Victories - beforeMAtchPlayerOneWins;
				currentMatch.PlayerTwoVictories = gamePlayers[1].Victories - beforeMAtchPlayerTwoWins;
				currentMatch.Draws = gamePlayers[1].Draws - beforeMAtchDraws;
				currentMatch.SetMMatchesPlayed();
				matches.Add(currentMatch);
			}

			Display.BackToMenu();

			return true;
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

			do
			{
				Display.GameInterface("Jogar!");
				gameBoard.PrintBoard();
				Console.WriteLine(Display.AlignMessage($"Jogador {currentPlayer.Nome}, insira posição: "));
				position = CheckMove(moveCount);
				if (position != 0)
				{
					gameBoard.UpdateBoard(position, currentPlayer.PlayOrder);
					playerOneTurn = !playerOneTurn;
					currentPlayer = playerOneTurn ? playerOne : playerTwo;
				}
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

			for (int i = 0; i < board.GetLength(0); i++) // verifica linhas
			{ 
				if ((board[i,0] == board[i,2] && board[i, 0] == board[i, 4]))				
					result = board[i, 0];
				
				for (int j = 0; j < board.GetLength(0); j += 2) // verifica colunas
				{ 
					if ((board[0, j] == board[1, j] && board[0, j] == board[2, j]))					
						result = board[0, j];					
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
