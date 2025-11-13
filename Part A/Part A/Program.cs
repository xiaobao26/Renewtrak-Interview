namespace Part_A;

public static class Program
{
    public static void Main(string[] args)
    {
        RunLinkedListPuzzle();
        Console.WriteLine();
        RunWordsReversePuzzle();
    }

    private static void RunLinkedListPuzzle()
    {
        Console.WriteLine("Part A - Puzzle One – Linked list");
        Console.WriteLine("Please enter a sequence of integers (separated by space)");

        try
        {
            var input = Console.ReadLine() ?? "";
            var numbers = Array.ConvertAll(input.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            var head = LinkedListPuzzles.BuildList(numbers);
            var result = LinkedListPuzzles.GetFifthFromTail(head);
            Console.WriteLine($"5th from tail: {result}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input: please enter integers only.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Input Error: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Logic Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void RunWordsReversePuzzle()
    {
        Console.WriteLine("Part A - Puzzle Two – Reverse Words");
        Console.Write("Enter a sentence to reverse each word: ");
        try
        {
            var words = Console.ReadLine() ?? "";
            var reversedWords = WordsReverse.ReverseWords(words);
            Console.WriteLine($"Reversed: {reversedWords}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}