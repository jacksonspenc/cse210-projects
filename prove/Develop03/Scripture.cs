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