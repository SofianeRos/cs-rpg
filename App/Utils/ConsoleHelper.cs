using System.Text;

namespace ConsoleRPG.App.Utils;

public static class ConsoleHelper
{
    /// <summary>
    /// Met en pause le programme et affiche un message d'attente.
    /// Reprend l'exécution après l'appui sur une touche
    /// </summary>
    /// <param name="message">Message à afficher</param>
    public static void Pause(string message = "Appuyez sur une touche pour continuer...")
    {
        Console.Write(ColorFormat.GetColoredString(message,Color256.DarkGray));
        Console.ReadKey(); // Attend l'appui sur une touche pour continuer et renvoie le code de la touche
        Console.WriteLine();
    }

    /// <summary>
    /// Affiche un message d'erreur stylisé
    /// </summary>
    /// <param name="message">Le message à afficher</param>
    public static void ErrorMessage(string message = "Une erreur s'est produite de manière tout à fais fortuite...")
    {
        Console.WriteLine(ColorFormat.GetColoredString($" {message} ",Color256.White, Color256.Red));
    }

    public static void Title(string text, Color256 foreground, Color256 background = Color256.Default)
    {
        // Longueur des lignes horizontales
        var lineLength = text.Length + 2;
        
        // On peut générer une chaîne par répétition d'un caractère
        // new string( '*', 5 ); => *****
        var line =  new string('═', lineLength);
        
        StringBuilder builder = new();
        builder
            .AppendLine(ColorFormat.GetColoredString($" ╔{line}╗ ", foreground, background))
            .AppendLine(ColorFormat.GetColoredString($" ║ {text} ║ ", foreground, background))
            .AppendLine(ColorFormat.GetColoredString($" ╚{line}╝ ", foreground, background));
        Console.Write(builder.ToString());
    }
    
}