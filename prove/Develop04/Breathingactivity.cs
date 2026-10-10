using System;
using System.Threading;
 
public class BreathingActivity : Activity
{
    private int _breatheInSeconds;
    private int _breatheOutSeconds;
    private int _barWidth;
 
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. " +
        "Clear your mind and focus on your breathing.")
    {
        _breatheInSeconds = 4;
        _breatheOutSeconds = 6;
        _barWidth = 30;
    }
 
    protected override void PerformActivity()
    {
        StartTimer();
 
        while (HasTimeLeft())
        {
            Console.WriteLine();
 
            // Never run a breath longer than the time that is left.
            int inSeconds = Math.Min(_breatheInSeconds, GetSecondsLeft());
            AnimateBreath("Breathe in...", inSeconds, true);
 
            int outSeconds = Math.Min(_breatheOutSeconds, GetSecondsLeft());
            if (outSeconds > 0)
            {
                AnimateBreath("Now breathe out...", outSeconds, false);
            }
        }
    }
    private void AnimateBreath(string message, int seconds, bool growing)
    {
        string label = message.PadRight(18);
        DateTime start = DateTime.Now;
 
        while (true)
        {
            double elapsed = (DateTime.Now - start).TotalSeconds;
            if (elapsed >= seconds)
            {
                break;
            }
 
            // progress goes from 0 to 1. I used a referene to make
            // the animation ease out.
            double progress = elapsed / seconds;
            double eased = 1 - Math.Pow(1 - progress, 3);
            double size = growing ? eased : 1 - eased;
 
            int width = (int)Math.Round(size * _barWidth);
            int secondsLeft = (int)Math.Ceiling(seconds - elapsed);
 
            DrawBreathLine(label, width, secondsLeft.ToString());
            Thread.Sleep(50);
        }
 
        // Finish with a full bar or an empty one.
        DrawBreathLine(label, growing ? _barWidth : 0, "");
        Console.WriteLine();
    }

    private void DrawBreathLine(string label, int width, string countdown)
    {
        string bar = new string('=', width).PadRight(_barWidth);
 
        // "\r" moves back to the start of the line so it is drawn over.
        Console.Write($"\r{label} {bar} {countdown.PadRight(3)}");
    }
}