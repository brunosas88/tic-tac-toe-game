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
			List<Player> players = new List<Player>();
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

				Console.Write("Escolha operação a ser realizada indicando seu número: ");

				validEntry = int.TryParse(Console.ReadLine(), out option);

				switch (option)
				{
					case 1:
						RegisterPlayer(players);
						break;
					case 2:
						
						break;
					case 3:

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

		static Player SelectPlayer(List<Player> players, int playerOrder)
		{
			string findPlayerAgain = "n";
			Player? player;

			do
			{
				Display.GameInterface("Configurações Iniciais do Jogo");

				Console.Write($"Nome do Jogador {playerOrder}: ");
				string dataEntry = Console.ReadLine();

				player = players.Find(player => player.Nome == dataEntry);

				if (player == null)
				{
					Display.ShowWarning("Jogador Não Encontrado!");
					Console.Write("Selecionar Outro Jogador? S - sim / Qualquer outra tecla - não: ");
					findPlayerAgain = Console.ReadLine();
				}
				else
					return player;

			} while (findPlayerAgain == "s" || findPlayerAgain == "S");

			return player;
			
		}

		static bool SelectGameOptions(List<Player> players)
		{
			Display.GameInterface("Configurações Iniciais do Jogo");
	
			Player? player = SelectPlayer(players, 1);

			Player? otherPlayer = SelectPlayer(players, 2);

			if (player == null || otherPlayer == null)			
				Display.ShowWarning("Jogador(es) Inválidos!");
			else
			{
				player.PlayOrder = 1;
				otherPlayer.PlayOrder = 2;
				int winner;
				string playAgain = "n";
				do
				{
					winner = PlayGame(player, otherPlayer);

					if(winner == 1)					
						player.Vctories++;					
					else if (winner == 2)
						otherPlayer.Vctories++;
					else
					{
						player.Draws++;
						otherPlayer.Draws++;
					}

					Console.Write("Selecionar Outro Jogador? S - sim / Qualquer outra tecla - não: ");
					playAgain = Console.ReadLine();
					

				

				} while (playAgain == "s" || playAgain == "S");
			}

			Display.BackToMenu();

			return true;
		}

		public static void RegisterPlayer(List<Player> players)
		{
			string name, cpf, password, warning = "Jogador já Cadastrado!";
			bool isRegistered;
			Display.GameInterface("Cadastro de Novo Jogador");	

			Console.Write("Insira nome do novo cliente: ");
			name = Console.ReadLine();

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

		static int PlayGame(Player playerOne, Player playerTwo)
		{
			Board gameBoard = new Board();
			Player currentPlayer = playerOne; 

			int position, winner;
			bool playerOneTurn = true;
			bool[] moveCount = new bool[9];

			do
			{
				Console.Clear();
				gameBoard.PrintBoard();
				Console.Write($"Jogador {currentPlayer.Nome}, insira posição: ");
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

			if (winner == 0)
			{
				Console.WriteLine("Jogadores finalizaram a partida sem vencedores");
			}
			else
				Console.WriteLine($"Jogador {currentPlayer.Nome} venceu");

			return winner;
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
				validEndtry = int.TryParse(Console.ReadLine(), out position);

				if (position > 0 && position < 10)
				{
					if (!moveCount[position - 1])
					{
						moveCount[position - 1] = true;
						break;
					}
					else
						Console.Write("Posição já ocupada, escolha outra: ");
				}
				else if (validEndtry && position == 0) // sair do jogo
					break;
				else
					Console.Write("Insira uma posição válida (1 - 9): ");					
				
			} while (true);

			return position;
		}
	}
}
