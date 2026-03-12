using System.Reflection;
using ConsoleRPG.App.Gameplay.Character;
using ConsoleRPG.App.Utils;

namespace ConsoleRPG.App;

public sealed class GameManager
{
    private static GameManager? _instance;

    public static GameManager Instance()
    {
        // Si _instance null crée une nouvelle instance et la renvoie
        return _instance ??= new GameManager();
    }

    public void StartGame()
    {
        // Effacer la console
        Console.Clear();
        
        // Ecran titre
        Console.Title = "Console RPG";
        ConsoleHelper.Title("Bienvenue Voyageur !", Color256.Yellow, Color256.DarkGreen);
        ConsoleHelper.Pause("Poussoie sur une clef pour commencer ton aventure");
        
        // Sélection des personnages
        Console.Clear();
        ConsoleHelper.Title("Recrute tes gladiateurs", Color256.Yellow, Color256.DarkGreen);
        Console.WriteLine();
        ConsoleHelper.Title("Premier gladiateur", Color256.White, Color256.DarkMagenta);
        Console.WriteLine(ColorFormat.GetColoredString("Classe :", Color256.White));
        
        // Liste des classes (dans le namespace App.Gameplay.Character.Playable)
        var playableClasses = Assembly
            .GetExecutingAssembly() // Récupère l'Assembly en cours d'éxécution
            .GetTypes() // Récupère la liste de toutes les classes de l'assembly
            .Where(t => t.IsClass && t.Namespace == "ConsoleRPG.App.Gameplay.Character.Playable");
        
        // On l'affiche
        foreach (var playableClass in playableClasses)
        {
            Console.WriteLine(playableClass.Name);
        }

    }
    
    private GameManager() {}
}