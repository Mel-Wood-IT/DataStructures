using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOne
{
    internal class MyDictionary
    {
        // Create dictionary data structure 
        Dictionary<string, NodeEntry> DictionaryDS { get; set; }

        public MyDictionary()
        {
            // Initialise the dictionary
            DictionaryDS = new Dictionary<string, NodeEntry>();
        }


        private void Insert(string key, NodeEntry value)
        {
            // Insert node into dictionary data structure
            DictionaryDS.Add(key, value);
        }


        public void AddOp(string word)
        {
            if (!DictionaryDS.ContainsKey(word) && !word.StartsWith("#"))
            {
                NodeEntry entry = new NodeEntry(word, word.Length);
                Insert(word, entry);
            }
        }

        public void AddingOp(string word)
        {
            if (!DictionaryDS.ContainsKey(word) && !word.StartsWith("#"))
            {
                NodeEntry entry = new NodeEntry(word, word.Length);
                Insert(word, entry);
                Console.WriteLine("New word added: " + word);
            }
            else
            {
                Console.WriteLine("Word not valid or already in Dictionary.");
            }
        }


        public string ToPrintOp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--- File Contents ---");
            foreach (KeyValuePair<string, NodeEntry> item in DictionaryDS)
            {
                // Simplest extraction of a class object
                sb.AppendLine("Key: " + item.Key + ", " + "Node Value: " +
                    item.Value.wordLen + ", " + "Node Key: " + item.Value.AWord);
            }
            sb.AppendLine("Number of items: " + DictionaryDS.Count);
            return sb.ToString();
        }


        public void FindOp(string word)
        {
            Console.WriteLine("- Find: " + word);
            Console.WriteLine("Word Exists? - " + DictionaryDS.ContainsKey(word));
            Console.WriteLine();
        }


        public void DeleteOp(string word)
        {
            Console.WriteLine("Deleted: " + word);
            Console.WriteLine("Word Deleted? - " + DictionaryDS.Remove(word));

            Console.WriteLine();  
        }


        public void Clear()
        {
            DictionaryDS.Clear();
        }
    }
}
