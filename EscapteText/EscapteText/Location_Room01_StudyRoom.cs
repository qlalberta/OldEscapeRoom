using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeText
{
    partial class Location
    {
        static void ResetStudyRoom() // example of room creation
        {
            Location studyRoom = new Location(1, "The Study Room");
            currentRoom = studyRoom;
            studyRoom.lightSources.AddRange(new String[] { "studyroomlantern" });
            studyRoom.knownExits.AddRange(new String[] { "studyroomdoor" });

            studyRoom.description.Add(new Setting("You wake up.  Barely.  You are injured with a gaping wound, in a room you don't recognize.  How you got here is a mystery.  There is a very feint light from a lantern next to you.  Maybe you should explore?", new List<string>() { "studyroomlantern" }));
            studyRoom.explore.Add(new Setting("This looks like it used to be someone's office. You can't imagine anyone being able to work here in its current state.", new List<string>() { "clownpainting", "studyroomdesk"}));
            studyRoom.explore.Add(new Setting("You turn your head, and immediately regret it.  You stare into cold, dead eyes.  Belonging to a corpse with injuries similar to your own.  You instinctively reel back in horror, nearly knocking the lantern over and setting the building ablaze.  In your haste, you barely got a look of the room around you.  Maybe you should check again?", new List<string>() { "studyroomcorpse" }));
            studyRoom.exploreDark.Add(new Setting("It's too dark to see."));

            studyRoom.contextualItems.Add("safe", "studyroomsafe");
            studyRoom.contextualItems.Add("desk", "studyroomdesk");
            studyRoom.contextualItems.Add("drawer", "studyroomdeskdrawer");
            studyRoom.contextualItems.Add("vent", "studyroomvent");
            studyRoom.contextualItems.Add("airvent", "studyroomvent");
            studyRoom.contextualItems.Add("corpse", "studyroomcorpse");
            studyRoom.contextualItems.Add("body", "studyroomcorpse");
            studyRoom.contextualItems.Add("door", "studyroomdoor");
            studyRoom.contextualItems.Add("tape", "tapedeck");
            studyRoom.contextualItems.Add("medicine", "greenmedicine");
            studyRoom.contextualItems.Add("wallsafe", "studyroomsafe");
            studyRoom.contextualItems.Add("lantern", "studyroomlantern");
            studyRoom.contextualItems.Add("batteries", "flashlightbatteries");
            studyRoom.contextualItems.Add("painting", "clownpainting"); // TODO:  Remove from contextual dictionary later.  Painting doesn't count as a contextual item because it can be moved.
            studyRoom.contextualItems.Add("key", "studyroomkey");


            Item studyRoomLantern = new Item("studyroomlantern", 2, true, "A Lantern", "Study Room Lantern", "lantern");
            studyRoomLantern.MakeLightSouce(true);
            studyRoomLantern.examText = "A lantern.  Hopefully you'll have enough fuel to get you through this.";

            Item flashlightBatteries = new Item("flashlightbatteries", 1, true, "A set of flashlight batteries", "Flashlight Batteries", "batteries");
            flashlightBatteries.locatedInObject = "flashlight";
            flashlightBatteries.examText = "Maybe they can fit in other items?";
            flashlightBatteries.specialFittings.AddRange(new String[] { "flashlight", "tape deck" });

            Item flashlight = new Item("flashlight", 2, true, "A Flashlight");
            flashlight.HoldItemsInside(new string[] { "flashlightbatteries" });
            flashlight.MakeLightSouce(false);
            flashlight.locatedInObject = "studyroomcorpse";
            flashlight.examText = "A handy dandy flashlight. Very useful for getting around.";

            Item studyroomcorpse = new Item("studyroomcorpse", 200,false, "A corpse on the ground");
            studyroomcorpse.HoldItemsOutside(new string[] { "flashlight" });
            studyroomcorpse.examText = "You're still trying to get over the shock, but right now you have to worry about surviving.";

            Item studyRoomKey = new Item("studyroomkey", 1, true, "Study Room Key", "Study Room Key", "key");
            studyRoomKey.locatedInObject = "studyroomsafe";
            studyRoomKey.specialPairings.Add("studyroomdoor");
            studyRoomKey.examText = "The key to the exit";

            Item medicine = new Item("greenmedicine", 1, true, "Green Medicine", "Green Medicine", "medicine");
            medicine.locatedInObject = "studyroomsafe";
            medicine.examText = "A vial of green liquid.";
            medicine.edible = true;

            Item studyRoomSafe = new Item("studyroomsafe", 50, false, "A Wall Safe", "Wall Safe", "safe");
            studyRoomSafe.HoldItemsInside(new string[] { "greenmedicine", "studyroomkey" });
            studyRoomSafe.LockWithCode(new string[] { "1021" });
            studyRoomSafe.examText = "A heavily fortified safe.";

            Item studyRoomDoor = new Item("studyroomdoor", 50, false, "A door", "A door", "door");
            studyRoomDoor.ConvertToDoor(new string[] { "studyroomkey" });
            studyRoomDoor.examText = "A worn down doorway.";


            Item studyRoomTape = new Item("studyroomtape", 1, true, "A tape reel", "Study room tape", "tape");
            studyRoomTape.locatedInObject = "tapedeck";
            studyRoomTape.examText = "I wonder what messages could be on this?";
            studyRoomTape.specialFittings.AddRange(new String[] { "tapedeck" });

            Item tapeDeck = new Item("tapedeck", 4, true, "A Tape Deck", "Study room tape player", "tapedeck");
            tapeDeck.locatedInObject = "studyroomdesk";
            tapeDeck.examText = "An old fashioned player.";
            tapeDeck.HoldItemsInside(new string[] { "studyroomtape" });

            Item studyRoomDesk = new Item("studyroomdesk", 100, true, "An office desk", "Study Room Desk", "desk");
            studyRoomDesk.examText = "Nothing too remarkable.";
            studyRoomDesk.HoldItemsOutside(new string[] { "tapedeck" });
            studyRoomDesk.isLocked = true;
            studyRoomDesk.canLock = true;
            studyRoomDesk.isOpen = false;
            studyRoomDesk.canOpen = true;

            Item clownPainting = new Item("clownpainting", 4, true, "An oversized painting", "Clown Painting", "painting");
            clownPainting.examText = "It's a painting of clowns playing poker.";
            clownPainting.takeEvent = TakeClownPainting;
        }

        static public bool TakeClownPainting()
        {
            Item temp = Item.byName["clownpainting"];
            bool success = Take.TakeItem(ref temp);
            if (success)
            {
                Console.WriteLine("You discover a wall safe behind the painting.");
                currentRoom.obviousItems.Add("studyroomsafe");
                temp.takeEvent = null;
            }
            return (success);
        }
    }
}