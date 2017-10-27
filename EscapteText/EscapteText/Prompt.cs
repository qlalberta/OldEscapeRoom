using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EscapeText
{
   
    public class Prompt
    {
        static public Dictionary<string, string[]> inputsToConcat = new Dictionary<string, string[]>(); // i.e., for converting phrases like "turn on" into "turnon" so they'll be easier for the computer to recognize
        static public Dictionary<string, string[]> validPrepositions = new Dictionary<string, string[]>(); // to determine whether he preposition 
        static public Dictionary<string, string> thesarus = new Dictionary<string, string>(); // to translate commands

        public Prompt() { }

        static public void Ask() // Parses the user input
        {
            if (Program.winCondition) {; } // skips to victory screen if you've already won
            else if (Program.turns > 0) // checks if you have any turns remaining
            {
                string command = "";
                if (Program.wantReset && Program.resetStep < Program.saveFile.Count)
                {
                    command = Program.saveFile[Program.resetStep];
                    Console.WriteLine($"> {command}");
                    Program.currentLog.Add(command);
                    Program.resetStep++;
                    Program.turns--;
                    if (Program.resetStep >= Program.saveFile.Count) Program.wantReset = false;
                }
                else
                {
                    while (String.IsNullOrWhiteSpace(command))
                    {
                        Console.Write($"\nYou have {Program.turns} turns left. What would you like to do? ");
                        command = Console.ReadLine().ToLower();
                        Program.turns--;
                        Program.currentLog.Add(command);
                        if (Program.turns < 1)
                        {
                            break; // ends loop if you're out of turns
                        }
                    }
                }
                if (!String.IsNullOrWhiteSpace(command))
                {
                    List<string> commands = new List<string>(command.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                    TranslateInput(ref commands);
                    if (command == "insert batteries into flashlight") Temp_Four_Word_Commands.InsertBatteriesInToFlashlight();
                    else if (command == "insert batteries into tapedeck") Temp_Four_Word_Commands.InsertBatteriesInToTapeDeck();
                    else if (command == "insert batteries into tape deck") Temp_Four_Word_Commands.InsertBatteriesInToTapeDeck();
                    else if (command == "insert batteries into tape player") Temp_Four_Word_Commands.InsertBatteriesInToTapeDeck();
                    else if (commands.Count == 1) OneWordCommands.Parse(commands);
                    else if (commands.Count == 2) TwoWordCommands.Parse(commands);
                    else MultiWordCommands.Parse(commands);
                    Ask();
                }
            }
            else Console.WriteLine("No turns left!");
        }


        static public void TranslateInput(ref List<string> prompt) // standardizes input variations into something that's easier for the computer to process
        {
            Dictionary<string, string> contextual = Location.CurrentRoom.contextualItems;
            for (var i = 0; i < prompt.Count; i++)
            {
                if (inputsToConcat.ContainsKey(prompt[i]) && i < prompt.Count - 1 && inputsToConcat[prompt[i]].Contains(prompt[i + 1]))
                {
                    prompt[i] = prompt[i] + prompt[i + 1];
                    prompt.RemoveAt(i + 1);
                }
                if (prompt[i] == "the") prompt.RemoveAt(i);
                try // using a try since deleting the word "the" will change the length length, possible resulting in an out of range error
                {
                    if (thesarus.ContainsKey(prompt[i])) prompt[i] = thesarus[prompt[i]]; // converts similar phrases into a single phrase to process, i.e., "pickup", "remove", "grab" will all become "take."
                    if (contextual.ContainsKey(prompt[i])) prompt[i] = contextual[prompt[i]]; // converts generic unmoveable items, such as doors, to room specific items
                }
                catch { }
            };
            //foreach (string s in prompt) Console.WriteLine(s); // diagnostics
        }

        static public void PreparePrompt() // creates dictionaries in advance for parser (Model)
        {
            CreateConcatDict();
            CreatePrepositionDict();
            CreateThesarus();
        }


        static public void CreateConcatDict() // a list of common concatenations
        {
            inputsToConcat.Add("turn", new String[] { "on", "off" });
            inputsToConcat.Add("pick", new String[] { "up" });
            inputsToConcat.Add("tape", new String[] { "deck", "player" });
            inputsToConcat.Add("look", new String[] { "around", "at", "inside" });
            inputsToConcat.Add("put", new String[] { "down" });
            inputsToConcat.Add("clown", new String[] { "painting" });
            inputsToConcat.Add("air", new String[] { "vent" });
            inputsToConcat.Add("dead", new String[] { "body" });
            inputsToConcat.Add("blow", new String[] { "out" });
            inputsToConcat.Add("check", new String[] { "room", "again" });
            inputsToConcat.Add("examine", new String[] { "room" });
        }

        static public void CreatePrepositionDict() // a list of valid prepositions for 4 word phrases
        {
            inputsToConcat.Add("place", new String[] { "on", "in", "into" });
        }

        static public void CreateThesarus() // converts variations of phrases
        {
            thesarus.Add("pickup", "take");
            thesarus.Add("grab", "take");
            thesarus.Add("remove", "take");
            thesarus.Add("move", "take");
            thesarus.Add("putdown", "drop");
            thesarus.Add("tapeplayer", "tapedeck");
            thesarus.Add("insert", "place");
            thesarus.Add("lookaround", "explore");
            thesarus.Add("lookat", "examine");
            thesarus.Add("inspect", "examine");
            thesarus.Add("investigate", "examine");
            thesarus.Add("deadbody", "corpse");
            thesarus.Add("search", "examine");
            thesarus.Add("blowout", "turnon");
            thesarus.Add("wall", "safe");
            thesarus.Add("lookinside", "examine");
            thesarus.Add("check", "examine");
            thesarus.Add("find", "take");
            thesarus.Add("checkagain", "explore");
            thesarus.Add("checkroom", "explore");
            thesarus.Add("examineroom", "explore");
        }

        static public List<string> AccessibleItems() // generates list of valid command items
        {
            List<string> accessible = new List<string>();
            bool canSee = Location.CanSee;
            foreach (string s in Player.inventory) accessible.Add(s);
            foreach (string s in Player.lessObviousInventory) accessible.Add(s);
            foreach (string s in Location.CurrentRoom.obviousItems) if (canSee || Item.byName[s].glowsInDark) accessible.Add(s);
            foreach (string s in Location.CurrentRoom.lessObviousItems) if (canSee || Item.byName[s].glowsInDark) accessible.Add(s);
            foreach (string s in Location.CurrentRoom.knownExits) if (canSee || Item.byName[s].glowsInDark) accessible.Add(s);
            return accessible;
        }

        static public string ClarifyItem(string generic, List<string> accessible) // TODO: If one of the items is a generic term, this method will try to determine which one the player is referring to, or prompt the player to clarify if it isn't clear
        {
            List<string> options = new List<string>();
            foreach (string s in accessible) if (Item.byName[s].genericType == generic) options.Add(s);
            if (options.Count == 0) return "invalid";
            else if (options.Count == 1) return options[0];
            else
            {
                // TODO:  Ask the player to clarify.  Present a list of options with "Item.byName[s].specificName".  Make sure to run any player input through the contextual dictionary.  An example of this is in the TranslateInput method.
                return "";
            }
        }
        
    }
}
