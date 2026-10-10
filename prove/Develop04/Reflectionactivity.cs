using System;
using System.Collections.Generic;
 
public class ReflectionActivity : Activity
{
    private PromptDeck _prompts;
    private PromptDeck _questions;
    private int _secondsPerQuestion;
 
    public ReflectionActivity() : base(
        "Reflection Activity",
        "This activity will help you reflect on times in your life when you have shown strength " +
        "and resilience. This will help you recognize the power you have and how you can use it " +
        "in other aspects of your life.")
    {
        _secondsPerQuestion = 8;
 
        _prompts = new PromptDeck(new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        });
 
        _questions = new PromptDeck(new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        });
    }
 
    protected override void PerformActivity()
    {
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();
        Console.WriteLine($" --- {_prompts.Draw()} ---");
        Console.WriteLine();
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();
 
        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.Clear();
 
        StartTimer();
 
        while (HasTimeLeft())
        {
            Console.Write($"> {_questions.Draw()} ");
 
            // Do not pause longer than the time that is left.
            int pause = Math.Min(_secondsPerQuestion, GetSecondsLeft());
            ShowSpinner(pause);
            Console.WriteLine();
        }
    }
}
 