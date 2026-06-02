using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    
    public void Play()
    {
        
        SceneManager.LoadScene("Main", LoadSceneMode.Single);

    }

}
