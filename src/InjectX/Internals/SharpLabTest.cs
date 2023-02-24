/*

using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        string[] tests = new[] { "MyApp.Services", "myapp.services", "MYAPP.SERVICES", "MyApp.Services.Web", "MyApp.ServicesExtensions" };
        string pattern = "(*+).([Ss]ervices|SERVICES)(.*+)?";

        foreach (string test in tests)
        {
            Console.WriteLine("-> " + (test.Matches(pattern) ? "Match Found" : "No Match"));
            Console.WriteLine();
        }
    }
}

public static class Extensions
{
    internal static bool IsAsciiAlphanumeric(this char c)
    {
        return (c >= 'a' && c <= 'z') ||
               (c >= 'A' && c <= 'Z') ||
               (c >= '0' && c <= '9');
    }

    internal static bool XOR(this string str, char first, char second)
    {
        bool hasFirst = str.Contains(first);
        bool hasSecond = str.Contains(second);

        return (hasFirst && !hasSecond) || (!hasFirst && hasSecond);
    }

    internal static bool Matches(this string str, string pattern)
    {
        if (string.IsNullOrWhiteSpace(str) != string.IsNullOrWhiteSpace(pattern) ||
            pattern.XOR(Token.PatternGroupStart, Token.PatternGroupEnd) ||
            pattern.XOR(Token.GroupStart, Token.GroupEnd))
            return false;

        int sIndex = 0, pIndex = 0;

        Console.WriteLine("str: {0} -> pat: {1}", str, pattern);

        while (pIndex < pattern.Length)
        {
            char token = pattern[pIndex];

            if (token is Token.PatternGroupStart)
            {
                SimpleRange pGroupRange = new(pIndex, pattern.IndexOf(Token.PatternGroupEnd, pIndex));

                Console.WriteLine("> PATTERN GROUP ({0}-{1} -> {2})", pGroupRange.Start, pGroupRange.End, pGroupRange.Length);

                int sIndexEnd = str
                    .Select((c, i) => new { Char = c, Index = i })
                    .FirstOrDefault(x => !x.Char.IsValidWildcard())?.Index ?? -1;

                if (!Matches(str[sIndex..sIndexEnd], pattern[(pGroupRange.Start + 1)..pGroupRange.Length]))
                    return false;
                else
                    return Matches(str[sIndexEnd..], pattern[(pGroupRange.Start + pGroupRange.Length + 1)..]);
            }
            else if (token is Token.GroupStart)
            {
                SimpleRange groupRange = new(pIndex, pattern.IndexOf(Token.GroupEnd, pIndex));
                string group = pattern[(groupRange.Start + 1)..groupRange.Length];

                Console.WriteLine("> GROUP ({0}-{1} -> {2})", groupRange.Start, groupRange.End, groupRange.Length);

                if (!group.Contains(str[sIndex]))
                    return false;

                pIndex = groupRange.End + 1;
                sIndex++;
            }
            else if (token is Token.Wildcard)
            {
                char chr = str[sIndex];

                if (!chr.IsValidWildcard())
                    return false;

                pIndex++;
                sIndex++;
            }
            else if (token is Token.ZeroPlus)
            {
                int matchCount = -1;
                char prevToken = pIndex - 1 < 1 ? pattern[pIndex - 1] : char.MinValue;

                if (prevToken is char.MinValue)
                    return false;

                while (sIndex < str.Length)
                {
                    if (!Matches(str[sIndex].ToString(), prevToken.ToString()))
                    {
                        if (matchCount < 0)
                            return false;
                        else break;
                    }

                    sIndex++;
                    matchCount++;
                }

                pIndex++;
            }
            else if (token is Token.OnePlus)
            {
                int matchCount = -1;
                char prevToken = pIndex - 1 < 1 ? pattern[pIndex - 1] : char.MinValue;

                if (prevToken is char.MinValue)
                    return false;

                while (sIndex < str.Length)
                {
                    if (!Matches(str[sIndex].ToString(), prevToken.ToString()))
                    {
                        if (matchCount < 1)
                            return false;
                        else break;
                    }

                    sIndex++;
                    matchCount++;
                }

                pIndex++;
            }
            else if (token is Token.Or)
            {
                int groupStart = pattern.LastIndexOf(Token.PatternGroupStart, pIndex);
                int groupEnd = pattern.IndexOf(Token.PatternGroupEnd, pIndex) + 1;

                string leftPattern, rightPattern;

                if (groupStart is not -1 && groupEnd is not -1)
                {
                    string groupPattern = pattern[groupStart..groupEnd];
                    string prePattern = pattern[..pattern.LastIndexOf(Token.PatternGroupStart, pIndex)];
                    string postPattern = pattern[(pattern.IndexOf(Token.PatternGroupEnd, pIndex) + 1)..];
                    leftPattern = prePattern + groupPattern[1..groupPattern.IndexOf(Token.Or)] + postPattern;
                    rightPattern = prePattern + groupPattern[(groupPattern.IndexOf(Token.Or) + 1)..^1] + postPattern;
                }
                else
                {
                    leftPattern = pattern[..(pIndex - 1)];
                    rightPattern = pattern[(pIndex + 1)..];
                }

                Console.WriteLine("left: " + leftPattern);
                Console.WriteLine("right: " + rightPattern);

                if (!Matches(str, leftPattern))
                    if (!Matches(str, rightPattern))
                        return false;
            }
            else // Literal
            {
                if (str[sIndex] != pattern[pIndex])
                    return false;

                Console.WriteLine("str: {0} -> literal", str[sIndex]);

                sIndex++;
                pIndex++;
            }
        }

        return true;
    }

    private static bool IsValidWildcard(this char chr)
    {
        return chr.IsAsciiAlphanumeric() || chr is '-' || chr is '_';
    }

    private static class Token
    {
        public const char
            // Groups a pattern (for use with other tokens like +)
            PatternGroupStart = '(', PatternGroupEnd = ')',
            // Matches one of any of the characters in the group
            GroupStart = '[', GroupEnd = ']',
            // Matches one of any of the following characters: A-Za-z0-9 (Alphanumeric), '-', and '_'
            Wildcard = '*',
            // Matches zero or more of the preceeding token,
            ZeroPlus = '?',
            // Matches one or more of the preceeding token
            OnePlus = '+',
            // Matches either the preceeding OR succeeding pattern
            Or = '|';
    }

    private record SimpleRange(int Start, int Length)
    {
        public int End => Start + Length;
    }
}

*/
