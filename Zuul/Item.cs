using System;

namespace Zuul
{
    public abstract class Item
    {
        public string name;
        public string description;
        public int weight;
        public bool keptAfterUsing;

        public Item()
        {

        }

        public Item(string name, string description, int weight, bool keptAfterUsing)
        {
            this.name = name;
            this.description = description;
            this.weight = weight;
            this.keptAfterUsing = keptAfterUsing;
        }

        public virtual void use(Object o)
        {

        }

        public virtual void use()
        {

        }

        public string show()
        {
            return (this.name + "(" + this.weight + " kg): " + this.description); 
        }
    }
}
