using System;
using System.Collections.Generic;
using System.Text;

public class Relic
{
    RelicEffect relicEffect; // actual effect itself
    RelicTrigger relicCondition;
    
    public Relic(RelicTrigger relicTrigger, RelicEffect relicEffect)
    {
        relicCondition = relicTrigger;
        relicCondition.OnTrigger += relicEffect.CallEffect;
    }

    public void RelicCondition()
    {

    }
    

}