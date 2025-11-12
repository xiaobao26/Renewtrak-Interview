namespace Part_A;

public static class Program
{
    public static void Main(string[] args)
    {
        #region Part A - Puzzle One – Linked list
        Console.WriteLine("Please enter a sequence of integers (separated by space)");
        var input = Console.ReadLine() ?? "";
        var numbers = Array.ConvertAll(input.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
        var head = LinkedListPuzzles.BuildList(numbers);
        var result = LinkedListPuzzles.GetFifthFromTail(head);
        Console.WriteLine($"5th from tail: {result}");
        #endregion

        #region Part A - Puzzle Two – Reverse Words
        #endregion
    }
}