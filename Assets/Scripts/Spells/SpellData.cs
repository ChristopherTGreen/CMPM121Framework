using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class SpellData
{
    
    public string name { get ; set;} = null;
    public string description { get ; set; } = null;
    public int icon { get ; set; } = -1;
    public SpellDamageData damage { get ; set; } = null;
    public string heal { get; set; } = null;
    public string mana_cost { get ; set; } = null;
    public string cooldown { get ; set; } = null;
    public string N { get; set; } = null;
    public string angle { get; set; } = null;
    public SpellProjectileData projectile { get ; set; } = null;



    //multipliers
    public string damage_multiplier { get ; set; } = null;
    public string mana_multiplier { get ; set; } = null;
    public string speed_multiplier { get ; set; } = null;
    public string cooldown_multiplier { get ; set; } = null;

    //angle
    public string angle { get ; set; } = null;

    //delay
    public string delay { get; set; } = null;

    //mana adder
    public string mana_adder { get ; set;} = null;

    //Singular projectile trajectory
    public string projectile_trajectory { get; set;} = null;
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
    public string lifetime { get ; set; } = null;
    public int sprite { get ; set; } = -1; // what does this do? - chris

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