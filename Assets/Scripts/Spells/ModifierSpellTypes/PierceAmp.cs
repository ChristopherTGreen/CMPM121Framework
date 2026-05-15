using UnityEngine;
using System.Collections;
public class PierceAmpModifier : SpellModifier
{
    //constructor
    public PierceAmpModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["pierce-amplified"];
        Debug.Log("Modifier: PierceAmp Constructed");
    }




}