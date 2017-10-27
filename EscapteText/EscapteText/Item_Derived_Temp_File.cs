using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// TODO: Break this into smaller chunks later on

namespace EscapeText
{
    partial class Item
    {
        public void HoldItemsInside(string[] items)
        {
            this.canOpen = true;
            this.itemsInside.AddRange(items);
            foreach (string s in items) if (byName.ContainsKey(s)) byName[s].locatedInObject = this.dictName; // objects located inside shoudl always be declared prior to the containing object
        }

        public void HoldItemsOutside(string[] items)
        {
            this.itemsOutside.AddRange(items);
            foreach (string s in items) if (byName.ContainsKey(s)) byName[s].locatedInObject = this.dictName; // objects located on top shoudl always be declared prior to the containing object
        }

        public void MakeLightSouce(bool isOn)
        {
            this.canLight = true;
            this.isLightSource = isOn;
            this.usableInDark = true;
        }


        public void LockWithCode(string[] codes)
        {
            this.isLocked = true;
            this.canLock = true;
            this.unlockCodes.AddRange(codes);
        }

        public void LockWithItems(string[] items)
        {
            this.isLocked = true;
            this.canLock = true;
            this.unlockCodes.AddRange(items);
        }


        public void ConvertToDoor(string[] items)
        {
            this.canOpen = true;
            this.canLock = true;
            this.isLocked = true;
            this.glowsInDark = true;
            this.unlockItems.AddRange(items);
        }
    }
}