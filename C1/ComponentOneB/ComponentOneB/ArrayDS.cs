using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneB
{
    internal class ArrayDS
    {
        // Create an array size to suit the file
        private static int ARRAY_SIZE = 1000;

        // Track number of stored nodes incase the entire array is not filled it can be counted
        private int ASize { get; set; }

        // Create array of Nodes
        private Node[] ArrayNodes;

        public ArrayDS()
        {
            // Instantiate the array data structure 
            ArrayNodes = new Node[ARRAY_SIZE];
            ASize = 0;
        }

        private void InsertNode(Node node)
        { // Method to insert the node
            int index = 0;
            bool inserted = false;

            while (index < ArrayNodes.Length && !inserted)
            { // within array size and inserting node
                if (ArrayNodes[index] == null)
                { // found unallocated location
                    ArrayNodes[index] = node;
                    ASize++; // keep count inserted nodes
                    inserted = true;
                }
                index++;
            }
        }

        public void Add(string value, int ValLength)
        {
            Node node = new Node(value, ValLength);
            InsertNode(node);
        }

        public string ToPrint()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (ArrayNodes == null)
            { // array structure is empty
                return null;
            }

            //for (int i = 0; i < ArrayNodes.Length; i++)
            for (int i = 0; i < ASize; i++)
            { // step through the array outputting all nodes

                stringBuilder.Append("[" + i + "] -> ");
                if (ArrayNodes[i] == null)
                { // null locations need to be handled
                    stringBuilder.Append("empty\n");
                }
                else
                {
                    stringBuilder.Append(ArrayNodes[i].ToString() + "\n");
                }
            }
            stringBuilder.Append("\nNumber of items: " + ASize.ToString());

            return stringBuilder.ToString();
        }

        private string ToPrintSort(Node[] input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (input == null)
            {
                return null;
            }

            for (int i = 0; i < input.Length; i++)
            {
                stringBuilder.Append("[" + i + "] -> ");
                if (input[i] == null)
                { // null locations need to be handled
                    stringBuilder.Append("empty\n");
                }
                else
                {
                    stringBuilder.Append(input[i].ToString() + "\n");
                }
            }
            stringBuilder.Append("\nNumber of items: " + ASize.ToString());
            return stringBuilder.ToString();
        }

        #region SelectionSort
        // Selection sort is my O(n2)
        
        public string SelectionSort()
        {
            // Use a reference so that the array is not altered
            Node[] input = ArrayNodes;
            StringBuilder sb = new StringBuilder();

            for (var i = 0; i < ASize; i++)
            {
                var min = i; // maintain next position for comparison and number
                for (var j = i + 1; j < ASize; j++)
                {
                    if (input[min].wordLen > input[j].wordLen)
                    { // found the lowest number
                        min = j;
                    }
                }

                if (min != i)
                { // Swap the numbers
                    Node lowerValue = input[min];
                    input[min] = input[i];
                    input[i] = lowerValue;
                }
            }
            sb.Append(ToPrintSort(input));
            return sb.ToString();
        }
        #endregion

        #region MergeSort
        // Merge Sort is my O(logn)
        
        private Node[] MergeSortOp(Node[] input, int left, int right)
        { // Private Merge method to find middle pivot
            int mid;
            if (left < right)
            {
                // Binary split the array by finding mid point
                mid = (left + right) / 2;
                // Recursively call the method until each element is split
                // into its own cell
                MergeSortOp(input, left, mid);
                MergeSortOp(input, mid + 1, right);

                // Merge elements in split arrays
                input = Merge(input, left, mid, right);
            }
            return input;
        }

        private Node[] Merge(Node[] input, int left, int mid, int right)
        {
            int n1 = mid - left + 1; //number of elements in LeftArray
            int n2 = right - mid; // number of elements in RightArray

            Node[] LeftArray = new Node[n1]; // create temp arrays for left and right
            Node[] RightArray = new Node[n2];

            for (int i = 0; i < n1; i++)
            { // copy all elements left of mid split, into a temporary array
                LeftArray[i] = input[left + i];
            }

            for (int i = 0; i < n2; i++)
            { // Copy all elements right of mid split into a temporary array
                RightArray[i] = input[mid + i + 1];
            }

            // x = index for LeftArray, y = index for RightArray,
            // z = the index for the merged array
            int x = 0, y = 0, z = left;
            while (x < n1 && y < n2)
            {
                if (LeftArray[x].wordLen < RightArray[y].wordLen)
                {
                    input[z] = LeftArray[x];
                    x++;
                }
                else
                {
                    input[z] = RightArray[y];
                    y++;
                }
                z++;
            }

            // Copying the remaining elements of LeftArray
            while (x < n1)
            {
                input[z] = LeftArray[x];
                x++;
                z++;
            }

            // Copying the remaining elements of RightArray
            while (y < n2)
            {
                input[z] = RightArray[y];
                y++;
                z++;
            }
            return input;
        }

        public string MergeSort()
        { // UI Call
            StringBuilder sb = new StringBuilder();

            // Use a reference so that the array is not altered
            Node[] input = ArrayNodes;
            input = MergeSortOp(input, 0, ASize - 1);

            sb.Append(ToPrintSort(input));
            return sb.ToString();
        }
        #endregion
    }
}
