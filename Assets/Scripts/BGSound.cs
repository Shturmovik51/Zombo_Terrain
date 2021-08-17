using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
    private AudioClip currentClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentClip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.clip = currentClip;
            audioSource.Play();
        }
    }
}
