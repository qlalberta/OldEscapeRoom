using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeText
{

    public partial class Item
    {
        static public Dictionary<string, Item> byName = new Dictionary<string, Item>();
        static public Dictionary<string, List<string>> shortHand = new Dictionary<string, List<string>>();
        public string dictName;
        public int size;
        public string commonDescription;
        public string commonDescriptionDark;
        public string clarificationName;
        public string genericType;
        public string examText;
        public bool hasBeenExamined = false;
        public string examTextDark;
        public bool canLight = false;
        public bool isLightSource = false;
        public bool portable = false;
        public bool isOpen = false;
        public bool canOpen = false;
        public bool isLocked = false;
        public bool canLock = false;
        public bool comboKnown = false;
        public bool edible = false;
        public List<string> unlockItems = new List<string>();
        public List<string> unlockCodes = new List<string>();
        public List<string> specialPairings = new List<string>();
        public List<string> specialFittings = new List<string>();
        public List<string> itemsInside = new List<string>();
        public List<string> itemsOutside = new List<string>();
        public StoryEvent unlockEvent = null;
        public StoryEvent examineEvent = null;
        public StoryEvent takeEvent = null;
        public StoryEvent turnonEvent = null;
        public StoryEvent turnoffEvent = null;
        public StoryEvent openEvent = null;
        public StoryEvent dropEvent = null;
        public StoryEvent enterEvent = null;
        public StoryEvent useEvent = null;
        //public string unlockMessage;
        //public string postChangeDescription;
        //public string postChangeExam;
        public bool usableInDark = false;
        public bool glowsInDark = false;
        public string locatedInObject = null;
        public int canGoToRoom;

        public Item() { }
        
        public Item(string dict, int s, bool p, string common, string specific, string generic)
        {
            this.dictName = dict;
            this.portable = p;
            this.size = s;
            this.commonDescription = common;
            this.clarificationName = specific;
            this.genericType = generic.ToLower();
            byName.Add(this.dictName, this);
            if (shortHand.ContainsKey(this.genericType)) shortHand[this.genericType].Add(this.dictName);
            else shortHand.Add(this.genericType, new List<string>() { this.dictName });
        }

        public Item(string dict, int s, bool p, string common, string specific)
        {
            this.dictName = dict;
            this.portable = p;
            this.size = s;
            this.commonDescription = common;
            this.clarificationName = specific;
            this.genericType = common.ToLower();
            byName.Add(this.dictName, this);
            if (shortHand.ContainsKey(this.genericType)) shortHand[this.genericType].Add(this.dictName);
            else shortHand.Add(this.genericType, new List<string>() { this.dictName });
        }

        public Item(string dict, int s, bool p, string common)
        {
            this.dictName = dict;
            this.portable = p;
            this.size = s;
            this.commonDescription = common;
            this.clarificationName = common;
            this.genericType = common.ToLower();
            byName.Add(this.dictName, this);
            if (shortHand.ContainsKey(this.genericType)) shortHand[this.genericType].Add(this.dictName);
            else shortHand.Add(this.genericType, new List<string>() { this.dictName });
        }

        public bool Visible
        {
            get
            {
                if (Location.CanSee) return true;
                if (Player.inventory.Contains(this.dictName)) return true;
                if (this.glowsInDark) return true;
                else return false;
            }
        }

    }
}
