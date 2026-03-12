using ConsoleRPG.App.Gameplay.Magic;
using ConsoleRPG.App.Utils;

namespace ConsoleRPG;

class Program
{
    static void Main(string[] args)
    {
        var test = ColorFormat.GetColoredString("Hello World!", Color256.White, Color256.DarkGreen);
        Console.WriteLine(test);
        Spell testSpell = new("toto", 15, 25);
        Console.WriteLine(testSpell.Name);
        Console.WriteLine(testSpell.ManaCost);
        Console.WriteLine(testSpell.Damage);
    }
}