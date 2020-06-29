using System;

namespace Zuul
{
    public class Player
    {
        public Inventory inventory;
        public Room currentRoom;
        public int health = 100;

        public Player()
        {
            inventory = new Inventory(30);
            createRooms();
        }

        public void damage(int amount)
        {
            health -= amount;
            if (health < 0) { health = 0; }
        }

        public void heal(int amount)
        {
            health += amount;
            if (health > 100) { health = 100; }
        }

        public bool isAlive()
        {
            if (health < 1) { return false; }
            return true;
        }

        public bool isSeriouslyHurt()
        {
            if (health < 70) { return true; }
            return false;
        }

        private void createRooms()
        {
            Room outside, theatre, pub, lab, office;

            Donut donut;
            Key key;
            Medkit medkit;

            // create the rooms
            outside = new Room("outside of the main entrance of the university");
            theatre = new Room("lecture theatre");
            pub = new Room("campus pub");
            lab = new Room("computing lab");
            office = new Room("computing admin office");

            // create the items
            donut = new Donut("donut", "donuts are delicous", 10, false);
            key = new Key("key", "keys can open specific doors", 5, true);
            medkit = new Medkit("medkit", "this medkit gives you 30 hp", 10, false);

            // initialise room exits
            outside.setExit("east", theatre);
            outside.setExit("up", lab);
            outside.setExit("west", pub);

            theatre.setExit("west", outside);

            pub.setExit("east", outside);

            lab.setExit("down", outside);
            lab.setExit("east", office);

            office.setExit("west", lab);

            currentRoom = outside;  // start game outside

            // store items
            lab.inventory.put(donut);
            office.inventory.put(medkit);
            inventory.put(key);

            // set lockstates
            theatre.hasLock = true;
            theatre.locked = true;
        }

        /**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
        public void goRoom(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("Go where?");
                return;
            }

            string direction = command.getSecondWord();

            // Try to leave current room.
            Room nextRoom = currentRoom.getExit(direction);

            if (nextRoom == null)
            {
                Console.WriteLine("There is no door to " + direction + "!");
            }
            else if (nextRoom.isLocked())
            {
                Console.WriteLine("The door is locked, you need a key");
            }
            else
            {
                if (isSeriouslyHurt()) { damage(5); }
                currentRoom = nextRoom;
                Console.WriteLine(currentRoom.getDescription());
            }
        }

        public void takeItem(string itemString)
        {
            Item item = string2Item(itemString);

            if (item != null)
            {        
                if (inventory.take(item) != null)
                {
                    Console.WriteLine("You grabbed the "+item.name);
                    currentRoom.inventory.drop(item);
                } 
            }
        }

        public void dropItem(string itemString)
        {
            Item item = string2Item(itemString);

            if (item != null)
            {
                if (currentRoom.inventory.take(item) != null)
                {
                    Console.WriteLine("You yeeted the " + item.name+ " away");
                    inventory.drop(item);
                }        
            }
        }

        public void useItem(string itemString)
        {
            Item item = string2Item(itemString);

            if (item != null)
            {
                if (!item.keptAfterUsing)
                {
                    inventory.drop(item);
                }
                if (itemString == "donut" || itemString == "medkit")
                {
                    item.use(this);
                }
                if (itemString == "key")
                {
                    item.use(currentRoom.getExit("east"));
                    /*
                    if (currentRoom.getExit("north").hasLock) { item.use(currentRoom.getExit("north")); }
                    else if (currentRoom.getExit("east").hasLock) { item.use(currentRoom.getExit("east")); }
                    else if (currentRoom.getExit("south").hasLock) { item.use(currentRoom.getExit("south")); }
                    else if (currentRoom.getExit("west").hasLock) { item.use(currentRoom.getExit("west")); }
                    else if (currentRoom.getExit("up").hasLock) { item.use(currentRoom.getExit("up")); }
                    else if (currentRoom.getExit("down").hasLock) { item.use(currentRoom.getExit("down")); }
                    */
                }
            }
        }

        public string getFullRoomDescription()
        {
            string returnString = "\n";
            returnString += "########################################################## \n";
            returnString += currentRoom.getDescription();
            returnString += "\n";
            returnString += currentRoom.inventory.getDescription("Room");
            returnString += "##########################################################";
            returnString += "\n";

            return returnString;
        }

        public string getFullPlayerDescription()
        {
            string returnString = "\n";
            returnString += "########################################################## \n";
            returnString += "you have "+ health + " hp"; 
            returnString += "\n";
            returnString += inventory.getDescription("Inventory");
            returnString += "##########################################################";
            returnString += "\n";

            return returnString;
        }

        private Item string2Item(string itemString)
        {
            for(int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i].name == itemString)
                {
                    return inventory.items[i];
                }
            }

            for (int i = 0; i < currentRoom.inventory.items.Count; i++)
            {
                if (currentRoom.inventory.items[i].name == itemString)
                {
                    return currentRoom.inventory.items[i];
                }
            }

            Console.WriteLine("There is no '" + itemString + "' in this room"); 
            return null;
        }
    }
}
