namespace ConsoleRPG.App.Gameplay.Magic;

public class Spell
{
    protected string _name;
    public string Name => _name;
    
    protected int _manaCost;
    public int ManaCost => _manaCost;
    
    protected int _damage;
    public int Damage => _damage;

    public Spell(string name, int manaCost, int damage)
    {
        _name = name;
        _manaCost = manaCost;
        _damage = damage;
    }
}