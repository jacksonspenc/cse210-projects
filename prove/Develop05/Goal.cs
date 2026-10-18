// This is the base class for every goal. It has all of the attributes
// that the goals share and what behaviors each one has to override 
using System.Reflection.Emit;

public abstract class Goal
{
    private string _shortName;
    private string _description;
    private int _points;
 
    protected Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }
 
    public string GetName()
    {
        return _shortName;
    }
 
    public string GetDescription()
    {
        return _description;
    }

    public int GetPoints()
    {
        return _points;
    }

    // Records one accomplishment of the goal and returns the points earned
    public abstract int RecordEvent();

    // True when the goal is complete
    public abstract bool IsComplete();

    // makes the file to be able to save it
    public abstract string GetStringRepresentation();

    // Label in the goal type comlumn
    public abstract string GetTypeLabel();


    public virtual string GetCheckbox()
    {
        return IsComplete() ? "[X]" : "[ ]";
    }
 
    public virtual string GetDetailsString()
    {
        return $"{GetCheckbox()} {_shortName} ({_description})";
    }
 
    protected static string Escape(string text)
    {
        return text.Replace("\\", "\\\\").Replace("|", "\\p");
    }
 
    public static string Unescape(string text)
    {
        return text.Replace("\\p", "|").Replace("\\\\", "\\");
    }
 
    protected string SharedFields()
    {
        return $"{Escape(_shortName)}|{Escape(_description)}|{_points}";
    }
}
 