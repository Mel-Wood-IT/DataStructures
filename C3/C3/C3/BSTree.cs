using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3
{
    internal class BSTree
    {
        public Node Root {  get; set; }

        public BSTree()
        {
            Root = null;
        }

        #region InsertOp
        public void Add(string AWord, int wordLen)
        {   // UI call
            Node node = new Node(AWord, wordLen);

            if (!AWord.Contains("#"))
            {
                if (Root == null)
                {
                    Root = node;
                }
                else
                {
                    InsertNode(Root, node);
                }
            }
            else
            {
                Console.WriteLine("Invaid word, remove '#'");
            }
        }

        public string InsertAdd(string AWord, int wordLen)
        {
            Node node = new Node(AWord, wordLen);

            if (AWord.Contains("#"))
            {
                return "Invalid word. Not added, remove '#'.";
            }
            if (Root == null)
            {
                Root = node;
                return $"Word '{AWord}' added as root.";
            }
            else
            {
                Root = InsertNode(Root, node);
                return $"Word '{AWord}' has been added.";
            }
        }

        public Node InsertNode(Node tree, Node node)
        {   // This is a recursive method used to traverse the tree
            // Compare by word length
            if (node.WordLen < tree.WordLen)
            {
                if (tree.Left == null)
                {   // left is empty, insert node
                    tree.Left = node;
                }
                else
                {   // left not empty traverse the tree using recursive call
                    InsertNode(tree.Left, node);
                }
            }
            // Compare node for greater than node in tree
            else if (node.WordLen > tree.WordLen) // CompareTo method
            {
                if (tree.Right == null)
                {   // list is empty
                    tree.Right = node;
                }
                else
                {   // right not empty, traverse the tree using 
                    // recursive call
                    InsertNode(tree.Right, node);
                }
            }
            else
            {
                // If word length is equal, compare them alphabetically 
                if (string.Compare(node.AWord, tree.AWord, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    // Insert in the left subtree
                    if (tree.Left == null)
                    {
                        tree.Left = node;
                    }
                    else
                    {
                        InsertNode(tree.Left, node);
                    }
                }
                else if (string.Compare(node.AWord, tree.AWord, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    // Insert in the right subtree if the word is lexicographically larger
                    if (tree.Right == null)
                    {
                        tree.Right = node;
                    }
                    else
                    {
                        InsertNode(tree.Right, node);
                    }
                }
                else
                {
                    Console.WriteLine("Duplicate word found, not adding.");
                }
            }
            return tree;
        }
        #endregion

        #region PreOrder
        private string TraversePreOrder(Node node)
        {
            if (node == null)
            {
                return "";  // if null, return an empty string
            }

            // Pre-order traversal: Root -> Left -> Right
            string result = node.ToString() + " ";
            result += TraversePreOrder(node.Left);
            result += TraversePreOrder(node.Right);

            return result;
        }

        public string PreOrder()
        {   // UI method for preorder
            if (Root == null)
            {
                return "TREE is EMPTY";
            }
            return TraversePreOrder(Root);
        }
        #endregion

        #region PostOrder
        private string TraversePostOrder(Node node)
        {
            StringBuilder sb = new StringBuilder();
            if (node != null)
            { 
                sb.Append(TraversePostOrder(node.Left));
                sb.Append(node.ToString() + " ");
                sb.Append(TraversePostOrder(node.Right));
            }
            return sb.ToString();
        }
        
        public string PostOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (Root == null)
            {
                sb.Append("Tree is EMPTY");
            }
            else
            {
                sb.Append(TraversePostOrder(Root));
            }
            return sb.ToString();
        }
        #endregion

        #region InOrder
        private string TraverseInOrder(Node node)
        {
            StringBuilder sb = new StringBuilder();
            if (node != null)
            {              
                sb.Append(TraverseInOrder(node.Left));
                sb.Append(TraverseInOrder(node.Right));
                sb.Append(node.ToString() + " ");
            }
            return sb.ToString();
        }

        public string InOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (Root == null)
            {
                sb.Append("TREE is EMPTY");
            }
            else
            {
                sb.Append(TraverseInOrder(Root));
            }

            return sb.ToString();
        }
        #endregion

        #region Searching
        private Node Search (Node tree, Node node)
        {
            if (tree != null)
            {   // have not reached end of a branch
                if (node.AWord == tree.AWord)
                {
                    return tree;
                }
                if (string.Compare(node.AWord, tree.AWord) < 0)
                {
                    return Search(tree.Left, node);
                }
                else
                {
                    return Search(tree.Right, node);
                }
            }
            return null;
        }

        public string Find(string word)
        {
            Node node = new Node(word, word.Length);
            node = Search(Root, node);
            if (node != null)
            {
                return "Target: " + word + ", NODE found: " + node.ToString();
            }
            else
            {
                return "Target: " + word + ", NODE not found or Tree empty";
            }
        }
        #endregion

        #region Deleting
        private Node Delete(Node tree, Node node)
        {
            if (tree == null)
            {   // Reached null side of the tree, return to unload stack
                return tree;
            }
            if (string.Compare(node.AWord, tree.AWord) < 0) //Compare the word
            {   // Traverse left side to find node
                tree.Left = Delete(tree.Left, node);
            }
            else if (string.Compare(node.AWord, tree.AWord) > 0) //Compare the word
            {   // Traverse right side to find node
                tree.Right = Delete(tree.Right, node);
            }
            else
            {   // Found node to delete
                // Check if node has only one child or no child
                if (tree.Left == null)
                {   // Pull right side of tree up
                    return tree.Right;
                }
                else if (tree.Right == null)
                {   // Pull left side of tree up
                    return tree.Left;
                }
                else
                {   // node has two leaf nodes, get the InOrder successor node
                    // (the smallest), therefore traverse right side and replace the
                    // node found with the current node
                    tree.AWord = MinValue(tree.Right);

                    // Traverse the right side of the tree to delete the InOrder successor
                    tree.Right = Delete(tree.Right, tree);
                }
            }
            return tree;
        }

        private string MinValue(Node node)
        {   // Finds the minimum node in the right side of tree
            string minval = node.AWord;
            while (node.Left != null)
            {   // Traverse the tree replacing the minval with the
                // node on the left side of the tree
                minval = node.Left.AWord;
                node = node.Left;
            }
            return minval;
        }

        public string Remove(string data)
        {   // UI method call
            Node node = new Node(data, data.Length);
            node = Search(Root, node);
            if (node != null)
            {
                Root = Delete(Root, node);
                return "Target: " + data.ToString() + ", NODE removed";
            }
            else
            {
                return "Target: " + data.ToString() + ", NODE not found";
            }
        }
        #endregion

        public void Clear()
        {
            Root = null;
        }
    }
}
