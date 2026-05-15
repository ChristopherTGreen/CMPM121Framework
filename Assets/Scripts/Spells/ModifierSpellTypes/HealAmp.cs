using UnityEngine;
using System.Collections;

public class HealAmpModifier : SpellModifier
{
    //constructor
    public HealAmpModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["heal-amplified"];
        Debug.Log("Modifier: HealAmp Constructed");
    }




}