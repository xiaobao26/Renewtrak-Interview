using Part_A;

namespace PartATests;

public class LinkedListUnitTest
{
    // BuildList Test
    [Fact]
    public void BuildList_EmptyArray_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => LinkedListPuzzles.BuildList(Array.Empty<int>()));
        Assert.Contains("at least one", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
    
    // GetFifthFromTail
    [Fact]
    public void GetFifthFromTail_ListWithMoreThan5_ReturnsCorrectValue()
    {
        var head = LinkedListPuzzles.BuildList(new[] { 1, 2, 3, 4, 5, 6, 7, 8 });
        var value = LinkedListPuzzles.GetFifthFromTail(head);
        Assert.Equal(4, value);
    }
    
    [Fact]
    public void GetFifthFromTail_ListWithExactly5_ReturnsCorrectValue()
    {
        var head = LinkedListPuzzles.BuildList(new[] { 1, 2, 3, 4, 5 });
        var value = LinkedListPuzzles.GetFifthFromTail(head);
        Assert.Equal(1, value);
    }
    
    [Fact]
    public void GetFifthFromTail_ListWithLessThan5_ThrowsInvalidOperationException()
    {
        var head = LinkedListPuzzles.BuildList(new[] { 1, 2, 3, 4 });
        var ex = Assert.Throws<InvalidOperationException>(() => LinkedListPuzzles.GetFifthFromTail(head));
        Assert.Contains("fewer than 5 elements", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
    
    [Fact]
    public void GetFifthFromTail_NullHead_ThrowsAggregateException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => LinkedListPuzzles.GetFifthFromTail(null!));
        Assert.Contains("Linked list is empty.", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}