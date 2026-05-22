using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// This is the SpellModifier wrapper Template. Any new spell modifier should inherit this class!
public class SpellModifier : Spell
{
    
    // inner should be the spell being wrapped.
    public Spell inner;

    public SpellData modData;
    
    // Variables for base class (we need to find default values)
    // inherit stats from spell which is what we pass
    


    //This is SpellModifier class' constructor. So we can call it like SpellModifier()
    //I THINK this constructor inherits the Spell class (Parent class') Constructor with the "[This Class' Constructor] : base(owner)" where base is the parent class' constructor
    public SpellModifier(Spell inner) : base(inner.owner)
    {
        this.inner = inner;
        

        if (inner.stats == null) inner.stats = new ValueModifier();
        if (this.stats == null) this.stats = new ValueModifier();

        //Syncs the data to the SpellModifier from the base spell.
        new SpellBuilder(this).SyncDataFrom(inner).Build(inner.owner);
        

        //COmbining the stats from 2 modifier classes together
        //this.stats.MergeFrom(inner.stats);

    }



    //Editing the Castroutine Method from the Parent (Spell) class
    protected override void Cast(ValueModifier valueModifier)
    {

        //Debug.Log("SpellModifier.cs_Cast() >> Modifier Cast is running!");
        //Debug.Log("SpellModifier.cs_Cast() >> Pre Mod Damage " + this.stats.amount);
        new SpellModifierBuilder(valueModifier).SpellModifierQuickBuilder(this.modData);
        //Debug.Log("SpellModifier.cs_Cast() >> Post Modifier Damage " + this.stats.amount);
        //Debug.Log(stats.amount.Count);
        ((ISpell)inner).Cast(valueModifier);

    }



    //This is a editable method that the children of this class can edit
    // This is where the spell modifier will be
    //protected virtual void ApplyModifier(ValueModifier valueModifier) {}

}
