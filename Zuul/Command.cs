namespace Zuul
{
	public class Command
	{
		private string commandWord;
		private string secondWord;
        private string thirdWord;

		/**
	     * Create a command object. First and second word must be supplied, but
	     * either one (or both) can be null. The command word should be null to
	     * indicate that this was a command that is not recognised by this game.
	     */
		public Command(string firstWord, string secondWord, string thirdWord)
		{
			this.commandWord = firstWord;
			this.secondWord = secondWord;
            this.thirdWord = thirdWord;
		}

        // return string
		public string getCommandWord()
		{
			return commandWord;
		}

		public string getSecondWord()
		{
			return secondWord;
		}

        public string getThirdWord()
        {
            return thirdWord;
        }

        // return bool
        public bool isUnknown()
		{
			return (commandWord == null);
		}

		public bool hasSecondWord()
		{
			return (secondWord != null);
		}

        public bool hasThirdWord()
        {
            return (thirdWord != null);
        }
    }
}
