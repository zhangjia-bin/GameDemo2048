using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdiouManager : MonoBehaviour
{
    //单例模式
    public static AdiouManager Instance;
    public List<AudioClip> list = new List<AudioClip>();
    AudioSource pl = new AudioSource();
    void Start()
    {
        pl = GetComponent<AudioSource>();
           Instance = this;
    }
    

    public void PlayClipAudiou1()
    {
        pl.clip = list[0];
        pl.Play();
    }
    public void PlayClipAudiou2()
    {
        print(list[1].name);
        pl.clip = list[1];
        pl.Play();
    }
    
}
