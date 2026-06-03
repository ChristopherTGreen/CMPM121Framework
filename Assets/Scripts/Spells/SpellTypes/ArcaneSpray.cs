using System;
using System.Collections.Generic;
using System.Text;

class ArcaneSpray : Spell
{
    public ArcaneSpray(SpellCaster owner) : base(owner)
    {

        SpellData data = GameManager.Instance.spells["Arcane Spray"];

        //Debug.Log("Magic Missile Constructor: Got Arcane Bolt from the GameManager");

        new SpellBuilder(this)
            .SpellQuickBuilder(data)
            .Build(owner);

        //Debug.Log("Magic Missile Constructor: Finished Building Arcane Bolt");
    }
}