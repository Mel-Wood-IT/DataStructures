using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneB
{
    internal class Node
    {
        // Data items to store
        public string AWord {  get; set; }

        public int wordLen {  get; set; }

        // Null Constructor
        public Node()
        {
            AWord = null;
            wordLen = 0;
        }

        // Second constructor
        public Node (string aWord, int wordLen)
        {
            AWord = aWord;
            this.wordLen = wordLen;
        }

        public override string ToString()
        {
            // Default method to print
            return "Word: " + AWord + " Length: " + wordLen.ToString();
        }
    }
}
