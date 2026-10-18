// Owns the list of goals and the player's score, and runs the menu.
// Also handles the points systems and titles.
public class GoalManager
{
    private const int PointsPerLevel = 1000;
    private const int LuckyChancePercent = 10;
 
    private static readonly string[] RankTitles =
    {
        "Novice Seeker",      // level 1
        "Faithful Apprentice",
        "Steady Pilgrim",
        "Valiant Disciple",
        "Stalwart Champion",
        "Noble Guardian",
        "Radiant Hero",
        "Legendary Sage"      // level 8 and beyond
    };
 
    private List<Goal> _goals;
    private int _score;
    private int _totalEventsRecorded;
    private Random _random;
 
    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _totalEventsRecorded = 0;
        _random = new Random();
    }
 
    public void Start()
    {
        Console.WriteLine("=========================================");
        Console.WriteLine("        Welcome to ETERNAL QUEST!");
        Console.WriteLine("=========================================");
 
        bool running = true;
        while (running)
        {
            Console.WriteLine();
            DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");
 
            string choice = Console.ReadLine();
            if (choice == null)
            {
                break;
            }
 
            Console.WriteLine();
            switch (choice.Trim())
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Keep questing! Goodbye.");
                    break;
                default:
                    Console.WriteLine("That isn't a menu option. Please choose 1-6.");
                    break;
            }
        }
    }
 
    // Score, levels, and ranks
 
    private int GetLevel()
    {
        return Math.Max(0, _score) / PointsPerLevel + 1;
    }
 
    private string GetRankTitle()
    {
        int index = Math.Min(GetLevel(), RankTitles.Length) - 1;
        return RankTitles[index];
    }
 
    private string GetLevelProgressBar()
    {
        const int width = 20;
        int intoLevel = Math.Max(0, _score) % PointsPerLevel;
        int filled = intoLevel * width / PointsPerLevel;
        return "[" + new string('=', filled) + new string(' ', width - filled) + "]";
    }
 
    public void DisplayPlayerInfo()
    {
        int nextLevelAt = GetLevel() * PointsPerLevel;
        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Level {GetLevel()} - {GetRankTitle()}  {GetLevelProgressBar()}  {nextLevelAt - Math.Max(0, _score)} pts to next level");
    }
 
    private void AddPoints(int points)
    {
        int oldLevel = GetLevel();
        _score += points;
        int newLevel = GetLevel();
 
        if (newLevel > oldLevel)
        {
            Console.WriteLine();
            Console.WriteLine("  ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");
            Console.WriteLine($"   LEVEL UP! You are now level {newLevel}!");
            Console.WriteLine($"   New rank: {GetRankTitle()}");
            Console.WriteLine("  ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");
        }
        else if (newLevel < oldLevel)
        {
            Console.WriteLine($"  You dropped to level {newLevel}. You can climb back up!");
        }
    }
}