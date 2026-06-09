using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerStats;

public class PlayerUpgradeUI : MonoBehaviour
{
    public GameObject upgradeButtonPrefab;
    public Transform buttonParent;
    public TextMeshProUGUI currentStatsLabel;

    private bool optionsDisplayed;
    private bool upgradeChosen;

    public void ShowUpgradeOptions()
    {
        if (optionsDisplayed)
        {
            RefreshCurrentStatsLabel();
            return;
        }

        optionsDisplayed = true;
        upgradeChosen = false;

        gameObject.SetActive(true);
        ClearButtons();
        RefreshCurrentStatsLabel();

        List<PlayerUpgrade> upgrades = GameManager.Instance.playerUpgradeManager.GetUpgradeOptions();

        foreach (PlayerUpgrade upgrade in upgrades)
        {
            GameObject buttonObject = Instantiate(upgradeButtonPrefab, buttonParent);

            TextMeshProUGUI label = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            label.text = BuildButtonText(upgrade);

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => ChooseUpgrade(upgrade));
        }
    }

    public void ResetPanel()
    {
        optionsDisplayed = false;
        upgradeChosen = false;
        ClearButtons();
        gameObject.SetActive(false);
    }

    private void ChooseUpgrade(PlayerUpgrade upgrade)
    {
        if (upgradeChosen)
        {
            return;
        }

        upgradeChosen = true;

        GameManager.Instance.playerUpgradeManager.AddBonus(upgrade.statType, upgrade.amount);

        ClearButtons();
        RefreshCurrentStatsLabel();

        currentStatsLabel.text += "\n\nSelected: " + upgrade.name;
    }

    private string BuildButtonText(PlayerUpgrade upgrade)
    {
        int currentValue = GetCurrentStatValue(upgrade.statType);
        int upgradedValue = currentValue + upgrade.amount;

        return upgrade.name
            + "\n" + upgrade.description;
    }

    private int GetCurrentStatValue(PlayerStatType statType)
    {
        PlayerController player = GameManager.Instance.player.GetComponent<PlayerController>();

        switch (statType)
        {
            case PlayerStatType.MaxHealth:
                return player.hp.max_hp;

            case PlayerStatType.SpellPower:
                return player.power;

            case PlayerStatType.ManaRegen:
                return player.spellcaster.mana_reg;

            case PlayerStatType.MaxMana:
                return player.spellcaster.max_mana;

            case PlayerStatType.MoveSpeed:
                return player.speed;

            default:
                return 0;
        }
    }

    private void RefreshCurrentStatsLabel()
    {
        PlayerController player = GameManager.Instance.player.GetComponent<PlayerController>();
        PlayerUpgradeManager upgrades = GameManager.Instance.playerUpgradeManager;

        currentStatsLabel.text =
            "Current Stats\n"
            + "Health: " + player.hp.hp + " / " + player.hp.max_hp
            + "  (+" + upgrades.GetBonus(PlayerStatType.MaxHealth) + ")\n"
            + "Spell Power: " + player.power
            + "  (+" + upgrades.GetBonus(PlayerStatType.SpellPower) + ")\n"
            + "Mana Regen: " + player.spellcaster.mana_reg
            + "  (+" + upgrades.GetBonus(PlayerStatType.ManaRegen) + ")\n"
            + "Max Mana: " + player.spellcaster.max_mana
            + "  (+" + upgrades.GetBonus(PlayerStatType.MaxMana) + ")\n"
            + "Move Speed: " + player.speed
            + "  (+" + upgrades.GetBonus(PlayerStatType.MoveSpeed) + ")";
    }

    private void ClearButtons()
    {
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }
    }
}