public class Mage : Character
{
    private int _mana;
    private int _maxMana;
    private const int FireballCost = 20;
 
    public Mage(string name) : base(name, 80, 8)
    {
        _maxMana = 50;
        _mana = _maxMana;
    }
 
    // New behavior that only Mages have.
    public void CastFireball(Character target)
    {
        if (_mana < FireballCost)
        {
            Console.WriteLine($"{_name} doesn't have enough mana! Using a normal attack instead.");
            Attack(target);   // falls back to the inherited Attack()
            return;
        }
 
        _mana -= FireballCost;
        int damage = _attackPower * 3;
        Console.WriteLine($"{_name} casts Fireball at {target.Name} for {damage} damage! (Mana: {_mana}/{_maxMana})");
        target.TakeDamage(damage);
    }
 
    public void RestoreMana(int amount)
    {
        _mana += amount;
        if (_mana > _maxMana)
        {
            _mana = _maxMana;
        }
        Console.WriteLine($"{_name} restores mana to {_mana}/{_maxMana}.");
    }
 
    public override string DisplayStats()
    {
        return base.DisplayStats() + $" | Mana: {_mana}/{_maxMana}";
    }
}