using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioSource musicsource;

    // Start is called before the first frame update
    void Start()
    {
        musicsource.clip = musicClip;
    }

    // Update is called once per frame
    void Update()
    {
    
    
    if(Input.GetMouseButtonUp(0))
        {
            musicsource.loop = false;
        }
    

    if(Input.GetMouseButtonDown(0))
    {
        musicsource.Play();
        musicsource.loop = true;
    }

    }
}
