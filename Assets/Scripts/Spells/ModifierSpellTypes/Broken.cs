using UnityEngine;

public class Broken : SpellModifier
{
    public Broken(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["Broken"].Clone();
        Debug.Log("Modifier: Broken Constructed");
    }
}
