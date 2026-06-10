using System.Collections.Generic;
using UnityEngine;

public static class SkillTreeRewardManager
{

    private static Dictionary<SpellNode.AvaliableSpells, int> spellSlots =
    new Dictionary<SpellNode.AvaliableSpells, int>();
    public static int selectionsAllowedPerRound = 1;
    private static int selectionsUsedThisRound = 0;

    public static void GrantSpell(SpellNode.AvaliableSpells spellType)
    {
        PlayerController player = GameManager.Instance.player.GetComponent<PlayerController>();
        Spell newSpell = CreateSpell(spellType, player.spellcaster);

        int slotIndex = FindFirstOpenSpellSlot();

        if (slotIndex == -1)
        {
            slotIndex = player.activeSpellIndex;

            if (slotIndex < 0 || slotIndex >= GameManager.Instance.activeSpells.Length)
            {
                slotIndex = 0;
            }
        }

        GameManager.Instance.activeSpells[slotIndex] = newSpell;
        spellSlots[spellType] = slotIndex;

        player.activeSpellIndex = slotIndex;
        player.spellcaster.CurrentActiveSpell(newSpell);

        RefreshSpellUI();
    }

    public static void ApplyModifier(
     ModifierNode.AvaliableModifiers modifierType,
     SpellNode.AvaliableSpells baseSpellType
 )
    {
        if (!spellSlots.ContainsKey(baseSpellType))
        {
            Debug.LogWarning("Base spell has not been collected yet: " + baseSpellType);
            return;
        }

        int index = spellSlots[baseSpellType];

        Spell currentSpell = GameManager.Instance.activeSpells[index];

        if (currentSpell == null)
        {
            Debug.LogWarning("No spell found in slot for: " + baseSpellType);
            return;
        }

        Spell modifiedSpell = CreateModifier(modifierType, currentSpell);

        GameManager.Instance.activeSpells[index] = modifiedSpell;

        PlayerController player = GameManager.Instance.player.GetComponent<PlayerController>();

        if (player.activeSpellIndex == index)
        {
            player.spellcaster.CurrentActiveSpell(modifiedSpell);
        }

        RefreshSpellUI();
    }


    public static void GrantRelic(RelicData relicData)
    {
        if (GameManager.Instance.activeRelics.ContainsKey(relicData.name))
        {
            Debug.Log("Relic already collected: " + relicData.name);
            return;
        }

        Relic relic = CreateRelic(relicData);
        relic.Enable();

        GameManager.Instance.activeRelics.Add(relicData.name, relic);
        GameManager.Instance.RelicDataActiveRelics.Add(relicData.name, relicData);
    }

    private static Spell CreateSpell(SpellNode.AvaliableSpells spellType, SpellCaster owner)
    {
        switch (spellType)
        {
            case SpellNode.AvaliableSpells.ArcaneBolt:
                return new ArcaneBolt(owner);

            case SpellNode.AvaliableSpells.MagicMissile:
                return new MagicMissile(owner);

            case SpellNode.AvaliableSpells.ArcaneBlast:
                return new ArcaneBlast(owner);

            case SpellNode.AvaliableSpells.ArcaneSpray:
                return new ArcaneSpray(owner);

            default:
                throw new System.Exception("Unhandled skill tree spell: " + spellType);
        }
    }

    private static Spell CreateModifier(ModifierNode.AvaliableModifiers modifierType, Spell inner)
    {
        switch (modifierType)
        {
            case ModifierNode.AvaliableModifiers.DamageAmp:
                return new DamageAmpModifier(inner);

            case ModifierNode.AvaliableModifiers.SpeedAmp:
                return new SpeedAmpModifier(inner);

            case ModifierNode.AvaliableModifiers.PierceAmp:
                return new PierceAmpModifier(inner);

            case ModifierNode.AvaliableModifiers.HealAmp:
                return new HealAmpModifier(inner);

            case ModifierNode.AvaliableModifiers.Bounce:
                return new BounceModifier(inner);

            case ModifierNode.AvaliableModifiers.Doubler:
                return new DoublerModifier(inner);

            case ModifierNode.AvaliableModifiers.Splitter:
                return new SplitterModifier(inner);

            case ModifierNode.AvaliableModifiers.Broken:
                return new Broken(inner);

            case ModifierNode.AvaliableModifiers.Massive:
                return new Massive(inner);

            case ModifierNode.AvaliableModifiers.Chaos:
                return new ChaosModifier(inner);

            case ModifierNode.AvaliableModifiers.Homing:
                return new HomingModifier(inner);

            case ModifierNode.AvaliableModifiers.Bubble:
                return new BubbleModifier(inner);

            default:
                throw new System.Exception("Unhandled skill tree modifier: " + modifierType);
        }
    }

    private static Relic CreateRelic(RelicData relicData)
    {
        switch (relicData.name)
        {
            case "Green Gem":
                return new GreenGem(relicData);

            case "Jade Elephant":
                return new JadeElephant(relicData);

            case "Golden Mask":
                return new GoldenMask(relicData);

            case "Cursed Scroll":
                return new CursedScroll(relicData);

            case "Armlet Replenish":
                return new WaveHeal(relicData);

            case "Life Steal":
                return new LifeSteal(relicData);

            case "Kings Charge":
                return new KingsCharge(relicData);

            case "Mana Bubble":
                return new ManaBubble(relicData);

            case "Warlocks Tome":
                return new WarlocksTome(relicData);

            case "Blood Amulet":
                return new BloodAmulet(relicData);

            default:
                throw new System.Exception("Unhandled skill tree relic: " + relicData.name);
        }
    }

    private static void RefreshSpellUI()
    {
        for (int i = 0; i < GameManager.Instance.activeSpells.Length; i++)
        {
            Spell spell = GameManager.Instance.activeSpells[i];

            if (spell == null)
            {
                continue;
            }

            GameObject spellUIObject = GameManager.Instance.spellUIcontainer.spellUIs[i];
            spellUIObject.SetActive(true);
            spellUIObject.GetComponent<SpellUI>().SetSpell(spell);
        }
    }

    private static int FindFirstOpenSpellSlot()
    {
        for (int i = 0; i < GameManager.Instance.activeSpells.Length; i++)
        {
            if (GameManager.Instance.activeSpells[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    public static bool CanSelectNode()
    {
        return selectionsUsedThisRound < selectionsAllowedPerRound;
    }

    public static void RegisterNodeSelection()
    {
        selectionsUsedThisRound++;
    }

    public static void ResetRoundSelections()
    {
        selectionsUsedThisRound = 0;
    }

    public static int GetSelectionsRemaining()
    {
        return selectionsAllowedPerRound - selectionsUsedThisRound;
    }
}