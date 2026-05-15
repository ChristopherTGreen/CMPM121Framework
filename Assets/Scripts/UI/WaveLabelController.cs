using UnityEngine;
using TMPro;

public class WaveLabelController : MonoBehaviour
{
    TextMeshProUGUI tmp;
    public TextMeshProUGUI rewardSpellDescription;
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
            rewardSpellDescription.text = "";
            tmp.text = "Enemies left: " + GameManager.Instance.enemy_count;
        }
        if (GameManager.Instance.state == GameManager.GameState.COUNTDOWN)
        {
            rewardSpellDescription.text = "";
            tmp.text = "Starting in " + GameManager.Instance.countdown;
        }
        if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {
            //commenting out wave stats - reimplement later
            //tmp.text = "Wave " + GameManager.Instance.wave_count + " Cleared!\n";
            //tmp.text += GameManager.Instance.sessionStats.getStats();

            tmp.text = "";
            rewardSpellDescription.text = GameManager.Instance.sessionStats.getSpellDescription(GameManager.Instance.currentRewardSpell);
    
        }
        if (GameManager.Instance.state == GameManager.GameState.GAMEOVER)
        {
            if (GameManager.Instance.enemy_count <= 0)
            {
                rewardSpellDescription.text = "";
                tmp.text = "You Win!\n" + GameManager.Instance.sessionStats.getStats();
            }
            else
            {
                rewardSpellDescription.text = "";
                tmp.text = "You Lose!\n" + GameManager.Instance.sessionStats.getStats();
            }
        }
    }

}