using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace ReferenceGame
{
    public class Parse
    {
        public String[] commands;
        public Game game;
        public Parse(string s, Game game)
        {
            s = string.IsNullOrWhiteSpace(s) ? "HELP" : s;
            commands = s.ToLower().Split(' ');
            this.game = game;
        }

        public void ExecuteCommand()
        {
            switch (commands[0])
            {
                case "help":
                    game.display.Show("1.Examine; 2. Explore; 3.Quit");
                    break;
                case "examine":
                    Examine();
                    break;
                case "explore":
                    Explore();
                    break;
                default:
                    game.display.Show("Command not found. Type help for a list of commands.");
                    break;
            }
        }

        void Explore()
        {
            game.display.Show("\nYou see the following items:");
            game.display.Show(game.CurrentRoom.GetFoundItemNames());
        }



        void Examine()
        {
            string keyOfItem = "";
            for (var i = 1; i < commands.Length; i += 1)
            {
                keyOfItem += commands[i];
            }
            var item = game.CurrentRoom.GetItemWithKey(keyOfItem);

            if (item != null)
            {
                if (game.CurrentRoom.DidUserFindExit(item))
                {
                     game.display.Show("You won!!!!!!!!!!!!", ConsoleColor.Cyan);
                    return;
                } else
                {
                    game.display.Show(item.Message);
                }
                if (item.GetChildren().Count > 0)
                {
                    var text = "You've found the following item" + (item.GetChildren().Count == 1 ? ":" : "s:");
                    game.display.Show(text);
                    game.display.Show(item.GetChildrenLongNames());

                    foreach( var itemChild in item.GetChildren())
                    {
                        game.CurrentRoom.AddFoundItem(itemChild);
                    }
                }
               
            } else
            {
                game.display.Show("Item not found.");
            }

        }

    }
}
