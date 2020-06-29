using System;

namespace Zuul
{
    class Key : Item
    {
        public Key()
        {
            keptAfterUsing = true;
        }

        public Key(string name, string description, int weight, bool keptAfterUsing) : base(name, description, weight, keptAfterUsing)
        {

        }

        public override void use(Object obj)
        {
            if (obj.GetType() == typeof(Room))
            {
                Room room = (Room)obj; // must cast
                room.useKey(room);
            }
            else
            {
                // Object o is not a Room
                Console.WriteLine("Can't use a Key on Object " + obj.GetType());
            }
        }

        public override void use()
        {

        }
    }
}
