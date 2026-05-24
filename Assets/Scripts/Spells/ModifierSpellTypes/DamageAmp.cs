using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner) : base(inner)
    {
        //SpellData template = GameManager.Instance.spells["damage-amplified"];
        //this.modData = GameManager.Instance.spells["damage-amplified"].MemberwiseClone()
        this.modData = GameManager.Instance.spells["damage-amplified"].Clone();
        Debug.Log("Modifier: Damage Amp Constructed");
    }

}