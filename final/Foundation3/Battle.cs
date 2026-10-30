public class Battle
{
    private Character _player;
    private Character _enemy;
 
    public Battle(Character player, Character enemy)
    {
        _player = player;
        _enemy = enemy;
    }
 
    public void Start()
    {
        Console.WriteLine($"--- {_player.Name} vs {_enemy.Name} ---");
        int round = 1;
 
        while (_player.IsAlive && _enemy.IsAlive)
        {
            Console.WriteLine($"\nRound {round}:");
            PlayerTurn();
            if (_enemy.IsAlive)
            {
                EnemyTurn();
            }
            round++;
        }
 
        string winner = _player.IsAlive ? _player.Name : _enemy.Name;
        Console.WriteLine($"\n{winner} wins the battle!");
    }
 
    private void PlayerTurn()
    {
        _player.Attack(_enemy);
    }
 
    private void EnemyTurn()
    {
        _enemy.Attack(_player);
    }
}
