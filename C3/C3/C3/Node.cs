using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3
{
    internal class Node
    {
        public string AWord { get; set; }
        public int WordLen {  get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node()
        {
            AWord = null;
            WordLen = 0;
            Left = null;
            Right = null;
        }
        
        public Node(string key, int data)
        {
            AWord = key;
            WordLen = data;
            Left = null;
            Right = null;
        }

        public override string ToString()
        {
            // Sets default method to export the value of the object
            return "Word: " + AWord + ", Length: " + WordLen.ToString();
        }
    }
}
