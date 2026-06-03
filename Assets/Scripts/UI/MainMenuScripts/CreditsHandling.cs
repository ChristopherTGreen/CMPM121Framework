using UnityEngine;

public class CreditsHandling : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject creditsUI;
    


    public void ShowCredits()
    {

        creditsUI.SetActive(true);

    }



    public void HideCredits()
    {
        
        creditsUI.SetActive(false);

    }

}
