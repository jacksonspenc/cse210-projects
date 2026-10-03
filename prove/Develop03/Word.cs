// Represents a single word and tracks whether it is hidden.
public class Word
{
    private string _text;
    private bool _isHidden;
 
    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }
 
    public void Hide()
    {
        _isHidden = true;
    }
 
    public void Show()
    {
        _isHidden = false;
    }
 
    public bool IsHidden()
    {
        return _isHidden;
    }
    // Returns the word or displays a blank space. Punctuation
    //is still visible as part of the stretch challenge.
    public string GetDisplayText()
    {
        if (!_isHidden)
        {
            return _text;
        }
 
        string hiddenText = "";
        foreach (char character in _text)
        {
            if (char.IsLetterOrDigit(character))
            {
                hiddenText += "_";
            }
            else
            {
                hiddenText += character;
            }
        }
 
        return hiddenText;
    }
}
 