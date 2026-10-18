// EXTRA GOAL TYPE: This is for bad habits and the user will lose points when recording in this section. 
// Goal is never complete and can be tracked for how many times you have made the same mistake.
public class NegativeGoal : Goal
{
    private int _timesSlipped;
 
    public NegativeGoal(string shortName, string description, int penalty)
        : this(shortName, description, penalty, 0)
    {
    }
 
    public NegativeGoal(string shortName, string description, int penalty, int timesSlipped)
        : base(shortName, description, Math.Abs(penalty))
    {
        _timesSlipped = timesSlipped;
    }
 
    public override int RecordEvent()
    {
        _timesSlipped++;
        Console.WriteLine("  Ouch! Every day is a new chance to do better.");
        return -GetPoints();
    }
 
    public override bool IsComplete()
    {
        return false;
    }
 
    public override string GetCheckbox()
    {
        return "[!]";
    }
 
    public override string GetTypeLabel()
    {
        return "Bad Habit";
    }
 
    public override string GetDetailsString()
    {
        return base.GetDetailsString() + $" -- -{GetPoints()} pts each, slipped {_timesSlipped} time(s)";
    }
 
    public override string GetStringRepresentation()
    {
        return $"NegativeGoal|{SharedFields()}|{_timesSlipped}";
    }
}
 