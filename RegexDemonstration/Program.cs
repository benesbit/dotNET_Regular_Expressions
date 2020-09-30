using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexDemonstration
{
    class Program
    {
        static void Main(string[] args)
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
                });
            });
        }
    }
}
