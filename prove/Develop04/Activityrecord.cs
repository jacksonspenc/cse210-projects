using System;
using System.Globalization;
 
// For completed activity, shows how long, when it was done
// and what it was.
public class ActivityRecord
{
    private DateTime _completedAt;
    private string _activityName;
    private int _seconds;
 
    public ActivityRecord(DateTime completedAt, string activityName, int seconds)
    {
        _completedAt = completedAt;
        _activityName = activityName;
        _seconds = seconds;
    }
 
    public DateTime GetCompletedAt()
    {
        return _completedAt;
    }
 
    public string GetActivityName()
    {
        return _activityName;
    }
 
    public int GetSeconds()
    {
        return _seconds;
    }
 
    public string ToFileLine()
    {
        string date = _completedAt.ToString("s", CultureInfo.InvariantCulture);
        return $"{date}|{_activityName}|{_seconds}";
    }
 
    // Turns one line of the log file back into a record. Returns null if the
    // line is not in the expected format, so a damaged line can be skipped.
    public static ActivityRecord FromFileLine(string line)
    {
        string[] parts = line.Split("|");
        if (parts.Length != 3)
        {
            return null;
        }
 
        bool dateOk = DateTime.TryParse(
            parts[0], CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime completedAt);
        bool secondsOk = int.TryParse(parts[2], out int seconds);
 
        if (!dateOk || !secondsOk)
        {
            return null;
        }
 
        return new ActivityRecord(completedAt, parts[1], seconds);
    }
}
 