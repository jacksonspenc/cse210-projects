public class Archer : Character
{
    private int _arrows;
    private double _critChance;
    private static Random _random = new Random(7);   // fixed seed so the demo runs the same every time
 
    public Archer(string name) : base(name, 90, 10)
    {
        _arrows = 3;
        _critChance = 0.5;
    }
 
    // Replaces the parent's Attack and uses an arrow and might land a critical hit.
    public override void Attack(Character target)
    {
        if (_arrows == 0)
        {
            Console.WriteLine($"{_name} is out of arrows and swings a dagger instead.");
            base.Attack(target);   // falls back to the parent's version
            return;
        }
 
        _arrows--;
        int damage = _attackPower;
        if (_random.NextDouble() < _critChance)
        {
            damage *= 2;
            Console.WriteLine($"{_name} lands a CRITICAL shot on {target.Name} for {damage} damage! (Arrows: {_arrows})");
        }
        else
        {
            Console.WriteLine($"{_name} shoots {target.Name} for {damage} damage. (Arrows: {_arrows})");
        }
        target.TakeDamage(damage);
    }
 
    public void RefillArrows(int count)
    {
        _arrows += count;
        Console.WriteLine($"{_name} picks up {count} arrows. (Arrows: {_arrows})");
    }
 
    public override string DisplayStats()
    {
        return base.DisplayStats() + $" | Arrows: {_arrows}";
    }
}