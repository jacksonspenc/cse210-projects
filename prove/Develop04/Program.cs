using System;

// Exceeding requirements:
// 
// In the Promptdeck class it deals out questions at random from
// a list and every question is asked before the list gets shuffled
// and it choose the questions at random again, like a deck of cards!

// In my Activitylog and Activityrecord classes they record each activity
// in a txt file. The file runs for the whole activity recording each activity
// and logging it. The view log option in the menu allows you to look at your
// past activity history with information about it like how long it took, what
// time you did it and what activity it was.
//
// For the breathing animation I made the bar start fast and slow down at the end
// to make the animation ease up at the end as a nice effect.
class Program
{
    static void Main(string[] args)
    {
        BreathingActivity breathing = new BreathingActivity();
        ReflectionActivity reflection = new ReflectionActivity();
        ListingActivity listing = new ListingActivity();
 
        ActivityLog log = new ActivityLog("mindfulness-log.txt");
        log.Load();
 
        bool running = true;
 
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. View activity log");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");
 
            string choice = Console.ReadLine();

            if (choice == null)
            {
                // Input was closed, so there is nothing more to read.
                break;
            }
 
            switch (choice.Trim())
            {
                case "1":
                    RunActivity(breathing, log);
                    break;
                case "2":
                    RunActivity(reflection, log);
                    break;
                case "3":
                    RunActivity(listing, log);
                    break;
                case "4":
                    Console.Clear();
                    log.DisplaySummary();
                    Console.WriteLine();
                    Console.Write("Press enter to return to the menu.");
                    Console.ReadLine();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Please enter a number from 1 to 5.");
                    System.Threading.Thread.Sleep(1500);
                    break;
            }
        }
    }

    static void RunActivity(Activity activity, ActivityLog log)
    {
        activity.Run();
        log.Add(activity.GetName(), activity.GetDuration());
    }
}