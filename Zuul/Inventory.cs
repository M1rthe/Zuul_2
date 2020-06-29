using System;
using System.Collections.Generic;

namespace Zuul
{
    public class Inventory
    {
        private int maxWeight;
        public List<Item> items = new List<Item>();

        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
        }

        // take an item
        public Item take(Item item)
        {
            if (item != null)
            {
                if (item.weight + totalWeight() < maxWeight)
                {
                    items.Add(item);
                    return item;
                } else { Console.WriteLine("The " + item.name + " is " + (item.weight + totalWeight() - maxWeight) + "kg too heavy"); }
            }           
            return null;
        }

        // drop an item
        public Item drop(Item item)
        {
            if (item != null)
            {
                items.Remove(item);
                return item;
            }
            return null;
        }

        // put
        public bool put(Item item)
        {
            if (item.weight + totalWeight() < maxWeight)
            {
                items.Add(item);
                return true;
            }
            return false;
        }

        private int totalWeight()
        {
            int weight = 0;

            for(int i = 0; i < items.Count; i++)
            {
                weight += items[i].weight;
            }

            return weight;
        }

        public string getDescription(string inventory)
        {
            string returnstring = inventory+"("+ totalWeight() + "/" + maxWeight + " kg): \n";  

            for (int i = 0; i < items.Count; i++)
            {
                returnstring += "-" + items[i].name + "(" + items[i].weight + " kg) \n";
            }

            if (items.Count < 1) { returnstring += "-none \n"; }

            return returnstring;
        }
    }
}
