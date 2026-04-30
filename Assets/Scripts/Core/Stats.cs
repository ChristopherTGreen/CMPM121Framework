using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{
    public int enemiesKilled { get; set; } = 0;
    public int totalDamageDealt { get; set; } = 0;

    public void resetStats()
    {
        enemiesKilled = 0;
        totalDamageDealt = 0;
    }

    public string getStats()
    {
        return "Enemies Killed: " + enemiesKilled + "\nTotal Damage Dealt: " + totalDamageDealt;

    }
}