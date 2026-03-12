namespace ConsoleRPG.App.Gameplay.Magic;

public class Spell(string name, int manaCost, int damage)
{
    protected string _name = name;
    public string Name => _name;
    
    protected int _manaCost = manaCost;
    public int ManaCost => _manaCost;
    
    protected int _damage = damage;
    public int Damage => _damage;
}