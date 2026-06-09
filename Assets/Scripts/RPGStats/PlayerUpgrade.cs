using UnityEngine;
using static PlayerStats;
[System.Serializable]
public class PlayerUpgrade
{
    public string name;
    public string description;
    public PlayerStatType statType;
    public int amount;

    public PlayerUpgrade(string name, string description, PlayerStatType statType, int amount)
    {
        this.name = name;
        this.description = description;
        this.statType = statType;
        this.amount = amount;
    }
}
