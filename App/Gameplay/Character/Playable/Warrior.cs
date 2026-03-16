namespace ConsoleRPG.App.Gameplay.Character.Playable;

public class Warrior(string name) : BaseCharacter(name)
{
    protected int _rage;
    public int Rage => _rage;

    public void Setup(int agility, int armor, int hp, int speed, int strength, int stamina, int rage)
    {
        base.Setup(agility, armor, hp, speed, strength, stamina);
        _rage = rage;
    }
    
    public void BreakOut()
    {
        
    }
}