using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{
    public class Program
    {
        static public bool winCondition = false; // states whether or not the player has met the end objectives
        static public int turns; // introduces a time limit based on turns
        static public List<string> itemShorthand = new List<string>() { "key" }; // for when one word could refer to multiple items
        static public List<string> currentLog;
        static public List<string> saveFile;
        static public bool wantReset = false;
        static public int resetStep = 0;

        static void Main(string[] args)
        {
            Prompt.PreparePrompt(); // creates dictionaries for prompt to refer to
            LoadGame(); // generates the load screen
            string tryAgain = ""; // after player loses, the player can try again
            while (!winCondition && (!new String[] { "n", "no" }.Contains(tryAgain))) // gives user chance to start over
            {
                Console.Write("You Lose!  Would you like to try again? ");
                tryAgain = Console.ReadLine().ToLower();
                if (tryAgain == "y" || tryAgain == "yes") LoadGame();
            }
            if (winCondition) Console.WriteLine("Congratulations, you won!");
            else Console.WriteLine("Better luck next time!");
            Console.ReadKey();
        }

        static public void LoadGame() // initialize all game settings
        {
            currentLog = new List<string>();
            turns = 30;
            StoryPoints.ResetStory(); // resets the story flags
            Player.ResetPlayer(); // resets the player
            Location.ResetRooms(); // resets the rooms
            Location.Enter(); // displays the first load screen
            Prompt.Ask(); // show prompt for parsing text
        }

    }
}
