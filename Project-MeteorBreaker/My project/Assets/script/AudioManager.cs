using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //•¡”‚Ì‰¹Œ¹‚ğ“ü‚ê‚é
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;

    public int mIndex;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        //Component‚ğæ“¾
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //‰¹(music)‚ğ–Â‚ç‚·
        if (mIndex == 1)
        {
            audio.PlayOneShot(music1);
        }
        else if (mIndex == 2)
        {
            audio.PlayOneShot(music2);
        }
        else if (mIndex == 3)
        {
            audio.PlayOneShot(music3);
        }
        else if (mIndex == 4)
        {
            audio.PlayOneShot(music4);
        }
        
    }
}
