using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOne
{
    internal class NodeEntry
    {
        public string AWord { get; set; }
        public int wordLen { get; set; }

        public NodeEntry()
        {
            AWord = null;
            wordLen = 0;
        }

        public NodeEntry(string key, int data)
        {
            AWord = key;
            wordLen = data;
        }

        public override string ToString()
        {
            // Sets default method to export the value of the object
            return "Word: " + AWord + ", Length: " + wordLen.ToString();
        }
    }
}
