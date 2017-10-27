using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Display
    {
        public string LastShow = " ";

        private void WriteLine(string text)
        {
            Console.WriteLine(text);
            LastShow = text;
        }
        public void Show(string text, ConsoleColor txColor = ConsoleColor.White)
        {
            Console.ForegroundColor = txColor;
            WriteLine(text);
            Console.ResetColor();
        }

        public void Show(List<string> textList, ConsoleColor txColor = ConsoleColor.Green)
        {
            string temp = "";
            foreach (string key in textList)
            {
                temp += key;
                temp += Environment.NewLine;
            }
            Show(temp, txColor);
        }

        public void Output(string text, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            Console.Write(text);
            Console.ResetColor();
        }

    }
}

