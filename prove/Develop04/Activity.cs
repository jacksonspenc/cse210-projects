using System;
using System.Collections.Generic;
using System.Threading;
 
// Base class for every mindfulness activity. It has everything that the
// classes share
public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;
    private DateTime _endTime;
 
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }
 
    public string GetName()
    {
        return _name;
    }
 
    public int GetDuration()
    {
        return _duration;
    }
    public void Run()
    {
        DisplayStartingMessage();
        PerformActivity();
        DisplayEndingMessage();
    }
 
    // Each derived class supplies its own version of the main activity.
    protected abstract void PerformActivity();
 
    private void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
 
        _duration = PromptForDuration();
 
        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(4);
        Console.WriteLine();
    }
 
    private void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(4);
    }
    private int PromptForDuration()
    {
        while (true)
        {
            Console.Write("How long, in seconds, would you like for your session? ");
            string input = Console.ReadLine();
 
            if (input == null)
            {
                // Input was closed, so fall back to a short session.
                return 10;
            }
 
            if (int.TryParse(input, out int seconds) && seconds > 0)
            {
                return seconds;
            }
 
            Console.WriteLine("Please enter a whole number greater than zero.");
        }
    }
 
    // Starts the clock for the timed part of the activity.
    protected void StartTimer()
    {
        _endTime = DateTime.Now.AddSeconds(_duration);
    }
 
    protected bool HasTimeLeft()
    {
        return DateTime.Now < _endTime;
    }
 
    // Whole seconds remaining
    protected int GetSecondsLeft()
    {
        double secondsLeft = (_endTime - DateTime.Now).TotalSeconds;
        return Math.Max(0, (int)Math.Ceiling(secondsLeft));
    }
 
    // Shows a spinning character
    protected void ShowSpinner(int seconds)
    {
        List<string> frames = new List<string> { "|", "/", "-", "\\" };
        DateTime stopTime = DateTime.Now.AddSeconds(seconds);
        int frame = 0;
 
        while (DateTime.Now < stopTime)
        {
            Console.Write(frames[frame]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            frame = (frame + 1) % frames.Count;
        }
    }
 
    // Counts down from the given number of seconds to 1
    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            string number = i.ToString();
            Console.Write(number);
            Thread.Sleep(1000);
 
            // Erase however many digits were just written.
            foreach (char digit in number)
            {
                Console.Write("\b \b");
            }
        }
    }
}