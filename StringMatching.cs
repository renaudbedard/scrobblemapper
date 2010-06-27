using System;
using System.Globalization;
using System.Text;

namespace ScrobbleMapper
{
    static class StringMatching
    {
        /// <summary>
        /// Neutralizes a string's accentuated characters, whitespace, punctuation and symbols.
        /// </summary>
        public static string Neutralize(this string str)
        {
            string formD = str.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            for (int i = 0; i < formD.Length; i++)
            {
                char c = formD[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark &&
                    !char.IsWhiteSpace(c) && !char.IsPunctuation(c) && !char.IsSymbol(c))
                {
                    builder.Append(char.ToLowerInvariant(c));
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Calculates the "edit distance" between two strings, 
        /// or how many operations would be needed to transform one into the other.
        /// </summary>
        public static int LevenshteinDistance(this string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            var d = new int[n + 1, m + 1];

            if (n == 0)
                return m;
            if (m == 0)
                return n;

            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }

            return d[n, m];
        }
    }
}
