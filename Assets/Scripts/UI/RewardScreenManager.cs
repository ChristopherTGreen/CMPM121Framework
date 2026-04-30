using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        else if (GameManager.Instance.state == GameManager.GameState.GAMEOVER)
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

    
    
    // This will get called in the enemy spawner right before the yield return wait for the RewardUI.
    // Using a similar method that I used in the MenuSelectorController when getting the enemy spawner class references
    
    /*
    * In EnemySpawner.cs, I had to declare levelReference at the top of the class + Declare the RewardScreenManager
    * Then in Start() of EnemySpawner, I had to tell Unity to find which object in the heirachy that class was attached to
    * That way, the script could find the Reward Screen UI so the method below could access the components in the children
    */
    public void NextWaveButtonHandler(EnemySpawner spawner)
    {

        Button NextWaveButton = rewardUI.GetComponentInChildren<Button>(); //finding the button component of the RewardUI
        TextMeshProUGUI NextButtonText = NextWaveButton.GetComponentInChildren<TextMeshProUGUI>(); //Finding the text component of the child of the button component

        NextButtonText.text = "Continue"; //changing the button text

        // Centers the button in the rewardUI. For some reason, the y position is set at -158?!
        RectTransform NextWaveButtonPosition = rewardUI.GetComponentInChildren<Button>().GetComponent<RectTransform>();
        NextWaveButtonPosition.anchoredPosition = new Vector2(0, 0);
        
        
        // RemoveAllListeners is so you dont spawn 5 waves at once at wave 5.
        // If I didn't have this here, it would spawn the previous five waves and mess up the countdown
        NextWaveButton.onClick.RemoveAllListeners();
        NextWaveButton.onClick.AddListener(() => spawner.NextWave(spawner.levelReference)); //When Continue button click, trigger the next wave

    }
    
}
