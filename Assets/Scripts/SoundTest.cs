using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] AudioSource stepSound;

    public void PlayStep()
    {
        stepSound.Play();
    }
}
