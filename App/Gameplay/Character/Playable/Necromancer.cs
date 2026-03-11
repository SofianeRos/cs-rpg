using ConsoleRPG.App.Gameplay.Character.NonPlayable;

namespace ConsoleRPG.App.Gameplay.Character.Playable;

public class Necromancer(string name) : BaseMagicalCharacter(name)
{
  protected List<BaseDemon> _demons;
  
  public void Summon()
  {
    
  }  
}