using System;
using System.Collections.Generic;
using System.Text;

public class RelicBuilder
{



    public RelicBuilder(RelicData relicData)
    {

    }


    public void RelicTriggerBuilder(RelicData relicData)
    {

    }

    public void RelicEffectBuilder(RelicData relicData)
    {

    }



    public ValueModifier Build()
    {
        return valueMod;
    }





    // Acts as an interface and class call
    public SpellModifierBuilder(ValueModifier existingValueModifier)
    {
        this.valueMod = existingValueModifier;
    }


}