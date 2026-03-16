using ConsoleRPG.App.Utils;

namespace ConsoleRPG.App.Gameplay.Character;

public abstract class BaseCharacter
{
    protected string _name;
    public string Name => _name;
    
    protected int _agility;
    public int Agility => _agility;
    
    protected int _armor;
    public int Armor => _armor;
    
    protected int _hp;
    public int Hp => _hp;
    
    protected int _speed;
    public int Speed => _speed;
    
    protected int _strength;
    public int Strength => _strength;
    
    protected int _stamina;
    public int Stamina => _stamina;

    public BaseCharacter(string name)
    {
        _name = name;
    }

    public void Setup(int agility, int armor, int hp, int speed, int strength, int stamina)
    {
        _agility = agility;
        _armor = armor;
        _hp = hp;
        _speed = speed;
        _strength = strength;
        _stamina = stamina;
    }
    
    /// <summary>
    /// Lance une attaque sur un personnage
    /// </summary>
    /// <param name="target">Le personnage ciblé</param>
    /// <returns>Cible tuée ou non</returns>
    public bool Attack(BaseCharacter target)
    {
        // Répartition des valeurs du tirage pour le défenseur
        // 0 => 5 : Dégâts + Bonus de dégâts
        // 6 => 33 : Dégâts
        // 34 => 66 : Tentative de block
        // 67 => 100 : Tentative d'esquive
        
        Console.WriteLine($"{_name} attaque {target.Name}");
        // Tirer un Dé 100
        var diceAttack = new Random().Next(100);
        // Calcul d'une variable d'ajustement en fonction de l'agilité (qui donne un avantage)
        var biasAttack = target.Agility - _agility;
        // Ajustement du tirage de dé
        var resultAttack = diceAttack + biasAttack;
        
        // Action résultantes
        // Tentative d'esquive
        if (resultAttack > 66)
            return target.Dodge();
        
        // Tentative de Block
        if (resultAttack > 33)
            return target.Block(_strength);
        
        // Force attackant diminué par l'armure du défenseur (facteur de 0 à 1)
        var hpDamage = _strength * (1 - (target.Armor / 100));
        
        // Si pas de bonus de dégat
        if (resultAttack >= 6)
            return target.Hurt(hpDamage);
        
        // Bonus: (Force + HP) / 10
        var bonus = (_strength + _hp) / 10;
        hpDamage += bonus;

        return target.Hurt(hpDamage);
    }

    /// <summary>
    /// Bloque une attaque
    /// </summary>
    /// <param name="attackerStrength">Force de l'attaquant</param>
    /// <returns>Mort ou pas</returns>
    public bool Block( int attackerStrength)
    {
        // TODO: Calcal réussite du blocage, avec dégâts Armure ou HP et mort possible
        Console.WriteLine(ColorFormat.GetColoredString($"{_name} Bloque !", Color256.Yellow));
        return false;
    }

    /// <summary>
    /// Esquive
    /// </summary>
    /// <returns>Mort ou pas</returns>
    public bool Dodge()
    {
        // TODO: Calcal réussite de l'esquive, avec dégâts HP et mort possible
        Console.WriteLine(ColorFormat.GetColoredString($"{_name} esquive !", Color256.Green));
        return false;
    }

    /// <summary>
    /// Applique des dommages HP au personnage
    /// </summary>
    /// <param name="damage">Points de dégâts</param>
    /// <returns>Mort ou Pas</returns>
    public bool Hurt(int damage)
    {
        // Calcul des futur HP du défenseur
        var newHp = _hp - damage;

        // Si les Hp deviennent négatifs, on les met à 0
        if (newHp < 0) newHp = 0;
        
        _hp = newHp;
        
        Console.WriteLine(ColorFormat.GetColoredString($"{_name} blessé ! {_hp} HP restants !", Color256.Red));
        
        return _hp <= 0;
    }
}