using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ItemMusic : MonoBehaviour
{
    public AudioSource item;
    public Toggle IsToggle;

    public static ItemMusic Instacne;
    void Start()
    {
        Instacne = this;
        IsToggle.onValueChanged.AddListener((i) =>
        {
            if (i)
            {
                item.Play();
            }
            else
            {
                item.Pause();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
