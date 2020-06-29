using System;

namespace Zuul
{
    class Medkit : Item
    {
        public Medkit()
        {

        }

        public Medkit(string name, string description, int weight, bool keptAfterUsing) : base(name, description, weight, keptAfterUsing)
        {

        }

        public override void use(Object obj)
        {
            if (obj.GetType() == typeof(Player))
            {
                Player player = (Player)obj; // must cast
                if (player.health != 100) { Console.WriteLine("You wrapped a bandage around your wound, you gained 30 hp"); }
                else { Console.WriteLine("That was quite a waste you had already full hp"); }
                player.heal(30);
            }
            else
            {
                // Object o is not a Room
                Console.WriteLine("Can't use a Medkit on Object " + obj.GetType());
            }
        }

        public override void use()
        {

        }
    }
}
