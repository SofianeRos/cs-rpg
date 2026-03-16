using ConsoleRPG.App.Gameplay.Magic;

namespace ConsoleRPG.App.Gameplay.Character;

// Syntaxe "Primary constructor" : si le constructeur est identique a parent
public abstract class BaseMagicalCharacter(string name) : BaseCharacter(name)
{
    protected int _mana;
    public int Mana => _mana;
    
    private List<Spell> _spells = [];
    public List<Spell> Spells => _spells;

    public void Setup(int agility, int armor, int hp, int speed, int strength, int stamina, int mana)
    {
        base.Setup(agility, armor, hp, speed, strength, stamina);
        _mana = mana;
        
        // TODO: Un vrai truc pour les sorts
        Spell fireBall = new ("FireBall", 20, 20);
        Spells.Add(fireBall);
    }

    public void Levitate()
    {
        
    }

    public void Cast(Spell spell, BaseCharacter target)
    {
        if(spell.ManaCost > Mana)
            Console.WriteLine("Heeeeee NON !");
    }
}