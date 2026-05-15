using UnityEngine;
using System.Collections;

public class DoublerModifier : SpellModifier
{
    //constructor
    public DoublerModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["doubled"];
        this.modData.repeat = "1";
        Debug.Log("Modifier: Doubler Constructed");
    }

    /*public override IEnumerator CastRoutine(Vector3 where, Vector3 target, Hittable.Team team)
    {
        this.team = team;
        this.Cast();
        for (int i = 0; i < GetNumber(); i++)
        {
            GameManager.Instance.projectileManager.CreateProjectile(GetIcon(), GetTrajectory(), where, target - where, GetSpeed(), OnHit);

            // Wait before the next shot (but don't wait after the final shot)
            if (i < GetNumber() - 1)
            {
                yield return new WaitForSeconds(GetDelay());
            }
        }
    }*/
    

}