using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public enum AudioTypes
    {
        BGMusic,
        CastSpell,
        PlayerWalk,
        Win,
        Lose,
        Damage
    }
    
    private static AudioManager Instance;
    private AudioSource audioSource;

    [Header("Audio References")]
    [SerializeField] private AudioClip[] AudioClips; 



    void Awake()
    {

        Instance = this;

    }



    void Start()
    {

        audioSource = GetComponent<AudioSource>();

    }



    public static void PlayAudio(AudioTypes audio, float volume = 1)
    {
        
        Instance.audioSource.PlayOneShot(Instance.AudioClips[(int)audio], volume); //the enum has a integer value. Using that int value to get the audio clip in the array

    }



}
