using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SpellData
{
    
    public List<SpellPropertiesData> spell = new List<SpellPropertiesData>();

}

public class SpellPropertiesData
{
    
    public string name { get ; set;} = null;
    public string description { get ; set; } = null;
    public int icon { get ; set; } = -1;
    public List<SpellDamageData> damage = new List<SpellDamageData>();
    public int mana_cost { get ; set; } = -1;
    public int cooldown { get ; set; } = -1;
    public List<SpellProjectileData> projectile = new List<SpellProjectileData>();

}

public class SpellDamageData
{
    
    public string amount { get ; set; } = null;
    public string type { get ; set; } = null;

}

public class SpellProjectileData
{

    public string trajectory { get; set;} = null;
    public string speed { get ; set; } = null;
    public int sprite { get ; set; } = -1;

}


//prgrammer can call RetrieveSpellData.SpellDictionary();
public class RetrieveSpellData
{
    
    private static SpellParsing spellparse = new SpellParsing();
    private static Dictionary<string, SpellData> spelldictionary = new Dictionary<string, SpellData>();

    public static Dictionary<string, SpellData> SpellDictionary()
    {
        spelldictionary = spellparse.StoreData(Resources.Load<TextAsset>("spells").text);
        return spelldictionary;
    }

}