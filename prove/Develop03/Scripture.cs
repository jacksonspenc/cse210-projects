using System;
using System.Collections.Generic;
 
// Represents a complete scripture
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;
 
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        _random = new Random();
 
        string[] parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            _words.Add(new Word(part));
        }
    }

    // Hides up to numberToHide words which chosen at random from only the words
    // that are still visible for the stretch challenge
    public void HideRandomWords(int numberToHide)
    {
        List<Word> visibleWords = new List<Word>();
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                visibleWords.Add(word);
            }
        }
 
        for (int i = 0; i < numberToHide && visibleWords.Count > 0; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }
 
    public string GetDisplayText()
    {
        List<string> displayWords = new List<string>();
        foreach (Word word in _words)
        {
            displayWords.Add(word.GetDisplayText());
        }
 
        return $"{_reference.GetDisplayText()} {string.Join(" ", displayWords)}";
    }
 
    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
 
        return true;
    }
}
 