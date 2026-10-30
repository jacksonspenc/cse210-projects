class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== RPG Characters (Inheritance Demo) ===\n");
 
        Warrior warrior = new Warrior("Thorin");
        Mage mage = new Mage("Elara");
        Archer archer = new Archer("Robin");
 
        // Every class gets DisplayStats() from Character, and each adds its own extra stat.
        Console.WriteLine("Starting stats:");
        Console.WriteLine("  " + warrior.DisplayStats());
        Console.WriteLine("  " + mage.DisplayStats());
        Console.WriteLine("  " + archer.DisplayStats());
 
        // Abilities that only exist in the child classes.
        Console.WriteLine("\nUnique abilities:");
        warrior.ShieldBlock();
        mage.CastFireball(warrior);     // Warrior's armor and shield reduce the damage
        archer.Attack(mage);            // Archer's own version of Attack()
 
        // Methods inherited from Character, used by every class without rewriting them.
        Console.WriteLine("\nInherited abilities (written once in Character):");
        mage.Heal(30);
        archer.LevelUp();
        Console.WriteLine("  " + archer.DisplayStats());
 
        // A short battle between two different kinds of characters.
        Console.WriteLine();
        Battle battle = new Battle(new Archer("Robin"), new Warrior("Grom"));
        battle.Start();
    }
}