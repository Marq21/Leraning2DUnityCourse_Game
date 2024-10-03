using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffector : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip loseSound;

    public void playLoseSound()
    {
        audioSource.PlayOneShot(loseSound);
    }

}
