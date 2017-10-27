using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeText
{
    public delegate bool StoryEvent();

    class StoryPoints // contains booleans for story triggers
    {
        static internal int secondsRemain;
        static internal bool chap_01_knowsOfPoison;
        static internal bool Room_01_SafeDiscovered;
        static internal bool Room_01_safe01KnowsCode;
        static internal bool Room_01_safe01Unlocked;
        static internal bool Room_01_ExaminedBasket;
        static internal bool Room_01_VentUnlocked;
        static internal bool Room_01_VentOpened;


        public StoryPoints() { }

        static internal void ResetStory()
        {
            secondsRemain = 600;
            chap_01_knowsOfPoison = false;
            Room_01_SafeDiscovered = false;
            Room_01_safe01KnowsCode = false;
            Room_01_safe01Unlocked = false;
            Room_01_ExaminedBasket = false;
            Room_01_VentUnlocked = false;
            Room_01_VentOpened = false;
        }

        static internal string TimeDisplay
        {
            get
            {
                if (chap_01_knowsOfPoison)
                {
                    TimeSpan time = TimeSpan.FromSeconds(secondsRemain);
                    if (secondsRemain >= 3600) return (time.ToString(@"hh\:mm\:ss remain."));
                    else return (time.ToString(@"mm\:ss remain. "));
                }
                else return ("");
            }
        }
        

        static internal bool RunStoryEvent(StoryEvent se)
        {
            return(se());
        }
    }
}
