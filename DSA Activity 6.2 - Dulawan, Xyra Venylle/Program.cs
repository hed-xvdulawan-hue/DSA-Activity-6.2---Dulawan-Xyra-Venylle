// DSA Activity 6.2 - Dulawan, Xyra Venylle
using System;
using System.Collections.Generic;

namespace DSAActivity6_2
{
    class Program
    {
        static string[] choiceNames = { "DEPOSIT", "WITHDRAW", "INQUIRE", "OTHERS" };
        static string[] listTitles = { "DEPOSIT", "WITHDRAWAL", "INQUIRE", "OTHERS" };
        static string[] tags = { "DEP", "WID", "INQ", "OTH" };

        static Queue<string>[] queues = new Queue<string>[4];
        static int[] served = new int[4];

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int i = 0; i < queues.Length; i++)
                queues[i] = new Queue<string>();

            Console.WriteLine("Welcome to ECO BANK, INC.\n");

            bool running = true;

            while (running)
            {
                ShowMenu();
                Console.Write("Enter code: ");
                string input = Console.ReadLine();

                if (input == null)
                    break;

                int code;
                if (!int.TryParse(input.Trim(), out code))
                {
                    Console.WriteLine("\nInvalid code. Please try again.\n");
                    continue;
                }

                if (code >= 1 && code <= 4)
                {
                    int index = code - 1;
                    Console.WriteLine("\nYou have chosen " + choiceNames[index] + "\n");

                    string account = "";
                    while (account.Length == 0)
                    {
                        Console.Write("Please enter your account name: ");
                        string name = Console.ReadLine();

                        if (name == null)
                        {
                            running = false;
                            break;
                        }

                        account = name.Trim();
                    }

                    if (!running)
                        break;

                    queues[index].Enqueue(account);
                    Console.WriteLine();
                    DisplayStatus();
                }
                else if (code == 10 || code == 20 || code == 30 || code == 40)
                {
                    int index = code / 10 - 1;

                    if (queues[index].Count == 0)
                    {
                        Console.WriteLine("\nNo client on the " + listTitles[index] + " line.\n");
                    }
                    else
                    {
                        queues[index].Dequeue();
                        served[index]++;
                        Console.WriteLine("\nTransaction Completed!\n");
                        DisplayStatus();
                    }
                }
                else if (code == 0)
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid code. Please try again.\n");
                }
            }

            ShowSummary();
        }

        static void ShowMenu()
        {
            Console.WriteLine("Select your transaction\n");
            Console.WriteLine("1 – DEPOSIT");
            Console.WriteLine("2 – WITHDRAW");
            Console.WriteLine("3 – INQUIRE");
            Console.WriteLine("4 – OTHERS");
            Console.WriteLine("(Teller: 10 / 20 / 30 / 40 = finished transaction, 0 = exit)\n");
        }
        static void DisplayStatus()
        {
            Console.WriteLine("NOW SERVING!!! NOW SERVING!!!\n");

            for (int i = 0; i < queues.Length; i++)
            {
                string who = "";
                if (queues[i].Count > 0)
                    who = queues[i].Peek();

                Console.WriteLine(tags[i] + " : " + who + "\n");
            }

            Console.WriteLine("WAITING LIST!!! WAITING LIST!!!\n");
            Console.WriteLine(new string('_', 60) + "\n");

            PrintRow(0, 1);
            PrintRow(2, 3);

            Console.WriteLine(new string('_', 60) + "\n");
        }
        static void PrintRow(int left, int right)
        {
            string[] leftNames = queues[left].ToArray();
            string[] rightNames = queues[right].ToArray();

            string leftHeader = listTitles[left] + " (" + served[left] + " – " + queues[left].Count + ")";
            string rightHeader = listTitles[right] + " (" + served[right] + " – " + queues[right].Count + ")";

            Console.WriteLine(leftHeader.PadRight(32) + "| " + rightHeader);

            int rows = Math.Max(leftNames.Length, rightNames.Length);
            for (int i = 0; i < rows; i++)
            {
                string leftText = "";
                string rightText = "";

                if (i < leftNames.Length)
                    leftText = (i + 1) + ". " + leftNames[i];
                if (i < rightNames.Length)
                    rightText = (i + 1) + ". " + rightNames[i];

                Console.WriteLine(leftText.PadRight(32) + "| " + rightText);
            }

            Console.WriteLine();
        }
        static void ShowSummary()
        {
            int totalServed = 0;
            int totalWaiting = 0;

            Console.WriteLine("\nNumber of clients served and number of clients on waiting\n");

            for (int i = 0; i < queues.Length; i++)
            {
                Console.WriteLine(listTitles[i].PadRight(12) + "Served: " + served[i] + "   Waiting: " + queues[i].Count);
                totalServed += served[i];
                totalWaiting += queues[i].Count;
            }

            Console.WriteLine("\nTotal clients served : " + totalServed);
            Console.WriteLine("Total clients waiting: " + totalWaiting);
            Console.WriteLine("\nThank you for using the program");
        }
    }
}
