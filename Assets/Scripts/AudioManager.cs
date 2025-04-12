using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public static AudioManager instance;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {Destroy(gameObject);}
    }
    
    void Start()
    {
        audioSource.Play();
    }
}
