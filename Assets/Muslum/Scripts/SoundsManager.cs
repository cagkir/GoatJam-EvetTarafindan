using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] public AudioSource pressButton;
    [SerializeField] public AudioSource openShelveDoor;
    [SerializeField] public AudioSource closeShelveDoor;
    [SerializeField] public AudioSource switche;
    [SerializeField] public AudioSource tableFall;
    [SerializeField] public AudioSource openEscapeDoor;
    [SerializeField] public AudioSource backround;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(PlayerPrefs.GetInt("music") == 0)
        {
            backround.volume = 0f;
        }
        else
        {
            backround.volume = 0.45f;
        }
    }
}



    
