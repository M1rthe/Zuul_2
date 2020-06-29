using System;

namespace Zuul
{
	public class Game
	{
        private Player player;
		private Parser parser;

		public Game ()
		{
            player = new Player();
			parser = new Parser();
		}

		/**
	     *  Main play routine.  Loops until end of play.
	     */
		public void play()
		{
			printWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
			bool finished = false;
			while (! finished && player.isAlive()) {
				Command command = parser.getCommand();
				finished = processCommand(command);
			}
			Console.WriteLine("Thank you for playing.");
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.currentRoom.getDescription());
            Console.WriteLine();
        }

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

			string commandWord = command.getCommandWord();
			switch (commandWord) {
				case "help":
					printHelp();
					break;
				case "go":
					player.goRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
                case "look":
                    look(command.getSecondWord());
                    break;
                case "take":
                    player.takeItem(command.getSecondWord());
                    break;
                case "drop":
                    player.dropItem(command.getSecondWord());
                    break;
                case "use":
                    player.useItem(command.getSecondWord());
                break;
            }
			return wantToQuit;
		}

		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
		private void printHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
		}

        private void look(string whatToLookAt)
        {
            if (whatToLookAt == "room") { Console.WriteLine(player.getFullRoomDescription()); }
            else if (whatToLookAt == "player") { Console.WriteLine(player.getFullPlayerDescription()); }
            else { Console.WriteLine("You can only look at: room or player"); }
        }
    }
}
