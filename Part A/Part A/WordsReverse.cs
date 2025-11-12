namespace Part_A;

public static class WordsReverse
{
    public static string ReverseWords(string input)
    {
        var chars = input.ToArray();
        int n = input.Length;
        int i = 0;

        if (n == 0)
            return input;
        
        // Scan the input
        while (i < n)
        {
            // Skip whitespace
            while (i < n && char.IsWhiteSpace(chars[i]))
                i++;

            int start = i;
            
            // Scan this input (a non-blank, continuous interval)
            while (i < n && !char.IsWhiteSpace(chars[i]))
                i++;

            ReverseRange(chars, start, i - 1);
        }

        return new string(chars);
    }

    private static void ReverseRange(char[] chars, int left, int right)
    {
        while (left < right)
        {
            char tmp = chars[left];
            chars[left] = chars[right];
            chars[right] = tmp;
            left++;
            right--;
        }
    }
}