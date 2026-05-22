using UnityEngine;
using System.Collections;
public class PierceAmpModifier : SpellModifier
{
    //constructor
    public PierceAmpModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["pierce-amplified"].Clone();
        Debug.Log("Modifier: PierceAmp Constructed");
    }




}