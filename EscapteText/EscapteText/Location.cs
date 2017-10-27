using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeText
{
    public struct Setting
    {
        public string text;
        public List<string> items;

        public Setting(string t)
        {
            text = t;
            items = new List<string>();
        }

        public Setting(string t, List<string> i)
        {
            text = t;
            items = i;
        }

        // Item inventory = lantern;
        // String inventory = "lantern"
        // dict:  "lantern" : lantern
    }

    public partial class Location
    {
        static public Dictionary<int, Location> byNum = new Dictionary<int, Location>(); // dictionary of possible rooms
        static private Location currentRoom = null;
        public string name; // room name
        public int id; // room name
        public List<Setting> description = new List<Setting>();
        public List<Setting> descriptionDark = new List<Setting>();
        public List<Setting> explore = new List<Setting>();
        public List<Setting> exploreDark = new List<Setting>();
        public List<string> lightSources = new List<string>(); // if list is empty, room goes dark
        public List<string> obviousItems = new List<string>(); // items listed when you explore
        public List<string> lessObviousItems = new List<string>(); // items that can be accessed with actions, but which are not listed during exploration
        //public List<string> unknownItems = new List<string>(); // items which are hidden
        public List<string> knownExits = new List<string>(); // pretty self-explanatory
        public Dictionary<string, string> contextualItems = new Dictionary<string, string>(); // changes general item calls to room specific item calls. (i.e., "safe" becomes "studyroomsafe" when accessed in the study room."  Only works for unmoveable items.
        public bool visited = false;
        public bool explored = false;

        static public Location CurrentRoom // pulls up the current room
        {
            get { return currentRoom; }
            set { currentRoom = value;}
        }
        static public bool CanSee // states whether or not the user has light
        { get { return (Player.HasLightOn || CurrentRoom.lightSources.Count > 0); }}

        public Location(int id, string n)
        {
            this.name = n;
            this.id = id;
            byNum.Add(id, this);
        }

        static public void Explore()
        {
            bool vision = CanSee;
            List<Setting> settings = (vision) ? currentRoom.explore : currentRoom.exploreDark;
            int count = settings.Count;
            Setting prioritySetting;
            List<string> newIems = new List<string>();
            if (count > 1) prioritySetting = settings[count - 1];
            else prioritySetting = settings[0];
            Display.roomText = prioritySetting.text;
            foreach (string s in prioritySetting.items) if (!currentRoom.obviousItems.Contains(s)) currentRoom.obviousItems.Add(s);
            if (count > 1) settings.RemoveAt(count - 1);
            else settings[0].items.Clear();
            //List<string> items = new List<string>();
            //foreach (string s in currentRoom.obviousItems)
            //{
            //    if (vision) items.Add(Item.byName[s].commonDescription);
            //    else if (Item.byName[s].glowsInDark) items.Add(Item.byName[s].commonDescriptionDark);
            //}
            //if (items.Count > 0 && count == 1)
            //{
            //    Console.WriteLine("\nYou see the following items:");
            //    foreach (string s in items) Console.WriteLine($"    {s}");
            //}
            //if (currentRoom.knownExits.Count > 0 && count == 1)
            //{
            //    Console.WriteLine("\nYou have access to the following exits:");
            //    foreach (string s in currentRoom.knownExits) Console.WriteLine($"    {Item.byName[s].commonDescription}");
            //}
            Display.DisplayFull();
        }

        static public void Enter()
        {
            Console.WriteLine($"You have entered {currentRoom.name.CapsDown()}");
            bool vision = CanSee;
            List<Setting> settings = (vision) ? currentRoom.description : currentRoom.descriptionDark;
            int count = settings.Count;
            if (count > 0)
            {
                Setting prioritySetting = settings[count - 1];
                Display.roomText = prioritySetting.text;
                //Console.WriteLine(prioritySetting.text);
                foreach (string s in prioritySetting.items) if (!currentRoom.obviousItems.Contains(s)) currentRoom.obviousItems.Add(s);
                settings.RemoveAt(count - 1);
            }
            Display.DisplayFull();
        }

        static public void ResetRooms() // resets all rooms for start of game
        {
            byNum = new Dictionary<int, Location>();
            Item.byName = new Dictionary<string, Item>();
            ResetStudyRoom();
        }
    }
}