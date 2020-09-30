using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexDemonstration
{
    class Program
    {
        static void Main(string[] args)
        {
            basicRegexExample();
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
