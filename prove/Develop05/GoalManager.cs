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
 
    // Score levels and ranks
 
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

    // ---------- Menu actions ----------
 
    public void ListGoalNames()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetName()}");
        }
    }
 
    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals yet. Create one to begin your quest!");
            return;
        }
 
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Goal goal = _goals[i];
            Console.WriteLine($"  {i + 1}. {goal.GetDetailsString()}  <{goal.GetTypeLabel()}>");
        }
        Console.WriteLine();
        Console.WriteLine("  Key: [ ] not done  [X] done  [∞] eternal  [!] bad habit");
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal     (do it once)");
        Console.WriteLine("  2. Eternal Goal    (repeat forever)");
        Console.WriteLine("  3. Checklist Goal  (do it a set number of times)");
        Console.WriteLine("  4. Progress Goal   (work toward a big total, e.g. 100 miles)");
        Console.WriteLine("  5. Bad Habit       (lose points each time it happens)");
        Console.Write("Which type of goal would you like to create? ");
        int type = InputHelper.ReadInt(1, 5);
 
        Console.Write("What is the name of your goal? ");
        string name = InputHelper.ReadText();
        Console.Write("What is a short description of it? ");
        string description = InputHelper.ReadText();
 
        Goal goal;
        switch (type)
        {
            case 1:
                Console.Write("How many points is it worth? ");
                goal = new SimpleGoal(name, description, InputHelper.ReadInt(1, 1_000_000));
                break;
 
            case 2:
                Console.Write("How many points each time you record it? ");
                goal = new EternalGoal(name, description, InputHelper.ReadInt(1, 1_000_000));
                break;
 
            case 3:
            {
                Console.Write("How many points each time you record it? ");
                int points = InputHelper.ReadInt(1, 1_000_000);
                Console.Write("How many times does it need to be accomplished for a bonus? ");
                int target = InputHelper.ReadInt(1, 10_000);
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = InputHelper.ReadInt(0, 1_000_000);
                goal = new ChecklistGoal(name, description, points, target, bonus);
                break;
            }
 
            case 4:
            {
                Console.Write("What unit are you measuring (e.g. miles, pages, hours)? ");
                string unit = InputHelper.ReadText();
                Console.Write($"What is your target total of {unit}? ");
                int target = InputHelper.ReadInt(1, 1_000_000);
                Console.Write($"How many points per {unit.TrimEnd('s')}? ");
                int perUnit = InputHelper.ReadInt(1, 1_000_000);
                Console.Write("What is the bonus for reaching the target? ");
                int bonus = InputHelper.ReadInt(0, 1_000_000);
                goal = new ProgressGoal(name, description, perUnit, unit, target, bonus);
                break;
            }
 
            default:
                Console.Write("How many points do you lose each time it happens? ");
                goal = new NegativeGoal(name, description, InputHelper.ReadInt(1, 1_000_000));
                break;
        }
 
        _goals.Add(goal);
        Console.WriteLine($"Goal \"{name}\" added to your quest!");
    }
 
    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("There are no goals to record yet.");
            return;
        }
 
        Console.WriteLine("The goals are:");
        ListGoalNames();
        Console.Write("Which goal did you accomplish? ");
        Goal goal = _goals[InputHelper.ReadInt(1, _goals.Count) - 1];
 
        if (goal.IsComplete())
        {
            Console.WriteLine($"\"{goal.GetName()}\" is already complete. Pick a new challenge!");
            return;
        }
 
        // Polymorphism: each goal type decides how recording works.
        int earned = goal.RecordEvent();
 
        // Lucky bonus: a small chance to double points on good deeds.
        if (earned > 0 && _random.Next(100) < LuckyChancePercent)
        {
            Console.WriteLine("  ~ LUCKY STRIKE! Your points are doubled this time! ~");
            earned *= 2;
        }
 
        _totalEventsRecorded++;
 
        if (earned >= 0)
        {
            Console.WriteLine($"Congratulations! You have earned {earned} points!");
        }
        else
        {
            Console.WriteLine($"You lost {-earned} points.");
        }
 
        AddPoints(earned);
 
        if (_totalEventsRecorded % 10 == 0)
        {
            Console.WriteLine($"  Milestone: you've recorded {_totalEventsRecorded} events on your quest!");
        }
 
        Console.WriteLine($"You now have {_score} points.");
    }
 
    // Saving and loading
 
    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = InputHelper.ReadText();
 
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);
                writer.WriteLine(_totalEventsRecorded);
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine($"Saved {_goals.Count} goal(s) and your score to {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not save the file: {ex.Message}");
        }
    }
 
    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = InputHelper.ReadText();
 
        if (!File.Exists(filename))
        {
            Console.WriteLine($"No file named {filename} was found.");
            return;
        }
 
        try
        {
            string[] lines = File.ReadAllLines(filename);
            List<Goal> loaded = new List<Goal>();
 
            for (int i = 2; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    loaded.Add(ParseGoal(lines[i]));
                }
            }
 
            // Only replace current data once the whole file parsed cleanly.
            _score = int.Parse(lines[0]);
            _totalEventsRecorded = int.Parse(lines[1]);
            _goals = loaded;
            Console.WriteLine($"Loaded {_goals.Count} goal(s) from {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"That file couldn't be read as a goal file ({ex.Message}).");
        }
    }
 
    private static Goal ParseGoal(string line)
    {
        string[] parts = line.Split('|');
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = Goal.Unescape(parts[i]);
        }
 
        string type = parts[0];
        string name = parts[1];
        string description = parts[2];
        int points = int.Parse(parts[3]);
 
        switch (type)
        {
            case "SimpleGoal":
                return new SimpleGoal(name, description, points, bool.Parse(parts[4]));
            case "EternalGoal":
                return new EternalGoal(name, description, points, int.Parse(parts[4]));
            case "ChecklistGoal":
                // bonus | target | amountCompleted
                return new ChecklistGoal(name, description, points,
                    int.Parse(parts[5]), int.Parse(parts[4]), int.Parse(parts[6]));
            case "ProgressGoal":
                // unit | target | bonus | current
                return new ProgressGoal(name, description, points, parts[4],
                    int.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[7]));
            case "NegativeGoal":
                return new NegativeGoal(name, description, points, int.Parse(parts[4]));
            default:
                throw new FormatException($"Unknown goal type '{type}'");
        }
    }
}
 