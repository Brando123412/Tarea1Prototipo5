using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerPersistent : SingeltonPersistent<MusicManagerPersistent>
{
    public AudioSource soundButtons;
    public AudioSource music;
    [SerializeField] AudioClip[] musicClips;
    private new void Awake()
    {
        base.Awake();
        soundButtons = GetComponent<AudioSource>();
    }
    public void SoundButtonPlay()
    {
        soundButtons.Play();
    }
    public void PlayRandomMusic()
    {
         int randomIndex = Random.Range(0, musicClips.Length);
         AudioClip randomClip = musicClips[randomIndex];
         music.clip = randomClip;
         music.Play();
    }
}
