namespace InjectX.Internals;

internal static partial class StringExtensions
{
    internal static bool Matches(this string str, string pattern)
    {
        if (string.IsNullOrWhiteSpace(str) != string.IsNullOrWhiteSpace(pattern) ||
            pattern.XOR(Token.PatternGroupStart, Token.PatternGroupEnd) ||
            pattern.XOR(Token.GroupStart, Token.GroupEnd))
            return false;

        int sIndex = 0, pIndex = 0;
        bool inPatternGroup = false;
        SimpleRange? pGroupRange = null;

        while (sIndex < str.Length)
        {
            char token = pattern[pIndex];
            char chr = str[sIndex];

            if (token is Token.PatternGroupStart)
            {
                pGroupRange = new(pIndex, pattern.IndexOf(Token.PatternGroupEnd, pIndex));
                string patternGroup = pattern.Substring(pGroupRange.Start + 1, pGroupRange.Length - 1);
            }
            else if (token is Token.GroupStart)
            {
                SimpleRange groupRange = new(pIndex, pattern.IndexOf(Token.GroupEnd, pIndex));
                string group = pattern.Substring(groupRange.Start + 1, groupRange.Length - 1);

                if (!group.Contains(str[sIndex]))
                    return false;

                sIndex++;
                pIndex = groupRange.End + 1;
                continue;
            }
            else if (token is Token.Wildcard)
            {
                if (!(chr.IsAsciiAlphanumeric() || chr is '-' || chr is '_'))
                    return false;

                sIndex++;
                pIndex++;
                continue;
            }
            else if (token is Token.ZeroPlus)
            {

            }
            else if (token is Token.OnePlus)
            {

            }
            else if (token is Token.Or)
            {

            }
            else // Literal
            {
                if (str[sIndex] != pattern[pIndex])
                    return false;

                sIndex++;
                pIndex++;
            }
        }

        return true;
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
