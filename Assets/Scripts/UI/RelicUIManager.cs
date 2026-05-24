using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicUIManager : MonoBehaviour
{
    public GameObject relicUIPrefab;
    public PlayerController player;

    private RelicUIManager activeRelicDisplayUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        activeRelicDisplayUI = GetComponent<RelicUIManager>();

        //DisplayActiveRelics();

        //EventBus.Instance.OnRelicPickup += OnRelicPickup;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.state == GameManager.GameState.COUNTDOWN)
        {

            DisplayActiveRelics(); //display active relics everytime the countdown starts

        } 
        else
        {

            return; 

        }

    }

    public void DisplayActiveRelics()
    {
        int firstActiveRelicPosX = -400;
        int x_pos = 0;

        int activeRelicGap = 50;

        // clear the active relics bar
        foreach (Transform child in activeRelicDisplayUI.transform) {
            Destroy(child.gameObject);
        }

        // respawn relics
        foreach (RelicData activerelic in GameManager.Instance.RelicDataActiveRelics.Values)
        {

            //Spawns a duplicate of the relic prefab (Instantiate)
            GameObject relicdisplay = Instantiate(relicUIPrefab, activeRelicDisplayUI.transform);
            relicdisplay.transform.localPosition = new Vector3(firstActiveRelicPosX + x_pos, 0); //moves the local position
            x_pos += activeRelicGap;

            // PLace the icon
            GameManager.Instance.relicIconManager.PlaceSprite(activerelic.sprite, relicdisplay.GetComponentInChildren<Image>()); //should place icon
            relicdisplay.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false); // hide the text

        }

    }

    /*public void OnRelicPickup(Relic r)
    {
        // make a new Relic UI representation
        GameObject rui = Instantiate(relicUIPrefab, transform);
        rui.transform.localPosition = new Vector3(-450 + 40 * (player.relics.Count - 1), 0, 0);
        RelicUI ruic = rui.GetComponent<RelicUI>();
        ruic.player = player;
        ruic.index = player.relics.Count - 1;
        
    }*/
}
