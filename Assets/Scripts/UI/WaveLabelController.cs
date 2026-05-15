using UnityEngine;
using TMPro;

public class WaveLabelController : MonoBehaviour
{
    TextMeshProUGUI tmp;
    public TextMeshProUGUI rewardSpellDescription;
    private bool rewardSpellDescriptionFlag = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.INWAVE)
        {
            rewardSpellDescriptionFlag = true;
            rewardSpellDescription.text = "";
            tmp.text = "Enemies left: " + GameManager.Instance.enemy_count;
        }
        if (GameManager.Instance.state == GameManager.GameState.COUNTDOWN)
        {
            rewardSpellDescriptionFlag = false;
            rewardSpellDescription.text = "";
            tmp.text = "Starting in " + GameManager.Instance.countdown;
        }
        if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {
            //commenting out wave stats - reimplement later
            //tmp.text = "Wave " + GameManager.Instance.wave_count + " Cleared!\n";
            //tmp.text += GameManager.Instance.sessionStats.getStats();

            tmp.text = "";

            rewardSpellDescription.text = GameManager.Instance.sessionStats.getSpellDescription();

            /*
            if (rewardSpellDescriptionFlag)
            {
        
            }
            */
    
        }
        if (GameManager.Instance.state == GameManager.GameState.GAMEOVER)
        {
            if (GameManager.Instance.enemy_count <= 0)
            {
                rewardSpellDescriptionFlag = false;
                rewardSpellDescription.text = "";
                tmp.text = "You Win!\n" + GameManager.Instance.sessionStats.getStats();
            }
            else
            {
                rewardSpellDescriptionFlag = false;
                rewardSpellDescription.text = "";
                tmp.text = "You Lose!\n" + GameManager.Instance.sessionStats.getStats();
            }
        }
    }

}