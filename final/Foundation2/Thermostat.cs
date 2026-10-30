public class Thermostat
{
    private const int MinTemp = 50;
    private const int MaxTemp = 90;
 
    private double _currentTemp;
    private int _targetTemp;
    private ThermostatMode _mode;
    private bool _isOn;
 
    public Thermostat(double startingTemp)
    {
        _currentTemp = startingTemp;
        _targetTemp = 70;
        _mode = ThermostatMode.Off;
        _isOn = false;
    }
 
    // Read-only: outside code can see the room temperature but can't change it.
    public double CurrentTemp
    {
        get { return _currentTemp; }
    }
 
    // Read-only, the only way to change this is TurnOn() or TurnOff().
    public bool IsOn
    {
        get { return _isOn; }
    }
 
    // The setter protects the data, values outside 50-90 are rejected.
    public int TargetTemp
    {
        get { return _targetTemp; }
        set
        {
            if (IsValidTemp(value))
            {
                _targetTemp = value;
                Log($"Target set to {_targetTemp}°F.");
            }
            else
            {
                Log($"Rejected: {value}°F is outside the safe range ({MinTemp}-{MaxTemp}°F).");
            }
        }
    }
 
    // The setter protects the data, the mode can't change while the unit is off.
    public ThermostatMode Mode
    {
        get { return _mode; }
        set
        {
            if (!_isOn)
            {
                Log("Rejected: turn the thermostat on before changing the mode.");
                return;
            }
            _mode = value;
            Log($"Mode set to {_mode}.");
        }
    }
 
    public void TurnOn()
    {
        _isOn = true;
        Log("Thermostat turned on.");
    }
 
    public void TurnOff()
    {
        _isOn = false;
        _mode = ThermostatMode.Off;
        Log("Thermostat turned off.");
    }
 
    public void IncreaseTarget()
    {
        TargetTemp = _targetTemp + 1;   // goes through the setter, so it's still validated
    }
 
    public void DecreaseTarget()
    {
        TargetTemp = _targetTemp - 1;
    }
 
    // Simulates one minute passing: the room moves 1 degree toward the target.
    public void Tick()
    {
        if (_isOn && _mode == ThermostatMode.Heat && _currentTemp < _targetTemp)
        {
            _currentTemp += 1;
        }
        else if (_isOn && _mode == ThermostatMode.Cool && _currentTemp > _targetTemp)
        {
            _currentTemp -= 1;
        }
    }
 
    public string GetStatus()
    {
        string power = _isOn ? "ON" : "OFF";
        return $"[{power}] Mode: {_mode} | Room: {_currentTemp}°F | Target: {_targetTemp}°F";
    }
 
    // Private helpers, used inside the class only.
    private bool IsValidTemp(int temp)
    {
        return temp >= MinTemp && temp <= MaxTemp;
    }
 
    private void Log(string message)
    {
        Console.WriteLine("  > " + message);
    }
}