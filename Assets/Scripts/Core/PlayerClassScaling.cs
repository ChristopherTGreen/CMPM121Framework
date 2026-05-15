using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerClassScaling 
{

    // Getting all the player class type - should be redone later for other classes
    public static int health;
    public static int mana_regeneration;
    public static int mana;
    public static int spellpower;
    public static int speed;

    public static void ScalePlayer(ClassData playerclass) 
    {

        health = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.health, GameManager.Instance.variables);
        mana_regeneration = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.mana_regeneration, GameManager.Instance.variables);
        mana = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.mana, GameManager.Instance.variables);
        spellpower = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.spellpower, GameManager.Instance.variables);
        speed = RPNEvaluator.RPNEvaluator.Evaluate(playerclass.speed, GameManager.Instance.variables);

        GameManager.Instance.classTypes["player"].health = health.ToString(); //updating game manager
        GameManager.Instance.classTypes["player"].mana_regeneration = mana_regeneration.ToString();
        GameManager.Instance.classTypes["player"].mana = mana.ToString();
        GameManager.Instance.classTypes["player"].spellpower = spellpower.ToString();
        GameManager.Instance.classTypes["player"].speed = speed.ToString();

    }

}

