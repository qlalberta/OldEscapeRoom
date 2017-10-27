using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{
    class MultiWordCommands
    {
        static public List<string> validMultiWordCommands = new List<string>() { "place" };

        public MultiWordCommands() { }

        static public void Parse(List<string> multiWord)
        {
            Console.WriteLine("Program not equipt for this command");
        }
    }
}
