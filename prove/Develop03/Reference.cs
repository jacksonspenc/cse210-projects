// Represents a scripture reference
public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;
 
    // Single verse
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }
}