using UnityEngine;
using UnityEngine.UI;

// Script will be attached to the reward screen 
public class SkillTreeLoader : MonoBehaviour
{
    
    [Header("References")]
    public GameObject SkillTree; //assigned in the inspector
    public GameObject buttonPrefab;
    public GameObject rewardScreen;



    void Start()
    {
        
        SkillTreeButtonSpawner();

    }



    private void SkillTreeButtonSpawner()
    {
        
        GameObject selector = Instantiate(buttonPrefab, rewardScreen.transform);
        
        //positioning the button:
        RectTransform buttonRectTransform = selector.GetComponent<RectTransform>();
        buttonRectTransform.pivot = new Vector2(1.0f, 1.0f); // button's pivot is now the upper right
        buttonRectTransform.anchorMin = new Vector2(0.0f, 1.0f); // Anchors the button to the upper left of the reward screen. 
        buttonRectTransform.anchorMax = new Vector2(0.0f, 1.0f);
        buttonRectTransform.anchoredPosition = Vector2.zero; //moves the button to thne upper left of the parent

        selector.GetComponent<MenuSelectorController>().label.text = "Skill Tree";
        selector.GetComponent<MenuSelectorController>().spawner = null; //sets the spawner to null so the StartLevel() in the MenuSelectorController just returns instead of staating the level
        selector.GetComponent<Button>().onClick.RemoveAllListeners();
        selector.GetComponent<Button>().onClick.AddListener(() => UnhideSkillTree());

    }



    // button listener will call this method
    public void UnhideSkillTree()
    {
        
        SkillTree.SetActive(true);

    }


}
