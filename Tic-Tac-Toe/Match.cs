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

		public Match(string playerOneName, string playerTwoName)
		{
			this.playerOne = playerOneName;
			this.playerTwo = playerTwoName;
		}

		public string PlayerOne { get => playerOne; }
		public string PlayerTwo { get => playerTwo;}
		public int PlayerOneVictories { get => playerOneVictories; set => playerOneVictories = value; }
		public int PlayerTwoVictories { get => playerTwoVictories; set => playerTwoVictories = value; }
		public int Draws { get => draws; set => draws = value; }	
		public int MatchesPlayed { get => matchesPlayed; }
		public void SetMMatchesPlayed()
		{
			matchesPlayed = playerOneVictories + playerTwoVictories + draws;
		}


	}
}
