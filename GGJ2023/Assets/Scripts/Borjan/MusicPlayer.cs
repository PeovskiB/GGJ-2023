using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public static MusicPlayer instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip menuMusic, gameMusic;

    void Awake(){
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(menuMusic, 1f);
    }

    public static void PlayGameMusic(){
        instance.audioSource.Stop();
        instance.audioSource.PlayOneShot(instance.gameMusic, 1f);
    }

    public static void PlayMenuMusic(){
        instance.audioSource.Stop();
        instance.audioSource.PlayOneShot(instance.menuMusic, 1f);
    }
}
