using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{

    class Display
    {
        static public int MaxWidth = 118;
        static public string roomText;
        static public int brightness;
        static public int roomHeight = 12;

        static public void DisplayFull()
        {
            Console.Clear();
            DisplayTitle();
            Location room = Location.CurrentRoom;
            brightness = Player.lightsOn.Count + room.lightSources.Count;
            ConsoleColor fg;
            ConsoleColor bg = ConsoleColor.Black;
            if (brightness <= 0) fg = (ConsoleColor)0;
            else if (brightness == 1) fg = (ConsoleColor)6;
            else fg = (ConsoleColor)14;
            TextBlock roomDescription = new TextBlock(roomText, -3);
            TextBlock itemsText = new TextBlock(Choke.ListItems("The Room", room.obviousItems, true, true, false), -3);
            TextBlock inventoryText = new TextBlock(Choke.ListItems("Your Inventory", Player.inventory, true, true, false), -3);
            TextBlock exitsText = new TextBlock(Choke.ListItems("The Room", room.knownExits, true, false, true), -3);
            int height = itemsText.Count + inventoryText.Count + exitsText.Count + 4;
            Console.WriteLine("");
            roomDescription.Output(fg, ConsoleColor.Black);
            Console.WriteLine("\n");
            if (brightness <= 0) fg = (ConsoleColor)0;
            else if (brightness == 1) fg = (ConsoleColor)8;
            else fg = (ConsoleColor)7;
            itemsText.Output(fg, ConsoleColor.Black);
            Console.WriteLine("\n");
            inventoryText.Output(fg, ConsoleColor.Black);
            Console.WriteLine(new String('\n', roomHeight - height));
            if (brightness <= 0) fg = (ConsoleColor)8;
            exitsText.Output(fg, ConsoleColor.Black);
            Console.WriteLine("");
        }

        static public void DisplayTitle()
        {
            int m = (int) Program.turns / 6;
            int s = Program.turns % 6;
            Console.BackgroundColor = (ConsoleColor)4;
            Console.ForegroundColor = (ConsoleColor)15;
            string time = $"Life Remaining: {m} minutes and {s}0 seconds ".PadLeft(50);
            if (Program.turns <= 0) time = "You are dead ".PadLeft(50);
            string title = $" The Study Room".PadRight(68);
            Console.Write(title);
            if (Program.turns < 18) Console.ForegroundColor = (ConsoleColor)7;
            if (Program.turns < 12) Console.ForegroundColor = (ConsoleColor)8;
            if (Program.turns < 6)
            {
                Console.ForegroundColor = (ConsoleColor)0;
                Console.BackgroundColor = (ConsoleColor)12;
            }
            Console.WriteLine(time);
            Console.ResetColor();
        }

        //static public void DisplayItems()
        //{
        //    Location room = Location.CurrentRoom;
        //    Console.WriteLine("");
        //    ConsoleColor fg;
        //    if (brightness <= 0) fg = (ConsoleColor) 0;
        //    else if (brightness == 1) fg = (ConsoleColor)6;
        //    else fg = (ConsoleColor)14;
        //    TextBlock itemsText = new TextBlock(Choke.ListItems("The Room", room.obviousItems, true, true, false));
        //    itemsText.Output(fg, ConsoleColor.Black);
        //    DisplayExits();
        //}

        //static public void DisplayInventory()
        //{
        //    Console.WriteLine("");
        //    Location room = Location.CurrentRoom;
        //    ConsoleColor fg;
        //    if (brightness <= 0) fg = (ConsoleColor)6;
        //    else if (brightness == 1) fg = (ConsoleColor)6;
        //    else fg = (ConsoleColor)14;
        //    TextBlock inventoryText = new TextBlock(Choke.ListItems("Your Inventory", Player.inventory, true, true, false));
        //    inventoryText.Output(fg, ConsoleColor.Black);
        //}

        //static public void DisplayExits()
        //{
        //    Console.WriteLine("");
        //    Location room = Location.CurrentRoom;
        //    ConsoleColor fg;
        //    if (brightness <= 0) fg = (ConsoleColor)6;
        //    else if (brightness == 1) fg = (ConsoleColor)6;
        //    else fg = (ConsoleColor)14;
        //    TextBlock exitsText = new TextBlock(Choke.ListItems("The Room", room.knownExits, true, false, true));
        //    exitsText.Output(fg, ConsoleColor.Black);
        //    DisplayInventory();
        //}


        static public void Typewrite(string message, int delay = 15)
        {
            //Console.ForegroundColor;
            //Console.ForegroundColor textColor;
            if (delay > 15) delay = 15;
            if (delay < 0) delay = 0;
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(delay);
            }
        }
    }

    public struct TextBlock
    {
        public List<string> text;

        public TextBlock(List<string> t)
        {
            text = t;
        }

        public TextBlock(string t, int tab = 0, int width = 118)
        {
            // int tab is how far forward the first line will be relative to body.  If negative, then the tabe will start at zero and the rest of the body will be indented 
            int upper = 0;
            int lower = 0;
            if (tab > 0) upper = tab;
            if (tab < 0) lower = -tab;
            if (upper > Display.MaxWidth - 1) upper = Display.MaxWidth - 1;
            if (lower > Display.MaxWidth - 1) upper = Display.MaxWidth - 1;
            List<string> output;
            Choke.Wrap(t, out output, width, upper, lower);
            text = output;
        }

        public int Count
        {
            get
            { return text.Count; }
        }

        public void Output(ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            if (!Program.wantReset) foreach (string s in text) Console.WriteLine(" " + s);
            Console.ResetColor();
        }

        public void Type(int delay, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            if (!Program.wantReset) foreach (string s in text) Display.Typewrite((" " + s), 15);
            Console.ResetColor();
        }
    }
}
