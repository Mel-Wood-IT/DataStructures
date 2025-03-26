using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component3B
{
    internal class Node
    {
        public string AWord { get; set; }

        public int WordLen { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node()
        {
            AWord = null;
            WordLen = 0;
            Left = null;
            Right = null;
        }

        public Node(string data, int wLength)
        {
            AWord = data;
            WordLen = wLength;
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
