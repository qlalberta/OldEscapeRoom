using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{
    class Take
    {

        static public bool Act(ref Item item)
        {
            string name = item.dictName;
            if (item.takeEvent != null)
            {
                StoryEvent se = new StoryEvent(item.takeEvent);
                return(StoryPoints.RunStoryEvent(se));
            }
            else
            {
                if (new string[] { "door", "airVent", "wall", "ceiling", "floor" }.Contains(item.genericType)) return (PartOfBuilding(name));
                else if (item.portable) return (TakeItem(ref item));
                else return (TooHeavy(item.commonDescription));
            }
        }
        

        static bool PartOfBuilding(string i)
        {
            Console.WriteLine($"{i} is part of the building.");
            return (false);
        }

        static public bool TakeItem(ref Item i)
        {
            if (Player.inventory.Contains(i.dictName))
            {
                Console.WriteLine("You're already holding that!");
                return false;
            }
            if (i.size <= Player.carryCapacity)
            {
                Player.carryCapacity -= i.size;
                Console.WriteLine($"You take {i.commonDescription.CapsDown()}");
                Location room = Location.CurrentRoom;
                if (room.obviousItems.Contains(i.dictName)) room.obviousItems.Remove(i.dictName);
                if (room.lightSources.Contains(i.dictName)) room.lightSources.Remove(i.dictName);
                if (Player.lessObviousInventory.Contains(i.dictName)) Player.lessObviousInventory.Remove(i.dictName);
                if (room.lessObviousItems.Contains(i.dictName)) room.lessObviousItems.Remove(i.dictName);
                if (i.locatedInObject != null)
                {
                    Item holder = Item.byName[i.locatedInObject];
                    if (holder.itemsInside.Contains(i.dictName)) holder.itemsInside.Remove(i.dictName);
                    if (holder.itemsOutside.Contains(i.dictName)) holder.itemsOutside.Remove(i.dictName);
                }
                string name = i.dictName;
                Player.inventory.Add(name);
                if (i.canLight) Player.lightsCarried.Add(name);
                if (i.isLightSource) Player.lightsOn.Add(name);
                return (true);
            }
            Console.WriteLine("Your hands are full.  You need to drop some items first.");
            return (false);
        }

        static bool TooHeavy(string i)
        {
            Console.WriteLine($"{i} is too heavy.");
            return (false);
        }

        static public void Drop(ref Item item)
        {
            string name = item.dictName;
            if (Player.inventory.Contains(name))
            {
                Player.carryCapacity += item.size;

                Player.inventory.Remove(name);
                if (Player.lightsCarried.Contains(item.dictName)) Player.lightsCarried.Remove(item.dictName);
                if (Player.lightsOn.Contains(item.dictName))
                {
                    Player.lightsOn.Remove(item.dictName);
                    Location.CurrentRoom.lightSources.Add(item.dictName);
                }
                Location.CurrentRoom.obviousItems.Add(name);
                Console.WriteLine($"{item.commonDescription} gets dropped");
            }
            else Console.WriteLine($"You aren't carrying {item.commonDescription}");
        }
        
        static public bool Eat (ref Item item)
        {
            string name = item.dictName;
            if (item.edible)
            {
                if (name == "greenmedicine")
                {
                    Console.WriteLine("You find the medicine rejuvenating you, giving you extra time...");
                    Program.turns += 51;
                    if (Player.inventory.Contains("greenmedicine")) Player.inventory.Remove("greenmedicine");
                    Player.carryCapacity += item.size;
                    return true;
                }
                Console.WriteLine($"Did I accidentally make {item.commonDescription} edible?  Must be a programmer glitch.");
                return true;
            }
            else
            {
                if (!item.portable)
                {
                    Console.WriteLine("Good luck with that!");
                }
                else Console.WriteLine("You would give yourself a stomachache...");
                return false;
            }
        }
    }
}
