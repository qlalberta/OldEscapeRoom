using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
   public class CommandPrompt
    {
        public string LastInput = "";
        
        public string Read(string input = "" )
        {
            LastInput = string.IsNullOrWhiteSpace(input) ? Console.ReadLine() : input;
            return LastInput;
        }
    }
}
