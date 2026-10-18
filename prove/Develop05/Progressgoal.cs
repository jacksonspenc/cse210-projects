// EXTRA GOAL TYPE made for long goals and shows progress in completing the goal. 
// There must be a set amount of units for the user to track in this goals section 
// to earn points based off of each unit.
public class ProgressGoal : Goal
{
    private string _unit;
    private int _targetAmount;
    private int _currentAmount;
    private int _bonus;
 
    public ProgressGoal(string shortName, string description, int pointsPerUnit, string unit, int targetAmount, int bonus)
        : this(shortName, description, pointsPerUnit, unit, targetAmount, bonus, 0)
    {
    }
 
    public ProgressGoal(string shortName, string description, int pointsPerUnit, string unit, int targetAmount, int bonus, int currentAmount)
        : base(shortName, description, pointsPerUnit)
    {
        _unit = unit;
        _targetAmount = targetAmount;
        _bonus = bonus;
        _currentAmount = currentAmount;
    }
 
    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }
 
        int remaining = _targetAmount - _currentAmount;
        Console.Write($"  How many {_unit} did you complete? (up to {remaining} remaining) ");
        int amount = InputHelper.ReadInt(1, remaining);
 
        _currentAmount += amount;
        int earned = amount * GetPoints();
 
        if (IsComplete())
        {
            earned += _bonus;
            Console.WriteLine($"  *** You reached {_targetAmount} {_unit}! Bonus of {_bonus} points awarded! ***");
        }
 
        return earned;
    }
 
    public override bool IsComplete()
    {
        return _currentAmount >= _targetAmount;
    }
 
    public override string GetTypeLabel()
    {
        return "Progress";
    }
 
    public override string GetDetailsString()
    {
        return base.GetDetailsString() + $" -- {_currentAmount}/{_targetAmount} {_unit} {ProgressBar()}";
    }
 
    private string ProgressBar()
    {
        const int width = 20;
        int filled = (int)((double)_currentAmount / _targetAmount * width);
        filled = Math.Clamp(filled, 0, width);
        return "[" + new string('#', filled) + new string('-', width - filled) + "]";
    }
 
    public override string GetStringRepresentation()
    {
        return $"ProgressGoal|{SharedFields()}|{Escape(_unit)}|{_targetAmount}|{_bonus}|{_currentAmount}";
    }
}
 