using UnityEngine;
using TMPro;

public class WaveLabelController : MonoBehaviour
{
    TextMeshProUGUI tmp;
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
            tmp.text = "Enemies left: " + GameManager.Instance.enemy_count;
        }
        if (GameManager.Instance.state == GameManager.GameState.COUNTDOWN)
        {
            tmp.fontSize = 36;
            tmp.text = "Starting in " + GameManager.Instance.countdown;
        }
        if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {
            //commenting out wave stats - reimplement later
            //tmp.text = "Wave " + GameManager.Instance.wave_count + " Cleared!\n";
            //tmp.text += GameManager.Instance.sessionStats.getStats();

            //shrinking font size for spell description
            tmp.fontSize = 15;
            tmp.text = GameManager.Instance.sessionStats.getSpellDescription();
    
        }
        if (GameManager.Instance.state == GameManager.GameState.GAMEOVER)
        {
            if (GameManager.Instance.enemy_count <= 0)
            {
                tmp.fontSize = 36;
                tmp.text = "You Win!\n" + GameManager.Instance.sessionStats.getStats();
            }
            else
            {
                tmp.fontSize = 36;
                tmp.text = "You Lose!\n" + GameManager.Instance.sessionStats.getStats();
            }
        }
    }

}