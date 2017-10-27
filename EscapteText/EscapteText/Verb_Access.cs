using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{
    class Access
    {
        static public bool Open(ref Item item)
        {
            string name = item.dictName;
            if (item.openEvent != null)
            {
                StoryEvent se = new StoryEvent(item.takeEvent);
                return(StoryPoints.RunStoryEvent(se));
            }
            else if (!item.canOpen)
            {
                Console.WriteLine("There's nothing to open.");
                return false;
            }
            else if (item.isOpen)
            {
                Console.WriteLine($"{item.commonDescription} is already open.");
                return false;
            }
            else if (item.isLocked)
            {
                Console.WriteLine($"{item.commonDescription} is locked.");
                return false;
            }
            else
            {
                item.isOpen = true;
                Console.WriteLine($"{item.commonDescription} is now open.");
                if (item.dictName == "flashlight")
                {
                    item.isLightSource = false;
                    item.canLight = false;
                    if (Player.lightsCarried.Contains("flashlight")) Player.lightsCarried.Remove("flashlight");
                    if (Player.lightsOn.Contains("flashlight")) Player.lightsOn.Remove("flashlight");
                }
                if (item.itemsInside.Count > 0)
                {
                    Console.WriteLine("You now have access to the following");
                    foreach (string s in item.itemsInside)
                    {
                        Console.WriteLine($"    {Item.byName[s].commonDescription}");
                    }
                    if (Player.inventory.Contains(item.dictName)) Player.lessObviousInventory.AddRange(item.itemsInside);
                    else Location.CurrentRoom.lessObviousItems.AddRange(item.itemsInside);
                }
                return true;
            }

        }



        static public bool Unlock(ref Item item)
        {
            string name = item.dictName;
            Location room = Location.CurrentRoom;
            if (!item.canLock)
            {
                Console.WriteLine("There's no lock.");
                return false;
            }
            if (!item.isLocked)
            {
                Console.WriteLine("It's already unlocked.");
                return false;
            }
            if (item.unlockCodes.Count > 0)
            {
                string attempt = "bad";
                bool cheat = false;
                do
                {
                    Console.WriteLine($"You must enter a four digit combination.  {Program.turns} turns left.");
                    Console.Write("Or you can 'quit' > ");
                    attempt = Console.ReadLine();
                    Program.turns -= 1;
                    if (item.unlockCodes.Contains(attempt) && !item.comboKnown)
                    {
                        Console.WriteLine("You feel that should have worked...  maybe you typed it in wrong.");
                        cheat = true;
                    }
                }
                while (attempt.ToLower() != "quit" && !cheat && Program.turns > 0 && !item.unlockCodes.Contains(attempt));
                if (attempt.ToLower() == "quit" || Program.turns == 0)
                {
                    Console.WriteLine("You failed!");
                    Console.WriteLine("Which is strange, because you felt like that should have worked.  Maybe you typed it in wrong?");
                    return false;
                }
                else
                {
                    Console.WriteLine($"{item.commonDescription} is now unlocked");
                    item.isLocked = false;
                    return true;
                }
            }
            else if (item.unlockItems.Count > 0)
            {
                Console.WriteLine("With what? ");
                Console.Write(">  ");
                string answer = Console.ReadLine();
                List<string> arr = new List<string>(answer.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                Prompt.TranslateInput(ref arr);
                if ((arr.Count > 0 && item.unlockItems.Contains(arr[0])) || (arr.Count > 1 && item.unlockItems.Contains(arr[1])))
                {
                    if (Player.inventory.Contains("studyroomkey"))
                    {
                        Console.WriteLine("\nYou've successfully opened the door.\n");
                        Console.WriteLine("Congratulations on making it through the first room!  More to come soon!\n");
                        Program.winCondition = true;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("You don't have a key.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("That won't work.");
                    return false;
                }

            }
            return false;
        }


        static public bool Close(ref Item item)
        {
            string name = item.dictName;
            if (!item.canOpen)
            {
                Console.WriteLine("There's nothing to close");
                return false;
            }
            else if (!item.isOpen)
            {
                Console.WriteLine("It's already closed");
                return false;
            }
            else
            {
                Console.WriteLine("You close the item");
                item.isOpen = false;
                if (item.dictName == "flashlight" && item.itemsInside.Count > 0)
                {
                    item.canLight = true;
                    Player.lightsCarried.Add("flashlight");
                }
                return (true);
            }
        }

        static public bool TurnOff(ref Item item)
        {
            string name = item.dictName;
            Location room = Location.CurrentRoom;
            if (name == "flashlight" || name == "studyroomlantern")
            {
                if (!item.isLightSource)
                {
                    Console.WriteLine("It's already off");
                    return false;
                }
                item.isLightSource = false;
                if (room.lightSources.Contains(name)) room.lightSources.Remove(name);
                if (Player.lightsOn.Contains(name)) Player.lightsOn.Remove(name);
                if (Player.lightsCarried.Contains(name) && name == "studyroomlantern") Player.lightsCarried.Remove(name);
                Console.WriteLine($"You turn off {item.commonDescription}");
                if (!Location.CanSee)
                {
                    Console.WriteLine("It is now totally dark");
                    if (Player.lightsCarried.Count == 0)
                    {
                        Console.WriteLine("And now you're stumbling around with no light source and will likely die.");
                    }
                }
                return true;
            }
            else if (name == "studyroomsafe")
            {
                Console.WriteLine("Nice try.");
                return false;
            }
            else
            {
                Console.WriteLine("There's no off switch");
                return false;
            }
        }


        static public bool TurnOn(ref Item item)
        {
            string name = item.dictName;
            Location room = Location.CurrentRoom;
            if (name == "studyroomlantern")
            {
                Console.WriteLine("You have nothing to light it with.");
                return false;
            }
            else if (name == "flashlight")
            {
                Item flashlight = Item.byName["flashlight"];
                if (!Player.inventory.Contains("flashlight"))
                {
                    Console.WriteLine("You aren't holding one.");
                    return false;
                }
                else if (flashlight.itemsInside.Count == 0)
                {
                    Console.WriteLine("You have no batteries.");
                    return false;
                }
                else if (flashlight.isOpen)
                {
                    Console.WriteLine("Close it first.");
                    return false;
                }
                else
                {
                    flashlight.isLightSource = true;
                    if (!Player.lightsOn.Contains("flashlight")) Player.lightsOn.Add("flashlight");
                    Console.WriteLine("It provides some extra illumination");
                    return true;
                }
            }
            else if (name == "studyroomcorpse")
            {
                Console.WriteLine("No.  Just.... no.");
                return false;
            }
            else if (name == "studyroomtape" || name == "tapedeck")
            {
                Item tapedeck = Item.byName["tapedeck"];
                if (!tapedeck.itemsInside.Contains("flashlightbatteries"))
                {
                    Console.WriteLine("There are no batteries.");
                    return false;
                }
                if (!tapedeck.itemsInside.Contains("studyroomtape"))
                {
                    Console.WriteLine("There is no tape.");
                    return false;
                }
                else
                {
                    Console.WriteLine("\nThe tape stirs to whir, and you hear a recording...\n");
                    Console.WriteLine("To whoever's is listening today:  The bad new is that you've been attacked and poison.  You don't have long to live.  The good news is that there's a cure, if you're clever enough to find it.  The person on the ground next to you?  Not so clever.\n");
                    Console.WriteLine("Along the way, you'll find various medications.  They won't cure the infection, but they will extend your lifespan enough.  Maybe enough to make a different.\n");
                    Console.WriteLine("But it won't be easy.\n\nSo here's a hint:  I peg your chance at survival as 10 to 1.  Remember that. And stay safe.\n");
                    Item.byName["studyroomsafe"].comboKnown = true;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("There's no on switch.");
                return false;
            }
        }

    }
}