using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> footStepAudioClips;

    public void PlayFootStepSound()
    {
        int index = Random.Range(0, footStepAudioClips.Count);
        audioSource.volume = Random.Range(0.3f, 0.7f);
        audioSource.PlayOneShot(footStepAudioClips[index]);
    }
}
