using ConsoleRPG.App.Gameplay.Magic;

namespace ConsoleRPG.App.Gameplay.Character;

// Syntaxe "Primary constructor" : si le constructeur est identique a parent
public abstract class BaseMagicalCharacter(string name) : BaseCharacter(name)
{
    protected int _mana;
    public int Mana => _mana;
    
    protected List<Spell> _spells;
    public List<Spell> Spells => _spells;

    public void Levitate()
    {
        
    }

    public void Cast(Spell spell, BaseCharacter target)
    {
        if(spell.ManaCost > Mana)
            Console.WriteLine("Heeeeee NON !");
    }
}