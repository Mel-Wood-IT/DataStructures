using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTwo
{
    internal class DbleListDS
    {
        public Node Head {  get; set; }
        public Node Tail { get; set; }
        public Node Current { get; set; }
        public int Counter { get; set; }


        public DbleListDS()
        {
            Head = null;
            Tail = null;
            Current = null;
            Counter = 0;
        }

        
        public void Clear()
        {
            Head = null;
            Tail = null;
            Current = null;
            Counter = 0;
        }

        #region Print
        public override string ToString()
        {   // Check if list is empty
            StringBuilder sb = new StringBuilder();
            if (Head == null)
            {
                sb.Append("Stack is empty\n");
                return sb.ToString();
            }
            else
            {
                Current = Head;
                int pos = 1; //Node position
                while (Current != null)
                {
                    // Node ToString override
                    sb.Append("Node: " + pos.ToString() + " -> " + Current.ToString() + "\n");
                    Current = Current.Next;
                    pos++;
                }
            }
            return sb.ToString();
        }

        // ToPrintOp for displaying the list
        public void ToPrintOp()
        {
            if (Head == null)
            {
                Console.WriteLine("List is empty");
                return;
            }
            Node current = Head;
            int pos = 1;
            while (current != null)
            {
                Console.WriteLine($"Node {pos}: {current}");
                current = current.Next;
                pos++;
            }
        }
        #endregion


        #region InsertFront/Back
        private void InsertAtFront(Node node)
        {
            // Check if list is empty
            if (Head == null)
            {
                Head = node;
                Tail = node;
                Current = node;
            }
            else
            {   // Attach the list to the new node
                node.Next = Head;
                Head.Prev = node;

                // Reassign Head to new node to keep head at top of stack
                Head = node;
                Current = node;
            }
            Counter++;
        }

        public void AddToFront(string AWord, int wordLen)
        {   // UI method call
            if (!Contains(AWord) && !AWord.StartsWith("#"))
            {
                Node temp = new Node(AWord, wordLen);
                InsertAtFront(temp);
            }
            else
            {
                Console.WriteLine("Duplicate or invalid input.");
            }
        }

        private void InsertAtBack(Node node)
        {
            // Check if list is empty
            if (Head == null)
            {   // Make Head, Tail and Current the new node
                Head = node;
                Tail = node;
                Current = node;
            }
            else
            {   // Insert node at the end of the list
                Tail.Next = node;
                node.Prev = Tail;

                // Reassign the Tail to the new node
                Tail = node;
                Current = node;
            }
            Counter++;
        }

        public void AddToEnd(string AWord, int wordLen)
        {   // UI method call
            if (!Contains(AWord) && !AWord.StartsWith("#"))
            {
                Node temp = new Node(AWord, wordLen);
                InsertAtBack(temp);
            }
            else
            {
                Console.WriteLine("Duplicate or invalid input");
            }
        }

        public void InsertEnd(string AWord, int wordLen)
        {   // UI method call
            if (!Contains(AWord) && !AWord.StartsWith("#"))
            {
                Node temp = new Node(AWord, wordLen);
                InsertAtBack(temp);
            }
        }
        #endregion


        #region InsertBefore/After
        private bool InsertBefore (Node node, Node targetNode)
        {
            bool inserted = false;
            if (Head == null)
            {   // Check if list is empty
                return inserted;
            }
            if (targetNode.AWord == Head.AWord)
            {   // Node inserted as new Head
                InsertAtFront(node);
                inserted = true;
            }
            else // List is not empty
            {
                // Set current to find the target node
                Current = Head;
                while (Current != null && !inserted)
                {   // Traverse the list
                    if (Current.AWord == targetNode.AWord)
                    {   // Target node found
                        node.Next = Current;
                        node.Prev = Current.Prev;
                        Current.Prev.Next = node;
                        Current.Prev = node;
                        inserted = true;
                        Counter++;
                    }
                    else
                    {   // Traverse the list
                        Current = Current.Next; // Assign to next node in list
                    }
                }
            }
            return inserted;
        }

        public string AddBefore(string AWord, int wordLen, string target)
        {
            Node newNode = new Node(AWord, wordLen);
            if(Contains(AWord) || AWord.StartsWith("#"))
            {
                return "Duplicate or Invalid input";
            }

            Node targetNode = FindNode(target);
            if (InsertBefore(newNode, targetNode))
            {
                return $"Target: {target} found, NODE: {newNode.ToString()} inserted";
            }
            return $"Target: {target} NOT found, NODE: {newNode.ToString()} NOT inserted";
        }

        private bool InsertAfter (Node node, Node targetNode)
        {
            bool inserted = false;
            if (Head == null)
            {   // Check if list is empty
                return inserted;
            }
            // List is not empty, traverse the list 
            Current = Head;
            while (Current != null && !inserted)
            {
                if (Current.AWord == targetNode.AWord)
                {
                    if (Current == Tail)
                    {   // Reassign Tail
                        InsertAtBack(node);
                    }
                    else
                    {
                        // Attach list to new node
                        node.Next = Current.Next;
                        node.Prev = Current;
                        node.Next.Prev = node;
                        Current.Next = node;
                        Current = node;
                    }
                    inserted = true;
                    Counter++;
                }
                else
                {
                    Current = Current.Next; // assign to next node in list
                }
            }
            return inserted;
        }

        public string AddAfter(string AWord, int wordLen, string target)
        {
            Node newNode = new Node(AWord, wordLen);
            if (Contains(AWord) || AWord.StartsWith("#"))
            {
                return "Duplicate or Invalid input";
            }

            Node targetNode = FindNode(target);
            if (InsertAfter(newNode, targetNode))
            {
                return $"Target: {target} found, NODE: {newNode.ToString()} inserted";
            }
            return $"Target: {target} NOT found, NODE: {newNode.ToString()} NOT inserted";
        }
        #endregion


        #region DeleteFunctions
        private Node DeleteAtFront()
        {
            if (Head == null)
            {   // Check if list is empty
                return null;
            }
            Node nodetoRemove = Head;
            // Reassign Head to the next node
            Head = Head.Next;
            if (Head != null)
            {
                Head.Prev = null;
            }
            else
            {
                Tail = null;
            }

            Current = Head;
            Counter--;
            return nodetoRemove;

        }

        public string RemoveFront()
        {   // UI method call
            Node nodeToRemove = new Node();
            nodeToRemove = DeleteAtFront();
            if (nodeToRemove != null)
            {   // Return node deleted
                return "Found, Node: " + nodeToRemove.ToString() + " removed";
            }
            else
            {
                return "Not found, OR list is empty";
            }
        }
        
        private Node DeleteAtEnd()
        {
            if (Head == null)
            {   // Check if list is empty
                return null;
            }
            else
            {
                Node nodeToRemove = new Node();
                nodeToRemove = Tail;

                // Reassign Head to next node in list
                Tail = Tail.Prev;
                Tail.Next = null;
                Current = Tail;
                Counter--;
                return nodeToRemove;
            }
        }

        public string RemoveRear()
        {   // UI method call
            Node nodeToRemove = null;
            nodeToRemove = DeleteAtEnd();
            if (nodeToRemove != null)
            {   // return node deleted
                return "NODE: " + nodeToRemove.ToString() + " removed";
            }
            else
            {
                return "Not found, OR list is empty";
            }
        }

        private Node DeleteNode(Node nodeToDelete)
        {
            Node nodeToRemove = null;
            if (Head == null)
            {   // Check if list is empty
                nodeToRemove = null;
            }
            else if (Head.AWord == nodeToDelete.AWord)
            {   // node to remove is the head
                nodeToRemove = Head;
                DeleteAtFront();
            }
            else if (Tail.AWord == nodeToDelete.AWord)
            {   // node to remove is the tail
                nodeToRemove = Tail;
                DeleteAtEnd();
            }
            else
            {   // node in middle, traverse through the list
                Current = Head;
                bool deleted = false;
                while (Current != null && !deleted)
                {   // not at the end of the list or found
                    if (Current.AWord == nodeToDelete.AWord)
                    {   // Found node, use the previous node and next node
                        // to remove current node from list
                        nodeToRemove = Current;
                        Current.Next.Prev = Current.Prev;
                        Current.Prev.Next = Current.Next;
                        deleted = true;
                        Counter--;
                    }
                    Current = Current.Next;
                }
            }
            return nodeToRemove;
        }

        public string RemoveNode(string word)
        {   // UI method call
            Node nodeToDelete = FindNode(word);
            if (nodeToDelete != null)
            {
                DeleteNode(nodeToDelete);
                return $"{word} has been deleted";
            }
            else
            {
                return $"{word} is not in list or list is empty";
            }
        }
        #endregion


        #region FindNode
        private int Search(Node nodeToFind)
        {
            int pos = 0; // returns position of node, or 0 if not found
            if (Head == null)
            {   // Check if list is empty
                return pos;
            }
            else
            {
                Current = Head;
                bool found = false;
                while (Current != null && !found)
                {
                    if (Current.AWord == nodeToFind.AWord)
                    {   // Node found
                        found = true;
                    }
                    else
                    {   // step to next node
                        Current = Current.Next;
                    }
                    pos++;
                }
                if (!found) { pos = 0; }
            }
            return pos;
        }

        public string Find(string AWord)
        {
            int pos = 0;
            Node nodeToFind = new Node(AWord, AWord.Length);
            pos = Search(nodeToFind);
            if (pos >= 1 && pos <= Counter)
            {
                return "Target: " + AWord.ToString() + ", NODE found at position: " + pos.ToString();
            }
            else
            {
                return "Target: " + AWord.ToString() + ", NODE not found, OR list is empty";
            }
        }

        public Node FindNode(string word)
        {
            Current = Head;
            while (Current != null)
            {
                if (Current.AWord == word)
                {
                    return Current;
                }
                Current = Current.Next;
            }
            return null;
        }
        #endregion

        private bool Contains(string word)
        {
            Node current = Head;
            while (current != null)
            {
                if (current.AWord == word)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
    }
}
