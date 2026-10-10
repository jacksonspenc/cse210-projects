using System;
using System.Collections.Generic;
 
public class ListingActivity : Activity
{
    private PromptDeck _prompts;
    private int _count;
 
    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list " +
        "as many things as you can in a certain area.")
    {
        _count = 0;
 
        _prompts = new PromptDeck(new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        });
    }
 
    protected override void PerformActivity()
    {
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($" --- {_prompts.Draw()} ---");
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.WriteLine();
 
        _count = 0;
        StartTimer();
 
        while (HasTimeLeft())
        {
            Console.Write("> ");
            string item = Console.ReadLine();
 
            if (item == null)
            {
                break;
            }
 
            if (item.Trim() != "")
            {
                _count++;
            }
        }
 
        string word = _count == 1 ? "item" : "items";
        Console.WriteLine($"You listed {_count} {word}!");
    }
}