using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerClassScaling 
{

    // Getting all the player class type - should be redone later for other classes
    public int health;
    public int mana_regeneration;
    public int mana;
    public int spellpower;
    public int speed;

    public PlayerClassScaling(ClassData playerclass) 
    {

        ClassData newData = playerclass;
        health = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.health, GameManager.Instance.variables);
        mana_regeneration = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.mana_regeneration, GameManager.Instance.variables);
        mana = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.mana, GameManager.Instance.variables);
        spellpower = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.spellpower, GameManager.Instance.variables);
        speed = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.speed, GameManager.Instance.variables);
      
    }

}

