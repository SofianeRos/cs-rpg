namespace ConsoleRPG.App.Gameplay.Character.PlayableCharacter;

public class Warrior(string name) : BaseCharacter(name)
{
    protected int _rage;
    public int Rage => _rage;

    public void BreakOut()
    {
        
    }
}