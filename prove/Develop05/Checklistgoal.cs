// A goal that that is accomplished over a set number of times. 
// If you complete it you get a point bonus.
public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;
 
    public ChecklistGoal(string shortName, string description, int points, int target, int bonus)
        : this(shortName, description, points, target, bonus, 0)
    {
    }
 
    public ChecklistGoal(string shortName, string description, int points, int target, int bonus, int amountCompleted)
        : base(shortName, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }
 
    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }
 
        _amountCompleted++;
        int earned = GetPoints();
 
        if (IsComplete())
        {
            earned += _bonus;
            Console.WriteLine($"  *** Checklist complete! Bonus of {_bonus} points awarded! ***");
        }
 
        return earned;
    }
 
    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }
 
    public override string GetTypeLabel()
    {
        return "Checklist";
    }
 
    public override string GetDetailsString()
    {
        return base.GetDetailsString() + $" -- Currently completed: {_amountCompleted}/{_target}";
    }
 
    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{SharedFields()}|{_bonus}|{_target}|{_amountCompleted}";
    }
}