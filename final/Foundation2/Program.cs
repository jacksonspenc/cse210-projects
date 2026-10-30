class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Thermostat (Encapsulation Demo) ===\n");
 
        Thermostat thermostat = new Thermostat(62);
        Console.WriteLine(thermostat.GetStatus() + "\n");
 
        Console.WriteLine("Try to set the mode while the thermostat is off:");
        thermostat.Mode = ThermostatMode.Heat;       // rejected
 
        Console.WriteLine("\nTurn it on and set it to heat:");
        thermostat.TurnOn();
        thermostat.Mode = ThermostatMode.Heat;
 
        Console.WriteLine("\nTry to set unsafe temperatures:");
        thermostat.TargetTemp = 200;                 // rejected
        thermostat.TargetTemp = 20;                  // rejected
 
        Console.WriteLine("\nSet a valid target, then bump it up by one:");
        thermostat.TargetTemp = 66;
        thermostat.IncreaseTarget();
 
        Console.WriteLine("\nLet some time pass while it heats:");
        for (int minute = 1; minute <= 6; minute++)
        {
            thermostat.Tick();
            Console.WriteLine($"  Minute {minute}: {thermostat.CurrentTemp}°F");
        }
 
        Console.WriteLine("\nTurn it off:");
        thermostat.TurnOff();
        Console.WriteLine(thermostat.GetStatus());
    }
}