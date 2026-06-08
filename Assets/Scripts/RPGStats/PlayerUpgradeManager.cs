using System.Collections.Generic;
using static PlayerStats;

public class PlayerUpgradeManager
{
    private Dictionary<PlayerStatType, int> flatBonuses = new Dictionary<PlayerStatType, int>();

    public PlayerUpgradeManager()
    {
        Reset();
    }

    public void AddBonus(PlayerStatType statType, int amount)
    {
        if (!flatBonuses.ContainsKey(statType))
        {
            flatBonuses[statType] = 0;
        }

        flatBonuses[statType] += amount;
    }

    public int GetBonus(PlayerStatType statType)
    {
        if (!flatBonuses.ContainsKey(statType))
        {
            return 0;
        }

        return flatBonuses[statType];
    }

    public void ApplyTestUpgrade()
    {
        AddBonus(PlayerStatType.MaxHealth, 100);
        AddBonus(PlayerStatType.SpellPower, 50);
        AddBonus(PlayerStatType.ManaRegen, 25);
        AddBonus(PlayerStatType.MaxMana, 100);
        AddBonus(PlayerStatType.MoveSpeed, 2);
    }

    public void Reset()
    {
        flatBonuses.Clear();

        flatBonuses[PlayerStatType.MaxHealth] = 0;
        flatBonuses[PlayerStatType.SpellPower] = 0;
        flatBonuses[PlayerStatType.ManaRegen] = 0;
        flatBonuses[PlayerStatType.MaxMana] = 0;
        flatBonuses[PlayerStatType.MoveSpeed] = 0;
    }
}