using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component3B
{
    internal class AVLTree
    {
        public Node Root { get; set; }

        public AVLTree()
        {
            Root = null;
        }

        #region Insert
        // Adding to tree through ReadFile
        public void Add(string AWord, int wordLen)
        {
            if (AWord.Contains("#") || FindBool(AWord))
            {
                return;
            }

            Node node = new Node(AWord, wordLen);
            if (Root == null)
            {
                Root = node;
            }
            else
            {
                Root = InsertNode(Root, node);
            }
        }

        // Add method for user input so it displays message if successful
        public string InsertAdd(string AWord, int wordLen)
        {
            
            if (AWord.Contains("#"))
            {
                return "Invalid word. Not added, remove '#'.";
            }
            // Check if the word already exists in the tree
            if (FindBool(AWord))
            {
                return $"Duplicate word '{AWord}' not added.";
            }

            // If valid continue with insert
            Node node = new Node(AWord, wordLen);
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

        private Node InsertNode(Node tree, Node node)
        {
            // Current sub-tree node is empty, insert node here
            if (tree == null)
            {
                return node;
            }
            // Compare by word length
            if (node.WordLen < tree.WordLen)
            {   // Traverse left side, insert when null then balance tree
                tree.Left = InsertNode(tree.Left, node);
                tree = BalanceTree(tree);
            }
            else if (node.WordLen > tree.WordLen)
            {   // Traverse right side, insert when null then balance tree
                tree.Right = InsertNode(tree.Right, node);
                tree = BalanceTree(tree);
            }
            else
            {
                // If the word length is equal then compare alphabetically
                int compareResult = string.Compare(node.AWord, tree.AWord, StringComparison.OrdinalIgnoreCase);
                if (compareResult < 0)
                {
                    tree.Left = InsertNode(tree.Left, node);
                    tree = BalanceTree(tree);
                }
                else if (compareResult > 0)
                {
                    tree.Right = InsertNode(tree.Right, node);
                    tree = BalanceTree(tree);
                }
            }
            return tree;
        }
        #endregion

        #region BalanceTree
        private Node BalanceTree(Node current)
        {
            int b_factor = BalanceFactor(current);
            if (b_factor > 1)
            {   // Left side of tree is unbalanced
                // Decide a left or right rotation
                if (BalanceFactor(current.Left) > 0)
                {   // left side requires rotation, perform a left
                    // sub-tree rotation
                    current = RotateLL(current);
                }
                else
                {   // Right side requires rotation, perform a right
                    // sub-tree rotation
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {   // Right side of tree is unbalanced, decide 
                // a left or right rotation
                if (BalanceFactor(current.Right) > 0)
                {   // left side requires rotation, perform a 
                    // left sub-tree rotation
                    current = RotateRL(current);
                }
                else
                {   // Right side requires rotation, perform a 
                    // right sub-tree rotation
                    current = RotateRR(current);
                }
            }
            return current;
        }

        private int BalanceFactor(Node current)
        {   // Determine if the sub-tree needs to rotate left or right by finding the
            // height of the left and right sides of the subtree, and then taking the
            // difference between the left and right.
            //
            // A balance factor greater than 1 (+2) indicates the left side is unbalanced.
            // A balance factor less than  -1 (-2) indicates the right side is unbalanced.
            // Every other balance factor does not require rotation.
            int left = GetHeight(current.Left);
            int right = GetHeight(current.Right);
            int b_factor = left - right;
            return b_factor;
        }
        #endregion

        #region Rotating Tree
        private Node RotateRR(Node parent)
        {   // Performing a rght rotation on the right side of the sub-tree
            // by swapping the nodes around based on reassigning the parent node
            // to the right side of the sub-tree.
            Node pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }

        private Node RotateRL(Node parent)
        {   // Perform a left rotation on the right side of the sub-tree by
            // swapping the nodes around based on performing a left rotation
            // on the right side of the sub-tree.
            Node pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }

        private Node RotateLL(Node parent)
        {   // Perform a left rotation on the left side of the sub-tree by swapping the nodes
            // around based on reassigning the parent node to the left side of the sub-tree.
            Node pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }

        private Node RotateLR(Node parent)
        {   // Perform a right rotation on the left side of the sub-tree by swapping the nodes
            // around based on performing a right rotation on the left side of the sub-tree
            Node pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }
        #endregion

        #region Details
        private int GetHeight(Node current)
        {   // Determine the height of the current sub-tree
            int height = 0;
            if (current != null)
            {
                int left = GetHeight(current.Left);
                int right = GetHeight(current.Right);
                int max = Max(left, right);
                height = max + 1;
            }
            return height;
        }

        private int MaxTreeDepth(Node tree)
        {
            if (tree == null) return 0;
            int left = MaxTreeDepth(tree.Left);
            int right = MaxTreeDepth(tree.Right);

            return Math.Max(left, right) + 1;
        }

        public string Details()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n Tree Details ");
            sb.Append("\nRoot Node: " + Root.ToString());
            sb.Append("\nMax Tree Depth: " + MaxTreeDepth(Root));

            return sb.ToString();
        }
        private int Max(int left, int right)
        {
            return left > right ? left : right;
        }
        #endregion

        #region PreOrder
        private string TraversePreOrder(Node node)
        {
            StringBuilder sb = new StringBuilder();
            if (node != null)
            {
                sb.Append(node.ToString() + " ");
                sb.Append(TraversePreOrder(node.Left));
                sb.Append(TraversePreOrder(node.Right));
            }
            return sb.ToString();
        }

        public string PreOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (Root == null)
            {
                sb.Append("Tree is EMPTY");
            }
            else
            {
                sb.Append(TraversePreOrder(Root));
            }
            return sb.ToString();
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
        {   // UI Call
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
                sb.Append(TraversePostOrder(node.Left));
                sb.Append(TraversePostOrder(node.Right));
                sb.Append(node.ToString() + " ");
            }
            return sb.ToString();
        }
          
        public string InOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (Root == null)
            {
                sb.Append("Tree is EMPTY");
            }
            else
            {
                sb.Append(TraverseInOrder(Root));
            }
            return sb.ToString();
        }
        #endregion

        #region Delete
        private Node Delete(Node current, Node target)
        {
            Node parent = null; // pivot node

            if (current == null)
            {   // Reached bottom of the tree path
                return null;
            } 
            else
            {
                if (string.Compare(target.AWord, current.AWord) < 0 )
                {
                    current.Left = Delete(current.Left, target);
                    if (BalanceFactor(current) == -2)
                    {   // after possible deletion we have to check and rebalance tree
                        if (BalanceFactor(current.Right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                else if (string.Compare(target.AWord, current.AWord) > 0 )
                {   // Traverse right side of sub-tree
                    current.Right = Delete(current.Right, target);
                    if (BalanceFactor(current) == 2)
                    {   // after possible deletion we have to check and rebalance tree
                        if (BalanceFactor(current.Left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {   // target found
                    if (current.Right != null)
                    {   // Delete inorder successor
                        // Find smallest value node on the right side of the tree
                        parent = current.Right;
                        while (parent.Left != null)
                        {
                            parent = parent.Left;
                        }
                        current.AWord = parent.AWord;
                        current.Right = Delete(current.Right, parent);
                        if (BalanceFactor(current) == 2)
                        {   // Rebalance tree
                            if (BalanceFactor(current.Left) >=0 )
                            {
                                current = RotateLL(current);
                            }
                            else
                            {
                                current = RotateLR(current);
                            }
                        }
                    }
                    else
                    {
                        // left side not null
                        return current.Left;
                    }
                }
            }
            return current;
        }

        public string Remove(string word)
        {   // UI method call
            Node node = new Node(word, word.Length);
            node = Search(Root, node);
            if (node != null)
            {
                Root = Delete(Root, node);
                return "Target: " + word.ToString() + ", NODE removed";
            }
            else
            {
                return "Target: " + word.ToString() + ", NODE not found or tree empty";
            }
        }
        #endregion

        #region Search
        private Node Search (Node tree, Node node)
        {
            if (tree != null)
            {   // have not reached end of branch
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
            return null; // Not found
        }

        public bool FindBool (string word)
        {
            Node node = new Node (word, word.Length);
            return Search(Root, node) != null;
        }
        
        public string Find(string word)
        {
            Node node = new Node(word, word.Length);
            node = Search(Root, node);
            if (node != null)
            {
                return "Target: " + word + ", Node found: " + node.ToString();
            }
            else
            {
                return "Target: " + word + ", NODE not found or Tree empty";
            }
        }
        #endregion

        public void Clear()
        {
            Root = null;
        }
    }
}
