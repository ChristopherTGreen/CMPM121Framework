using UnityEngine;

//THIS SCRIPT DOESNT WORK BECAUSE DONTDESTORY NEEDS TO BE ON ROOT OBJECT ;-;
public class KeepColorOverlay : MonoBehaviour
{
    private static KeepColorOverlay _instance;

    private void Awake()
    {
        // Enforce a single instance of the overlay
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
