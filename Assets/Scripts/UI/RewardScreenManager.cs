using UnityEngine;

public class RewardScreenManager : MonoBehaviour
{
    public static GameObject GlobalRewardUI;
    public GameObject rewardUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalRewardUI = rewardUI;
        // below is me experimenting
        //selector.GetComponent<MenuSelectorController>().spawner = EnemySpawnerClassReferences
       // selector.GetComponent<MenuSelectorController>().SetLevel(difficulty.Key); //Sets the text on the button (selector)
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {
            rewardUI.SetActive(true);
        }
        else
        {
            rewardUI.SetActive(false);
        }
    }
    
    // RewardScreenActive()
    // Returns if the reward screen is active in the scene or not
    public static bool RewardScreenActive()
    {
        return GlobalRewardUI.activeSelf;
    }
}
