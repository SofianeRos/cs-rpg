namespace ConsoleRPG.App.Gameplay.Character.Playable;

public class Thief(string name) : BaseCharacter(name)
{
    protected int _luck;
    public int Luck => _luck;

    public void Setup(int agility, int armor, int hp, int speed, int strength, int stamina, int luck)
    {
        base.Setup(agility, armor, hp, speed, strength, stamina);
        _luck = luck;
    }
    
    public void Hideout()
    {
        
    }
}