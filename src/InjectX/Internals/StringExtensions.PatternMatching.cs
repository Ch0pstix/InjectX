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

        for (;pIndex < pattern.Length; pIndex++)
        {
            char token = pattern[pIndex];

            if (token is Token.PatternGroupStart)
            {
                SimpleRange pGroupRange = new(pIndex, pattern.IndexOf(Token.PatternGroupEnd, pIndex));
                return Matches(str[sIndex..(sIndex + pGroupRange.Length - 1)], pattern[(pGroupRange.Start + 1)..(pGroupRange.Length - 1)]);
            }
            else if (token is Token.GroupStart)
            {
                SimpleRange groupRange = new(pIndex, pattern.IndexOf(Token.GroupEnd, pIndex));
                string group = pattern.Substring(groupRange.Start + 1, groupRange.Length - 1);

                if (!group.Contains(str[sIndex]))
                    return false;

                sIndex++;
                pIndex = groupRange.End + 1;
            }
            else if (token is Token.Wildcard)
            {
                char chr = str[sIndex];

                if (!(chr.IsAsciiAlphanumeric() || chr is '-' || chr is '_'))
                    return false;

                sIndex++;
            }
            else if (token is Token.ZeroPlus)
            {
                int matchCount = 0;
                char prevToken = pIndex - 1 > 0 ? pattern[pIndex - 1] : char.MinValue;

                if (prevToken is char.MinValue)
                    return false;

                while (true)
                {
                    if (!Matches(str[sIndex..(sIndex + 1)], prevToken.ToString()))
                    {
                        if (matchCount is 0)
                            return false;
                        else break;
                    }    

                    sIndex++;
                    matchCount++;
                }
            }
            else if (token is Token.OnePlus)
            {
                int matchCount = 0;
                char prevToken = pIndex - 1 > 0 ? pattern[pIndex - 1] : char.MinValue;

                if (prevToken is char.MinValue)
                    return false;

                while (true)
                {
                    if (!Matches(str[sIndex..(sIndex + 1)], prevToken.ToString()))
                        break;

                    sIndex++;
                    matchCount++;
                }

                if (matchCount is not >= 1)
                    return false;
            }
            else if (token is Token.Or)
            {
                return Matches(str, pattern[++pIndex..]);
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
