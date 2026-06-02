using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUnloader : MonoBehaviour
{

    [Header("References")]
    public GameObject buttonPrefab;
    public GameObject skillTreeUI;
    
    void Start()
    {
        
        SkillTreeHideButtonSpawner();

    }



    private void SkillTreeHideButtonSpawner()
    {
        
        GameObject selector = Instantiate(buttonPrefab, skillTreeUI.transform);
        
        //positioning the button:
        RectTransform buttonRectTransform = selector.GetComponent<RectTransform>();
        buttonRectTransform.pivot = new Vector2(0.0f, 0.0f); // button's pivot is now the bottom left
        buttonRectTransform.anchorMin = new Vector2(0.01f, 0.02f); // Anchors the button to the bottom left of the reward screen. 
        buttonRectTransform.anchorMax = new Vector2(0.01f, 0.02f);
        buttonRectTransform.localScale = new Vector2(0.085f, 0.2f);
        buttonRectTransform.anchoredPosition = Vector2.zero; //moves the button to thne bottom left of the parent

        selector.GetComponent<MenuSelectorController>().label.text = "Close";
        selector.GetComponent<MenuSelectorController>().spawner = null; //sets the spawner to null so the StartLevel() in the MenuSelectorController just returns instead of staating the level
        selector.GetComponent<Button>().onClick.RemoveAllListeners();
        selector.GetComponent<Button>().onClick.AddListener(() => HideSkillTree());

    }



    //listener for the button
    public void HideSkillTree()
    {
        
        skillTreeUI.SetActive(false);

    }

}
