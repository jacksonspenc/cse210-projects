using System;
using System.Collections.Generic;
 
// The set of prompts is picked at random, but
// it doesn't repeat questions until it has
// gone through all of the options
public class PromptDeck
{
    private List<string> _allPrompts;
    private List<string> _unusedPrompts;
    private string _lastPrompt;
    private Random _random;
 
    public PromptDeck(List<string> prompts)
    {
        _allPrompts = new List<string>(prompts);
        _unusedPrompts = new List<string>();
        _lastPrompt = "";
        _random = new Random();
    }
 
    // Returns a random prompt that has not been used since the last refill.
    public string Draw()
    {
        if (_allPrompts.Count == 0)
        {
            return "";
        }
 
        if (_unusedPrompts.Count == 0)
        {
            _unusedPrompts.AddRange(_allPrompts);
        }
 
        int index = _random.Next(_unusedPrompts.Count);
 
        // Right after a refill, avoid dealing the same prompt twice in a row.
        if (_unusedPrompts[index] == _lastPrompt && _unusedPrompts.Count > 1)
        {
            index = (index + 1) % _unusedPrompts.Count;
        }
 
        string prompt = _unusedPrompts[index];
        _unusedPrompts.RemoveAt(index);
        _lastPrompt = prompt;
        return prompt;
    }
}
 