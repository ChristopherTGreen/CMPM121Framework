using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter: MonoBehaviour
{
    // takes the current scene and restarts it
    public static void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        foreach (string relicName in GameManager.Instance.activeRelics.Keys) 
        {
            GameManager.Instance.activeRelics[relicName].applyTrigger.RemoveObserver();
            GameManager.Instance.activeRelics[relicName].Disable();
            

        }
        GameManager.Instance.activeRelics.Clear();
        GameManager.Instance.RelicDataActiveRelics.Clear();



        SceneManager.LoadScene(currentScene.name);
    }
}
