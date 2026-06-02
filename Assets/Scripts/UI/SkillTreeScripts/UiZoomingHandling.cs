using UnityEngine;
using UnityEngine.EventSystems;

// Credits: Used Jason Weimann's 'Unity3D - How to Zoom an Image / Create a zoomable pannable sprite' yt video
// IScrollHandler comes from UnityEngine.EventSystems - Not one of our interfaces, it's Unity's
public class NewMonoBehaviourScript : MonoBehaviour, IScrollHandler
{

    private Vector3 initialScale;
    [SerializeField] private float zoomSpeed = 1.0f; //default. Can be adjusted in the inspector
    [SerializeField] private float maxZoom = 10.0f;

    private void Awake()
    {

        initialScale = transform.localScale; // getting the initial scale of the skill tree UI

    }
    


    // from the IScrollHandler interface 
    public void OnScroll(PointerEventData eventData)
    {
        
        //Vector3.one gives a vector <1, 1, 1>, scrollDelta.y will be 1 or -1 (scrolling up or down) * zoom speed
        Vector3 delta = Vector3.one * (eventData.scrollDelta.y * zoomSpeed); 
        Vector3 desiredScale = transform.localScale + delta;

        desiredScale = ClampDesiredScale(desiredScale);

        transform.localScale = desiredScale;

    }

    

    // Player can't zoom below the initial size or zoom beyond the max size
    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {

        desiredScale = Vector3.Max(initialScale, desiredScale);
        desiredScale = Vector3.Min(initialScale * maxZoom, desiredScale);
        return desiredScale;

    }

}
