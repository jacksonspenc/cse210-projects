public class Warrior : Character
{
    private int _armor;
    private bool _isBlocking;
 
    // ": base()" passes values up to the Character constructor.
    public Warrior(string name) : base(name, 120, 12)
    {
        _armor = 3;
        _isBlocking = false;
    }
 
    // New behavior that only Warriors have.
    public void ShieldBlock()
    {
        _isBlocking = true;
        Console.WriteLine($"{_name} raises a shield! The next hit will be halved.");
    }
 
    // Changes how a Warrior takes damage, then reuses the parent's version.
    public override void TakeDamage(int amount)
    {
        int reduced = amount - _armor;
        if (_isBlocking)
        {
            reduced /= 2;
            _isBlocking = false;
        }
        if (reduced < 0)
        {
            reduced = 0;
        }
        Console.WriteLine($"  {_name}'s armor reduces the hit to {reduced}.");
        base.TakeDamage(reduced);
    }
 
    // Adds to the parent's stats instead of rewriting them.
    public override string DisplayStats()
    {
        return base.DisplayStats() + $" | Armor: {_armor}";
    }
}