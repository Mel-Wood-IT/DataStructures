using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3
{
    internal class Program
    {      
        // Base directory as a relative path to the word files
        private static readonly string BaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "WordFiles");
        // Initialise the data structure
        private static BSTree bsTree = new BSTree();

        static void Main(string[] args)
        {
            int opt = 0;

            // Main loop for the application
            while (true)
            {
                // Folder selection
                opt = MainMenu();

                if (opt >= 1 && opt <= 3)
                {
                    MainMenuOutput(opt);
                }
                else
                {
                    Console.WriteLine("Please enter a number between 1-3.");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        #region Read/Insert File
        static string[] ReadFile(string folder, string fileName)
        {
            return File.ReadAllLines(Path.Combine(BaseDirectory, folder, fileName));
        }

        static void FileInsert(string folder, string filename)
        {
            Console.Clear();
            string[] fileToAdd = ReadFile(folder, filename);
            if (fileToAdd.Length == 0)
            {
                Console.WriteLine("File is empty");
            }
            else
            {
                // Use foreach loop to add into bsTree
                foreach (string line in fileToAdd)
                {
                    string wordAdd = line.Trim();

                    // Exclude empty lines and words starting with '#'
                    if (!string.IsNullOrEmpty(wordAdd) && !wordAdd.Contains("#"))
                    {
                        int wordLen = wordAdd.Length;
                        bsTree.Add(wordAdd, wordLen);
                    }
                }
                Console.WriteLine("Words added successfully.");
            }
        }
        #endregion

        #region MainMenu
        static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("**** Binary Tree ****");
            Console.WriteLine("Enter a folder to use");
            Console.WriteLine("[1] - Ordered Folder");
            Console.WriteLine("[2] - Random Folder");
            Console.WriteLine("[3] - Exit");
            Console.WriteLine("Enter Option: ");
            return int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 3 ? option : 0;

        }

        // Set the folder name for random or ordered using if-else statement and return the folder name
        static void MainMenuOutput(int opt)
        {
            string folder = string.Empty;
            Console.Clear();
            switch (opt)
            {
                case 1:
                    folder = @"ordered";
                    break;
                case 2:
                    folder = @"random";
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid menu option");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    MainMenu();
                    return;
            }

            if (!string.IsNullOrEmpty(folder))
            {
                string fullPath = Path.Combine(BaseDirectory, folder);
                FileMenuOutput(fullPath); // This now passes the folder path
            }
        }
        #endregion

        #region FileMenu      
        // Displays the file menu options for user to choose which file they wish to use
        // Menu will display until a valid option is selected
        static int FileMenu()
        {
            Console.Clear();
            Console.WriteLine("**** Select a file ****");
            Console.WriteLine("");
            Console.WriteLine("[1] - 1000 Word file");
            Console.WriteLine("[2] - 5000 Word file");
            Console.WriteLine("[3] - 10,000 Word file");
            Console.WriteLine("[4] - 15,000 Word file");
            Console.WriteLine("[5] - 20,000 Word file");
            Console.WriteLine("[6] - 25,000 Word file");
            Console.WriteLine("[7] - 30,000 Word file");
            Console.WriteLine("[8] - 35,000 Word file");
            Console.WriteLine("[9] - 40,000 Word file");
            Console.WriteLine("[10] - 45,000 Word file");
            Console.WriteLine("[11] - 50,000 Word file");
            Console.WriteLine("[12] - Exit");
            Console.WriteLine("Enter option: ");
            return int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 12 ? option : 0;
        }


        // Selected file name is returned to main or invalid option is printed.
        static void FileMenuOutput(string folderPath)
        {
            Console.Clear();
            int opt = FileMenu();          
            string filename = string.Empty;

            switch (opt)
            {
                case 1:
                    filename = @"1000-words.txt";
                    break;
                case 2:
                    filename = @"5000-words.txt";
                    break;
                case 3:
                    filename = @"10000-words.txt";
                    break;
                case 4:
                    filename = @"15000-words.txt";
                    break;
                case 5:
                    filename = @"20000-words.txt";
                    break;
                case 6:
                    filename = @"25000-words.txt";
                    break;
                case 7:
                    filename = @"30000-words.txt";
                    break;
                case 8:
                    filename = @"35000-words.txt";
                    break;
                case 9:
                    filename = @"40000-words.txt";
                    break;
                case 10:
                    filename = @"45000-words.txt";
                    break;
                case 11:
                    filename = @"50000-words.txt";
                    break;
                case 12:
                    return;
                default:
                    Console.WriteLine("Invalid");
                    return;
            }
            // Insert words from the file into the tree Then switch to the operation menu
            if (!string.IsNullOrEmpty(filename))
            {
                string fullPath = Path.Combine(folderPath, filename);               
                if (File.Exists(fullPath))
                {
                    Console.WriteLine($"Full Path: {fullPath}");
                    FileInsert(folderPath, filename);
                    SubMenu(folderPath, filename);
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
        }
        #endregion

        #region SubMenu
        // Displays the menu till the user input entered is valid
        static int DisplaySubMenu()
        {      
            Console.Clear();
            int option = 0;
            while (option < 1 || option > 7)
            {
                Console.WriteLine("**** What would you like to do ****");
                Console.WriteLine("");
                Console.WriteLine("[1] - Insert Entry");
                Console.WriteLine("[2] - Find Entry");
                Console.WriteLine("[3] - Delete Entry");
                Console.WriteLine("[4] - Printing Options");
                Console.WriteLine("[5] - Time Complexity");
                Console.WriteLine("[6] - Demonstration");
                Console.WriteLine("[7] - Back to main menu");
                Console.WriteLine("Enter Option: ");
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    option = 0;
                }
            }
            return option;
        }

        // Sub menu switch that calls on AddingOp, FindOp, DeleteOp, ToPrint, TimeComplex, and Demo
        // If not valid, by default displays error message
        static void SubMenu(string folder, string fileName)
        {
            Console.Clear();
            ReadFile(folder, fileName);
            
            int option;
            do
            {
                option = DisplaySubMenu();
                switch (option)
                {
                    case 1:
                        InsertOp();
                        break;
                    case 2:
                        FindOp();
                        break;
                    case 3:
                        DeleteOp();
                        break;
                    case 4:
                        DisplayPrintMenu();
                        break;
                    case 5:
                        TimeComplex();
                        break;
                    case 6:
                        Demonstration();
                        break;
                    case 7:
                        return;
                }
                if (option != 7)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (option != 7);
        }
        #endregion

        #region PrintOptions Menu
        static void DisplayPrintMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("**** What would you like to Print ****");
                Console.WriteLine("");
                Console.WriteLine("1 - Display Pre-Order");
                Console.WriteLine("2 - Display In-Order");
                Console.WriteLine("3 - Display Post-Order");
                Console.WriteLine("4 - Back");
                Console.WriteLine("Enter Option: ");
                if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 4)
                {
                    if (option == 4) return;
                    PrintMenuOutput(option);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        static void PrintMenuOutput(int option)
        {
            switch (option)
            {
                case 1:
                    PreOrderOp();
                    break;
                case 2:
                    InOrderOp();
                    break;
                case 3:
                    PostOrderOp();
                    break;
                case 4:
                    return;
            }
        }
        #endregion

        #region Operations
        static void PreOrderOp()
        {
            Console.Clear();
            Console.WriteLine("*** PreOrder ***");
            string printPreOrder = bsTree.PreOrder();
            Console.WriteLine(printPreOrder);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void PostOrderOp()
        {
            Console.Clear();
            Console.WriteLine("*** PostOrder ***");
            string printPostOrder = bsTree.PostOrder();
            Console.WriteLine(printPostOrder);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void InOrderOp()
        {
            Console.Clear();
            Console.WriteLine("*** InOrder ***");
            string printInOrder = bsTree.InOrder();
            Console.WriteLine(printInOrder);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void FindOp()
        {
            Console.WriteLine("Enter word to find: ");
            string findWord = Console.ReadLine();
            string wordFound = bsTree.Find(findWord);
            Console.WriteLine(wordFound);
        }

        static void DeleteOp()
        {
            Console.WriteLine("Enter word to delete: ");
            string deleteWord = Console.ReadLine();
            string wordToDelete = bsTree.Remove(deleteWord);
            Console.WriteLine(wordToDelete);
        }

        static void InsertOp()
        {
            Console.WriteLine("Enter word to insert: ");
            string insertWord = Console.ReadLine();
            string wordInserted = bsTree.InsertAdd(insertWord, insertWord.Length);
            Console.WriteLine(wordInserted);
        }

        #endregion

        #region Demo
        static void Demonstration()
        {
            Console.WriteLine("**** DEMONSTRATION ****");
            // Show printing options
            Console.WriteLine("-- Display PreOrder --");
            PreOrderOp();
            Console.Clear();
            Console.WriteLine("-- Display PostOrder --");
            PostOrderOp();
            Console.Clear();
            Console.WriteLine("-- Display InOrder --");
            InOrderOp();

            // Show insert
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-- Insert --");           
            // Insert a word that cant be added
            Console.WriteLine("Insert '#Hello'");
            bsTree.Add("#Hello", 6);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // Insert a duplicate that cant be added
            Console.WriteLine("Insert duplicate word 'able'");
            bsTree.Add("able", 4);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // Insert a word that can be added
            Console.WriteLine("Insert 'Forget'");
            bsTree.Add("Forget", 6);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
            // Show what words were added/not added
            Console.WriteLine("Show if words were added/not added");
            PreOrderOp();

            // Show delete
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-- Delete --");
            // Delete word that doesnt exist
            Console.WriteLine("Delete the word '#Hello'");
            string deleteInvalid = "#Hello";
            string invalidDelete = bsTree.Remove(deleteInvalid);
            Console.WriteLine(invalidDelete);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // Delete word that does exist
            Console.WriteLine("Delete the word 'able'");
            string deleteWord = "able";
            string wordToDelete = bsTree.Remove(deleteWord);
            Console.WriteLine(wordToDelete);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Show that word was deleted or not deleted
            Console.WriteLine("Show if words were deleted or not deleted");
            PreOrderOp();

            // Show find
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-- Find --");
            // Find word that doesnt exist
            Console.WriteLine("Try find word that doesnt exist '#Hello'");
            string invalidWord = "#Hello";
            string notFound = bsTree.Find(invalidWord);
            Console.WriteLine(notFound);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("Find word that does exist 'allowed'");
            string findWord = "act";
            string wordFound = bsTree.Find(findWord);
            Console.WriteLine(wordFound);
            Console.WriteLine("Press any key to go back to menu...");
            Console.ReadKey();
        }
        #endregion

        #region TimeComplexity Report
        static void InsertSW(string folder, string wordFile)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            ReadFile(folder, $"{wordFile}-words.txt");

            sw.Stop();
            TimeSpan timespan = sw.Elapsed;
            Console.WriteLine("- Time taken to perform insert");
            Console.WriteLine(wordFile + " Words: " + timespan.ToString(@"mm\:ss\.fffffff") + " {m:ss}");
            Console.WriteLine(wordFile + " Words: " + timespan.ToString(@"ss\.fffffff") + " {s}");
        }

        static void FindSW(string folder, string wordFile)
        {
            ReadFile(folder, $"{wordFile}-words.txt");

            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            bsTree.Find("after");

            sw.Stop();
            TimeSpan timespan = sw.Elapsed;
            Console.WriteLine("- Time taken to perform Find");
            Console.WriteLine(wordFile + " Words: " + timespan.ToString(@"mm\:ss\.fffffff") + " {m:ss}");
            Console.WriteLine(wordFile + " Words: " + timespan.ToString(@"ss\.fffffff") + " {s}");
        }

        static void InsertTest(string folder)
        {
            string[] sizes = {"1000", "5000", "10000", "15000", "20000", "25000", "30000", "35000",
            "40000", "45000", "50000"};
            foreach (var size in sizes)
            {
                InsertSW(folder, size);
            }
        }

        static void FindTest(string folder)
        {
            string[] sizes = {"1000", "5000", "10000", "15000", "20000", "25000", "30000", "35000",
            "40000", "45000", "50000"};
            foreach (var size in sizes)
            {
                FindSW(folder, size);
            }
        }

        static void TimeComplex()
        {
            // Clear the bsTree so results aren't affected
            bsTree.Clear();
            Console.WriteLine("**** Time Complexity Report ****");
            Console.WriteLine("-- Insert Times --");
            Console.WriteLine("Ordered Words: ");
            Console.WriteLine();

            // Calls InsertTest method to repeat test for each file size
            InsertTest("ordered");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("Random Words: ");
            Console.WriteLine();
            InsertTest("random");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("-- Find Times --");

            Console.WriteLine("Ordered Words: ");
            Console.WriteLine();
            // Calls FindTest method to repeat the test for each file size
            FindTest("ordered");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("Random Words: ");
            Console.WriteLine();
            FindTest("random");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

    }
}
