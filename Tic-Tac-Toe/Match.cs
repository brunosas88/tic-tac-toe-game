namespace Tic_Tac_Toe
{
	class Match
	{
		private string playerOne;
		private string playerTwo;
		private int playerOneVictories;
		private int playerTwoVictories;
		private int draws;
		private int matchesPlayed;

		public Match() { }

		public Match(string playerOneName, string playerTwoName)
		{
			playerOne = playerOneName;
			playerTwo = playerTwoName;
			playerOneVictories = 0;
			playerTwoVictories = 0;
			draws = 0;
			matchesPlayed = 0;
		}

		public string PlayerOne { get => playerOne; set => playerOne = value; }
		public string PlayerTwo { get => playerTwo; set => playerTwo = value; }
		public int PlayerOneVictories { get => playerOneVictories; set => playerOneVictories = value; }
		public int PlayerTwoVictories { get => playerTwoVictories; set => playerTwoVictories = value; }
		public int Draws { get => draws; set => draws = value; }	
		public int MatchesPlayed { get => matchesPlayed; set => matchesPlayed = value; }
		public void CalculateMatchesPlayed()
		{
			matchesPlayed = playerOneVictories + playerTwoVictories + draws;
		}


	}
}
