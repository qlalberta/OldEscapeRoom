using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    class View
    {
        static public int MaxWidth = 118;

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

      
        public List<string> TextBlock(string t, int tab = 0, int width = 118)
        {
            // int tab is how far forward the first line will be relative to body.  If negative, then the tabe will start at zero and the rest of the body will be indented 
            int upper = 0;
            int lower = 0;
            if (tab > 0) upper = tab;
            if (tab < 0) lower = -tab;
            if (upper > MaxWidth - 1) upper = MaxWidth - 1;
            if (lower > MaxWidth - 1) upper = MaxWidth - 1;
            List<string> output;
            Wrap(t, out output, width, upper, lower);
            return output;
        }
    }
}
