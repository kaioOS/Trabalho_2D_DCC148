using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;

    public void SoundVolume(float value)
    {
        backgroundMusic.volume = value;
    }
}
