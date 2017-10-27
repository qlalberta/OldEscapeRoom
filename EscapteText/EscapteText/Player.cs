using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{
    class Player
    {
        static public int location;
        static public List<string> lightsCarried = new List<string>();
        static public List<string> lightsOn = new List<string>();
        static public int carryCapacity; // how much weight the character can carry
        static public List<string> inventory = new List<string>(); // items that the player is carrying
        static public List<string> lessObviousInventory = new List<string>(); // items within other items
        static public string examining; // the current item the character is interacting with
        
        public Player() { }

        static public void ResetPlayer ()
        {
            location = 1;
            lightsCarried = new List<string>();
            lightsOn = new List<string>();
            carryCapacity = 5;
            inventory = new List<string>();
            //inventory.Add("clownpainting"); //for diagnostics
            examining = "";
        }

        public void Test()
        { }

        static public bool HasLightOn
        {
            get { return lightsOn.Count > 0; }
        }

        static public bool HasLightCarried
        {
            get { return lightsCarried.Count > 0; }
        }

        static public List<string> HasAccessTo(string i) // TODO: Add method that will return a combined list of player's inventory and readily accessible room items which the player can interact with.
        {
            return (new List<string>());
        }


        static public string ClarifyItem(string s) // TODO: If the player enters a short hand name for an object (i.e., stating "key" without specifying which one), the game will check how many objects the player can access which match that description.  If it's only one, then it will return that object.  If it's multiple, the game will provide a choice.  If it's none, the game will say that the player has no access. 
        {
            Console.WriteLine($"Which {s} do you mean? ");
            return (Console.ReadLine());
        }

    }
}
