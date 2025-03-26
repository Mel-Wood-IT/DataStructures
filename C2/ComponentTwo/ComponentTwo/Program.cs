using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTwo
{
    internal class Program
    {
        // Base directory as a relative path to the word files
        private static readonly string BaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "WordFiles");
        // Double list
        private static DbleListDS listDS = new DbleListDS();

        static void Main(string[] args)
        {
            int opt = 0;

            // While true loop to keep program running until valid input is entered.
            while (true)
            {
                opt = DisplayMenu();

                if (opt >= 1 && opt <= 13)
                {
                    MenuOutput(opt);

                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter a number between 1-12.");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
        }

        #region ReadFile
        // Reads the file and returns the file content to the InsertOp
        // Uses try-catch to show if the file was successfully found, displays error if file not found
        static String[] ReadFile(string filename)
        {
            try
            {
                return File.ReadAllLines(filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {filename}: {ex.Message}");
                return new string[0];
            }
        }

        // Insert operation uses foreach to run through each line in
        // the file and removes any lines that start with # or duplicates
        static void FileInsert(string filename)
        {
            String[] filetoadd = ReadFile(filename);
            if (filetoadd.Length == 0)
            {
                Console.WriteLine("File is empty");
            }
            else
            {
                foreach (String line in filetoadd)
                {   // Insert into list
                    string wordAdd = line;
                    int wordLen = wordAdd.Length;
                    listDS.InsertEnd(wordAdd, wordLen);
                }
            }
        }
        #endregion


        #region MainMenu
        // Displays the files for the user to choose
        static int DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("**** Select a file ****");
            Console.WriteLine("");
            Console.WriteLine("[1] - 1000 Word file");
            Console.WriteLine("[2] - 5000 Word file");
            Console.WriteLine("[3] - 10000 Word file");
            Console.WriteLine("[4] - 15000 Word file");
            Console.WriteLine("[5] - 20000 Word file");
            Console.WriteLine("[6] - 25000 Word file");
            Console.WriteLine("[7] - 30000 Word file");
            Console.WriteLine("[8] - 35000 Word file");
            Console.WriteLine("[9] - 40000 Word file");
            Console.WriteLine("[10] - 45000 Word file");
            Console.WriteLine("[11] - 50000 Word file");
            Console.WriteLine("[12] - Time Complexity Report");
            Console.WriteLine("[13] - Exit");
            Console.WriteLine("Enter option: ");

            int option;

            // If the option is valid it will return the option selected
            // else it is not valid and returns 0
            if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 13)
            {
                return option;
            }
            else
            {
                return 0;
            }
        }

        // Selected option is passed on to menu output, this then passes the file
        // pathway selected to the sub menu and breaks.
        static void MenuOutput(int option)
        {
            string filename = string.Empty;
            switch (option)
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
                    TimeComplexity();
                    break;
                case 13:
                    // Exit the program
                    Environment.Exit(0);
                    break;
            }

            // If the filename is not empty it will combine it with the BaseDirectory
            // It then displays the submenu with that filename
            if (!string.IsNullOrEmpty(filename))
            {
                string fullPath = Path.Combine(BaseDirectory, filename);
                SubMenuOutput(fullPath);
            }
        }
        #endregion


        #region SubMenu
        static int DisplaySubMenu()
        {
            Console.Clear();
            Console.WriteLine("**** What would you like to do ****");
            Console.WriteLine("[1] - Display list");
            Console.WriteLine("[2] - Insert functions");
            Console.WriteLine("[3] - Find Word");
            Console.WriteLine("[4] - Delete Functions");
            Console.WriteLine("[5] - Demonstration");
            Console.WriteLine("[6] - Back to Main");
            Console.WriteLine("Enter option: ");

            int option;

            // If the option is valid it will return the option selected
            // else it is not valid and returns 0, which is invalid
            if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 6)
            {
                return option;
            }
            else
            {
                return 0;
            }
        }
        
        static void SubMenuOutput(string filename)
        {   
            listDS.Clear();
            // Used this to test that list was being emptied before adding new file
            //listDS.ToPrintOp();
            //Console.WriteLine();
            //Console.WriteLine("Any key");
            //Console.ReadKey();
            FileInsert(filename);
            
            int option;

            do
            {
                option = DisplaySubMenu();
                switch (option)
                {
                    case 1:
                        listDS.ToPrintOp();
                        break;
                    case 2:
                        InsertMenuOutput();
                        break;
                    case 3:
                        FindOp();
                        break;
                    case 4:
                        DeleteMenuOutput();
                        break;
                    case 5:
                        Demonstration();
                        break;
                    case 6: 
                        DisplayMenu();
                        break;
                }
                if (option != 6)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (option != 6);
        }
        #endregion


        #region Menu Operations
        static void FindOp()
        {
            Console.WriteLine("Enter word to find: ");
            string findWord = Console.ReadLine();
            string wordFound = listDS.Find(findWord);
            Console.WriteLine(wordFound);
        }

        static void InsertFrontOp()
        {
            Console.WriteLine("Enter word to insert at front: ");
            string frontWord = Console.ReadLine();
            listDS.AddToFront(frontWord, frontWord.Length);
        }

        static void InsertBackOp()
        {
            Console.WriteLine("Enter word to insert at back: ");
            string backWord = Console.ReadLine();
            listDS.AddToEnd(backWord, backWord.Length);
        }

        static void InsertBeforeOp()
        {
            Console.WriteLine("Enter word to insert before target: ");
            string beforeWord = Console.ReadLine();
            Console.WriteLine("Enter target word: ");
            string beforeTarget = Console.ReadLine();
            string resultBefore = listDS.AddBefore(beforeWord, beforeWord.Length, beforeTarget);
            Console.WriteLine(resultBefore);
        }

        static void InsertAfterOp()
        {
            Console.WriteLine("Enter word to insert after target: ");
            string afterWord = Console.ReadLine();
            Console.WriteLine("Enter target word: ");
            string afterTarget = Console.ReadLine();
            string resultAfter = listDS.AddAfter(afterWord, afterWord.Length, afterTarget);
            Console.WriteLine(resultAfter);
        }

        static void DeleteFrontOp()
        {
            string deleteFront = listDS.RemoveFront();
            Console.WriteLine(deleteFront);
        }

        static void DeleteBackOp()
        {
            string deleteEnd = listDS.RemoveRear();
            Console.WriteLine(deleteEnd);
        }

        static void DeleteNodeOp()
        {
            Console.WriteLine("Enter word to delete from list: ");
            string deleteWord = Console.ReadLine();
            string deleteResult = listDS.RemoveNode(deleteWord);
            Console.WriteLine(deleteResult);
        }

        #endregion


        #region InsertMenu
        static int DisplayInsertMenu()
        {
            Console.Clear();
            Console.WriteLine("**** Select Insert Operation ****");
            Console.WriteLine("[1] - Insert Front");
            Console.WriteLine("[2] - Insert Back");
            Console.WriteLine("[3] - Insert Before");
            Console.WriteLine("[4] - Insert After");
            Console.WriteLine("[5] - Back to SubMenu");
            Console.WriteLine("Enter option: ");

            int option;
            // If the option is valid it will return the option selected
            // else it is not valid and returns 0, which is invalid
            if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 5)
            {
                return option;
            }
            else
            {
                return 0;
            }
        }

        static void InsertMenuOutput()
        {
            int option;

            do
            {
                option = DisplayInsertMenu();
                switch (option)
                {
                    case 1:
                        InsertFrontOp();
                        break;
                    case 2:
                        InsertBackOp();
                        break;
                    case 3:
                        InsertBeforeOp();
                        break;
                    case 4:
                        InsertAfterOp();
                        break;
                    case 5:
                        DisplaySubMenu();
                        break;
                }
                if (option != 5)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (option != 5);
        }
        #endregion


        #region DeleteMenu
        static int DisplayDeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("[1] - Delete at Front");
            Console.WriteLine("[2] - Delete at End");
            Console.WriteLine("[3] - Delete Node");
            Console.WriteLine("[4] - Back to SubMenu");
            Console.WriteLine("Enter option: ");

            int option;
            // If the option is valid it will return the option selected
            // else it is not valid and returns 0, which is invalid
            if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 4)
            {
                return option;
            }
            else
            {
                return 0;
            }
        }

        static void DeleteMenuOutput()
        {
            int option;

            do
            {
                option = DisplayDeleteMenu();
                switch (option)
                {
                    case 1:
                        DeleteFrontOp();
                        break;
                    case 2:
                        DeleteBackOp();
                        break;
                    case 3:
                        DeleteNodeOp();
                        break;
                    case 4:
                        DisplaySubMenu();
                        break;
                }
                if (option != 4)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (option != 4);
        }
        #endregion


        #region Demonstration
        static void Demonstration()
        {
            // Show ToPrintOp
            Console.WriteLine("**** DEMONSTRATION ****");
            Console.WriteLine("-- Display List --");
            listDS.ToPrintOp();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Show insert front
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-- Insert front --");
            Console.WriteLine("Insertfront '#Hello'");
            listDS.AddToFront("#Hello", 6);
            Console.WriteLine("Insertfront duplicate word 'the'");
            listDS.AddToFront("the", 3);
            Console.WriteLine("Insertfront word 'Ghost' successfully");
            listDS.AddToFront("Ghost", 5);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // Show that words have/haven't been added
            listDS.ToPrintOp();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Show insert back
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-- Insert Back --");
            Console.WriteLine("Insertback '#Hello'");
            listDS.AddToEnd("#Hello", 6);
            Console.WriteLine("Insertback duplicate word 'the'");
            listDS.AddToEnd("the", 3);
            Console.WriteLine("Insertback word 'Forget' successfully");
            listDS.AddToEnd("Forget", 5);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // Show that words have/haven't been added
            listDS.ToPrintOp();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Show insert after
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-- Insert after --");
            Console.WriteLine("Insert '#Hello' after target 'allowed'");
            string resultAfter1 = listDS.AddAfter("#Hello", 6, "allowed");
            Console.WriteLine(resultAfter1);
            Console.WriteLine("Insert 'the' after target 'allowed'");
            string resultAfter2 = listDS.AddAfter("the", 3, "allowed");
            Console.WriteLine(resultAfter2);
            Console.WriteLine("Insert 'Zoom' after target 'started'");
            string resultAfter3 = listDS.AddAfter("Zoom", 4, "started");
            Console.WriteLine(resultAfter3);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // Show that words have/haven't been added
            listDS.ToPrintOp();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Show insert before
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-- Insert before --");
            Console.WriteLine("Insert '#Hello' before target 'allowed'");
            string resultBefore1 = listDS.AddBefore("#Hello", 6, "allowed");
            Console.WriteLine(resultBefore1);
            Console.WriteLine("Insert 'the' before target 'allowed'");
            string resultBefore2 = listDS.AddBefore("the", 3, "allowed");
            Console.WriteLine(resultBefore2);
            Console.WriteLine("Insert 'Plate' before target 'started'");
            string resultBefore3 = listDS.AddBefore("Plate", 4, "started");
            Console.WriteLine(resultBefore3);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // Show that words have/haven't been added
            listDS.ToPrintOp();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Show delete functions
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-- Delete Front --");
            Console.WriteLine("Will delete 'Ghost' from the front");
            string deleteFront = listDS.RemoveFront();
            Console.WriteLine(deleteFront);
            Console.WriteLine("-- Delete End --");
            Console.WriteLine("Will delete 'Forget' from the end");
            string deleteEnd = listDS.RemoveRear();
            Console.WriteLine(deleteEnd);
            Console.WriteLine("-- Delete Node --");
            Console.WriteLine("Delete 'Cake' not in list");
            string deleteResult1 = listDS.RemoveNode("Cake");
            Console.WriteLine(deleteResult1);
            Console.WriteLine("Remove node 'sister'");
            string deleteResult2 = listDS.RemoveNode("sister");
            Console.WriteLine(deleteResult2);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // Show that words have/haven't been deleted
            listDS.ToPrintOp();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Show find function
            Console.WriteLine("-- Find word --");
            Console.WriteLine("Try find word '#Hello' that doesn't exist");
            string wordNotFound = listDS.Find("#Hello");
            Console.WriteLine(wordNotFound);
            Console.WriteLine("Find word 'battle' in the list");
            string wordFound = listDS.Find("battle");
            Console.WriteLine(wordFound);
            Console.WriteLine();
            Console.WriteLine("Press any key to go back to menu...");
            Console.ReadKey();
        }
        #endregion


        #region TimeComplexity
        static void TimeComplexity()
        {
            Console.WriteLine("**** Time Complexity Report ****");
            Console.WriteLine("-------------------------------------");
            // Record inserting a single word against the number of words
            Console.WriteLine("-- Inserting single word --");
            Console.WriteLine();
            InsertTest();
        }

        static void InsertSW(string fullPath)
        {
            string[] lines = ReadFile(fullPath);
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            listDS.AddToEnd("Zoom", 4);

            sw.Stop();
            TimeSpan timespan = sw.Elapsed;
            Console.WriteLine($"- Time taken to perform insert");
            Console.WriteLine($"{lines.Length} Words: {timespan.ToString(@"mm\:ss\.fffffff")}");
            Console.WriteLine($"{lines.Length} Words: {timespan.ToString(@"ss\.fffffff")}");
        }
        
        
        static void InsertTest()
        {
            string[] sizes = {"1000-words.txt", "5000-words.txt", "10000-words.txt", "15000-words.txt", 
                "20000-words.txt", "25000-words.txt", "30000-words.txt", "35000-words.txt", "40000-words.txt", 
                "45000-words.txt", "50000-words.txt"};
            foreach (var size in sizes)
            {
                string fullPath = Path.Combine(BaseDirectory, size);
                listDS.Clear();
                // Used this to test that list was being emptied before adding new file
                listDS.ToPrintOp();
                Console.WriteLine();
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                InsertSW(fullPath);
            }
        }
        #endregion
    }
}
