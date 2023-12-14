using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioSource musicsource;

    // Start is called before the first frame update
    void Start()
    {
        musicsource = musicClip;
        musicsource.Play();
    }


}
