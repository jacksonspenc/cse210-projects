public class Character
{
    // "protected" means child classes can use these fields, but outside code can't.
    protected string _name;
    protected int _health;
    protected int _maxHealth;
    protected int _attackPower;
    protected int _level;
 
    public string Name { get { return _name; } }
    public int Health { get { return _health; } }
    public bool IsAlive { get { return _health > 0; } }
 
    public Character(string name, int maxHealth, int attackPower)
    {
        _name = name;
        _maxHealth = maxHealth;
        _health = maxHealth;
        _attackPower = attackPower;
        _level = 1;
    }
 
    // "virtual" means a child class is allowed to replace this with its own version.
    public virtual void Attack(Character target)
    {
        Console.WriteLine($"{_name} attacks {target.Name} for {_attackPower} damage.");
        target.TakeDamage(_attackPower);
    }
 
    public virtual void TakeDamage(int amount)
    {
        _health -= amount;
        if (_health < 0)
        {
            _health = 0;
        }
        Console.WriteLine($"  {_name} has {_health}/{_maxHealth} health left.");
    }
 
    public void Heal(int amount)
    {
        _health += amount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        Console.WriteLine($"{_name} heals to {_health}/{_maxHealth} health.");
    }
 
    public void LevelUp()
    {
        _level++;
        _maxHealth += 10;
        _health = _maxHealth;
        _attackPower += 3;
        Console.WriteLine($"{_name} reached level {_level}!");
    }
 
    public virtual string DisplayStats()
    {
        return $"{_name} (Lv {_level}) | HP: {_health}/{_maxHealth} | Attack: {_attackPower}";
    }
}