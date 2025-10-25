using System;
using System.Collections.Generic;
using System.Security.RightsManagement;
using System.Threading;
using ClosedXML.Excel;

class Program
{
    //unsure why it has to be readonly
    static readonly Random rand = new Random();
    static void Main()
    {
        //location for this file is in the project/bin/debuggin
        string filePath = "Parts.xlsx";

        List<string> blades = new List<string>();
        List<string> ratchets = new List<string>();
        List<string> bits = new List<string>();

        //had to get a nuget package
        using (var workbook = new XLWorkbook(filePath))
        {
            //first spreadsheet, uses all beyblade x blades, ratchets and bit types.
            var worksheet = workbook.Worksheet(1);
            //second spreadsheet, uses my personal stash
            //var worksheet = workbook.Worksheet(2);
            //bc headers
            int row = 2;

            while
                (!worksheet.Cell(row, 1).IsEmpty() ||
                !worksheet.Cell(row, 2).IsEmpty() ||
                !worksheet.Cell(row, 3).IsEmpty())
            {
                var blade = worksheet.Cell(row, 1).GetString();
                var ratchet = worksheet.Cell(row, 2).GetString();
                var bit = worksheet.Cell(row, 3).GetString();

                if (!string.IsNullOrWhiteSpace(blade))
                    blades.Add(blade);

                if (!string.IsNullOrWhiteSpace(ratchet))
                    ratchets.Add(ratchet);

                if (!string.IsNullOrWhiteSpace(bit))
                    bits.Add(bit);

                row++;
            }
        }

        string combo = "";
        string combo2 = "";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Beyblade X Combo Generator\n");

            //ascii art header for tha stylez (but i'm commenting it out bc it's too big for my smol monitor)
            Console.WriteLine();
            Console.WriteLine("    ____             __    __          __        _  __    ______                __             ______                           __            \r\n   / __ )___  __  __/ /_  / /___ _____/ /__     | |/ /   / ____/___  ____ ___  / /_  ____     / ____/__  ____  ___  _________ _/ /_____  _____\r\n  / __  / _ \\/ / / / __ \\/ / __ `/ __  / _ \\    |   /   / /   / __ \\/ __ `__ \\/ __ \\/ __ \\   / / __/ _ \\/ __ \\/ _ \\/ ___/ __ `/ __/ __ \\/ ___/\r\n / /_/ /  __/ /_/ / /_/ / / /_/ / /_/ /  __/   /   |   / /___/ /_/ / / / / / / /_/ / /_/ /  / /_/ /  __/ / / /  __/ /  / /_/ / /_/ /_/ / /    \r\n/_____/\\___/\\__, /_.___/_/\\__,_/\\__,_/\\___/   /_/|_|   \\____/\\____/_/ /_/ /_/_.___/\\____/   \\____/\\___/_/ /_/\\___/_/   \\__,_/\\__/\\____/_/     \r\n           /____/                                                                                                                             \n");

            Console.WriteLine("Random Combo: " + (string.IsNullOrEmpty(combo) ? "" : combo));
            Console.WriteLine("Random Combo: " + (string.IsNullOrEmpty(combo2) ? "" : combo2));
            Console.WriteLine();
            Console.WriteLine("Press [S] to generate a solo combo, [D] for a duel combo, or press [Q] to quit.\n");
            Console.WriteLine("Spreadsheet accurate as of 05/01/2025, but is subject to change.\n");
            Console.WriteLine("Check out the stats for this combo on beybladeplanner.com\n");
            Console.WriteLine(">>\n");

            var userInput = Console.ReadKey(intercept: true);

            //user presses enter, gets a cool combo
            if (userInput.Key == ConsoleKey.S)
            {
                combo = GenerateRandomCombo(blades, ratchets, bits);
                combo2 = "";
            }

            //user presses d, gets a duelling combo
            else if (userInput.Key == ConsoleKey.D)
            {
                combo = GenerateRandomCombo(blades, ratchets, bits);
                combo2 = GenerateRandomCombo(blades, ratchets, bits);
            }

            //user presses q, quits console
            else if (userInput.Key == ConsoleKey.Q)
            {
                Console.WriteLine("Let it rip!\n");
                Thread.Sleep(1500);
                break;
            }

            //user is a moron, let us remind them of that.
            else
            {
                Console.WriteLine("Oops! Try again, blader!\n");
                Thread.Sleep(1500);
            }
        }
    }

    //what it's all aboot.
    static string GenerateRandomCombo(List<string> blades, List<string> ratchets, List<string> bits)
    {
        string blade = blades[rand.Next(blades.Count)];
        string ratchet = ratchets[rand.Next(ratchets.Count)];
        string bit = bits[rand.Next(bits.Count)];

        return $"{blade} {ratchet} {bit}";
    }
}