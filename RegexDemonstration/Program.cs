﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexDemonstration
{
    class Program
    {
        static void Main(string[] args)
        {
            //basicRegexExample();
            //escapeCharacterRegexExample();
            //phoneNumbersRegex();
        }

        public static void parseReceipts()
        {
            var patterns = new List<string>
            {
                @"([A-Za-z]+).*\$(\d+.\d+)"
            };

            var inputs = new List<string>
            {
                @"
                |-----------------------|
                | Receipt from          |
                | Benjamin's Shop       |
                |                       |
                | Thanks for shopping!  |
                |-----------|-----------|
                |  Item     |Price $USD |
                |-----------|-----------|
                | Shoes     |   $47.99  |
                | Cabbage   |    $2.99  |
                | Carrots   |    $1.23  |
                | Chicken   |    $9.99  |
                | Beef      |   $12.47  |
                | Shirt     |    $5.97  |
                | Salt      |    $2.99  |
                |-----------|-----------|"
            };
        }

        public static void phoneNumbersRegex()
        {
            var patterns = new List<string>
            {
                @"^\d\d\d-\d\d\d-\d\d\d\d$"
            };

            var inputs = new List<string>
            {
                "5555555555",
                "(555)-555-5555",
                "012-345-6789",
                "555-555-5555",
                "555-555-555a",
                "5555-555-5555",
                "555-5555555",
                "000-000-0000",
                "a",
                "5.55-555-5555",
                "...-...-....",
            };

            patterns.ForEach(pattern =>
            {
                Console.WriteLine($"Regular expression: {pattern}");
                var regex = new Regex(pattern);

                inputs.ForEach(input =>
                {
                    Console.WriteLine($"\tInput pattern: {input}");

                    var isMatch = regex.IsMatch(input);

                    Console.WriteLine($"\t\t{(isMatch ? "Accepted" : "Rejected")}.");

                    if (!isMatch)
                    {
                        return;
                    }

                    var splits = Regex.Split(input, @"-\d\d\d-").ToList();

                    Console.WriteLine($"\t\t\tArea code: {splits[0]}");
                    Console.WriteLine($"\t\t\tLast 4 digits: {splits[1]}");
                });
            });
            Console.ReadKey();
        }

        public static void escapeCharacterRegexExample()
        {
            var patterns = new List<string> { "\\?", @"\?", Regex.Escape("?") };
            var inputs = new List<string> { "?" };

            patterns.ForEach(pattern =>
            {
                Console.WriteLine($"Regular expression: {pattern}");
                var regex = new Regex(pattern);

                inputs.ForEach(input =>
                {
                    Console.WriteLine($"\tInput pattern: {input}");

                    var results = regex.Matches(input);

                    if (results.Count <= 0)
                    {
                        Console.WriteLine("\t\tNo matches found.");
                    }
                    else
                    {
                        foreach (Match result in results)
                        {
                            Console.WriteLine($"\t\tMatch found at index {result.Index}. Length: {result.Length}.");
                        }
                    }
                });
            });
            Console.ReadKey();
        }

        public static void basicRegexExample()
        {
            var patterns = new List<string> { "a*b", "a+b", "a?b" };
            var inputs = new List<string> { "a", "b", "ab", "aab", "abb" };

            patterns.ForEach(pattern =>
            {
                Console.WriteLine($"Regular expression: {pattern}");
                var regex = new Regex(pattern);

                inputs.ForEach(input =>
                {
                    Console.WriteLine($"\tInput pattern: {input}");

                    var results = regex.Matches(input);

                    if (results.Count <= 0)
                    {
                        Console.WriteLine("\t\tNo matches found.");
                    }
                    else
                    {
                        foreach (Match result in results)
                        {
                            Console.WriteLine($"\t\tMatch found at index {result.Index}. Length: {result.Length}.");
                        }
                    }
                });
            });
            Console.ReadKey();
        }
    }
}
