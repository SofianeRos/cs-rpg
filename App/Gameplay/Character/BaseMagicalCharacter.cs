namespace ConsoleRPG.App.Gameplay.Character;

// syntaxe "Primary constructor"
public abstract class BaseMagicalCharacter (string name) : BaseCharacter(name)
{
    protected int _mana;
    public int Mana => _mana;
    
    //TODO: Spells
    
    public void Levitate()
    {
        
    }

    public void Cast(BaseCharacter target)
    {
        
    }
}