using UnityEngine;

public class Massive : SpellModifier
{
    public Massive(Spell inner) : base(inner)
    {

        this.modData = GameManager.Instance.spells["Massive"].Clone();
        Debug.Log("Modifier: massive Constructed");
    }
}
