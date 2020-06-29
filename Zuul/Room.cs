using System.Collections.Generic;
using System;

namespace Zuul
{
	public class Room
	{
		private string description;
		private Dictionary<string, Room> exits; // stores exits of this room.
        public Inventory inventory;
        public bool hasLock = false;
        public bool locked = false;

        /**
	     * Create a room described "description". Initially, it has no exits.
	     * "description" is something like "in a kitchen" or "in an open court
	     * yard".
	     */
        public Room(string description)
		{
			this.description = description;
			exits = new Dictionary<string, Room>();
            inventory = new Inventory(100);
        }

		/**
	     * Define an exit from this room.
	     */
		public void setExit(string direction, Room neighbor)
		{
			exits[direction] = neighbor;
		}

        public void useKey(Room room)
        {
            if (room.hasLock)
            {
                if (room.locked) { room.locked = false; Console.WriteLine("You unlocked the door to the " + room.description); }
                else { room.locked = true; Console.WriteLine("You locked the door again"); }
            }
            else { Console.WriteLine("This room: "+room.description+", doesn't have a door with a lock "); }
        }

        public bool isLocked()
        {
            return locked;
        }

		/**
	     * Return the description of the room (the one that was defined in the
	     * constructor).
	     */
		public string getShortDescription()
		{
			return description;
		}

		/**
	     * Return a long description of this room, in the form:
	     *     You are in the kitchen.
	     *     Exits: north west
	     */
		public string getDescription()
		{
			string returnstring = "You are in the ";
			returnstring += description;
			returnstring += ".\n";
			returnstring += getExitstring();
			return returnstring;
		}

		/**
	     * Return a string describing the room's exits, for example
	     * "Exits: north, west".
	     */
		private string getExitstring()
		{
			string returnstring = "Exits:";

			// because `exits` is a Dictionary, we can't use a `for` loop
			int commas = 0;
			foreach (string key in exits.Keys) {
				if (commas != 0 && commas != exits.Count) {
					returnstring += ",";
				}
				commas++;
				returnstring += " " + key;
			}
            returnstring += "\n";

            return returnstring;
		}

		/**
	     * Return the room that is reached if we go from this room in direction
	     * "direction". If there is no room in that direction, return null.
	     */
		public Room getExit(string direction)
		{
			if (exits.ContainsKey(direction)) {
				return (Room)exits[direction];
			} else {
				return null;
			}
		}
	}
}