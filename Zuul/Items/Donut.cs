using System;

namespace Zuul
{
    class Donut : Item
    {
        public Donut()
        {
            keptAfterUsing = false;
        }

        public Donut(string name, string description, int weight, bool keptAfterUsing) : base(name, description, weight, keptAfterUsing)
        {

        }

        public override void use(Object obj)
        {
            if (obj.GetType() == typeof(Player))
            {
                Player player = (Player)obj; // must cast
                Console.WriteLine("That was a solid donut, you lost all your teeth, that cost you 20 hp");
                player.damage(20);
            }
            else
            {
                // Object o is not a Room
                Console.WriteLine("Can't use a Donut on Object " + obj.GetType());
            }
        }

        public override void use()
        {

        }
    }
}
