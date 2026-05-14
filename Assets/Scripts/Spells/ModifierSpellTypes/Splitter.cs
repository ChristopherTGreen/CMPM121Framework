using UnityEngine;
using System.Collections;

public class SplitterModifier : SpellModifier
{
    //constructor
    public SplitterModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["split"];
        Debug.Log("Modifier: Splitter Constructed");
    }




}