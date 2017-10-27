using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeText
{
    public class Temp_Four_Word_Commands
    {
        
        static public void InsertBatteriesInToFlashlight()
        {
            List<string> accessible = Prompt.AccessibleItems();
            Item flashlight = Item.byName["flashlight"];
            Item batteries = Item.byName["flashlightbatteries"];
            if (!Player.inventory.Contains("flashlightbatteries"))
            {
                Console.WriteLine("You aren't carrying any batteries");
            }
            else if ((!accessible.Contains("flashlight")))
            {
                Console.WriteLine("There is no flashlight");
            }
            else if (!flashlight.isOpen)
            {
                Console.WriteLine("Your flashlight is closed.");
            }
            else
            {
                Console.WriteLine("You place the batteries inside the flashlight");
                flashlight.itemsInside.Add("flashlightbatteries");
                Player.inventory.Remove("flashlightbatteries");
                Player.carryCapacity += batteries.size;
            }
        }
        
        static public void InsertBatteriesInToTapeDeck()
        {
            List<string> accessible = Prompt.AccessibleItems();
            Item tapedeck = Item.byName["tapedeck"];
            Item batteries = Item.byName["flashlightbatteries"];
            if (!Player.inventory.Contains("flashlightbatteries"))
            {
                Console.WriteLine("You aren't carrying any batteries");
            }
            else if (!accessible.Contains("tapedeck"))
            {
                Console.WriteLine("There is no tape deck");
            }
            else if (!tapedeck.isOpen)
            {
                Console.WriteLine("The tape deck is closed.");
            }
            else
            {
                Console.WriteLine("You place the batteries inside the tape deck.");
                tapedeck.itemsInside.Add("flashlightbatteries");
                Location.CurrentRoom.obviousItems.Add("flashlightbatteries");
                Player.inventory.Remove("flashlightbatteries");
                Player.carryCapacity += batteries.size;
            }
        }
    }
}
