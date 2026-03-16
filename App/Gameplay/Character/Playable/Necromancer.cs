using ConsoleRPG.App.Gameplay.Character.NonPlayable;

namespace ConsoleRPG.App.Gameplay.Character.Playable;

public class Necromancer(string name) : BaseMagicalCharacter(name)
{
    private List<BaseDemon> _demons = [];
    public List<BaseDemon> Demons => _demons;

    public void Summon()
    {
        // TODO: Un vrai truc pour les démons
        Balrog balrog = new("Balrog");
        balrog.Setup(30, 100, 100, 35, 100, 100);
        _demons.Add(balrog);
    }
}