using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{
    class TwoWordCommands
    {

        static public List<string> validTwoWordCommands = new List<string>() { "examine", "take", "open", "drop", "blowout", "unlock", "turnon", "turnoff", "play", "eat", "drink", "close"};

        public TwoWordCommands() { }

        static public void Parse(List<string> twoWord)
        {
            Console.WriteLine("");
            if (validTwoWordCommands.Contains(twoWord[0]))
            {
                List<string> accessible = Prompt.AccessibleItems();
                if (!accessible.Contains(twoWord[1])) Console.WriteLine($"No such item available.");
                else
                {
                    Item item = Item.byName[twoWord[1]];
                    if (twoWord[0] == "examine") Examine(ref item);
                    if (twoWord[0] == "take") Take.Act(ref item);
                    if (twoWord[0] == "open") Access.Open(ref item);
                    if (twoWord[0] == "drop") Take.Drop(ref item);
                    if (new string[] { "turnon", "play" }.Contains(twoWord[0])) Access.TurnOn(ref item);
                    if (new string[] { "turnoff", "blowout" }.Contains(twoWord[0])) Access.TurnOff(ref item);
                    if (new string[] { "unlock" }.Contains(twoWord[0])) Access.Unlock(ref item);
                    if (new string[] { "eat", "drink" }.Contains(twoWord[0])) Take.Eat(ref item);
                    if (new string[] { "close" }.Contains(twoWord[0])) Access.Close(ref item);
                }
            }
            else if (MultiWordCommands.validMultiWordCommands.Contains(twoWord[0]))
            {
                MultiWordCommands.Parse(twoWord);
            }
            else Console.WriteLine("Invalid Command");
        }

        static public void Examine(ref Item item)
        {
            if (item.examineEvent == null)
            {
                Console.WriteLine(item.examText);
                item.hasBeenExamined = true;
                if ((item.itemsInside.Count > 0 && item.isOpen) || item.itemsOutside.Count > 0)
                {
                    Console.WriteLine("You also see the following");
                    List<string> bonusItems = new List<string>();
                    foreach (string s in item.itemsOutside)
                    {
                        bonusItems.Add(s);
                        Console.WriteLine($"    {Item.byName[s].commonDescription}");
                    }
                    if (item.isOpen) foreach (string s in item.itemsInside)
                        {
                            bonusItems.Add(s);
                            Console.WriteLine($"    {Item.byName[s].commonDescription}");
                        }
                    if (Player.inventory.Contains(item.dictName)) Player.lessObviousInventory.AddRange(bonusItems);
                    else Location.CurrentRoom.lessObviousItems.AddRange(bonusItems);
                }
            }
            else
            {
                StoryEvent se = new StoryEvent(item.examineEvent);
                StoryPoints.RunStoryEvent(se);
            }
        }
    }
}
