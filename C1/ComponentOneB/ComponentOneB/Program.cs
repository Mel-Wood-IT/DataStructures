using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneB
{
    internal class Program
    {
        // Base directory as a relative path to the random files
        private static readonly string BaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Words Files", "random");

        static void Main(string[] args)
        {
            ArrayDS arrayDS = new ArrayDS();

            int opt = 0;

            // While true loop to keep program running until valid input is entered.
            while (true)
            {
                opt = DisplayMenu();

                if (opt >= 1 && opt <= 12)
                {
                    MenuOutput(opt, arrayDS);
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


        #region ReadFile+Insert
        // Insert operation uses foreach to run through each line in
        // the file and removes any lines that start with # or duplicates
        static void InsertOp(string filename, ArrayDS arrayDS)
        {
            String[] filetoadd = ReadFile(filename);
            foreach (String line in filetoadd)
            {
                if (!line.StartsWith("#"))
                {
                    string wordAdd = line;
                    int wordLen = wordAdd.Length;
                    arrayDS.Add(wordAdd, wordLen);
                }
            }
        }

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
                return new string[0]; // Return an empty array in case of an error
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
            Console.WriteLine("12 - Exit");
            Console.WriteLine("Enter option: ");

            int option;

            // If the option is valid it will return the option selected
            // else it is not valid and returns 0, which is invalid
            if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 12)
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
        static void MenuOutput(int option, ArrayDS arrayDS)
        {
            string filename = string.Empty;
            switch (option)
            {
                case 1:
                    Console.WriteLine("1000-words selected.");
                    filename = @"1000-words.txt";
                    break;
                case 2:
                    Console.WriteLine("5000-words selected.");
                    filename = @"5000-words.txt";
                    break;
                case 3:
                    Console.WriteLine("10000-words selected.");
                    filename = @"10000-words.txt";
                    break;
                case 4:
                    Console.WriteLine("15000-words selected.");
                    filename = @"15000-words.txt";
                    break;
                case 5:
                    Console.WriteLine("20000-words selected.");
                    filename = @"20000-words.txt";
                    break;
                case 6:
                    Console.WriteLine("25000-words selected.");
                    filename = @"25000-words.txt";
                    break;
                case 7:
                    Console.WriteLine("30000-words selected.");
                    filename = @"30000-words.txt";
                    break;
                case 8:
                    Console.WriteLine("35000-words selected.");
                    filename = @"35000-words.txt";
                    break;
                case 9:
                    Console.WriteLine("40000-words selected.");
                    filename = @"40000-words.txt";
                    break;
                case 10:
                    Console.WriteLine("45000-words selected.");
                    filename = @"45000-words.txt";
                    break;
                case 11:
                    Console.WriteLine("50000-words selected.");
                    filename = @"50000-words.txt";
                    break;
                case 12:
                    Console.WriteLine("Exiting program...");
                    // Exit the program
                    Environment.Exit(0);
                    break; // Terminates the program
            }
            // If the filename is not empty it will combine it with the BaseDirectory
            // It then displays the submenu with that filename
            if (!string.IsNullOrEmpty(filename))
            {
                string fullPath = Path.Combine(BaseDirectory, filename);
                SubMenu(fullPath);
            }

        }
        #endregion

        #region SubMenu

        // Displays the submenu with the option to print array, and both sorting methods
        static int DisplaySubMenu()
        {
            Console.Clear();
            Console.WriteLine("**** What would you like to do ****");
            Console.WriteLine("1 - Print Array");
            Console.WriteLine("2 - Selection Sort");
            Console.WriteLine("3 - Merge Sort");
            Console.WriteLine("4 - Time Complexity");
            Console.WriteLine("5 - Back to main");
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

        // Submenu output that call the methods ToPrint, SelectionSort, and MergeSort 
        // do-while loop will keep the submenu on screen till the user chooses go back to main menu
        static void SubMenu(string filename)
        {
            int option;
            ArrayDS arrayDS = new ArrayDS();
            InsertOp(filename, arrayDS);

            do
            {
                option = DisplaySubMenu();
                switch (option)
                {
                    case 1:
                        Console.WriteLine(arrayDS.ToPrint());
                        break;
                    case 2:
                        Console.WriteLine(arrayDS.SelectionSort()); 
                        break;
                    case 3:
                        Console.WriteLine(arrayDS.MergeSort());
                        break;
                    case 4:
                        TimeComplex();
                        break;
                    case 5:
                        DisplayMenu();
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

        #region TimeComplexity
        static void TimeComplex()
        {
            Console.WriteLine("**** Time Complexity Report ****");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("* Selection times *");
            Console.WriteLine();
            // Calls SelectionTest method to repeat the test for each file size
            SelectionTest();
            Console.WriteLine();

            Console.WriteLine("* Merge times *");
            Console.WriteLine();
            // Calls MergeTest method to repeat the test for each file size
            MergeTest();
        }

        // Creates an array of all the different file sizes and then for each file size it performs the SelectionSW method
        static void SelectionTest()
        {
            string[] sizes = {"1000-words.txt", "5000-words.txt", "10000-words.txt", "15000-words.txt", "20000-words.txt", 
                "25000-words.txt", "30000-words.txt", "35000-words.txt", "40000-words.txt", "45000-words.txt", "50000-words.txt" };
            foreach (var size in sizes)
            {
                //Combine the size with the BaseDirectory
                string fullPath = Path.Combine(BaseDirectory, size);
                SelectionSW(fullPath);
            }
        }

        // Creates an array of all the different file sizes and then for each file size it performs the MergeSW method
        static void MergeTest()
        {
            string[] sizes = {"1000-words.txt", "5000-words.txt", "10000-words.txt", "15000-words.txt", "20000-words.txt", "25000-words.txt",
                "30000-words.txt", "35000-words.txt", "40000-words.txt", "45000-words.txt", "50000-words.txt" };            
            
            foreach (var size in sizes)
            {
                //Combine the size with the BaseDirectory
                string fullPath = Path.Combine(BaseDirectory, size);
                MergeSW(fullPath);
            }
        }
        
        
        // stopwatch method for recording the time it takes to sort the file using Selection Sort
        static void SelectionSW(string filename)
        {
            ArrayDS arrayDS = new ArrayDS();    
            InsertOp(filename, arrayDS);

            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            arrayDS.SelectionSort();

            sw.Stop();
            TimeSpan timespan = sw.Elapsed;
            Console.WriteLine("- Time taken to perform Selection Sort");
            Console.WriteLine("Time: " + timespan.ToString(@"mm\:ss\.fffffff") + " {m:ss}");
        }

        // stopwatch method for recording the time it takes to sort the file using Merge Sort
        static void MergeSW(string filename)
        {
            ArrayDS arrayDS = new ArrayDS();
            InsertOp(filename, arrayDS);

            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            arrayDS.MergeSort();

            sw.Stop();
            TimeSpan timespan = sw.Elapsed;
            Console.WriteLine("- Time taken to perform Merge Sort");
            Console.WriteLine("Time: " + timespan.ToString(@"mm\:ss\.fffffff") + " {m:ss}");
        }
        #endregion
    }
}
