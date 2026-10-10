using System;

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