using System.Reflection;
using ConsoleRPG.App.Gameplay.Character;
using ConsoleRPG.App.Gameplay.Character.Playable;
using ConsoleRPG.App.Utils;

namespace ConsoleRPG.App;

public sealed class GameManager
{
    #region Champs
    
    private static GameManager? _instance;

    public List<Type> PlayableClasses { get; private set; } = [];
    
    public BaseCharacter? Fighter1 { get; private set; }
    public BaseCharacter? Fighter2 { get; private set; }
    
    #endregion
    
    #region Méthodes
    public static GameManager Instance()
    {
        return _instance ??= new GameManager();
    }
    
    public void StartGame()
    {
        Initialize();
        SceneFighterChoice();
    }

    private GameManager() {}

    private void Initialize()
    {
        PlayableClasses = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, Namespace: "ConsoleRPG.App.Gameplay.Character.Playable" })
            .ToList();
        
        Console.Clear();
        Console.Title = "Console RPG";
        ConsoleHelper.Title("Bienvenue Voyageur !", Color256.Yellow, Color256.DarkGreen);
        ConsoleHelper.Pause("Poussoie sur une clef pour commencer ton aventure");
    }

    private void SetupFighter(BaseCharacter fighter)
    {
        switch (fighter)
        {
            case Necromancer n:
                n.Setup(75, 40, 75, 100, 50, 80, 100);
                break;
            case Thief t:
                t.Setup(100, 65, 80, 100, 85, 80, 100);
                break;
            case Warrior w:
                w.Setup(50, 100, 100, 40, 100, 100, 0);
                break;
            case Wizard wiz:
                wiz.Setup(75, 40, 100, 100, 50, 80, 100);
                break;
        }
    }
    
    #region Scenes
    private void SceneFighterChoice()
    {
        Console.Clear();
        ConsoleHelper.Title("Recrute tes gladiateurs", Color256.Yellow, Color256.DarkGreen);
        Console.WriteLine();
        
        bool menuSuccess;
        do
        {
            menuSuccess = MenuFighterChoice(
                "Premier Gladiateur", 
                Color256.White, 
                Color256.DarkMagenta,
                out var chosenFighter
            );
            Fighter1 = chosenFighter as BaseCharacter;
        } while (!menuSuccess);
        
        do
        {
            menuSuccess = MenuFighterChoice(
                "Deuxième Gladiateur", 
                Color256.White, 
                Color256.DarkYellow,
                out var chosenFighter
            );
            Fighter2 = chosenFighter as BaseCharacter;
        } while (!menuSuccess);

        if (Fighter1 == null || Fighter2 == null) return;
        
        SetupFighter(Fighter1);
        SetupFighter(Fighter2);
        
        var first = Fighter1.Speed >= Fighter2.Speed ? Fighter1 : Fighter2;
        var second = Fighter1.Speed < Fighter2.Speed ? Fighter1 : Fighter2;
        
        SceneFight(first, second);
    }

    private void SceneFight(BaseCharacter firstFighter, BaseCharacter secondFighter)
    {
        Console.Clear();
        ConsoleHelper.Title("Le Combat Commence !", Color256.Yellow, Color256.Red);
        Console.WriteLine($"{firstFighter.Name} (Vitesse: {firstFighter.Speed}) vs {secondFighter.Name} (Vitesse: {secondFighter.Speed})");
        ConsoleHelper.Pause();

        int round = 1;
        // Boucle de combat : continue tant que les deux sont en vie
        while (firstFighter.Hp > 0 && secondFighter.Hp > 0)
        {
            Console.WriteLine(ColorFormat.GetColoredString($"\n--- Round {round} ---", Color256.Cyan));
            
            // Tour du premier attaquant
            bool isSecondKilled = firstFighter.Attack(secondFighter);
            if (isSecondKilled) break;

            // Tour du deuxième attaquant
            bool isFirstKilled = secondFighter.Attack(firstFighter);
            if (isFirstKilled) break;

            round++;
            ConsoleHelper.Pause("Fin du round...");
        }

        // Annonce du vainqueur
        BaseCharacter winner = firstFighter.Hp > 0 ? firstFighter : secondFighter;
        Console.WriteLine();
        ConsoleHelper.Title($"VICTOIRE DE {winner.Name.ToUpper()} !", Color256.White, Color256.DarkGreen);
        Console.WriteLine($"Statut final : {winner.Hp} HP restants.");
        ConsoleHelper.Pause("Fin de la partie.");
    }
    
    #endregion
    
    #region Menus
    private bool MenuFighterChoice(string title, Color256 titleTextColor, Color256 titleBgColor, out dynamic? fighterInstance )
    {
        fighterInstance = null;
        
        ConsoleHelper.Title(title, titleTextColor, titleBgColor);
        Console.WriteLine(ColorFormat.GetColoredString("Classe :", Color256.White));
        
        foreach (var menuItem in PlayableClasses.Select((typeClass, i) => new { i = i + 1, className = typeClass.Name }))
        {
            Console.WriteLine($"{menuItem.i} - {menuItem.className}");
        }

        Console.Write(ColorFormat.GetColoredString("Elisez votre préférence : ", Color256.White));
        
        var parseChoice = int.TryParse(Console.ReadLine(), out var userChoice);
        
        if (!parseChoice || userChoice < 1 || userChoice > PlayableClasses.Count)
        {
            ConsoleHelper.ErrorMessage("Seigneur, ce gladiateur n'est plus !");
            ConsoleHelper.Pause("Poussoie sur une clef");
            return false;
        }
        
        Console.Write(ColorFormat.GetColoredString("Baptisez votre champion : ", Color256.White));
        var inputName = Console.ReadLine()?.Trim();
        var type = PlayableClasses[userChoice - 1];
        
        string chosenName = string.IsNullOrEmpty(inputName) 
            ? string.Concat(type.Name, new Random().Next(999)) 
            : inputName;
        
        fighterInstance = Activator.CreateInstance(type, [chosenName]);
        
        return fighterInstance != null;
    }
    
    #endregion
    
    #endregion
}