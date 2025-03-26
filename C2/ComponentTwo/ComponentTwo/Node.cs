using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTwo
{
    internal class Node
    {
        public string AWord {  get; set; }
        public int wordLen {  get; set; }

        public Node Next { get; set; } // Next node in list
        public Node Prev { get; set; } // Previous node in list

        // Null constructor 
        public Node()
        {
            AWord = null;
            wordLen = 0;
            Next = null;
            Prev = null;
        }
        
        
        public Node (string key, int data)
        {
            this.AWord = key;
            this.wordLen = data;
            Next = null;
            Prev = null;
        }

        public override string ToString()
        {
            // Sets default method to export the value of the object
            return "Word: " + AWord + ", Length: " + wordLen.ToString();
        }
    }
}
