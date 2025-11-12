namespace Part_A;

public sealed class Node
{
    public int Value { get; }
    public Node? Next { get; }

    public Node(int value, Node? next = null)
    {
        Value = value;
        Next = next;
    }
}

public static class LinkedListPuzzles
{
    /// <summary>
    /// Insert node starting from list end to list start
    /// </summary>
    /// <param name="values">User input a sequence of integers</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Node BuildList(int[] values)
    {
        if (values.Length == 0)
            throw new ArgumentException("List must contain at least one element.");

        Node? head = null;
        for (int i = values.Length - 1; i >= 0; i--)
        {
            head = new Node(values[i], head);
        }

        return head!;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static int GetFifthFromTail(Node head)
    {
        if (head is null)
            throw new AggregateException($"This is an empty LinkedList");

        Node slow = head;
        Node? fast = head;

        for (int i = 0; i < 5; i++)
        {
            if (fast is null)
                throw new InvalidOperationException("The linked list cannot be fewer than 5 elements");

            fast = fast.Next!;
        }

        while (fast is not null)
        {
            slow = slow.Next!;
            fast = fast.Next;
        }

        return slow.Value;
    }
}