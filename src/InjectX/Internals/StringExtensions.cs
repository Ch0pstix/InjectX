namespace InjectX.Internals;

internal static class StringExtensions
{
    internal static string Format(this string str, params object[] args)
    {
        return string.Format(str, args);
    }

    internal static bool MatchesPattern(this string str, string pattern)
    {
        /* Tokens:
         *   *   - Match one or more of any character
         *   |   - Match the pattern before or after
         *  [ ]  - Match any of the characters inside
         */

        const char GROUP_START = '[', GROUP_END = ']', WILDCARD = '*', OR = '|';
        int strIndex = 0, patternIndex = 0;

        if (string.IsNullOrEmpty(pattern) ||
            (pattern.Contains(GROUP_START) && !pattern.Contains(GROUP_END)) ||
            (pattern.Contains(GROUP_END) && !pattern.Contains(GROUP_START)))
            return false;

        while (strIndex < str.Length)
        {
            if (pattern[patternIndex] is GROUP_START)
            {
                int groupStartIndex = patternIndex, groupEndIndex = pattern.IndexOf(GROUP_END, groupStartIndex);
                string group = pattern.Substring(groupStartIndex + 1, groupEndIndex - groupStartIndex - 1);

                if (group.Contains(str[strIndex]))
                {
                    strIndex++;
                    patternIndex = ++groupEndIndex;
                    continue;
                }
                else return false;
            }
            else if (pattern[patternIndex] is WILDCARD)
            {
                patternIndex++;

                if (patternIndex == pattern.Length)
                    return true;

                char nextChar = pattern[patternIndex];
                int nextIndex = str.IndexOf(nextChar, strIndex);

                if (nextIndex < 1)
                    return false;

                strIndex = ++nextIndex;
                patternIndex++;
            }
            else if (pattern[patternIndex] is OR)
            {
                return MatchesPattern(str, pattern[++patternIndex..]);
            }
            else
            {
                if (str[strIndex] != pattern[patternIndex])
                    return false;

                strIndex++;
                patternIndex++;
            }
        }
        return true;
    }
}
