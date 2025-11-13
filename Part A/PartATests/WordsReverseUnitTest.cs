using Part_A;

namespace PartATests;

public class WordsReverseUnitTest
{
    [Fact]
    public void ReverseWords_EmptyString_ReturnsEmpty()
    {
        var input = string.Empty;
        var result = WordsReverse.ReverseWords(input);
        Assert.Equal(string.Empty, result);
    }
    
    [Fact]
    public void ReverseWords_OnlySpaces_PreservesSpaces()
    {
        var input = "    ";
        var result = WordsReverse.ReverseWords(input);
        Assert.Equal("    ", result);
    }
    
    [Fact]
    public void ReverseWords_SingleWord_Reversed()
    {
        var result = WordsReverse.ReverseWords("abc");
        Assert.Equal("cba", result);
    }
    
    [Fact]
    public void ReverseWords_MultipleWordsWithMultipleSpaces_Reversed()
    {
        var input = "Cat and dog";
        var result = WordsReverse.ReverseWords(input);
        Assert.Equal("taC dna god", result);
    }
    
    [Fact]
    public void ReverseWords_TabsAndNewlines_PreservedSeparators()
    {
        var input = "\tfoo\nbar";
        var result = WordsReverse.ReverseWords(input);
        Assert.Equal("\toof\nrab", result);
    }
}