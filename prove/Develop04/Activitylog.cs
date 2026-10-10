using System;
using System.Collections.Generic;
using System.IO;
 
// Keeps a history of completed activities and saves it to a file so the
// history is still there the next time the program runs.
public class ActivityLog
{
    private string _fileName;
    private List<ActivityRecord> _records;
    private int _sessionCount;
 
    public ActivityLog(string fileName)
    {
        _fileName = fileName;
        _records = new List<ActivityRecord>();
        _sessionCount = 0;
    }
 
    // Reads earlier records from the log file, if there is one.
    public void Load()
    {
        _records.Clear();
 
        if (!File.Exists(_fileName))
        {
            return;
        }
 
        foreach (string line in File.ReadAllLines(_fileName))
        {
            ActivityRecord record = ActivityRecord.FromFileLine(line);
            if (record != null)
            {
                _records.Add(record);
            }
        }
    }
 
    // Records a finished activity and adds it to the end of the log file.
    public void Add(string activityName, int seconds)
    {
        ActivityRecord record = new ActivityRecord(DateTime.Now, activityName, seconds);
        _records.Add(record);
        _sessionCount++;
 
        try
        {
            File.AppendAllText(_fileName, record.ToFileLine() + Environment.NewLine);
        }
        catch (IOException)
        {
            Console.WriteLine("(The activity log file could not be saved.)");
        }
    }
 
    // Shows how many times each activity has been done and for how long.
    public void DisplaySummary()
    {
        Console.WriteLine("Activity Log");
        Console.WriteLine();
 
        if (_records.Count == 0)
        {
            Console.WriteLine("You have not completed any activities yet.");
            return;
        }
 
        // Add up the count and total time for each activity.
        List<string> names = new List<string>();
        Dictionary<string, int> counts = new Dictionary<string, int>();
        Dictionary<string, int> totals = new Dictionary<string, int>();
        int allSeconds = 0;
 
        foreach (ActivityRecord record in _records)
        {
            string name = record.GetActivityName();
            if (!counts.ContainsKey(name))
            {
                names.Add(name);
                counts[name] = 0;
                totals[name] = 0;
            }
 
            counts[name]++;
            totals[name] += record.GetSeconds();
            allSeconds += record.GetSeconds();
        }
 
        foreach (string name in names)
        {
            string times = counts[name] == 1 ? "time" : "times";
            Console.WriteLine($"  {name,-20} {counts[name],3} {times,-5}  {FormatTime(totals[name])}");
        }
 
        ActivityRecord last = _records[_records.Count - 1];
 
        Console.WriteLine();
        Console.WriteLine($"Total mindful time: {FormatTime(allSeconds)}");
        Console.WriteLine($"Completed this session: {_sessionCount}");
        Console.WriteLine($"Most recent: {last.GetActivityName()} on {last.GetCompletedAt():g}");
    }
 
    // Turns a number of seconds into text
    private string FormatTime(int seconds)
    {
        if (seconds < 60)
        {
            return $"{seconds}s";
        }
 
        return $"{seconds / 60}m {seconds % 60:00}s";
    }
}
 