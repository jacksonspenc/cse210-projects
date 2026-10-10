using System;
using System.Collections.Generic;
using System.Threading;
 
// Base class for every mindfulness activity. It has everything that the
// classes share
public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;
    private DateTime _endTime;
 
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }
 
    public string GetName()
    {
        return _name;
    }
 
    public int GetDuration()
    {
        return _duration;
    }
}