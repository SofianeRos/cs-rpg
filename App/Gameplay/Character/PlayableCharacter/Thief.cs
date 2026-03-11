namespace ConsoleRPG.App.Gameplay.Character.PlayableCharacter;

public class Thief(string name) : BaseCharacter(name)
{
    protected int _luck;
    public int Luck => _luck;

    public void Hideout()
    {
        
    }
}