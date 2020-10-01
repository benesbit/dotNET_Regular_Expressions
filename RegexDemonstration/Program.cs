using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //parseReceipts();
            //regexAnchorsAndBoundaries();
            //regexExpressionOptions();
            //regexLookaheadLookbehind();
            regexRunawyExpression();
        }

        public static void regexRunawyExpression()
        {
            var patterns = new List<string>
            {
                @"<.+>",
                @"<.+?>",
                @"(a+(aa)+(aaa)+(aa)+a+)+b."
            };
            var inputs = new List<string>
            {
                "a<tag>b</tag>c",
                "aaaaaaaaaaaaaaaaaaaab",
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab"
            };

            patterns.ForEach(pattern =>
            {
                Console.WriteLine($"Regular expression {pattern}");

                var regex = new Regex(pattern);
                inputs.ForEach(input =>
                {
                    Console.WriteLine($"\tInput pattern: {input}");
                    var watch = new Stopwatch();
                    watch.Start();
                    var results = regex.Matches(input);
                    if (results.Count <= 0)
                    {
                        Console.WriteLine($"\t\tNo matches found.");
                    }
                    foreach (Match result in results)
                    {
                        Console.WriteLine($"\t\tMatch found at index {result.Index}. Length: {result.Length}.");
                    }
                    watch.Stop();
                    Console.WriteLine($"\t\tRuntime: {watch.Elapsed.TotalMilliseconds}ms");
                });
            });
            Console.ReadKey();
        }

        public static void regexLookaheadLookbehind()
        {
            var patterns = new List<string>
            {
                @"[A-Z]",
                "[A-Z-[Z]]",
                "a(?=b)",
                "a(?!b)",
                "(?<=c)a",
                "(?<!c)a",
            };

            var inputs = new List<string>
            {
                "",
                "a",
                "b",
                "c",
                "ab",
                "ca",
                "cab",
                "A",
                "B",
                "Z",
                "AA",
            };

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
                        Console.WriteLine($"\t\tNo matches found.");
                    }
                    foreach (Match result in results)
                    {
                        Console.WriteLine($"\t\tMatch found at index {result.Index}. Length: {result.Length}");
                    }
                });
            });
            Console.ReadKey();
        }

        public static void regexExpressionOptions()
        {
            var patterns = new List<string>
            {
                "(?x)Hey#This is a comment",
                "He(?# This is a comment...)y",
                "H(?i)e(?-i)y",
                @"(?m)^hey$",
                "(he)y",
                "(?n)(he)(?-n)y",
                "(?x) \r\n h e y",
            };

            var inputs = new List<string>
            {
                "hey\nhey",
                " hey\nhey",
                " hey\n hey",
                " hey\n hey ",
                "Hey",
                "hey",
                "HEy",
                "HEY",
                " hey",
                "hey ",
                " hey ",
            };

            patterns.ForEach(pattern =>
            {
                Console.WriteLine($"Regular expression: \"{pattern}\"");

                var regex = new Regex(pattern);

                inputs.ForEach(input =>
                {
                    Console.WriteLine($"\tInput pattern: \"{input}\"");

                    var results = regex.Matches(input);
                    if (results.Count <= 0)
                    {
                        Console.WriteLine($"\t\tNo matches found.");
                    }
                    foreach (Match match in results)
                    {
                        Console.WriteLine($"\t\tMatch found at index {match.Index}. Length: {match.Length}.");

                        foreach (Group group in match.Groups)
                        {
                            Console.WriteLine($"\t\t\tGroup at index {group.Index} has value {group.Value}");
                        }
                    }
                });
            });

            Console.ReadKey();
        }

        public static void regexAnchorsAndBoundaries()
        {
            var patterns = new List<string>
            {
                @"\b",
                @"\B",
                @"^hi",
                @"hi$",
            };

            var inputs = new List<string>
            {
                "a b",
                "a",
                "a b ",
                " a b ",
                " ",
                "",
                "hi",
                " hi",
                "him",
                " him",
                "him ",
                " him ",
            };

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

                    foreach (Match result in results)
                    {
                        Console.WriteLine($"\t\tMatch found at index {result.Index}. Length: {result.Length}.");
                    }
                });
            });
            Console.ReadKey();
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

            patterns.ForEach(pattern =>
            {
                Console.WriteLine($"Regular expression: {pattern}");
                var regex = new Regex(pattern);

                inputs.ForEach(input =>
                {
                    Console.WriteLine($"\tInput pattern: {input}");

                    var matches = regex.Matches(input);
                    if (matches.Count <= 0)
                    {
                        Console.WriteLine("\t\tNo matches found.");
                    }

                    foreach (Match match in matches)
                    {
                        Console.WriteLine($"\t\tMatch at index {match.Index} with length {match.Length}.");
                        foreach (Group group in match.Groups)
                        {
                            Console.WriteLine($"\t\t\tGroup at index {group.Index} has value {group.Value}.");
                        }
                    }

                    Console.WriteLine($"Simple replacement results: {Regex.Replace(input, @"(Chicken)(.*)\$(9.99)", @"$1$2$$0.00")}");
                    var results = Regex.Replace(input, pattern, (match) =>
                    {
                        if (match.Groups[1].Value == "Chicken")
                        {
                            return match.Value.Replace(match.Groups[2].Value, "0.00");
                        }
                        return match.Value;
                    });
                    Console.WriteLine($"Advanced replacement results: {results}");
                });
            });
            Console.ReadKey();
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
