// A goal that is never complete. Every time it is recorded the user earns points
// and keeps track of how many times it is used.

public class EternalGoal : Goal
{
    private int _timesRecorded;
 
    public EternalGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
        _timesRecorded = 0;
    }
 
    public EternalGoal(string shortName, string description, int points, int timesRecorded)
        : base(shortName, description, points)
    {
        _timesRecorded = timesRecorded;
    }
 
    public override int RecordEvent()
    {
        _timesRecorded++;
        return GetPoints();
    }
 
    public override bool IsComplete()
    {
        return false;
    }
 
    public override string GetCheckbox()
    {
        return "[∞]";
    }
 
    public override string GetTypeLabel()
    {
        return "Eternal";
    }
 
    public override string GetDetailsString()
    {
        return base.GetDetailsString() + $" -- Recorded {_timesRecorded} time(s)";
    }
 
    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{SharedFields()}|{_timesRecorded}";
    }
}
 