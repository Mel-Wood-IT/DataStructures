using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOne
{
    internal class Program
    {              
        // Global dictionary instance
        private static MyDictionary myDictionary = new MyDictionary();

        static void Main(string[] args)
        {
            string folName, filName;
            int folNum, filNum;
            int opt;

            // Retrieving the folder name from the main menu input
            folNum = MainMenu();
            folName = MainMenuOutput(folNum);

            // Retrieving the file name from the file menu input
            filNum = FileMenu();
            filName = FileMenuOutput(filNum);

            // try-catch for error handling
            try
            {
                // try to read the file the user chose
                ReadFile(folName, filName);

                // displaying and performing the operation the user chooses
                opt = DisplaySubMenu();
                SubMenu(opt);
            }
            catch
            {
                // Prints error message if the file can not be found
                Console.WriteLine("Error reading file");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        #region MainMenu
        // Display the main menu for the user to choose either ordered or random folder
        // This will keep displaying until valid option is choosen.
        static int MainMenu()
        {
            int opt = 0;
            
            while (opt < 1 || opt > 2)
            {
                Console.Clear();
                Console.WriteLine("**** Dictionary ****");
                Console.WriteLine("Enter a folder to use");
                Console.WriteLine("1 - Ordered Folder");
                Console.WriteLine("2 - Random Folder");
                Console.WriteLine("Enter Option: ");
                opt = int.Parse(Console.ReadLine());
            }
            return opt;    
        }
               
        // Set the folder name for random or ordered using if-else statement and return the folder name
        static string MainMenuOutput(int opt)
        {
            string folder;
            if (opt == 1)
            {
                folder = "ordered";
            }
            else
            {
                folder = "random";
            }
            return folder;
        }
        #endregion


        #region FileMenu      
        // Displays the file menu options for user to choose which file they wish to use
        // Menu will display until a valid option is selected
        static int FileMenu()
        {
            int opt = 0;
            while (opt < 1 || opt > 11)
            {
                Console.Clear();
                Console.WriteLine("**** Select a file ****");
                Console.WriteLine("");
                Console.WriteLine("1 - 1000 Word file");
                Console.WriteLine("2 - 5000 Word file");
                Console.WriteLine("3 - 10,000 Word file");
                Console.WriteLine("4 - 15,000 Word file");
                Console.WriteLine("5 - 20,000 Word file");
                Console.WriteLine("6 - 25,000 Word file");
                Console.WriteLine("7 - 30,000 Word file");
                Console.WriteLine("8 - 35,000 Word file");
                Console.WriteLine("9 - 40,000 Word file");
                Console.WriteLine("10 - 45,000 Word file");
                Console.WriteLine("11 - 50,000 Word file");
                Console.WriteLine("Enter option: ");
                opt = int.Parse(Console.ReadLine());
            }
            return opt;
        }        
               
               
        // Selected file name is returned to main or invalid option is printed.
        static string FileMenuOutput(int file)
        {
            string filename = string.Empty;
            switch (file)
            {
                case 1:
                    return "1000-words";
                case 2:
                    return "5000-words";
                case 3:
                    return "10000-words";
                case 4:
                    return "15000-words";
                case 5:
                    return "20000-words";
                case 6:
                    return "25000-words";
                case 7:
                    return "30000-words";
                case 8:
                    return "35000-words";
                case 9:
                    return "40000-words";
                case 10:
                    return "45000-words";
                case 11:
                    return "50000-words";
                default:
                    Console.WriteLine("Invalid option.");
                    return null;
            }
        }

        #endregion


        #region SubMenu
        // Displays the menu till the user input entered is valid
        static int DisplaySubMenu()
        {
            int opt = 0;
            while (opt < 1 || opt > 7)
            {
                Console.Clear();
                Console.WriteLine("**** What would you like to do ****");
                Console.WriteLine("");
                Console.WriteLine("1 - Insert Entry");
                Console.WriteLine("2 - Find Entry");
                Console.WriteLine("3 - Delete Entry");
                Console.WriteLine("4 - Display Contents");
                Console.WriteLine("5 - Time Complexity");
                Console.WriteLine("6 - Demonstration");
                Console.WriteLine("7 - Back to main menu");
                Console.WriteLine("Enter Option: ");
                opt = int.Parse(Console.ReadLine());
            }
            return opt;
        }

        // Sub menu switch that calls on AddingOp, FindOp, DeleteOp, ToPrint, TimeComplex, and Demo
        // If not valid, by default displays error message
        static void SubMenu(int option)
        {

            Console.Clear();
            switch (option)
            {
                case 1:
                    Console.WriteLine("**Insert Operation selected**");
                    myDictionary.AddingOp("nugget");
                    myDictionary.AddingOp("#nugget");
                    break;
                case 2:
                    Console.WriteLine("**Find Operation selected**");
                    // Finding word example
                    myDictionary.FindOp("woman");
                    // Misspelt example
                    myDictionary.FindOp("woooman");
                    // Cannot be added
                    myDictionary.FindOp("#hashtag");
                    break;
                case 3:
                    Console.WriteLine("**Delete Operation selected**");
                    // Deleting word example
                    myDictionary.DeleteOp("woman");
                    // Misspelt example
                    myDictionary.DeleteOp("woooman");
                    break;
                case 4:
                    Console.WriteLine("**Print Operation selected**");
                    Console.WriteLine(myDictionary.ToPrintOp());
                    break;
                case 5:
                    TimeComplex();
                    break;
                case 6:
                    Console.WriteLine("**** Print Operation Demo ****");
                    Console.WriteLine(myDictionary.ToPrintOp());
                    Console.WriteLine();
                    Console.WriteLine("**** Find Operation Demo ****");
                    Console.WriteLine("Finding a word: ");
                    myDictionary.FindOp("woman");
                    Console.WriteLine("Finding a misspelt word: ");
                    myDictionary.FindOp("woooman");
                    Console.WriteLine();
                    Console.WriteLine("**** Add Operation Demo ****");
                    Console.WriteLine("Adding a word: ");
                    myDictionary.AddingOp("nugget");
                    Console.WriteLine("Adding a word that should not be added: ");
                    myDictionary.AddingOp("#nugget");
                    Console.WriteLine("Adding a duplicate word: ");
                    myDictionary.AddingOp("woman");
                    Console.WriteLine();
                    Console.WriteLine("**** Delete Operation Demo ****");
                    Console.WriteLine("Deleting a word: ");
                    myDictionary.DeleteOp("woman");
                    Console.WriteLine("Deleting a word that does not exist: ");
                    myDictionary.DeleteOp("#hashtag");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();                   
                    Console.WriteLine(myDictionary.ToPrintOp());
                    break;
                case 7:
                    Console.WriteLine("Back to main menu");
                    MainMenu(); 
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        #endregion
        
        
        
        // Insert operation uses foreach to run through each line in
        // the file and adds it to the dictionary
        static void ReadFile(string folder, string fileName)
        {
            String[] filetoadd = System.IO.File.ReadAllLines($@"Words Files/{folder}/{fileName}.txt");
            foreach (String line in filetoadd)
            {
                myDictionary.AddOp(line);
            }
        }

        #region TimeComplexity
        
        // stopwatch method for recording the time it takes to insert the files into the dictionary
        // Using the input from the InsertTest method.
        static void InsertSW(string folder, string wordFile)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            ReadFile(folder, $"{wordFile}-words");

            sw.Stop();
            TimeSpan timespan = sw.Elapsed;
            Console.WriteLine("- Time taken to perform insert");
            Console.WriteLine(wordFile + " Words: " + timespan.ToString(@"mm\:ss\.fffffff") + " {m:ss}");
            Console.WriteLine(wordFile + " Words: " + timespan.ToString(@"ss\.fffffff") + " {s}");
        }

        // stopwatch method for recording the time it takes to find the word "woman" in the dictionary
        // Using the input from the FindTest method.
        static void FindSW(string folder, string wordFile)
        {
            ReadFile(folder, $"{wordFile}-words");

            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            myDictionary.FindOp("woman");

            sw.Stop();
            TimeSpan timespan = sw.Elapsed;
            Console.WriteLine("- Time taken to perform Find");
            Console.WriteLine(wordFile + " Words: " + timespan.ToString(@"mm\:ss\.fffffff") + " {m:ss}");
            Console.WriteLine(wordFile + " Words: " + timespan.ToString(@"ss\.fffffff") + " {s}");
        }

        // Timecomplex method runs all the tests at the same time so all times can be recorded
        static void TimeComplex()
        {
            myDictionary.Clear(); // clearing the dictionary before report
            
            Console.WriteLine("**** Time Complexity Report ****");
            Console.WriteLine("* Insert times *");
            
            Console.WriteLine("Ordered Words:");
            Console.WriteLine();
            // Calls InsertTest method to repeat the test for each file size
            InsertTest("ordered");
            Console.WriteLine();
            
            Console.WriteLine("Random Words:");
            Console.WriteLine();
            InsertTest("random");

            Console.WriteLine("* Find times *");
            
            Console.WriteLine("Ordered Words:");
            Console.WriteLine();
            // Calls FindTest method to repeat the test for each file size
            FindTest("ordered");
            
            Console.WriteLine("Random Words:");
            Console.WriteLine();
            FindTest("random");
        }

        // Creates an array of all the different file sizes and then for each file size it performs the InsertSW method
        static void InsertTest(string folder)
        {
            string[] sizes = {"1000", "5000", "10000", "15000", "20000", "25000", "30000", "35000",
            "40000", "45000", "50000"};
            foreach (var size in sizes)
            {
                InsertSW(folder, size);
            }
        }

        // Creates an array of all the different file sizes and then for each file size it performs the FindSW method
        static void FindTest(string folder)
        {
            string[] sizes = {"1000", "5000", "10000", "15000", "20000", "25000", "30000", "35000",
            "40000", "45000", "50000"};
            foreach (var size in sizes)
            {
                FindSW(folder, size);
            }
        }

        #endregion
    }
}