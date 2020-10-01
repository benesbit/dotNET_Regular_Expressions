using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexDemonstration
{
    class RegexExamples
    {
        public static void regexCapturesAndBalancingGroups()
        {
            var input = "µ";
            var pattern = "μ";

            Console.WriteLine($"Micro and Mu Symbols:");
            Console.WriteLine($"\t Does {input} match {pattern}? {Regex.IsMatch(input, pattern)}");

            Console.WriteLine($"\tInput's code point: {GetCodePoint(input, 0)}");
            Console.WriteLine($"\tPatterns's code point: {GetCodePoint(pattern, 0)}");

            Console.WriteLine();

            input = "مرحبا العالم مرحبا";
            pattern = @"مرحبا";

            Console.WriteLine($"Right to Left Option:");
            Console.WriteLine($"\tWithout the RightToLeft Option:");
            foreach (Match match in Regex.Matches(input, pattern))
            {
                Console.WriteLine($"\t\tMatch at index {match.Index} of length {match.Length}");
            }

            Console.WriteLine($"\tWith the RightToLeft Option:");
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.RightToLeft))
            {
                Console.WriteLine($"\t\tMatch at index {match.Index} of length {match.Length}");
            }

            Console.WriteLine();

            input = "Hello World";
            var patterns = new List<string>
            {
                @"\b\w+\b",
                @"\b(\w)+\b",
            };
            Console.WriteLine("Captures:");
            foreach (var examplePattern in patterns)
            {
                Console.WriteLine($"\tInput: {input}");
                Console.WriteLine($"\tPattern: {examplePattern}");
                foreach (Match match in Regex.Matches(input, examplePattern))
                {
                    Console.WriteLine($"\t\t{match.Value}: Match at index {match.Index} of length {match.Length}");
                    foreach (Group group in match.Groups)
                    {
                        Console.WriteLine($"\t\t\t{group.Value}: Group at index {group.Index} of length {group.Length}.");
                        foreach (Capture capture in group.Captures)
                        {
                            Console.WriteLine($"\t\t\t\t{capture.Value}: Capture at index {capture.Index} of length {capture.Length}.");
                        }
                    }
                }
            }

            Console.ReadKey();
        }

        public static void regexUnicodeDemonstration()
        {
            var input = "a";

            Console.WriteLine(input);
            Console.WriteLine($"\tCode point: {GetCodePoint(input, 0)}");
            Console.WriteLine($"\tLength of input string: {input.Length}");

            Console.WriteLine($"\tIs an uppercase letter? {Regex.IsMatch(input, @"\p{Lu}")}");
            Console.WriteLine($"\tIs not an uppercase letter? {Regex.IsMatch(input, @"\P{Lu}")}");

            Console.WriteLine();

            input = "❌";
            Console.WriteLine($"Cross Mark");
            Console.WriteLine($"\tCode point: {GetCodePoint(input, 0)}");
            Console.WriteLine($"\tLength: {input.Length}");

            Console.WriteLine($"\tDoes the input match \\u274C? {Regex.IsMatch(input, "\u274C")}");

            Console.WriteLine($"\tIs in Dingbats? {Regex.IsMatch(input, @"\p{IsDingbats}")}"); // Dingbats are from U+2700 to U+27BF
            Console.WriteLine($"\tIs not in Dingbats? {Regex.IsMatch(input, @"\P{IsDingbats}")}");

            Console.WriteLine();

            input = "😎";
            Console.WriteLine($"Smiling Face with Sunglasses Emoji");
            Console.WriteLine($"\tCode point: {GetCodePoint(input, 0)}");
            Console.WriteLine($"\tLength: {input.Length}");

            Console.WriteLine($"\tDoes the emoji match \\U0001F60E? {Regex.IsMatch(input, "\U0001F60E")}");

            Console.WriteLine();

            input = "İ"; // Turkish capital i
            var pattern = "i";

            Console.WriteLine("Turkish Culture");
            Console.WriteLine($"\tMatches without CultureInvariant? {Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase)}");
            Console.WriteLine($"\tMatches with CultureInvariant? {Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)}");

            Console.ReadKey();
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

        public static void regexParseReceipts()
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

        public static void regexPhoneNumbers()
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

        public static void regexEscapeCharacterExample()
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

        public static void regexBasicExample()
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

        private static string GetCodePoint(string input, int index)
        {
            if (Char.IsSurrogatePair(input, index))
            {
                return String.Format($"U+{Char.ConvertToUtf32(input, index):X8}");
            }
            return String.Format($"U+{Char.ConvertToUtf32(input, index):X4}");
        }
    }
}
