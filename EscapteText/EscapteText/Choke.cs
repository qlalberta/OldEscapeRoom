using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{


    public class Choke
    {
        static public int maxWidth = 30;
        static public bool lastLineHadBreak = false;

        static public int MaxWidth
        {
            get { return maxWidth; }
            set { maxWidth = value; }
        }


        static public void Wrap(string str, out List<string> final, int width = 118, int upperMargin = 0, int lowerMargin = 0)
        {
            if (str == null) final = new List<string>();
            List<string> output = new List<string>();
            str = new string(' ', upperMargin) + str;
            string lower = new string(' ', lowerMargin);
            while (str.Substring(str.Length - 1) == "\n") str = str.Substring(0, str.Length - 1);
            while (str.Length > width || str.IndexOf("\n") != -1 || str[0] == '\n')
            {
                bool newLineBreak = false;
                int lineBreak = str.IndexOf("\n");
                int lastSpace = str.Substring(0, width).LastIndexOf(" ");
                int space;
                if (lineBreak == 0)
                {
                    output.Add("");
                    str = new string(' ', upperMargin) + str.Substring(1);
                    newLineBreak = true;
                    lastSpace = str.Substring(0, width).LastIndexOf(" ");
                }
                if (lineBreak > 0 && lineBreak < width) space = lineBreak;
                else if (lastSpace > upperMargin && lastSpace < width) space = lastSpace;
                else space = width - 1;
                output.Add(str.Substring(0, space));
                str = str.Substring(space);
                if (space < str.Length && str[0] != '\n') str = new string(' ', lowerMargin) + str;
            }
            output.Add(str);
            final = output;
        }

        static public List<string> ListSubItems(string name)
        {
            Item item = Item.byName[name];
            if (item.Visible && item.hasBeenExamined)
            {
                List<string> innerItems = new List<string>();
                foreach (string sc in item.itemsInside) innerItems.Add(Item.byName[sc].commonDescription.IsolateSubject());
                foreach (string sc in item.itemsOutside) if (Item.byName[sc].Visible) innerItems.Add(Item.byName[sc].commonDescription.IsolateSubject());
                return innerItems;
            }
            return new List<string>();
        }

        static public string ExpandItem(string name)
        {
            Item item = Item.byName[name];
            List<string> innerItems = ListSubItems(name);
            string subItemString = "";
            if (innerItems.Count > 0)
            {
                foreach (string sc in innerItems) subItemString = subItemString + ", " + sc;
                subItemString = subItemString.Substring(2);
            }
            return subItemString;
        }

        static public string ListItems(string container, List<string> contents, bool showEmpty = false, bool showSubContents = false, bool exits = false)
        {
            //Item item = Item.byName[itemName];

            // TODO: add checks to see if item is visible
            List<string> tempItems = new List<string>();
            foreach (string c in contents)
            {
                Item item = Item.byName[c];
                string itemPlusContents = item.commonDescription.CapsA();
                if (showSubContents && !exits)
                {
                    List<string> innerItems = ListSubItems(c);
                    string subItemString = ExpandItem(c);
                    if (subItemString.Length > 0) subItemString = " +(" + subItemString + ")";
                    itemPlusContents = itemPlusContents + subItemString;
                }
                if (item.Visible) tempItems.Add(itemPlusContents);
            }
            if (tempItems.Count == 0)
            {
                if (showEmpty) return $"{container} is empty.";
                if (exits) return $"{container} has no exits.";
                else return null;
            }
            //string s = $"{item.commonDescription.CapThe()} contains the following: ";
            string s = "";
            foreach (string listedItem in tempItems) s = s + ", " + listedItem;
            if (exits) return $"{container} contains the following exits: " + s.Substring(2);
            return $"{container} contains the following items: " + s.Substring(2);
        }

        static public List<string> Input(string prompt)
        {
            // TODO: this is where we would add the save/load functions
            // if Program.wantReset = true, you load c from a file
            // otherwise, you state the prompt and set c to null, as before (see 
            Console.Write(prompt);
            String c = null;
            
            while (String.IsNullOrWhiteSpace(c))
            {
                c = Convert.ToString(Console.ReadLine());
            }
            // load c to current log & json
            return (new List<string>());
        }

    }
}