namespace ConsoleRPG.App.Utils;

/// <summary>
/// Outils de formatage des couleurs pour le terminal
/// </summary>
public static class ColorFormat
{
    /// <summary>
    /// Renvoie le code de format pour une couleur de texte donnée.
    /// </summary>
    /// <param name="color">Code de la couleur voulue</param>
    /// <returns>Code de formatage de couleur</returns>
    public static string GetForegroundCode(Color256 color)
    {
        return GetColorCode(color, ColorType.Foreground);
    }

    /// <summary>
    /// Renvoie le code de format pour une couleur de fond donnée.
    /// </summary>
    /// <param name="color">Code de la couleur voulue</param>
    /// <returns>Code de formatage de couleur</returns>
    public static string GetBackgroundCode(Color256 color)
    {
        return GetColorCode(color, ColorType.Background);
    }

    /// <summary>
    /// Renvoie le code de reset du format
    /// </summary>
    /// <returns>Code de reset</returns>
    public static string GetResetCode()
    {
        return $"\x1B[0m";
    }

    public static string GetStyleCode(FontStyle fontStyle)
    {
        return $"\x1B[{(int)fontStyle}m";
    }
    
    public static string GetColoredString(string message, Color256 foreground, Color256 background = Color256.Default)
    {
        return string.Format(
            "{0}{1}{2}{3}",
            foreground != Color256.Default ? GetForegroundCode(foreground) : "", // Valeur de {0}
            background != Color256.Default ? GetBackgroundCode(background) : "", // Valeur de {1}
            message, // Valeur de {2}
            GetResetCode() // Valeur de {3}
            );
    }
    
    /// <summary>
    /// Renvoie le code de format pour une couleur et un type de couleur donnés.
    /// </summary>
    /// <param name="color">Code de la couleur voulue</param>
    /// <param name="colorType">Type de couleur voulu</param>
    /// <returns>Code de formatage de couleur</returns>
    private static string GetColorCode(Color256 color, ColorType colorType)
    {
        return $"\x1B[{(int)colorType};5;{(int)color}m";
    }
}

/// <summary>
/// Enumération des <b>16 couleurs ANSI</b> convertis en <b>couleurs 256</b>
/// pour garder le même aspect sur tous de types de terminal
/// </summary>
public enum Color256
{
    Default = -1,
    Black = 16,
    DarkRed = 88,
    DarkGreen = 28,
    DarkYellow = 100,
    DarkBlue = 18,
    DarkMagenta = 90,
    DarkCyan = 30,
    DarkGray = 244,
    White = 231,
    Red = 196,
    Green = 46,
    Yellow = 226,
    Blue = 21,
    Magenta = 201,
    Cyan = 51,
    Gray = 251
}

/// <summary>
/// Codes des types de couleur (fond ou texte)
/// </summary>
public enum ColorType
{
    Background = 48,
    Foreground = 37,
}