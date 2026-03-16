using System.Reflection;
using System.Security.Cryptography;
using ConsoleRPG.App.Gameplay.Character;
using ConsoleRPG.App.Gameplay.Character.Playable;
using ConsoleRPG.App.Utils;

namespace ConsoleRPG.App;

public sealed class GameManager
{
    #region Champs
    
    private static GameManager? _instance;

    public List<Type> PlayableClasses { get; private set; } = [];
    
    public BaseCharacter Fighter1 { get; private set; }
    public BaseCharacter Fighter2 { get; private set; }
    
    #endregion
    
    #region Méthodes
    public static GameManager Instance()
    {
        // Si _instance null crée une nouvelle instance et la renvoie
        return _instance ??= new GameManager();
    }
    
    public void StartGame()
    {
        // Initializations
        Initialize();
        
        // Scene 1: Choix des personnages
        SceneFighterChoice();
        
        // Scene 2: Combat
        //SceneFight();
    }

    private GameManager() {}

    private void Initialize()
    {
        // Liste des classes (dans le namespace App.Gameplay.Character.Playable)
        PlayableClasses = Assembly
            .GetExecutingAssembly() // Récupère l'Assembly en cours d'éxécution
            .GetTypes() // Récupère la liste de toutes les classes de l'assembly
            .Where(t => t is { IsClass: true, Namespace: "ConsoleRPG.App.Gameplay.Character.Playable" })
            .ToList();
        
        Console.Clear();
        // Ecran titre
        Console.Title = "Console RPG";
        ConsoleHelper.Title("Bienvenue Voyageur !", Color256.Yellow, Color256.DarkGreen);
        ConsoleHelper.Pause("Poussoie sur une clef pour commencer ton aventure");
    }

    /// <summary>
    /// Redonne le bon type enfant aux combattants
    /// </summary>
    private void SetupFighter(BaseCharacter fighter)
    {
        switch (fighter.GetType().Name)
        {
            case "Necromancer":
                (fighter as Necromancer).Setup(75, 40, 75, 100, 50, 80, 100);
                break;
            case "Thief":
                (fighter as Thief).Setup(100, 65, 80, 100, 85, 80, 100);
                break;
            case "Warrior":
                (fighter as Warrior).Setup(50, 100, 100, 40, 100, 100, 0);
                break;
            case "Wizard":
                (fighter as Wizard).Setup(75, 40, 100, 100, 50, 80, 100);
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
        // Menu Combattant 1
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
        
        // Menu Combattant 2
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

        // Consolidation de la vérification de la présence effective des 2 combattants
        // TODO: On pourrait aussi afficher une erreur avant de sortir
        if (Fighter1 == null || Fighter2 == null) return;
        
        // Initialisation des combattants
        SetupFighter(Fighter1);
        SetupFighter(Fighter2);
        //Console.WriteLine($"F1 => Name: {Fighter1.Name}, Agility : {Fighter1.Agility}, Strength : {Fighter1.Strength}");
        //Console.WriteLine($"F2 => Name: {Fighter2.Name}, Agility : {Fighter2.Agility}, Strength : {Fighter2.Strength}");
        
        // Choix du premier attaquant en fonction de Speed
        var first = Fighter1.Speed >= Fighter2.Speed ? Fighter1 : Fighter2;
        var second = Fighter1.Speed < Fighter2.Speed ? Fighter1 : Fighter2;
        
        // Lancement du combat
        SceneFight(first, second);
    }

    private void SceneFight(BaseCharacter firstFighter, BaseCharacter secondFighter)
    {
        Console.WriteLine(firstFighter.Name);
        Console.WriteLine(secondFighter.Name);
        ConsoleHelper.Pause();
        Console.Clear();
        ConsoleHelper.Title("Combattez !", Color256.Yellow, Color256.Red);
        Console.WriteLine();
        
        // TODO: Boucle dde déroulement du combat
        var secondKilled = firstFighter.Attack(secondFighter);
        var firstKilled = secondFighter.Attack(firstFighter);
    }
    
    #endregion
    
    #region Menus
    private bool MenuFighterChoice(string title, Color256 titleTextColor, Color256 titleBgColor, out dynamic? fighterInstance )
    {
        fighterInstance = null;
        
        ConsoleHelper.Title(title, titleTextColor, titleBgColor);
        Console.WriteLine(ColorFormat.GetColoredString("Classe :", Color256.White));
        
        // On Affiche la liste des classes jouables
        foreach (var menuItem in PlayableClasses.Select((typeClass, i) => new { i = i + 1, className = typeClass.Name } ))
        {
            Console.WriteLine($"{menuItem.i} - {menuItem.className}");
        }

        Console.Write(ColorFormat.GetColoredString("Elisez votre préférence : ", Color256.White));
        
        var parseChoice = int.TryParse(Console.ReadLine(), out var userChoice);
        
        if (!parseChoice || (userChoice < 1 || userChoice > PlayableClasses.Count))
        {
            ConsoleHelper.ErrorMessage("Seigneur, ce gladiateur n'est plus !");
            ConsoleHelper.Pause("Poussoie sur une clef");
            return false;
        }
        
        // Choix du nom du combattant
        Console.Write(ColorFormat.GetColoredString("Baptisez votre champion : ", Color256.White));
        var chosenName = Console.ReadLine()?.Trim();
        Console.WriteLine();

        var type = PlayableClasses[userChoice - 1];
        
        if( chosenName == string.Empty )
            chosenName = string.Concat(type.Name, new Random().Next(999));
        
        fighterInstance = Activator.CreateInstance(type, [chosenName]);
        
        return fighterInstance != null;
    }
    
    #endregion
    
    #endregion Méthodes
}