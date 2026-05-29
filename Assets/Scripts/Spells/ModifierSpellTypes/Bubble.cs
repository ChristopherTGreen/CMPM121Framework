using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BubbleModifier : SpellModifier
{
    //constructor
    public BubbleModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["bubble"].Clone();
        Debug.Log("Modifier: Bubble Constructed");
    }
}