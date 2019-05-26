using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiscManager : MonoBehaviour
{
    public AudioClip mainThem;
    public AudioClip menuThem;
    
    void Start()
    {
        AudioManager.instance.PlayMusic(mainThem, 2);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.PlayMusic(mainThem, 3);
        }
    }
}
