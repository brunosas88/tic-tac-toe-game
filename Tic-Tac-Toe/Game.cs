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
			Board gameBoard = new Board();
			
			int player = 1, position, winner;
			bool playerOneTurn = true;
			bool[] moveCount = new bool[9];

			do
			{
				Console.Clear();
				gameBoard.PrintBoard();
				Console.Write($"Jogador {player}, insira posição: ");
				position = CheckMove(moveCount);
				if (position != 0)
				{
					gameBoard.UpdateBoard(position, player);					
					playerOneTurn = !playerOneTurn;
					player = playerOneTurn ? 1 : 2;
				}
				winner = CheckWinner(gameBoard.GetBoard());
				if (!moveCount.Contains(false))
					position = 0;
			} while (position != 0 && winner == 0);

			if (winner == 0)
			{
				Console.WriteLine("Jogadores finalizaram a partida sem resultado final obtido");
			}
			else
				Console.WriteLine($"Jogador {winner} venceu");
		}

		public static int CheckWinner(char[,] board)
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

		public static int CheckMove(bool[] moveCount)
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
