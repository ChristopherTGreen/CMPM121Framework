using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerClassScaling 
{

    // Getting all the player class type - should be redone later for other classes

    public static void ScalePlayer(ClassData playerclass) 
    {

        playerclass.health = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.health, GameManager.Instance.variables).ToString();
        playerclass.mana_regeneration = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.mana_regeneration, GameManager.Instance.variables).ToString();
        playerclass.mana = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.mana, GameManager.Instance.variables).ToString();
        playerclass.spellpower = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.spellpower, GameManager.Instance.variables).ToString();
        playerclass.speed = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.speed, GameManager.Instance.variables).ToString();

        GameManager.Instance.classTypes["player"] = playerclass; //updating game manager

    }

}

