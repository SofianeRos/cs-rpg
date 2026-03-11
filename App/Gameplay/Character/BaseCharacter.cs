namespace ConsoleRPG.App.Gameplay.Character;

public abstract class BaseCharacter
{
    protected string _name;
    public string _Name
    {
        get => _name;
    }
    protected int _agility;
    public int Agility => _agility;
    
    protected int _armor;
   public int Armor => _armor;
    
    protected int _hp;
    public int Hp => _hp;
    
    protected int _speed;
    public int Speed => _speed;
    
    protected int _strength;
    public int Strength => _strength;
    
    protected int _stamina;
    public int Stamina => _stamina;
    
    public BaseCharacter(string name)
    {
        _name = name;
    }

    public void Attack(BaseCharacter target)
    {
        
    }

    public void Block()
    {
        
    }
    
    public void Dodge()
    {
        
    }
    
}