namespace ConsoleRPG.App.Gameplay.PlayableCharacter;

public abstract class Character
{
    protected string name;
    protected int agility;
    protected int armor;
    protected int hp;
    protected int speed;
    protected int strength;
    protected int stamina;
    
    public Character(string name)
    {
        this.name = name;
    }
}