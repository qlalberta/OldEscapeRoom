using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{
    class OneWordCommands
    {

        static public List<string> validOneWordCommands = new List<string>() { "win", "explore", "inventory", "access" , "help", "move", "save", "load"};

        public OneWordCommands() { }

        static public void Parse(List<string> oneWord)
        {
            if (validOneWordCommands.Contains(oneWord[0]))
            {
                if (oneWord[0] == "win") Win(); // does series of if/else if checkes to determine which method to operate
                else if (oneWord[0] == "explore") Explore();
                else if (oneWord[0] == "inventory") Inventory();
                else if (oneWord[0] == "move") Move();
                else if (oneWord[0] == "access") Access();
                else if (oneWord[0] == "save") Save();
                else if (oneWord[0] == "load") Load();
                else if (oneWord[0] == "help") Help();
                else
                {
                    Console.WriteLine("Invalid command!");
                }
            }
            else if (TwoWordCommands.validTwoWordCommands.Contains(oneWord[0]) || MultiWordCommands.validMultiWordCommands.Contains(oneWord[0]))
            {
                Console.Write($"{oneWord[0]} what? "); // asks for more works if command is recognized but not complete
                List<string> commands = new List<string>(Console.ReadLine().ToLower().Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                Prompt.TranslateInput(ref commands);
                if (oneWord[0] != commands[0]) commands.Insert(0, oneWord[0]); // checcks to see if original verb was repeated
                if (commands.Count == 1) Parse(commands);
                else if (commands.Count == 2) TwoWordCommands.Parse(commands);
                else MultiWordCommands.Parse(commands);
            }
            else Console.WriteLine("Invalid Command");
        }

        public static void Save()
        {
            Console.WriteLine($"Game Saved {Program.currentLog.Count}");
            Program.saveFile = new List<string>();
            foreach (string s in Program.currentLog) if (s != "save") Program.saveFile.Add(s);
            Program.turns++;
        }


        public static void Help()
        {
            Console.WriteLine($"Basic commands:  Explore, Examine, Inventory, Take, Open, Unlock, Save, Load");
            Program.turns += 1;
        }


        public static void Load()
        {
            Console.WriteLine($"\n\n\n\n\n\n\n\n\n\n\n\nLOADING GAME\n\n\n\n\n\n\n\n\n\n\n\n");
            Program.wantReset = true;
            Program.resetStep = 0;
            Program.LoadGame();
        }

        public static void Win() // example of method
        {
            Program.winCondition = true;
        }


        public static void Move() // example of method
        {
            if (Location.CurrentRoom.knownExits.Count > 0)
            {
                Console.WriteLine("\nYou are aware of the following exits:");
                foreach (string s in Location.CurrentRoom.knownExits) Console.WriteLine($"    {Item.byName[s].commonDescription}");
            }
        }

        public static void Access() 
        {
            foreach (string s in Prompt.AccessibleItems()) Console.WriteLine(s);
        }

        public static void Inventory() // example of method
        {
            Console.WriteLine(Choke.ListItems("Your inventory", Player.inventory));
            if (Player.inventory.Count == 0) Console.WriteLine("You aren't carrying anything.");
            else
            {
                Console.WriteLine("You are carrying the following items:");
                foreach (string s in Player.inventory) Console.WriteLine(Item.byName[s].commonDescription);
            }
            Console.WriteLine($"Captacity {Player.carryCapacity}");
        }

        public static void Explore()
        {
            Location.Explore();
        }
    }
}
