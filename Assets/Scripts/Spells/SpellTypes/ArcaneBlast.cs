using System;
using System.Collections.Generic;
using System.Text;

class ArcaneBlast : Spell
{
    public ArcaneBlast(SpellCaster owner) : base(owner)
    {

        SpellData data = GameManager.Instance.spells["Arcane Blast"];

        //Debug.Log("Magic Missile Constructor: Got Arcane Bolt from the GameManager");

        new SpellBuilder(this)
            .SpellQuickBuilder(data)
            .Build(owner);

        //Debug.Log("Magic Missile Constructor: Finished Building Arcane Bolt");
    }
}